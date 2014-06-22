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

namespace LumaQQ.NET.Packets.In
{
    /// <summary>
    ///  * 好友状态改变包，这个是从服务器发来的包，格式为
    /// * 1. 头部
    /// * 2. 好友QQ号，4字节
    /// * 3. 未知的4字节
    /// * 4. 未知的4字节
    /// * 5. 好友改变到的状态，1字节
    /// * 6. 好友的客户端版本，2字节。这个版本号不是包头中的source，是内部表示，比如2004是0x04D1
    /// * 7. 未知用途的密钥，16字节
    /// * 8. 用户属性标志，4字节
    /// * 9. 我自己的QQ号，4字节
    /// * 10. 未知的2字节
    /// * 11. 未知的1字节
    /// * 12. 尾部
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class FriendChangeStatusPacket : BasicInPacket
    {
        public uint FriendQQ { get; set; }
        public uint MyQQ { get; set; }
        public QQStatus Status { get; set; }
        public uint UserFlag { get; set; }
        public byte[] UnknownKey { get; set; }
        public char ClientVersion { get; set; }
        public FriendChangeStatusPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Friend Change Status Packet";
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            FriendQQ = buf.GetUInt();
            buf.GetUInt();
            buf.GetUInt();
            Status = (QQStatus)buf.Get();
            ClientVersion = buf.GetChar();
            UnknownKey = buf.GetByteArray(QQGlobal.QQ_LENGTH_KEY);
            UserFlag = buf.GetUInt();
            MyQQ = buf.GetUInt();
            buf.GetChar();
            buf.Get();
            
        }
    }
}
