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
    /// 接收到的消息的头部格式封装类
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class ReceiveIMHeader
    {
        public uint Sender { get; set; }
        public uint Receiver { get; set; }
        public uint Sequence { get; set; }
        public byte[] SenderIP { get; set; }
        public uint SenderPort { get; set; }
        public RecvSource Type { get; set; }

        /// <summary>
        /// 给定一个输入流，解析ReceiveIMHeader结构
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 发送者QQ号或者群内部ID
            Sender = buf.GetUInt();
            // 接收者QQ号
            Receiver = buf.GetUInt();
            // 包序号，这个序号似乎和我们发的包里面的序号不同，至少这个是int，我们发的是char
            //     可能这个序号是服务器端生成的一个总的消息序号
            Sequence = buf.GetUInt();
            // 发送者IP，如果是服务器转发的，那么ip就是服务器ip
            SenderIP = buf.GetByteArray(4);
            // 发送者端口，如果是服务器转发的，那么就是服务器的端口 两个字节
            SenderPort = buf.GetUShort();
            // 消息类型，是好友发的，还是陌生人发的，还是系统消息等等
            Type = (RecvSource)buf.GetUShort();
        }
    }
}
