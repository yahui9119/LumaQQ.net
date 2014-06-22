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
using System.Net;
using System.Net.Sockets;

using Org.Mentalis.Network.ProxySocket;

using LumaQQ.NET.Packets;
namespace LumaQQ.NET.Net.Sockets
{
    public abstract class SocketConnection : IConnection
    {
        protected ProxySocket socket;
        //protected Queue<OutPacket> outPacketQueue;
        protected ConnectionPolicy policy;
        private ByteBuffer receiveBuf;
        private EndPoint epServer;
        public SocketConnection(ConnectionPolicy policy, EndPoint server)
        {
            this.policy = policy;
            this.epServer = server;
            //outPacketQueue = new Queue<OutPacket>();
        }

        #region IConnection Members
        /// <summary>
        /// 连接名称
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <value></value>
        public string Name { get { return this.policy.ID; } }

        /// <summary>
        /// 添加一个输出包
        /// <remark>abu 2008-03-03 </remark>
        /// </summary>
        /// <param name="outPacket">The out packet.</param>
        /// <param name="monitor">The monitor. true为同步发送，false为异步发送</param>
        public void Send(LumaQQ.NET.Packets.OutPacket outPacket, bool monitor)
        {
            //记录发送历史
            policy.PutSent(outPacket);
            //outPacketQueue.Enqueue(outPacket);
            if (monitor)
            {
                SendData(this.socket, outPacket);
            }
            else
            {
                BeginSendData(this.socket, outPacket);
            }
        }

        /// <summary>
        /// 清空输出队列
        /// <remark>abu 2008-03-03 </remark>
        /// </summary>
        public void ClearSendQueue()
        {
            //outPacketQueue.Clear();
        }

        /// <summary>
        /// 连接到服务器
        /// <remark>abu 2008-03-03 </remark>
        /// </summary>
        public bool Connect()
        {
            if (socket != null && socket.Connected)
            {
                return true;
            }
            int retry = 0;
        Connect:
            try
            {
                socket = GetSocket();
                socket.Connect(epServer);
                BeginDataReceive(this.socket);
                return true;
            }
            catch (Exception e)
            {
                retry++;
                if (retry < 3)
                {
                    goto Connect;
                }
                policy.OnConnectServerError(e);
                return false;
            }
        }

        /// <summary>
        /// 关闭连接
        /// <remark>abu 2008-03-03 </remark>
        /// </summary>
        public void Close()
        {
            if (socket != null && socket.Connected)
            {
                ((IDisposable)socket).Dispose();
                this.socket = null;
            }
        }

        public ConnectionPolicy Policy
        {
            get { return policy; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion

        #region abstract
        /// <summary>
        /// 创建Socket对象
        /// 	<remark>abu 2008-03-03 </remark>
        /// </summary>
        /// <returns></returns>
        protected abstract ProxySocket GetSocket();
        protected abstract void FillHeader(ByteBuffer buf);
        #endregion
        private void EndConnectCallback(IAsyncResult ar)
        {
            try
            {
                socket.EndConnect(ar);
                policy.OnConnected();

            }
            catch (Exception e)
            {
                policy.OnNetworkError(e);
            }
        }
        protected virtual void BeginDataReceive(ProxySocket socket)
        {
            if (this.socket == null || !this.socket.Connected)
            {
                return;
            }
            receiveBuf = new ByteBuffer();
            socket.BeginReceive(receiveBuf.ByteArray, 0, receiveBuf.ByteArray.Length, SocketFlags.None, new AsyncCallback(EndDataReceive), socket);
        }
        /// <summary>
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="ar">The ar.</param>
        protected virtual void EndDataReceive(IAsyncResult ar)
        {
            if (this.socket == null || !this.socket.Connected)
            {
                return;
            }
            int cnt = 0;
            try
            {
                ProxySocket socket = (ProxySocket)ar.AsyncState;
                cnt = socket.EndReceive(ar);
                if (cnt != 0)
                {
                    receiveBuf.Length = cnt;
                    try
                    {
                        policy.PushIn(receiveBuf);
                    }
                    catch (Exception e)
                    {
                        policy.PushIn(policy.CreateErrorPacket(ErrorPacketType.RUNTIME_ERROR, this.Name, e));
                    }

                    //创建一个新的字节对象
                    receiveBuf = new ByteBuffer();
                }
                BeginDataReceive(socket);
            }
            catch (Exception e)
            {
                policy.OnNetworkError(e);
            }
        }
        /// <summary>异步发送数据
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="outPacket">The out packet.</param>
        protected virtual void BeginSendData(ProxySocket socket, OutPacket outPacket)
        {
            try
            {
                ByteBuffer sendBuf = new ByteBuffer();
                FillBytebuf(outPacket, sendBuf);
                socket.BeginSend(sendBuf.ByteArray, 0, sendBuf.Length, SocketFlags.None, new AsyncCallback(EndSendData), outPacket);
            }
            catch (Exception e)
            {
                policy.OnNetworkError(e);
            }
        }

        /// <summary>
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="outPacket">The out packet.</param>
        private void FillBytebuf(OutPacket outPacket, ByteBuffer sendBuf)
        {
            sendBuf.Initialize();
            FillHeader(sendBuf);
            outPacket.Fill(sendBuf);
        }
        /// <summary>
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="ar">The ar.</param>
        protected virtual void EndSendData(IAsyncResult ar)
        {
            OutPacket outPacket = (OutPacket)ar.AsyncState;
            try
            {
                outPacket.DateTime = DateTime.Now;
                int sendCount = socket.EndSend(ar);
                if (outPacket.NeedAck())
                {
                    outPacket.TimeOut = Utils.Util.GetTimeMillis(DateTime.Now) + QQGlobal.QQ_TIMEOUT_SEND;
                    policy.PushResend(outPacket, this.Name);
                }
                //outPacketQueue.Dequeue();
            }
            catch (Exception e)
            {
                policy.OnNetworkError(e);
            }
        }
        /// <summary>同步发送数据
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="outPacket">The out packet.</param>
        protected virtual void SendData(ProxySocket socket, OutPacket outPacket)
        {
            try
            {
                ByteBuffer sendBuf = new ByteBuffer();
                FillBytebuf(outPacket, sendBuf);
                socket.Send(sendBuf.ByteArray, 0, sendBuf.Length, SocketFlags.None);
                outPacket.DateTime = DateTime.Now;
                if (outPacket.NeedAck())
                {
                    outPacket.TimeOut = Utils.Util.GetTimeMillis(DateTime.Now) + QQGlobal.QQ_TIMEOUT_SEND;
                    policy.PushResend(outPacket, this.Name);
                }
            }
            catch (Exception e)
            {
                policy.OnNetworkError(e);
            }
        }
        #region IConnection Members


        public bool IsConnected
        {
            get { return socket.Connected; }
        }

        #endregion


    }
}
