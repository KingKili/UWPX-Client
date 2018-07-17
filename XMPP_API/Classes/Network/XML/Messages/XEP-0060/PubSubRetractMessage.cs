﻿using System.Xml.Linq;

namespace XMPP_API.Classes.Network.XML.Messages.XEP_0060
{
    public abstract class PubSubRetractMessage : PubSubMessage
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public readonly string NODE_NAME;

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <history>
        /// 04/07/2018 Created [Fabian Sauter]
        /// </history>
        public PubSubRetractMessage(string from, string to, string nodeName) : base(from, to)
        {
            this.NODE_NAME = nodeName;
        }

        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--
        protected override XElement getContent(XNamespace ns)
        {
            XElement retractNode = new XElement(ns + "retract");
            retractNode.Add(new XAttribute("node", NODE_NAME));
            AbstractPubSubItem item = getPubSubItem();
            if (item != null)
            {
                retractNode.Add(item.toXElement(ns));
            }
            return retractNode;
        }

        protected abstract AbstractPubSubItem getPubSubItem();

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
