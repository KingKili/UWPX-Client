﻿using Data_Manager2.Classes.DBManager;
using Data_Manager2.Classes.DBTables;
using Data_Manager2.Classes.Events;
using Shared.Classes;
using UWPX_UI_Context.Classes.DataContext.Pages;
using UWPX_UI_Context.Classes.Events;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XMPP_API.Classes;

namespace UWPX_UI.Pages
{
    public sealed partial class ContactInfoPage: Page
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly ContactInfoPageContext VIEW_MODEL = new ContactInfoPageContext();

        public ChatTable Chat
        {
            get { return (ChatTable)GetValue(ChatProperty); }
            set { SetValue(ChatProperty, value); }
        }
        public static readonly DependencyProperty ChatProperty = DependencyProperty.Register(nameof(Chat), typeof(ChatTable), typeof(ContactInfoPage), new PropertyMetadata(null, OnChatChanged));

        public XMPPClient Client
        {
            get { return (XMPPClient)GetValue(ClientProperty); }
            set { SetValue(ClientProperty, value); }
        }
        public static readonly DependencyProperty ClientProperty = DependencyProperty.Register(nameof(Client), typeof(XMPPClient), typeof(ContactInfoPage), new PropertyMetadata(null));

        // So we don't have to always interrupt the main task when a chat changed:
        private string chatId = null;

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        public ContactInfoPage()
        {
            InitializeComponent();
        }

        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--


        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--


        #endregion

        #region --Misc Methods (Private)--


        #endregion

        #region --Misc Methods (Protected)--


        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            titleBar.OnPageNavigatedTo();

            if (e.Parameter is NavigatedToContactInfoPageEventArgs args)
            {
                Chat = args.CHAT;
                Client = args.CLIENT;
            }
            ChatDBManager.INSTANCE.ChatChanged -= INSTANCE_ChatChanged;
            ChatDBManager.INSTANCE.ChatChanged += INSTANCE_ChatChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            titleBar.OnPageNavigatedFrom();
            ChatDBManager.INSTANCE.ChatChanged -= INSTANCE_ChatChanged;
        }

        private static void OnChatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContactInfoPage contactInfoPage)
            {
                contactInfoPage.chatId = e.NewValue is ChatTable chat ? chat.id : null;
            }
        }

        private async void INSTANCE_ChatChanged(ChatDBManager handler, ChatChangedEventArgs args)
        {
            if (!(args.CHAT is null) && string.Equals(args.CHAT.id, chatId))
            {
                await SharedUtils.CallDispatcherAsync(() => Chat = args.CHAT).ConfAwaitFalse();
            }
        }

        #endregion
    }
}
