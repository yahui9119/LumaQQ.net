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
    /// 包管理器
    /// 发送包，输入包，包事件
    /// 	<remark>abu 2008-03-08 </remark>
    /// </summary>
    public class PacketManager
    {
        internal PacketManager() { }
        /// <summary>
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <value></value>
        public QQClient QQClient { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        private Queue<InPacket> receiveQueue;
        private PacketIncomeTrigger packetIncomTrigger;
        private ResendTrigger resendTrigger;
        private ProcessorRouter router;
        private KeepAliveTrigger keepAliveTrigger;
        /// <summary>
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="client">The client.</param>
        internal PacketManager(QQClient client)
        {
            router = new ProcessorRouter(client);
            router.InstallProcessor(new BasicFamilyProcessor(client));

            this.QQClient = client;
            receiveQueue = new Queue<InPacket>();
            this.packetIncomTrigger = new PacketIncomeTrigger(client);
            this.resendTrigger = new ResendTrigger(client);
            keepAliveTrigger = new KeepAliveTrigger(client);
        }
        #region 输入包处理

        /// <summary>
        /// 添加输入包
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        public void AddIncomingPacket(InPacket inPacket, string portName)
        {
            if (inPacket == null)
            {
                return;
            }
            inPacket.PortName = portName;
            receiveQueue.Enqueue(inPacket);
            //inConn.Add(inPacket, portName);
            ThreadExcutor.Submit(this.packetIncomTrigger, this);
        }
        /// <summary> 从接收队列中得到第一个包，并且把这个包从队列中移除
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <returns></returns>
        public InPacket RemoveIncomingPacket()
        {
            if (receiveQueue.Count == 0)
            {
                return null;
            }
            return receiveQueue.Dequeue();
        }

        /// <summary>
        /// 收到服务器确认
        /// 删除一个重发包
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void RemoveResendPacket(InPacket packet)
        {
            resendTrigger.Remove(packet);
        }

        /// <summary>通知包处理器包到达事件
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        public void FirePacketArrivedEvent(InPacket inPacket)
        {
            router.PacketArrived(inPacket);
        }
        /// <summary>
        /// 添加重发包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="outPacket">The out packet.</param>
        public void AddResendPacket(OutPacket outPacket, string portName)
        {
            resendTrigger.Add(outPacket, portName);
        }
        #endregion

        #region 发送包
        /// <summary>
        /// 通用方法，发送一个packet
        ///* 这个方法用在一些包构造比较复杂的情况下，比如上传分组信息这种包，
        ///* 包中数据的来源是无法知道的也不是用几个参数就能概括的，可能也和实现有关。
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void SendPacket(OutPacket packet)
        {
            SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary> 通过指定port发送一个包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="port">The port.</param>
        public void SendPacket(OutPacket packet, string port)
        {
            SendPacket(packet, port, false);
        }

        /// <summary>通过指定port发送一个包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="port">The port.</param>
        /// <param name="monitor">if set to <c>true</c> [monitor].</param>
        public void SendPacket(OutPacket packet, string port, bool monitor)
        {
            if (QQClient.QQUser.IsLoggedIn)
            {
                if (QQClient.ConnectionManager.EnsureConnection(port, true))
                {
                    QQClient.ConnectionManager.ConnectionPool.Send(port, packet, monitor);
                }
                else
                {
                    OnLostConnection(new QQEventArgs<InPacket, OutPacket>(QQClient, null, packet));
                }

            }
        }
        /// <summary>不管有没有登录，都把包发出去
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="port">The port.</param>
        public void SendPacketAnyway(OutPacket packet, string port)
        {
            if (QQClient.ConnectionManager.EnsureConnection(port, true))
            {
                QQClient.ConnectionManager.ConnectionPool.Send(port, packet, false);
            }
            else
            {
                OnLostConnection(new QQEventArgs<InPacket, OutPacket>(QQClient, null, packet));
            }
        }
        #endregion

        #region events

        /// <summary>
        /// 收到未知包
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<UnknownInPacket, OutPacket>> ReceivedUnknownPacket;
        /// <summary>
        /// Raises the <see cref="E:ReceivedUnknownPacket"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.UnknownInPacket&gt;"/> instance containing the event data.</param>
        internal void OnReceivedUnknownPacket(QQEventArgs<UnknownInPacket, OutPacket> e)
        {
            if (ReceivedUnknownPacket != null)
            {
                ReceivedUnknownPacket(this, e);
            }
        }

        /// <summary>
        /// 当一个包向服务器发送成功，并且收到服务器确认后
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<InPacket, OutPacket>> SendPacketSuccessed;
        /// <summary>
        /// Raises the <see cref="E:SendedPacketSuccess"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.OutPacket&gt;"/> instance containing the event data.</param>
        internal void OnSendPacketSuccessed(QQEventArgs<InPacket, OutPacket> e)
        {
            if (SendPacketSuccessed != null)
            {
                SendPacketSuccessed(this, e);
            }
        }

        /// <summary>
        /// Occurs when [send packet time out].包发送超时事件 InPacket为null
        /// </summary>
        public event EventHandler<QQEventArgs<InPacket, OutPacket>> SendPacketTimeOut;
        /// <summary>
        /// Raises the <see cref="E:SendPacketTimeOut"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.InPacket,LumaQQ.NET.Packets.OutPacket&gt;"/> instance containing the event data.</param>
        internal void OnSendPacketTimeOut(QQEventArgs<InPacket, OutPacket> e)
        {
            if (SendPacketTimeOut != null)
            {
                SendPacketTimeOut(this, e);
            }
        }

        /// <summary>无法得到有效的网络连接来发送包
        /// 	<remark>abu 2008-03-13 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<InPacket, OutPacket>> LostConnection;
        /// <summary>
        /// Raises the <see cref="E:LostConnection"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.InPacket,LumaQQ.NET.Packets.OutPacket&gt;"/> instance containing the event data.</param>
        internal void OnLostConnection(QQEventArgs<InPacket, OutPacket> e)
        {
            if (LostConnection != null)
            {
                LostConnection(this, e);
            }
        }
        #endregion

    }

}
