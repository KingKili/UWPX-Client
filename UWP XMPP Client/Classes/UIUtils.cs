﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Controls;
using UWP_XMPP_Client.DataTemplates;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Metadata;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using XMPP_API.Classes;

namespace UWP_XMPP_Client.Classes
{
    internal static class UiUtils
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        private static TaskCompletionSource<ContentDialog> contentDialogShowRequest;
        private static readonly Regex HEX_COLOR_REGEX = new Regex("^#[0-9a-fA-F]{6}$");
        private static readonly Random RANDOM = new Random();
        public static readonly char[] TRIM_CHARS = { ' ', '\t', '\n', '\r' };

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--


        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--
        public static bool isDarkThemeActive()
        {
            return Application.Current.RequestedTheme == ApplicationTheme.Dark;
        }

        public static async Task<ContentDialogResult> showDialogAsyncQueue(ContentDialog dialog)
        {
            // Make sure it gets invoked by the UI thread:
            if (!Window.Current.Dispatcher.HasThreadAccess)
            {
                throw new InvalidOperationException("This method can only be invoked from UI thread.");
            }

            while (contentDialogShowRequest != null)
            {
                await contentDialogShowRequest.Task;
            }

            contentDialogShowRequest = new TaskCompletionSource<ContentDialog>();
            ContentDialogResult result = await dialog.ShowAsync();
            contentDialogShowRequest.SetResult(dialog);
            contentDialogShowRequest = null;

            return result;
        }

        public static void setBackgroundImage(ImageEx imgControl)
        {
            BackgroundImageTemplate img = BackgroundImageCache.selectedImage;
            if (img is null || img.imagePath is null)
            {
                imgControl.Source = null;
                imgControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                imgControl.Source = new BitmapImage(new Uri(img.imagePath));
                imgControl.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Returns a random RGB color as hex string.
        /// </summary>
        public static string getRandomColorHexString()
        {
            return string.Format("#{0:X6}", RANDOM.Next(0x1000000));
        }

        /// <summary>
        /// Returns a random RGB color.
        /// </summary>
        public static Color getRandomColor()
        {
            return convertHexStringToColor(getRandomColorHexString());
        }

        public static SolidColorBrush getPresenceBrush(Presence presence)
        {
            switch (presence)
            {
                case Presence.Online:
                    return (SolidColorBrush)Application.Current.Resources["PresenceOnlineBrush"];

                case Presence.Chat:
                    return (SolidColorBrush)Application.Current.Resources["PresenceChatBrush"];

                case Presence.Away:
                    return (SolidColorBrush)Application.Current.Resources["PresenceAwayBrush"];

                case Presence.Xa:
                    return (SolidColorBrush)Application.Current.Resources["PresenceXaBrush"];

                case Presence.Dnd:
                    return (SolidColorBrush)Application.Current.Resources["PresenceDndBrush"];

                default:
                    return (SolidColorBrush)Application.Current.Resources["PresenceUnavailableBrush"];

            }
        }

        public static bool isHexColor(string color)
        {
            return color != null && HEX_COLOR_REGEX.Match(color).Success;
        }

        /// <summary>
        /// Checks whether the current device is a Windows Mobile device.
        /// </summary>
        public static bool isRunningOnMobileDevice()
        {
            return AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Mobile");
        }

        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--
        /// <summary>
        /// Generates a random bare JID.
        /// e.g. 'chat.shakespeare.lit'
        /// </summary>
        /// <returns>A random bare JID string.</returns>
        public static string genRandomBareJid()
        {
            StringBuilder sb = new StringBuilder(genRandomString(RANDOM.Next(4, 10)));
            sb.Append('@');
            sb.Append(genRandomString(RANDOM.Next(4, 6)));
            sb.Append('.');
            sb.Append(genRandomString(RANDOM.Next(2, 4)));
            return sb.ToString();
        }

        /// <summary>
        /// Generates a random string and returns it.
        /// Based on: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
        /// </summary>
        /// <param name="length">The length of the string that should be generated.</param>
        /// <returns>A random string.</returns>
        private static string genRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[RANDOM.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Converts the given Color to a hex string e.g. #012345.
        /// </summary>
        /// <param name="c">The Color that should get converted.</param>
        /// <returns>A hex string representing the given Color object.</returns>
        public static string convertColorToHexString(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        /// <summary>
        /// Converts the given hex string into a Color object.
        /// </summary>
        /// <param name="hexString">The hex color string e.g. #012345.</param>
        /// <returns>Returns the Color object representing the given hex color.</returns>
        public static Color convertHexStringToColor(string hexString)
        {
            hexString = hexString.Replace("#", string.Empty);
            byte r = (byte)(Convert.ToUInt32(hexString.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hexString.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hexString.Substring(4, 2), 16));
            return Color.FromArgb(255, r, g, b);
        }

        /// <summary>
        /// Converts the given hex string into a SolidColorBrush object.
        /// </summary>
        /// <param name="hexString">The hex color string e.g. #012345.</param>
        /// <returns>Returns the SolidColorBrush object representing the given hex color.</returns>
        public static SolidColorBrush convertHexStringToBrush(string hexString)
        {
            return new SolidColorBrush(convertHexStringToColor(hexString));
        }

        /// <summary>
        /// Hides the StatusBar on Windows Mobile devices asynchronously.
        /// </summary>
        public static async Task hideStatusBarAsync()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }
        }

        /// <summary>
        /// Shows the StatusBar on Windows Mobile devices asynchronously.
        /// </summary>
        public static async Task showStatusBarAsync()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().ShowAsync();
            }
        }

        /// <summary>
        /// Launches the default application for the given Uri.
        /// </summary>
        /// <param name="url">The Uri that defines the application that should get launched.</param>
        /// <returns>Returns true on success.</returns>
        public static async Task<bool> launchUriAsync(Uri url)
        {
            return await Windows.System.Launcher.LaunchUriAsync(url);
        }

        public static void addTextToClipboard(string text)
        {
            DataPackage package = new DataPackage();
            package.SetText(text);
            Clipboard.SetContent(package);
        }

        /// <summary>
        /// Manages the Windows Mobile StatusBar asynchronously.
        /// </summary>
        public static async Task onPageNavigatedFromAsync()
        {
            if (isRunningOnMobileDevice())
            {
                await showStatusBarAsync();
            }
        }

        /// <summary>
        /// Manages the Windows Mobile StatusBar asynchronously.
        /// </summary>
        public static async Task onPageSizeChangedAsync(SizeChangedEventArgs e)
        {
            if (isRunningOnMobileDevice())
            {
                if (e.NewSize.Height < e.NewSize.Width)
                {
                    await hideStatusBarAsync();
                }
                else
                {
                    await showStatusBarAsync();
                }
            }
        }
        #endregion

        #region --Misc Methods (Private)--


        #endregion

        #region --Misc Methods (Protected)--


        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--


        #endregion
    }
}
