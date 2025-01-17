﻿using System.ComponentModel;
using UWPX_UI.Classes.Events;
using UWPX_UI.Controls;
using UWPX_UI_Context.Classes.DataContext.Dialogs;
using Windows.UI.Xaml.Controls;

namespace UWPX_UI.Dialogs
{
    public sealed partial class AddMucDialog: ContentDialog
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly AddMucDialogContext VIEW_MODEL = new AddMucDialogContext();

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        public AddMucDialog()
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
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            VIEW_MODEL.OnCancel();
            Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            VIEW_MODEL.OnConfirm();
            Hide();
        }

        private void AccountSelectionControl_AddAccountClick(AccountSelectionControl sender, CancelEventArgs args)
        {
            VIEW_MODEL.OnCancel();
            Hide();
        }

        private void AccountSelectionControl_AccountSelectionChanged(AccountSelectionControl sender, AccountSelectionChangedEventArgs args)
        {
            VIEW_MODEL.MODEL.Client = args.CLIENT;
        }

        private void Browse_ibtn_Click(IconButtonControl sender, Windows.UI.Xaml.RoutedEventArgs args)
        {
            VIEW_MODEL.OnCancel();
            Hide();
        }

        #endregion
    }
}
