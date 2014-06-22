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

using LumaQQ.NET.Net;
using LumaQQ.NET.Events;
using LumaQQ.NET.Packets;
using LumaQQ.NET.Entities;
using LumaQQ.NET.Threading;
using LumaQQ.NET.Packets.In;
using LumaQQ.NET.Packets.Out;
namespace LumaQQ.NET
{
    /// <summary>
    /// 连接管理
    /// 	<remark>abu 2008-03-06 </remark>
    /// </summary>
    public class ConnectionManager : IDisposable
    {
        internal ConnectionManager() { }
        /// <summary>
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <value></value>
        public IConnectionPool ConnectionPool { get; private set; }
        /// <summary>
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <value></value>
        public QQClient QQClient { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionManager"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        internal ConnectionManager(QQClient client)
        {
            QQClient = client;
            ConnectionPool = new LumaQQ.NET.Net.Sockets.ConnectionPool();
        }
        /// <summary>用户可以使用这个方法更改连接池的实现
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="pool">The pool.</param>
        public void SetConnectionPool(IConnectionPool pool)
        {
            this.ConnectionPool = pool;
        }

        /// <summary>确认指定的PortName的连接存在
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="serverHost">The server host.</param>
        /// <param name="port">The port.</param>
        /// <param name="portName">Name of the port.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        public bool EnsureConnection(string portName, bool start)
        {
            if (ConnectionPool.HasConnection(portName) && ConnectionPool.GetConnection(portName).IsConnected)
            {
                return true;
            }
            else
            {
                IConnection conn = QQPort.GetPort(portName).Create(QQClient, QQClient.LoginServerHost, QQClient.LoginPort, start);
                if (conn == null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 根据连接名称获得连接
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="portName">Name of the port.</param>
        public IConnection GetConnection(string portName)
        {
            return ConnectionPool.GetConnection(portName);
        }
        /// <summary>
        /// 一般网络错误事件
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        public event EventHandler<ErrorEventArgs> NetworkError;
 
        /// <summary>
        /// 网络连接成功
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        public event EventHandler ConnectSuccessed;

        /// <summary>
        /// 连接服务器失败
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<ErrorEventArgs> ConnectServerError;
        /// <summary>
        /// Raises the <see cref="E:ConnectServerError"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.ErrorEventArgs"/> instance containing the event data.</param>
        internal void OnConnectServerError(Exception e)
        {
            if (ConnectServerError != null)
            {
                ConnectServerError(this, new ErrorEventArgs(e));
            }
        }
        /// <summary>
        /// Called when [network error].
        /// </summary>
        /// <param name="e">The e.</param>
        internal protected void OnNetworkError(Exception e)
        {
            if (NetworkError != null)
            {
                NetworkError(this, new ErrorEventArgs(e));
            }
        }


        /// <summary>
        /// Called when [connect successed].
        /// </summary>
        internal protected void OnConnectSuccessed()
        {
            if (ConnectSuccessed != null)
            {
                ConnectSuccessed(this, EventArgs.Empty);
            }
        }

        /// <summary>接收到保持连接回复包
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<KeepAliveReplyPacket, KeepAlivePacket>> ReceivedKeepAlive;
        /// <summary>
        /// Raises the <see cref="E:ReceivedKeepAlive"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.KeepAliveReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceivedKeepAlive(QQEventArgs<KeepAliveReplyPacket, KeepAlivePacket> e)
        {
            if (ReceivedKeepAlive != null)
            {
                ReceivedKeepAlive(this, e);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            ConnectionPool.Dispose();
        }

        #endregion
    }
}
