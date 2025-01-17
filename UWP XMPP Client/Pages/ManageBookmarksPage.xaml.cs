﻿using Logging;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using UWP_XMPP_Client.Classes;
using UWP_XMPP_Client.Classes.Collections;
using UWP_XMPP_Client.Dialogs;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XMPP_API.Classes;
using XMPP_API.Classes.Network.XML.Messages;
using XMPP_API.Classes.Network.XML.Messages.XEP_0048;

namespace UWP_XMPP_Client.Pages
{
    public sealed partial class ManageBookmarksPage : Page
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        private MessageResponseHelper<IQMessage> messageResponseHelper;
        private readonly CustomObservableCollection<ConferenceItem> BOOKMARKS;

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <history>
        /// 13/06/2018 Created [Fabian Sauter]
        /// </history>
        public ManageBookmarksPage()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BrowseMUCRoomsPage_BackRequested;
            messageResponseHelper = null;
            BOOKMARKS = new CustomObservableCollection<ConferenceItem>();
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
        private void requestBookmarks(XMPPClient c)
        {
            messageResponseHelper = c.PUB_SUB_COMMAND_HELPER.requestBookmars_xep_0048(onMessage, onTimeout);
        }

        private void onTimeout(MessageResponseHelper<IQMessage> helper)
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => noneFound_notification.Show("Request failed - timeout!")).AsTask();
        }

        private bool onMessage(MessageResponseHelper<IQMessage> helper, AbstractMessage msg)
        {
            if (msg is IQErrorMessage errorMsg)
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    BOOKMARKS.Clear();
                    if (errorMsg.ERROR_OBJ.ERROR_NAME == ErrorName.ITEM_NOT_FOUND)
                    {
                        noneFound_notification.Show("Found 0 bookmarks.", 2000);
                    }
                    else
                    {
                        noneFound_notification.Show("Request failed with:\n" + errorMsg.ERROR_OBJ.ERROR_NAME + " and " + errorMsg.ERROR_OBJ.ERROR_MESSAGE);
                    }
                    reload_abb.IsEnabled = true;
                    loading_grid.Visibility = Visibility.Collapsed;
                    main_grid.Visibility = Visibility.Visible;
                }).AsTask();
                return true;
            }
            else if (msg is BookmarksResultMessage result)
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    BOOKMARKS.Clear();
                    BOOKMARKS.AddRange(result.STORAGE.CONFERENCES);
                    if (result.STORAGE.CONFERENCES.Count > 1)
                    {
                        noneFound_notification.Show("Found " + result.STORAGE.CONFERENCES.Count + " bookmarks.", 2000);
                    }
                    else
                    {
                        noneFound_notification.Show("Found " + result.STORAGE.CONFERENCES.Count + " bookmark.", 2000);
                    }
                    reload_abb.IsEnabled = true;
                    loading_grid.Visibility = Visibility.Collapsed;
                    main_grid.Visibility = Visibility.Visible;
                }).AsTask();
                return true;
            }
            return false;
        }

        private void reload(XMPPClient c)
        {
            reload_abb.IsEnabled = false;
            if (c != null && c.isConnected())
            {
                loading_grid.Visibility = Visibility.Visible;
                main_grid.Visibility = Visibility.Collapsed;
                requestBookmarks(c);
            }
        }

        private void removeBookmark(ConferenceItem item)
        {
            XMPPClient c = account_asc.getSelectedAccount();
            if (c != null)
            {
                if (c.isConnected())
                {
                    noneFound_notification.Show("Can't remove bookmark - account not connected!");
                }
                else
                {

                }
                c.PUB_SUB_COMMAND_HELPER.setBookmars_xep_0048(BOOKMARKS, null, null);
            }
        }

        private bool onRemoveMessage(IQMessage msg)
        {
            if (msg is IQErrorMessage errMsg)
            {
                Logger.Warn("Failed to set bookmarks: " + errMsg.ToString());
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => noneFound_notification.Show("Failed to add bookmark - server error!\n" + errMsg.ToString())).AsTask();
                return true;
            }
            if (string.Equals(msg.TYPE, IQMessage.RESULT))
            {
                return true;
            }
            return false;
        }

        private void onRemoveTimeout()
        {
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => noneFound_notification.Show("Failed to add bookmark - server timeout!")).AsTask();
        }

        #endregion

        #region --Misc Methods (Protected)--


        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UiUtils.setBackgroundImage(backgroundImage_img);
        }

        private void BrowseMUCRoomsPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                return;
            }
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void reload_abb_Click(object sender, RoutedEventArgs e)
        {
            reload(account_asc.getSelectedAccount());
        }

        private void addAccount_hlb_Click(object sender, RoutedEventArgs e)
        {
            //(Window.Current.Content as Frame).Navigate(typeof(AccountSettingsPage));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            messageResponseHelper?.stop();
        }

        private async void add_abb_Click(object sender, RoutedEventArgs e)
        {
            AddBookmarkDialog dialog = new AddBookmarkDialog(account_asc.getSelectedAccount());
            await UiUtils.showDialogAsyncQueue(dialog);
            if (dialog.success)
            {
                noneFound_notification.Show("Bookmark added.", 2000);
            }
        }

        private void account_asc_AccountSelectionChanged(Controls.AccountSelectionControl sender, Classes.Events.AccountSelectionChangedEventArgs args)
        {
            reload(args.CLIENT);
        }

#pragma warning disable CS0618 // Type or member is obsolete
        private void SlidableListItem_SwipeStatusChanged(SlidableListItem sender, SwipeStatusChangedEventArgs args)
        {
            if (args.NewValue == SwipeStatus.Idle)
            {
                if (args.OldValue == SwipeStatus.SwipingPassedLeftThreshold || args.OldValue == SwipeStatus.SwipingPassedRightThreshold && sender.LeftCommandParameter is ConferenceItem)
                {
                    removeBookmark(sender.LeftCommandParameter as ConferenceItem);
                }
            }
        }
#pragma warning restore CS0618 // Type or member is obsolete

        private void ManageBookmarksDetailsControl_SaveClicked(Controls.ManageBookmarksDetailsControl sender, RoutedEventArgs args)
        {

        }

        private async void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            await UiUtils.onPageSizeChangedAsync(e);
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            await UiUtils.onPageNavigatedFromAsync();
        }

        private void BackRequest_btn_Click(object sender, RoutedEventArgs e)
        {
            if (masterDetail_pnl.ViewState == MasterDetailsViewState.Details)
            {
                masterDetail_pnl.SelectedItem = null;
            }
            else
            {
                Frame.GoBack();
            }
        }
        #endregion
    }
}
