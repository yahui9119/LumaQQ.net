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
    ///  * 请求好友列表的应答包，格式为
    /// * 1. 头部
    /// * 2. 下一次好友列表开始位置，这个位置是你所有好友排序后的位置，如果为0xFFFF，那就是你的好友已经全部得到了
    /// *    每次都固定的返回50个好友，所以如果不足50个了，那么这个值一定是0xFFFF了
    /// * 3. 好友QQ号，4字节
    /// * 4. 头像，2字节
    /// * 5. 年龄，1字节
    /// * 6. 性别，1字节
    /// * 7. 昵称长度，1字节
    /// * 8. 昵称，不定字节，由8指定
    /// * 9. 用户标志字节，4字节
    /// * 10. 重复3-9的结构
    /// * 11.尾部
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class GetFriendListReplyPacket : BasicInPacket
    {
        /// <summary>是否已经结束
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <value></value>
        public bool Finished { get; set; }
        public ushort Position { get; set; }
        public List<QQFriend> Friends { get; set; }
        public GetFriendListReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Get Friend List Reply Packet";
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            // 当前好友列表位置
            Position = buf.GetUShort();
            Finished = Position == 0xFFFF;
            // 只要还有数据就继续读取下一个friend结构
            Friends = new List<QQFriend>();
            while (buf.HasRemaining())
            {
                QQFriend friend = new QQFriend();
                friend.Read(buf);
                Friends.Add(friend);
            }
        }
    }
}
