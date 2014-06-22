#region 版权声明
/**
 * 版权声明：LumaQQ.NET是基于LumaQQ分析的QQ协议，将其部分代码进行修改和翻译为.NET版本，并且继续使用LumaQQ的开源协议。
 * 本人没有对其核心协议进行改动， 也没有与腾讯公司的QQ软件有直接联系，请尊重LumaQQ作者Luma的著作权和版权声明。
 * 同时在使用此开发包前请自行协调好多方面关系，本人不享受和承担由此产生的任何权利以及任何法律责任。
 * 
 * 作者：阿不
 * 博客：http://hjf1223.cnblogs.com
 * Email：hjf1223@gmail.com
 * LumaQQ：http://lumaqq.linuxsir.org 
 * LumaQQ - Java QQ Client
 * 
 * Copyright (C) 2004 luma <stubma@163.com>
 * 
 * LumaQQ - For .NET QQClient
 * Copyright (C) 2008 阿不<hjf1223@gmail.com>
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace LumaQQ.NET.Net.Sockets
{
    public class ConnectionPool : IConnectionPool
    {
        private Dictionary<string, IConnection> registry;
        private Dictionary<IConnection, int> references;

        public ConnectionPool()
        {
            registry = new Dictionary<string, IConnection>();
            references = new Dictionary<IConnection, int>();
        }
        #region IConnectionPool Members

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Release(IConnection conn)
        {
            lock (registry)
            {
                conn.Close();
                conn.Dispose();
                foreach (string name in registry.Keys)
                {
                    if (registry[name] == conn)
                    {
                        registry.Remove(name);
                        break;
                    }
                }
                references.Remove(conn);
            }           
        }

        public void Release(string id)
        {
            IConnection conn = GetConnection(id);
            if (conn != null)
            {
                Release(conn);
            }
        }

        /// <summary>
        /// 发送一个包，由于是异步发送包，所以keepSent目前暂时无用
        /// <remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="keepSent">if set to <c>true</c> [keep sent].</param>
        public void Send(string id, LumaQQ.NET.Packets.OutPacket packet, bool keepSent)
        {
            IConnection conn = GetConnection(id);
            if (conn != null)
            {
                conn.Send(packet, keepSent);
            }
        }

        public IConnection NewUDPConnection(ConnectionPolicy policy, System.Net.EndPoint server, bool start)
        {
            IConnection conn = null;
            if (policy.Proxy.ProxyType == ProxyType.None)
            {
                conn = new UDPConnection(policy, server);
            }
            else
            {
                return null;
            }
            registry.Add(policy.ID, conn);
            if (start)
            {
                conn.Connect();
            }
            return conn;
        }

        public IConnection NewTCPConnection(ConnectionPolicy policy, System.Net.EndPoint server, bool start)
        {
            IConnection conn = null;
            if (policy.Proxy.ProxyType == ProxyType.None)
            {
                return null;
            }
            else
            {
                conn = new ProxyTCPConnection(policy, server, policy.Proxy);
            }
            if (start)
            {
                //如果网络连接失败，则返回null
                if (!conn.Connect())
                {
                    return null;
                }
            }
            registry.Add(policy.ID, conn);
            return conn;
        }

        public IConnection GetConnection(string id)
        {
            return registry[id];
        }

        public IConnection GetConnection(System.Net.EndPoint server)
        {
            return null;
        }

        public void Dispose()
        {
            foreach (IConnection conn in registry.Values)
            {
                conn.Dispose();
            }
            registry.Clear();
        }

        public bool HasConnection(string id)
        {
            return registry.ContainsKey(id);
        }

        public List<IConnection> GetConnections()
        {
            return new List<IConnection>(registry.Values);
        }

        #endregion
    }
}
