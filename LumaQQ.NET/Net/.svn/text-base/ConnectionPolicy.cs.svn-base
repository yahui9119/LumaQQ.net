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

using LumaQQ.NET.Packets;
namespace LumaQQ.NET.Net
{
    /// <summary>
    /// 连接策略
    /// 	<remark>abu 2008-03-03 </remark>
    /// </summary>
    public class ConnectionPolicy
    {
        QQClient client;
        PacketHelper helper;
        public ProtocolFamily SupportedFamily { get; private set; }
        public ProtocolFamily RelocateFamily { get; private set; }
        /// <summary>连接ID
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <value></value>
        public string ID { get; private set; }
        public ConnectionPolicy(QQClient client, string id, ProtocolFamily supportedFamily, ProtocolFamily relocateFamily)
        {
            this.ID = id;
            this.client = client;
            this.SupportedFamily = supportedFamily;
            this.RelocateFamily = relocateFamily;
            helper = new PacketHelper();
        }
        /// <summary>
        /// 一般网络错误时
        /// Called when [exception].
        /// </summary>
        /// <param name="e">The e.</param>
        public void OnNetworkError(Exception e)
        {
            client.ConnectionManager.OnNetworkError(e);
        }
        public void OnConnectServerError(Exception e)
        {
            client.ConnectionManager.OnConnectServerError(e);
        }

        /// <summary>
        /// 连接服务器成功后
        /// Called when [connected].
        /// </summary>
        public void OnConnected()
        {
            client.ConnectionManager.OnConnectSuccessed();
        }


        /// <summary>
        /// 创建一个错误包
        /// 	<remark>abu 2008-03-06 </remark>
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns></returns>
        public ErrorPacket CreateErrorPacket(ErrorPacketType errorCode, string portName, Exception e)
        {
            ErrorPacket errorPacket = new ErrorPacket(errorCode, client.QQUser, e);
            errorPacket.Family = SupportedFamily;
            errorPacket.ConnectionId = portName;
            return errorPacket;
        }

        /// <summary>压入一个重发包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="outPacket">The out packet.</param>
        public void PushResend(OutPacket outPacket, string portName)
        {
            client.PacketManager.AddResendPacket(outPacket, portName);
        }

        public void PushIn(InPacket inPacket)
        {
            client.PacketManager.AddIncomingPacket(inPacket, ID);
        }
        public void PushIn(ByteBuffer receiveIn)
        {
            InPacket inPacket = ParseIn(receiveIn);
            this.PushIn(inPacket);
        }

        /// <summary>
        /// 解析输入包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns></returns>
        public InPacket ParseIn(ByteBuffer buf)
        {
            return helper.ParseIn(SupportedFamily, buf, client.QQUser);
        }
        public void PutSent(OutPacket outPacket)
        {
            helper.PutSent(outPacket);
        }
        public bool IsReplied(OutPacket packet, bool add)
        {
            return helper.IsReplied(packet, add);
        }
        public OutPacket RetrieveSent(InPacket inPacket)
        {
            return helper.RetriveSent(inPacket);
        }
        /// <summary>
        /// 使用的代理信息
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <value></value>
        public Proxy Proxy
        {
            get { return client.Proxy; }
        }
    }
}
