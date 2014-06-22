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

using LumaQQ.NET.Entities;
namespace LumaQQ.NET.Packets.In
{
    /// <summary>
    /// * 用户属性回复包
    /// * 1. 头部
    /// * 2. 子命令，1字节
    /// * 当2部分为0x01时：
    /// * 3. 下一个包的起始位置，2字节
    /// * 4. 6部分的长度，1字节
    /// * 5. QQ号，4字节
    /// * 6. 用户属性字节，已知位如下
    /// * 	  bit30 -> 是否有个性签名
    /// * 7. 如果有更多好友，重复5-6部分
    /// * Note: 当2部分为其他值时，尚未仔细解析过后面的格式，非0x01值一般出现在TM中
    /// * 8. 尾部
    /// 	<remark>abu 2008-02-26 </remark>
    /// </summary>
    public class UserPropertyOpReplyPacket : BasicInPacket
    {
        public UserPropertySubCmd SubCommand { get; set; }
        public bool Finished { get; set; }
        public ushort StartPosition { get; set; }
        public List<UserProperty> Properties { get; set; }
        public UserPropertyOpReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "User Property Op Reply Packet";
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            SubCommand = (UserPropertySubCmd)buf.Get();
            switch (SubCommand)
            {
                case UserPropertySubCmd.GET:
                    StartPosition = buf.GetUShort();
                    Finished = StartPosition == QQGlobal.QQ_POSITION_USER_PROPERTY_END;
                    int pLen = buf.Get() & 0xFF;
                    Properties = new List<UserProperty>();
                    while (buf.HasRemaining())
                    {
                        UserProperty p = new UserProperty(pLen);
                        p.Read(buf);
                        Properties.Add(p);
                    }
                    break;
            }
        }
    }
}
