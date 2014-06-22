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
    /// 信息管理
    /// <remark>abu 2008-03-10 </remark>
    /// </summary>
    public class MessageManager
    {
        internal MessageManager() { }
        /// <summary>
        /// Gets or sets the QQ client.
        /// </summary>
        /// <value>The QQ client.</value>
        public QQClient QQClient { get; private set; }
        /// <summary>
        /// Gets the QQ user.
        /// </summary>
        /// <value>The QQ user.</value>
        public QQUser QQUser { get { return QQClient.QQUser; } }
        /// <summary>
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="client">The client.</param>
        internal MessageManager(QQClient client)
        {
            QQClient = client;
        }
        /// <summary>发送普通信息
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        public void SendIM(int receiver, string message)
        {
            SendIM(receiver, message, new FontStyle());
        }
        /// <summary>
        /// 发送普通信息
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="fontSytle">The font sytle.</param>
        public void SendIM(int receiver, string message, FontStyle fontSytle)
        {
            SendIM(receiver, message, 1, 0, fontSytle);
        }
        /// <summary>
        /// 发送普通信息
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="totalFragments">The total fragments.总分块数</param>
        /// <param name="fragementSequence">The fragement sequence.当前当块序号</param>
        /// <param name="fontSytle">The font sytle.</param>
        public void SendIM(int receiver, string message, int totalFragments,
            int fragementSequence, FontStyle fontSytle)
        {
            SendIMPacket packet = new SendIMPacket(QQUser);
            packet.Receiver = receiver;
            packet.Message = Utils.Util.GetBytes(message);
            packet.TotalFragments = totalFragments;
            packet.FragmentSequence = fragementSequence;
            packet.FontStyle = fontSytle;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>发送临时信息
        /// Sends the temp IM.
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="myNick">My nick.</param>
        public void SendTempIM(int receiver, string message, string myNick)
        {
            SendTempIM(receiver, message, myNick, new FontStyle());
        }
        /// <summary>
        /// 发送临时信息
        /// <remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="receiver">The receiver.</param>
        /// <param name="message">The message.</param>
        /// <param name="myNick">My nick.你的昵称</param>
        /// <param name="fontSytle">The font sytle.</param>
        public void SendTempIM(int receiver, string message, string myNick, FontStyle fontSytle)
        {
            TempSessionOpPacket packet = new TempSessionOpPacket(QQUser);
            packet.SubCommand = TempSessionSubCmd.SendIM;
            packet.Receiver = receiver;
            packet.Message = message;
            packet.Nick = myNick;
            packet.FontStyle = fontSytle;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>发送接收信息回复包
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="imPacket">The im packet.</param>
        internal void SendReceiveReplyPacket(ReceiveIMPacket inPacket)
        {
            ReceiveIMReplyPacket reply = new ReceiveIMReplyPacket(inPacket.Reply, QQUser);
            reply.Sequence = inPacket.Sequence;
            QQClient.PacketManager.SendPacket(reply);
        }
        #region events

        /// <summary>
        /// 收到一条重复信息
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> ReceiveDuplicatedIM;
        /// <summary>
        /// Raises the <see cref="E:ReceiveDuplicatedIM"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceiveDuplicatedIM(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (ReceiveDuplicatedIM != null)
            {
                ReceiveDuplicatedIM(this, e);
            }
        }

        /// <summary>
        /// 收到一条普通信息
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> ReceiveNormalIM;
        /// <summary>
        /// Raises the <see cref="E:ReceiveNormalIM"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceiveNormalIM(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (ReceiveNormalIM != null)
            {
                ReceiveNormalIM(this, e);
            }
        }

        /// <summary>收到一条未知类型的信息，暂时无法处理
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> ReceiveUnknownIM;
        /// <summary>
        /// Raises the <see cref="E:ReceiveUnknownIM"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceiveUnknownIM(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (ReceiveUnknownIM != null)
            {
                ReceiveUnknownIM(this, e);
            }
        }

        /// <summary>事件在收到你的QQ号在其他地方登陆导致你被系统踢出时发生，
        /// * source是SystemNotificationPacket。系统通知和系统消息是不同的两种事件，系统通知是对你一个人发
        /// * 出的（或者是和你相关的），系统消息是一种广播式的，每个人都会收到，要分清楚这两种事件。此外
        /// * 系统通知的载体是SystemNotificationPacket，而系统消息是ReceiveIMPacket，ReceiveIMPacket的功
        /// * 能和格式很多。这也是一个区别。注意其后的我被其他人加为好友，验证被通过被拒绝等等，都是系统
        /// * 通知范畴
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> ReceiveKickOut;
        /// <summary>
        /// Raises the <see cref="E:ReceiveKickOut"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceiveKickOut(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            //设置为未登录
            QQClient.LoginManager.SetLogout();
            if (ReceiveKickOut != null)
            {
                ReceiveKickOut(this, e);
            }
        }

        /// <summary>
        /// 收到系统消息
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> ReceiveSysMessage;
        /// <summary>
        /// Raises the <see cref="E:ReceiveSysMessage"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceiveSysMessage(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (ReceiveSysMessage != null)
            {
                ReceiveSysMessage(this, e);
            }
        }

        /// <summary>事件发生在有人将我加为好友时
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysAddedByOthers;
        /// <summary>
        /// Raises the <see cref="E:SysAddedByOthers"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysAddedByOthers(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysAddedByOthers != null)
            {
                SysAddedByOthers(this, e);
            }
        }

        /// <summary> 事件发生在有人将我加为好友时
        /// 当对方使用0x00A8命令
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysAddedByOthersEx;
        /// <summary>
        /// Raises the <see cref="E:AddedByOthersEx"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnAddedByOthersEx(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysAddedByOthersEx != null)
            {
                SysAddedByOthersEx(this, e);
            }
        }

        /// <summary>事件发生在有人请求加我为好友时，SysAddedByOthers是我没有设置验证
        /// 是发生的，这个事件是我如果设了验证时发生的，两者不会都发生。
        /// 当对方不使用0x00A8命令发送认证消息，才会收到此系统通知
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysRequestAddMe;
        /// <summary>
        /// Raises the <see cref="E:SysRequestAddMe"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysRequestAddMe(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysRequestAddMe != null)
            {
                SysRequestAddMe(this, e);
            }
        }
        /// <summary>事件发生在有人请求加我为好友时
        /// 这是SysRequestAddMe的扩展事件，在2005中使用 当对方使用0x00A8命令发送认证消息，才会收到此系统通知
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysRequestAddMeEx;
        /// <summary>
        /// Raises the <see cref="E:SysRequestAddMeEx"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysRequestAddMeEx(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysRequestAddMeEx != null)
            {
                SysRequestAddMeEx(this, e);
            }
        }

        /// <summary>事件发生在我请求加一个人，
        /// 那个人同意我加的时候 
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysAddOtherApproved;
        /// <summary>
        /// Raises the <see cref="E:SysAddOtherApproved"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysAddOtherApproved(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysAddOtherApproved != null)
            {
                SysAddOtherApproved(this, e);
            }
        }

        /// <summary> 事件发生在我请求加一个人，那个人拒绝时
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysAddOtherRejected;
        /// <summary>
        /// Raises the <see cref="E:SysAddOtherRejected"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysAddOtherRejected(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysAddOtherRejected != null)
            {
                SysAddOtherRejected(this, e);
            }
        }

        /// <summary>广告
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysAdvertisment;
        /// <summary>
        /// Raises the <see cref="E:SysAdvertisment"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysAdvertisment(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysAdvertisment != null)
            {
                SysAdvertisment(this, e);
            }
        }

        /// <summary>对方同意加你为好友，并且把你加为好友
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>> SysApprovedAddOtherAndAddMe;
        /// <summary>
        /// Raises the <see cref="E:SysApprovedAddOtherAndAddMe"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SystemNotificationPacket&gt;"/> instance containing the event data.</param>
        internal void OnSysApprovedAddOtherAndAddMe(QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            if (SysApprovedAddOtherAndAddMe != null)
            {
                SysApprovedAddOtherAndAddMe(this, e);
            }
        }

        /// <summary>
        /// 收到一条临时会话信息
        /// 	<remark>abu 2008-03-15 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> ReceiveTempSessionIM;
        /// <summary>
        /// Raises the <see cref="E:ReceiveTempSessionIM"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket,LumaQQ.NET.Packets.OutPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceiveTempSessionIM(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (ReceiveTempSessionIM != null)
            {
                ReceiveTempSessionIM(this, e);
            }
        }
        #endregion
    }
}
