﻿using Data_Manager2.Classes.DBManager;
using System.Threading.Tasks;
using UWPX_UI_Context.Classes.DataTemplates.Dialogs;
using UWPX_UI_Context.Classes.DataTemplates.Pages;
using XMPP_API.Classes;
using XMPP_API.Classes.Network;

namespace UWPX_UI_Context.Classes.DataContext.Pages
{
    public sealed class AddAccountPageContext
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly AddAccountPageDataTemplate MODEL = new AddAccountPageDataTemplate();

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--


        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--
        public void SetAccount(XMPPAccount account)
        {
            MODEL.Account = account;
        }

        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--
        public async Task CreateAccount()
        {
            await Task.Run(() =>
            {
                XMPPUser user = new XMPPUser(MODEL.BareJidText, Utils.getRandomResourcePart());
                XMPPAccount account = new XMPPAccount(user)
                {
                    color = UiUtils.GenRandomHexColor()
                };
                account.generateOmemoKeys();
                SetAccount(account);
            });
        }

        public async Task SaveAccountAsync()
        {
            // await Task.Delay(5000);
            await Task.Run(() => AccountDBManager.INSTANCE.setAccount(MODEL.Account, true));
        }

        public void ColorSelected(ColorPickerDialogDataTemplate dataTemplate)
        {
            if (dataTemplate.Confirmed)
            {
                MODEL.Account.color = UiUtils.ColorToHexString(dataTemplate.SelectedColor);
            }
        }

        public async Task DeleteAccountAsync(DeleteAccountConfirmDialogDataTemplate dataTemplate)
        {
            if (dataTemplate.Confirmed)
            {
                await Task.Run(async () =>
                {
                    if (!dataTemplate.KeepChatMessages)
                    {
                        await ChatDBManager.INSTANCE.deleteAllChatMessagesForAccountAsync(MODEL.Account.getBareJid());
                    }
                    if (!dataTemplate.KeepChats)
                    {
                        ChatDBManager.INSTANCE.deleteAllChatsForAccount(MODEL.Account.getBareJid());
                    }
                    AccountDBManager.INSTANCE.deleteAccount(MODEL.Account, true, true);
                });
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