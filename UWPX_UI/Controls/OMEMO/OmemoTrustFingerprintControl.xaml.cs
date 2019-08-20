﻿using UWPX_UI_Context.Classes.DataContext.Controls.OMEMO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XMPP_API.Classes.Network.XML.Messages.XEP_0384;

namespace UWPX_UI.Controls.OMEMO
{
    public sealed partial class OmemoTrustFingerprintControl: UserControl
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public OmemoFingerprint Fingerprint
        {
            get { return (OmemoFingerprint)GetValue(FingerprintProperty); }
            set { SetValue(FingerprintProperty, value); }
        }
        public static readonly DependencyProperty FingerprintProperty = DependencyProperty.Register(nameof(Fingerprint), typeof(OmemoFingerprint), typeof(OmemoTrustFingerprintControl), new PropertyMetadata(null));

        public readonly OmemoTrustFingerprintControlContext VIEW_MODEL = new OmemoTrustFingerprintControlContext();
        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        public OmemoTrustFingerprintControl()
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


        #endregion
    }
}
