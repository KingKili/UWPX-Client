﻿using System.Threading.Tasks;
using Shared.Classes;
using UWPX_UI.Controls;
using UWPX_UI_Context.Classes.DataContext.Dialogs;
using UWPX_UI_Context.Classes.Events;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPX_UI.Dialogs
{
    public sealed partial class QrCodeScannerDialog: ContentDialog
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly QrCodeScannerDialogContext VIEW_MODEL = new QrCodeScannerDialogContext();

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        public QrCodeScannerDialog()
        {
            InitializeComponent();
            UpdateViewState(Scanning_State.Name);
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
        private async void QrCodeScannerControl_NewInvalidQrCode(QrCodeScannerControl sender, NewQrCodeEventArgs args)
        {
            await SharedUtils.CallDispatcherAsync(async () =>
            {
                invalidQrCode_grid.Visibility = Visibility.Visible;
                validQrCode_grid.Visibility = Visibility.Collapsed;
                await Task.Delay(5000);
                invalidQrCode_grid.Visibility = Visibility.Collapsed;
            });
        }

        private async void QrCodeScannerControl_NewValidQrCode(QrCodeScannerControl sender, NewQrCodeEventArgs args)
        {
            VIEW_MODEL.OnValidQrCode(args.QR_CODE);
            await SharedUtils.CallDispatcherAsync(async () =>
            {
                IsSecondaryButtonEnabled = false;
                invalidQrCode_grid.Visibility = Visibility.Collapsed;
                validQrCode_grid.Visibility = Visibility.Visible;
                await Task.Delay(1500);
                Hide();
            });
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            VIEW_MODEL.OnCancel();
        }

        private void UpdateViewState(string state)
        {
            VisualStateManager.GoToState(this, state, true);
        }
        #endregion
    }
}
