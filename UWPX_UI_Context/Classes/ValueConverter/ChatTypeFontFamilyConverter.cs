﻿using System;
using Data_Manager2.Classes;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UWPX_UI_Context.Classes.ValueConverter
{
    public sealed class ChatTypeFontFamilyConverter: IValueConverter
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--


        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--


        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--


        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ChatType chatType && chatType == ChatType.MUC)
            {
                return ThemeUtils.GetThemeResource<FontFamily>("SymbolThemeFontFamily");
            }
            return ThemeUtils.GetThemeResource<FontFamily>("ContentControlThemeFontFamily");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
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
