﻿using Data_Manager2.Classes.DBTables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using UWP_XMPP_Client.DataTemplates;

namespace Thread_Save_Components.Classes.Collections
{
    public class ObservableOrderedDictionary : ICollection<ChatTemplate>, INotifyCollectionChanged, INotifyPropertyChanged, IDisposable, IList
    {
        //--------------------------------------------------------Attributes:-----------------------------------------------------------------\\
        #region --Attributes--
        public int Count => SORTED_LIST.Count;
        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        object IList.this[int index]
        {
            get => SORTED_LIST[index];
            set
            {
                ChatTemplate oldChat = SORTED_LIST[index];
                SORTED_LIST[index] = (ChatTemplate)value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldChat, value));
            }
        }

        private readonly Dictionary<string, ChatTemplate> DICTIONARY;
        private readonly List<ChatTemplate> SORTED_LIST;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        //--------------------------------------------------------Constructor:----------------------------------------------------------------\\
        #region --Constructors--
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <history>
        /// 20/07/2018 Created [Fabian Sauter]
        /// </history>
        public ObservableOrderedDictionary()
        {
            this.DICTIONARY = new Dictionary<string, ChatTemplate>();
            this.SORTED_LIST = new List<ChatTemplate>();
        }

        #endregion
        //--------------------------------------------------------Set-, Get- Methods:---------------------------------------------------------\\
        #region --Set-, Get- Methods--
        public IEnumerator<ChatTemplate> GetEnumerator()
        {
            return SORTED_LIST.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SORTED_LIST.GetEnumerator();
        }

        #endregion
        //--------------------------------------------------------Misc Methods:---------------------------------------------------------------\\
        #region --Misc Methods (Public)--
        public void Add(ChatTemplate item)
        {
            InternalAdd(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public bool UpdateChat(ChatTable chat)
        {
            if (!DICTIONARY.ContainsKey(chat.id))
            {
                return false;
            }
            else
            {
                ChatTemplate node = DICTIONARY[chat.id];
                ChatTemplate cur = node;
                int i = cur.chat.lastActive.CompareTo(chat.lastActive); // Sorted ascending
                cur.chat = chat;
                return true;
            }
        }

        /// <summary>
        /// Based on: https://stackoverflow.com/questions/670577/observablecollection-doesnt-support-addrange-method-so-i-get-notified-for-each/45364074#45364074
        /// </summary>
        public void AddRange(IEnumerable<ChatTemplate> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection is ICollection<ChatTemplate> list)
            {
                if (list.Count == 0) return;
            }
            else if (!collection.Any())
            {
                return;
            }
            else
            {
                list = new List<ChatTemplate>(collection);
            }

            foreach (var i in collection)
            {
                InternalAdd(i);
            }
            OnCollectionReset();
        }

        public void Clear()
        {
            DICTIONARY.Clear();
            SORTED_LIST.Clear();
            OnCollectionReset();
        }

        public bool Contains(ChatTemplate item)
        {
            return DICTIONARY.ContainsKey(item.chat.id);
        }

        public void CopyTo(ChatTemplate[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (var item in DICTIONARY)
            {
                array[i++] = item.Value;
            }
        }

        public bool Remove(string id)
        {
            if (DICTIONARY.ContainsKey(id))
            {
                ChatTemplate item = DICTIONARY[id];
                item.PropertyChanged -= Item_PropertyChanged;
                SORTED_LIST.Remove(DICTIONARY[id]);
                DICTIONARY.Remove(id);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
                return true;
            }
            return false;
        }

        public bool Remove(ChatTemplate item)
        {
            return Remove(item.chat.id);
        }

        public void Dispose()
        {
            foreach (var item in SORTED_LIST)
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }
        }

        public int IndexOf(ChatTemplate item)
        {
            return SORTED_LIST.IndexOf(item);
        }

        public void Insert(int index, ChatTemplate item)
        {
            if (!DICTIONARY.ContainsKey(item.chat.id))
            {
                SORTED_LIST.Insert(index, item);
                DICTIONARY.Add(item.chat.id, item);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }
        }

        public void RemoveAt(int index)
        {
            ChatTemplate item = SORTED_LIST[index];
            SORTED_LIST.RemoveAt(index);
            DICTIONARY.Remove(item.chat.id);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        public int Add(object value)
        {
            int index = InternalAdd((ChatTemplate)value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            return index;
        }

        public bool Contains(object value)
        {
            return Contains((ChatTemplate)value);
        }

        public int IndexOf(object value)
        {
            return IndexOf((ChatTemplate)value);
        }

        public void Insert(int index, object value)
        {
            Insert(index, (ChatTemplate)value);
        }

        public void Remove(object value)
        {
            Remove((ChatTemplate)value);
        }

        public void CopyTo(Array array, int index)
        {
            int i = index;
            foreach (var item in DICTIONARY)
            {
                array.SetValue(item, index);
            }
        }

        #endregion

        #region --Misc Methods (Private)--
        private int InternalAdd(ChatTemplate item)
        {
            if (!DICTIONARY.ContainsKey(item.chat.id))
            {
                item.PropertyChanged += Item_PropertyChanged;
                int index = InternalAddSortedToList(item);
                DICTIONARY.Add(item.chat.id, item);
                return index;
            }
            return IndexOf(item);
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        #endregion

        #region --Misc Methods (Protected)--
        protected int InternalAddSortedToList(ChatTemplate item)
        {
            for (int i = 0; i < SORTED_LIST.Count; i++)
            {
                if (SORTED_LIST[i].chat.lastActive.CompareTo(item.chat.lastActive) >= 0)
                {
                    SORTED_LIST.Insert(i, item);
                    return i;
                }
            }
            SORTED_LIST.Add(item);
            return SORTED_LIST.Count - 1;
        }

        #endregion
        //--------------------------------------------------------Events:---------------------------------------------------------------------\\
        #region --Events--
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion
    }
}
