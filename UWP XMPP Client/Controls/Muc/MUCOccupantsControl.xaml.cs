﻿using System;
using Data_Manager2.Classes.DBManager;
using Data_Manager2.Classes.DBTables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWP_XMPP_Client.DataTemplates;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XMPP_API.Classes;
using UWP_XMPP_Client.Dialogs;
using XMPP_API.Classes.Network.XML.Messages.XEP_0249;
using XMPP_API.Classes.Network.XML.Messages.XEP_0045;
using UWP_XMPP_Client.Classes;

namespace UWP_XMPP_Client.Controls.Muc
{
    public sealed partial class MucOccupantsControl : UserControl
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public ChatTable Chat
        {
            get { return (ChatTable)GetValue(ChatProperty); }
            set
            {
                SetValue(ChatProperty, value);
                loadMembers();
            }
        }
        public static readonly DependencyProperty ChatProperty = DependencyProperty.Register("Chat", typeof(ChatTable), typeof(MucOccupantsControl), null);

        public XMPPClient Client
        {
            get { return (XMPPClient)GetValue(ClientProperty); }
            set { SetValue(ClientProperty, value); }
        }
        public static readonly DependencyProperty ClientProperty = DependencyProperty.Register("Client", typeof(XMPPClient), typeof(MucOccupantsControl), null);

        public MUCChatInfoTable MUCInfo
        {
            get { return (MUCChatInfoTable)GetValue(MUCInfoProperty); }
            set { SetValue(MUCInfoProperty, value); }
        }
        public static readonly DependencyProperty MUCInfoProperty = DependencyProperty.Register("MUCInfo", typeof(MUCChatInfoTable), typeof(MucOccupantsControl), null);

        private readonly ObservableCollection<MUCOccupantTemplate> OCCUPANTS;

        private bool canBan;
        private bool canKick;

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <history>
        /// 06/02/2018 Created [Fabian Sauter]
        /// </history>
        public MucOccupantsControl()
        {
            OCCUPANTS = new ObservableCollection<MUCOccupantTemplate>();
            MUCDBManager.INSTANCE.MUCOccupantChanged += INSTANCE_MUCMemberChanged;
            canBan = false;
            canKick = false;
            InitializeComponent();
        }

        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--
        private void setCanKickBan()
        {
            if (MUCInfo != null)
            {
                string chatId = MUCInfo.chatId;
                string nickname = MUCInfo.nickname;
                Task.Run(() =>
                {
                    MUCOccupantTable occupant = MUCDBManager.INSTANCE.getMUCOccupant(chatId, nickname);
                    if (occupant is null)
                    {
                        canBan = false;
                        canKick = false;
                    }
                    else
                    {
                        canKick = occupant.role >= MUCRole.MODERATOR;
                        canBan = occupant.affiliation >= MUCAffiliation.ADMIN;
                    }
                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => setIfKickBanButtonEnabled()).AsTask();
                });
            }
            else
            {
                canBan = false;
                canKick = false;
                setIfKickBanButtonEnabled();
            }
        }

        private void setIfKickBanButtonEnabled()
        {
            kickBan_btn.IsEnabled = (canBan || canKick) && members_dgrid.SelectedItems.Count > 0;
        }

        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--


        #endregion

        #region --Misc Methods (Private)--
        private void loadMembers()
        {
            if (Chat != null)
            {
                string chatId = Chat.id;
                Task.Run(async () =>
                {
                    List<MUCOccupantTable> list = MUCDBManager.INSTANCE.getAllMUCMembers(chatId);
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        OCCUPANTS.Clear();
                        foreach (MUCOccupantTable m in list)
                        {
                            OCCUPANTS.Add(new MUCOccupantTemplate() { occupant = m });
                        }
                    });
                });
            }
        }

        private async Task inviteUserAsync()
        {
            List<string> membersJidList = new List<string>();
            foreach (MUCOccupantTemplate m in OCCUPANTS)
            {
                if (m.jid != null)
                {
                    membersJidList.Add(m.jid);
                }
            }
            InviteUserMUCDialog dialog = new InviteUserMUCDialog(Client, membersJidList);
            await UiUtils.showDialogAsyncQueue(dialog);

            if (!dialog.canceled)
            {
                string reason = null;
                if (!string.IsNullOrWhiteSpace(dialog.Reason))
                {
                    reason = dialog.Reason;
                }
                DirectMUCInvitationMessage msg = new DirectMUCInvitationMessage(Client.getXMPPAccount().getBareJid(), dialog.UserJid, Chat.chatJabberId, MUCInfo.password, reason);
                await Client.sendAsync(msg);
            }
        }

        #endregion

        #region --Misc Methods (Protected)--


        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--
        private void members_dgrid_SelectionChanged(object sender, Telerik.UI.Xaml.Controls.Grid.DataGridSelectionChangedEventArgs e)
        {
            setIfKickBanButtonEnabled();
        }

        private async void invite_btn_Click(object sender, RoutedEventArgs e)
        {
            await inviteUserAsync();
        }

        private async void INSTANCE_MUCMemberChanged(MUCDBManager handler, Data_Manager2.Classes.Events.MUCOccupantChangedEventArgs args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (Chat is null || !Equals(args.MUC_OCCUPANT.chatId, Chat.id))
                {
                    return;
                }

                // Update kick ban button IsEnabled:
                if (MUCInfo != null && Equals(args.MUC_OCCUPANT.nickname, MUCInfo.nickname))
                {
                    setCanKickBan();
                    setIfKickBanButtonEnabled();
                }

                // Add occupant to collection:
                for (int i = 0; i < OCCUPANTS.Count; i++)
                {
                    if (Equals(OCCUPANTS[i].occupant.id, args.MUC_OCCUPANT.id))
                    {
                        if (args.REMOVED)
                        {
                            OCCUPANTS.RemoveAt(i);
                        }
                        else
                        {
                            OCCUPANTS[i] = new MUCOccupantTemplate()
                            {
                                occupant = args.MUC_OCCUPANT
                            };
                        }
                        return;
                    }
                }
                OCCUPANTS.Add(new MUCOccupantTemplate() { occupant = args.MUC_OCCUPANT });
            });
        }

        private async void kickBan_btn_Click(object sender, RoutedEventArgs e)
        {
            if (members_dgrid.SelectedItems.Count > 0)
            {
                ObservableCollection<MUCOccupantTemplate> collection = new ObservableCollection<MUCOccupantTemplate>();
                foreach (object o in members_dgrid.SelectedItems)
                {
                    if (o is MUCOccupantTemplate)
                    {
                        collection.Add(o as MUCOccupantTemplate);
                    }
                }

                MUCKickBanOccupantDialog dialog = new MUCKickBanOccupantDialog(collection, Client, Chat, canKick, canBan);
                await UiUtils.showDialogAsyncQueue(dialog);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            setCanKickBan();
        }

        #endregion
    }
}
