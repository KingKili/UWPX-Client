﻿using UWPX_UI_Context.Classes.DataTemplates.Dialogs;

namespace UWPX_UI_Context.Classes.DataContext.Dialogs
{
    public sealed class AddMucDialogContext
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly AddMucDialogDataTemplate MODEL = new AddMucDialogDataTemplate();

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--


        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--


        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--
        public void OnConfirm()
        {
            MODEL.Confirmed = true;
        }

        public void OnCancel()
        {
            MODEL.Confirmed = false;
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
