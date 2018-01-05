﻿using UWP_XMPP_Client.Classes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP_XMPP_Client.Controls
{
    public sealed partial class BrowseMUCRoomsMasterControl : UserControl
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public MUCRoomInfo RoomInfo
        {
            get { return (MUCRoomInfo)GetValue(RoomInfoProperty); }
            set
            {
                SetValue(RoomInfoProperty, value);
                showRoom();
            }
        }
        public static readonly DependencyProperty RoomInfoProperty = DependencyProperty.Register("RoomInfo", typeof(MUCRoomInfo), typeof(BrowseMUCRoomsDetailsControl), null);

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <history>
        /// 04/01/2018 Created [Fabian Sauter]
        /// </history>
        public BrowseMUCRoomsMasterControl()
        {
            this.InitializeComponent();
        }

        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--


        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--


        #endregion

        #region --Misc Methods (Private)--
        private void showRoom()
        {
            if (RoomInfo != null)
            {
                if (string.IsNullOrEmpty(RoomInfo.name))
                {
                    name_tblck.Visibility = Visibility.Collapsed;
                }
                else
                {
                    name_tblck.Visibility = Visibility.Visible;
                    name_tblck.Text = RoomInfo.name;
                }
                jid_tblck.Text = RoomInfo.jid ?? "";
            }
        }

        #endregion

        #region --Misc Methods (Protected)--


        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--


        #endregion
    }
}
