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
    /// 	<remark>abu 2008-03-07 </remark>
    /// </summary>
    public class QQClient
    {
        public QQUser QQUser { get; private set; }
        /// <summary>
        /// 服务器地址
        /// 	<remark>abu 2008-03-05 </remark>
        /// </summary>
        /// <value></value>
        public string LoginServerHost { get; set; }
        /// <summary>
        /// 登录端口
        /// 	<remark>abu 2008-03-05 </remark>
        /// </summary>
        /// <value></value>
        public int LoginPort { get; set; }
        /// <summary>
        /// 是否已登录
        /// 	<remark>abu 2008-03-05 </remark>
        /// </summary>
        /// <value></value>
        public bool IsLogon { get; set; }
        /// <summary>
        /// 经过重定向登录
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <value></value>
        public bool LoginRedirect { get; set; }
        /// <summary>
        /// 连接管理
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        /// <value></value>
        public ConnectionManager ConnectionManager { get; private set; }
        /// <summary>包管理
        /// Gets or sets the packet manager.
        /// </summary>
        /// <value>The packet manager.</value>
        public PacketManager PacketManager { get; private set; }
        /// <summary>登录管理
        /// Gets or sets the login manager.
        /// </summary>
        /// <value>The login manager.</value>
        public LoginManager LoginManager { get; private set; }
        /// <summary>消信管理
        /// Gets or sets the message manager.
        /// </summary>
        /// <value>The message manager.</value>
        public MessageManager MessageManager { get; private set; }
        /// <summary>好友管理
        /// Gets or sets the friend manager.
        /// </summary>
        /// <value>The friend manager.</value>
        public FriendManager FriendManager { get; private set; }
        /// <summary>个人资料管理
        /// Gets or sets the private manager.
        /// </summary>
        /// <value>The private manager.</value>
        public PrivateManager PrivateManager { get; private set; }
        /// <summary>
        /// 使用的代理服务器
        /// 	<remark>abu 2008-03-05 </remark>
        /// </summary>
        /// <value></value>
        public Proxy Proxy { get; set; }

        /// <summary>
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="user">The user.</param>
        public QQClient(QQUser user)
        {
            PacketManager = new PacketManager(this);
            ConnectionManager = new ConnectionManager(this);
            LoginManager = new LoginManager(this);
            MessageManager = new MessageManager(this);
            FriendManager = new FriendManager(this);
            PrivateManager = new PrivateManager(this);
            // this.inConn = new Dictionary<InPacket, string>();
            this.QQUser = user;
            this.Proxy = new Proxy();
        }

        /// <summary>
        /// 登录
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public void Login(string server, int port)
        {
            this.LoginServerHost = server;
            this.LoginPort = port;
            this.Login();
        }

        /// <summary>
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        public void Login()
        {
            LoginManager.Login();
        }
        /// <summary>
        /// 保持登录状态
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public void KeepAlive()
        {
            if (IsLogon)
            {
                KeepAlivePacket packet = new KeepAlivePacket(QQUser);
                PacketManager.SendPacket(packet, QQPort.Main.Name);
            }
        }


        #region Events
        /// <summary>
        /// 错误事件
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ErrorPacket, OutPacket>> Error;
        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="e">The e.</param>
        internal void OnError(QQEventArgs<ErrorPacket, OutPacket> e)
        {
            if (Error != null)
            {
                Error(this, e);
            }
        }
        #endregion

        /// <summary>
        /// 在程序出现运行时异常时产生一个崩溃报告
        /// 	<remark>abu 2008-03-05 </remark>
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public string GenerateCrashReport(Exception e, Packets.Packet p)
        {
            return e.Message + "\n" + e.StackTrace;
        }
    }
}
