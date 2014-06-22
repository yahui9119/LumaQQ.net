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
    /// <summary>好友状态结构
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class FriendStatus
    {
        public int QQ { get; set; }
        public byte Unknown1 { get; set; }
        /// <summary>
        /// 好友IP
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public byte[] IP { get; set; }
        /// <summary>
        /// 好友端口
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public ushort Port { get; set; }
        public byte Unknown2 { get; set; }
        /// <summary>
        /// 好友状态，定义在QQ接口中
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public QQStatus Status { get; set; }
        public char Unknown3 { get; set; }
        /// <summary>
        /// 未知的密钥，会不会是用来加密和好友通讯的一些信息的，比如点对点通信的时候
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public byte[] UnknownKey { get; set; }

        public bool IsOnline()
        {
            return Status == QQStatus.ONLINE;
        }
        public bool IsAway()
        {
            return Status == QQStatus.AWAY;
        }
        /// <summary>
        ///  给定一个输入流，解析FriendStatus结构
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 000-003: 好友QQ号
            QQ = buf.GetInt();
            // 004: 0x01，未知含义
            Unknown1 = buf.Get();
            // 005-008: 好友IP
            IP = buf.GetByteArray(4);
            // 009-010: 好友端口
            Port = buf.GetUShort();
            // 011: 0x01，未知含义
            Unknown2 = buf.Get();
            // 012: 好友状态
            Status = (QQStatus)buf.Get();
            // 013-014: 未知含义
            Unknown3 = buf.GetChar();
            // 015-030: key，未知含义
            UnknownKey = buf.GetByteArray(QQGlobal.QQ_LENGTH_KEY);
        }
    }
}
