﻿using System.Linq;

namespace UWP_XMPP_Client.Classes.AppActivation
{
    class ToastActivationArgumentParser
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--


        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <history>
        /// 19/08/2018 Created [Fabian Sauter]
        /// </history>
        public ToastActivationArgumentParser()
        {
        }

        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--


        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--
        public static AbstractToastActivation parseArguments(string activationString)
        {
            if (string.IsNullOrEmpty(activationString) || !activationString.Contains('='))
            {
                return null;
            }

            string type = activationString.Substring(0, activationString.IndexOf('='));
            string args = activationString.Substring(activationString.IndexOf('=') + 1);

            switch (type)
            {
                case ChatToastActivation.TYPE:
                    return new ChatToastActivation(args, true);
            }

            return null;
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
