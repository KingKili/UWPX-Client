﻿using System;
using System.Threading.Tasks;
using UWPX_UI.Controls;
using UWPX_UI.Dialogs;
using UWPX_UI.Pages.Settings;
using UWPX_UI_Context.Classes;
using UWPX_UI_Context.Classes.DataContext.Pages;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XMPP_API.Classes.Network;

namespace UWPX_UI.Pages
{
    public sealed partial class AddAccountPage : Page
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly AddAccountPageContext VIEW_MODEL = new AddAccountPageContext();

        /// <summary>
        /// Where should we navigate the frame to once we finished?
        /// </summary>
        private Type doneTargetPage = null;
        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        public AddAccountPage()
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
        private void UpdateViewState(string state)
        {
            VisualStateManager.GoToState(this, state, true);
        }

        private async Task Next()
        {
            UpdateViewState(State_1_Next.Name);
            await VIEW_MODEL.CreateAccount();
            UpdateViewState(State_2.Name);
            password_pwb.Focus(FocusState.Keyboard);
        }

        private async Task Save()
        {
            UpdateViewState(State_2_Save.Name);
            await VIEW_MODEL.SaveAccountAsync();
            UpdateViewState(State_1.Name);
        }

        private void NavigateAway()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                return;
            }

            if (doneTargetPage is null)
            {
                UiUtils.NavigateToPage(typeof(AccountsSettingsPage));
            }
            else
            {
                UiUtils.NavigateToPage(doneTargetPage);
            }

            // Make sure we remove the last entry from the back stack to prevent navigation back to this page:
            UiUtils.RemoveLastBackStackEntry();
        }

        #endregion

        #region --Misc Methods (Protected)--


        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--
        private async void Next_ipbtn_Click(IconProgressButtonControl sender, RoutedEventArgs args)
        {
            await Next();
        }

        private async void Save_ipbtn_Click(IconProgressButtonControl sender, RoutedEventArgs args)
        {
            await Save();

            NavigateAway();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is XMPPAccount account)
            {
                UpdateViewState(State_Edit.Name);
                UpdateViewState(State_2.Name);
                VIEW_MODEL.SetAccount(account);
            }
            else
            {
                if (e.Parameter is Type type)
                {
                    doneTargetPage = type;
                }
                UpdateViewState(State_Add.Name);
                UpdateViewState(State_1.Name);
            }
        }

        private async void Jid1_tbx_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && VIEW_MODEL.MODEL.IsValidBareJid)
            {
                await Next();
            }
        }

        private void Jid2_tbx_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && VIEW_MODEL.MODEL.IsValidBareJid)
            {
                password_pwb.Focus(FocusState.Keyboard);
            }
        }

        private async void Password_pwb_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && VIEW_MODEL.MODEL.IsValidBareJid)
            {
                await Save();
            }
        }

        private async void PickColor_btn_Click(object sender, RoutedEventArgs e)
        {
            Color color;
            if (UiUtils.IsHexColor(VIEW_MODEL.MODEL.Account.color))
            {
                color = UiUtils.HexStringToColor(VIEW_MODEL.MODEL.Account.color);
            }
            else
            {
                color = UiUtils.HexStringToColor(UiUtils.GenRandomHexColor());
            }
            ColorPickerDialog dialog = new ColorPickerDialog(color);
            await UiUtils.ShowDialogAsync(dialog);
            VIEW_MODEL.ColorSelected(dialog.VIEW_MODEL.MODEL);
        }

        private async void Delete_ipbtn_Click(IconProgressButtonControl sender, RoutedEventArgs args)
        {
            UpdateViewState(State_2_Delete.Name);
            DeleteAccountConfirmDialog dialog = new DeleteAccountConfirmDialog();
            await UiUtils.ShowDialogAsync(dialog);
            await VIEW_MODEL.DeleteAccountAsync(dialog.VIEW_MODEL.MODEL);
            UpdateViewState(State_2.Name);
            NavigateAway();
        }

        #endregion
    }
}