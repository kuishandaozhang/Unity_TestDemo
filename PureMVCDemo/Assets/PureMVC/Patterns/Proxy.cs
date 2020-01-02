﻿namespace PureMVC.Patterns
{
    using PureMVC.Interfaces;
    using System;

    public class Proxy : Notifier, IProxy, INotifier
    {
        protected object m_data;
        protected string m_proxyName;
        public static string NAME = "Proxy";

        public Proxy() : this(NAME, null)
        {
        }

        public Proxy(string proxyName) : this(proxyName, null)
        {
        }

        public Proxy(string proxyName, object data)
        {
            this.m_proxyName = (proxyName != null) ? proxyName : NAME;
            if (data != null)
            {
                this.m_data = data;
            }
        }

        public virtual void OnRegister()
        {
        }

        public virtual void OnRemove()
        {
        }

        public object Data
        {
            get
            {
                return this.m_data;
            }
            set
            {
                this.m_data = value;
            }
        }

        public string ProxyName
        {
            get
            {
                return this.m_proxyName;
            }
        }
    }
}

