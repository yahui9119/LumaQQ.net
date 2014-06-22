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
    /// 登录管理
    /// 	<remark>abu 2008-03-08 </remark>
    /// </summary>
    public class LoginManager
    {
        internal LoginManager() { }
        /// <summary>
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <value></value>
        public QQClient QQClient { get; private set; }
        public QQUser QQUser { get { return QQClient.QQUser; } }
        internal LoginManager(QQClient client)
        {
            QQClient = client;
        }

        /// <summary>
        /// 登录
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        public void Login()
        {
            Check.Require(QQClient.LoginServerHost, "LoginServerHost", Check.NotNull);
            if (QQClient.LoginPort == 0)
            {
                if (QQUser.IsUdp)
                {
                    QQClient.LoginPort = QQGlobal.QQ_PORT_UDP;
                }
                else
                {
                    QQClient.LoginPort = QQGlobal.QQ_PORT_TCP;
                }
            }
            OutPacket outPacket = null;
            if (QQClient.QQUser.LoginToken == null)
            {
                outPacket = new RequestLoginTokenPacket(QQClient.QQUser);
            }
            else
            {
                outPacket = new LoginPacket(QQClient.QQUser);
            }
            QQClient.PacketManager.SendPacketAnyway(outPacket, QQPort.Main.Name);
        }

        /// <summary>在成功获取登录令牌后向服务器发送用户名密码
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        private void LoginUser()
        {
            LoginPacket packet = new LoginPacket(QQUser);
            QQClient.PacketManager.SendPacketAnyway(packet, QQPort.Main.Name);
        }

        /// <summary>
        /// 退出登录
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public void Logout()
        {
            if (QQClient.IsLogon)
            {
                LogoutPacket packet = new LogoutPacket(QQUser);
                QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name, true);
            }
            SetLogout();
        }
        /// <summary>
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        internal void SetLogout()
        {
            QQClient.IsLogon = false;
            QQUser.IsLoggedIn = false;
            QQUser.LoginToken = null;
            QQClient.LoginRedirect = false;
            QQClient.ConnectionManager.Dispose();
        }
        #region events
        /// <summary>
        /// 请求登录令牌成功
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<RequestLoginTokenReplyPacket, RequestLoginTokenPacket>> RequestLoginTokenSuccessed;
        /// <summary>
        /// Raises the <see cref="E:RequestLoginTokenSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.RequestLoginTokenReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnRequestLoginTokenSuccessed(QQEventArgs<RequestLoginTokenReplyPacket, RequestLoginTokenPacket> e)
        {
            if (RequestLoginTokenSuccessed != null)
            {
                RequestLoginTokenSuccessed(this, e);
            }
            //发送用户名密码
            LoginUser();
        }

        /// <summary>
        /// 请求登录令牌失败
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<RequestLoginTokenReplyPacket, RequestLoginTokenPacket>> RequestLoginTokenFailed;
        /// <summary>
        /// Raises the <see cref="E:RequestLoginTokenFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.RequestLoginTokenReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnRequestLoginTokenFailed(QQEventArgs<RequestLoginTokenReplyPacket, RequestLoginTokenPacket> e)
        {
            if (RequestLoginTokenFailed != null)
            {
                RequestLoginTokenFailed(this, e);
            }
        }

        /// <summary>登录成功事件
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<LoginReplyPacket, LoginPacket>> LoginSuccessed;
        /// <summary>
        /// Raises the <see cref="E:LoginSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.LoginReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnLoginSuccessed(QQEventArgs<LoginReplyPacket, LoginPacket> e)
        {
            this.QQClient.IsLogon = true;
            this.QQUser.IsLoggedIn = true;

            if (LoginSuccessed != null)
            {
                LoginSuccessed(this, e);
            }
        }
        /// <summary>
        /// 登录重定向事件
        /// </summary>
        public event EventHandler<QQEventArgs<LoginReplyPacket, LoginPacket>> LoginRedirect;
        internal void OnLoginRedirect(QQEventArgs<LoginReplyPacket, LoginPacket> e)
        {
            // 如果是登陆重定向，继续登陆
            QQClient.LoginRedirect = true;
            QQClient.ConnectionManager.ConnectionPool.Release(QQPort.Main.Name);
            QQClient.Login(Utils.Util.GetIpStringFromBytes(e.InPacket.RedirectIP), e.InPacket.RedirectPort);
            if (LoginRedirect != null)
            {
                LoginRedirect(this, e);
            }
        }

        /// <summary>
        /// 重定向登录时，重定向服务器为空
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<LoginReplyPacket, LoginPacket>> LoginRedirectNull;
        /// <summary>
        /// Raises the <see cref="E:LoginRedirectNull"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.LoginReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnLoginRedirectNull(QQEventArgs<LoginReplyPacket, LoginPacket> e)
        {
            if (LoginRedirectNull != null)
            {
                LoginRedirectNull(this, e);
            }
        }

        /// <summary>
        /// 登录失败触发的事件
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<LoginReplyPacket, LoginPacket>> LoginFailed;
        /// <summary>
        /// Raises the <see cref="E:LoginFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.LoginReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnLoginFailed(QQEventArgs<LoginReplyPacket, LoginPacket> e)
        {
            if (LoginFailed != null)
            {
                LoginFailed(this, e);
            }
        }

        /// <summary>
        /// 登录过程中的未知错误
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<LoginReplyPacket, LoginPacket>> LoginUnknownError;
        /// <summary>
        /// Raises the <see cref="E:LoginUnknownError"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.LoginReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnLoginUnknownError(QQEventArgs<LoginReplyPacket, LoginPacket> e)
        {
            if (LoginUnknownError != null)
            {
                LoginUnknownError(this, e);
            }
        }
        #endregion
    }
}
