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

namespace LumaQQ.NET.Entities
{
    /// <summary>
    /// 普通消息的头部，普通消息是指从好友或者陌生人那里来的消息。什么不是普通消息？比如系统消息
    /// * 这个头部跟在ReceiveIMHeader之后
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class NormalIMHeader
    {
        public char SenderVersion { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public byte[] FileSessionKey { get; set; }
        public NormalIMType Type { get; set; }
        public char Sequence { get; set; }
        public long SendTime { get; set; }
        public char SenderHeader { get; set; }


        /// <summary>
        /// 给定一个输入流，解析NormalIMHeader结构
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 发送者的QQ版本
            SenderVersion = buf.GetChar();
            // 发送者的QQ号
            Sender = buf.GetInt();
            // 接受者的QQ号
            Receiver = buf.GetInt();
            // md5处理的发送方的uid和session key，用来在传送文件时加密一些消息
            FileSessionKey = buf.GetByteArray(16);
            // 普通消息类型，比如是文本消息还是其他什么消息
            Type = (NormalIMType)buf.GetUShort();
            // 消息序号
            Sequence = buf.GetChar();
            // 发送时间
            SendTime = (long)buf.GetUInt() * 1000L;
            // 发送者头像
            SenderHeader = buf.GetChar();
        }

        public String toString()
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("NormalIMHeader [");
            temp.Append("senderVersion(char): ");
            temp.Append((int)SenderVersion);
            temp.Append(", ");
            temp.Append("sender: ");
            temp.Append(Sender);
            temp.Append(", ");
            temp.Append("receiver: ");
            temp.Append(Receiver);
            temp.Append(", ");
            temp.Append("type(char): ");
            temp.Append((int)Type);
            temp.Append(", ");
            temp.Append("sequence(char): ");
            temp.Append((int)Sequence);
            temp.Append(", ");
            temp.Append("sendTime: ");
            temp.Append(SendTime);
            temp.Append(", ");
            temp.Append("senderHead(char): ");
            temp.Append((int)SenderHeader);
            temp.Append("]");
            return temp.ToString();
        }
    }
}
