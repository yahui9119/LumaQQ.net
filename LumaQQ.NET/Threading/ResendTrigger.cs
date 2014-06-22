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
using System.Threading;

using LumaQQ.NET.Net;
using LumaQQ.NET.Packets;
namespace LumaQQ.NET.Threading
{
    /// <summary>重发包触发器
    /// 	<remark>abu 2008-03-11 </remark>
    /// </summary>
    public class ResendTrigger : IRunable
    {
        private QQClient client;
        // 超时队列
        private List<OutPacket> timeOutQueue;

        //string portName;
        public ResendTrigger(QQClient client)
        {
            this.client = client;
            timeOutQueue = new List<OutPacket>();
            //toPort = new Dictionary<OutPacket, string>();
            ThreadExcutor.RegisterIntervalObject(this, this, QQGlobal.QQ_TIMEOUT_SEND, true);
        }
        /// <summary>添加一个包到超时队列
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        public void Add(OutPacket packet, string portName)
        {
            packet.PortName = portName;
            timeOutQueue.Add(packet);
        }
        /// <summary>清空重发队列
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        public void Clear()
        {
            timeOutQueue.Clear();
            //toPort.Clear();
        }
        /// <summary> 得到超时队列的第一个包，不把它从队列中删除
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <returns></returns>
        public OutPacket Get()
        {
            if (timeOutQueue.Count > 0)
            {
                return timeOutQueue[timeOutQueue.Count - 1];
            }
            return null;
        }
        /// <summary>
        /// 得到超时队列的第一个包，并把它从队列中删除
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <returns></returns>
        public OutPacket Remove()
        {
            if (timeOutQueue.Count > 0)
            {
                OutPacket packet = timeOutQueue[timeOutQueue.Count - 1];
                timeOutQueue.Remove(packet);
                //portName = toPort[packet];
                //toPort.Remove(packet);
                return packet;
            }
            return null;

        }
        /// <summary>删除ack对应的请求包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="ack">The ack.</param>
        public void Remove(InPacket ack)
        {
            foreach (OutPacket packet in timeOutQueue)
            {
                if (packet.Equals(ack))
                {
                    timeOutQueue.Remove(packet);
                    //toPort.Remove(packet);
                    break;
                }
            }
        }
        /// <summary>得到下一个包的超时时间
        /// 下一个包的超时时间，如果队列为空，返回一个固定值
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <returns></returns>
        private long GetTimeoutLeft()
        {
            OutPacket packet = Get();
            if (packet == null)
            {
                return QQGlobal.QQ_TIMEOUT_SEND;
            }
            else
            {
                return packet.TimeOut - Utils.Util.GetTimeMillis(DateTime.Now);
            }
        }
        /// <summary>触发超时事件
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void FireOperationTimeOutEvent(OutPacket packet, string portName)
        {
            ErrorPacket error = new ErrorPacket(ErrorPacketType.ERROR_TIMEOUT, client.QQUser);
            error.TimeOutPacket = packet;
            error.Header = packet.Header;
            error.Family = packet.GetFamily();
            error.ConnectionId = portName;
            client.PacketManager.AddIncomingPacket(error, portName);
        }

        #region IRunable Members

        public bool IsRunning
        {
            get;
            private set;
        }
        public WaitHandle WaitHandler { get; set; }
        #endregion

        #region IRunable Members


        public void Run(object state, bool timedOut)
        {
            if (IsRunning == false)
            {
                lock (this)
                {
                    if (!IsRunning)
                    {
                        IsRunning = true;
                        long t = GetTimeoutLeft();
                        while (t <= 0)
                        {
                            OutPacket packet = Remove();
                            IConnection conn = client.ConnectionManager.ConnectionPool.GetConnection(packet.PortName);
                            if (conn != null && packet != null && !conn.Policy.IsReplied(packet, false))
                            {
                                if (packet.NeedResend())
                                {
                                    // 重发次数未到最大，重发
                                    client.PacketManager.SendPacketAnyway(packet, packet.PortName);
                                }
                                else
                                {
                                    // 触发操作超时事件
                                    FireOperationTimeOutEvent(packet, packet.PortName);
                                }
                            }
                            t = GetTimeoutLeft();
                        }
                        IsRunning = false;

                        // 继续等待 t 时间后再执行 // 先反注册原来的线程
                        this.RegisterdHandler.Unregister(this.WaitHandler);
                        ThreadExcutor.RegisterIntervalObject(this, this, t, true);
                    }
                }
            }
        }

        public System.Threading.RegisteredWaitHandle RegisterdHandler
        {
            get;
            set;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.WaitHandler != null && this.RegisterdHandler != null)
            {
                RegisterdHandler.Unregister(this.WaitHandler);
            }
        }

        #endregion
    }
}
