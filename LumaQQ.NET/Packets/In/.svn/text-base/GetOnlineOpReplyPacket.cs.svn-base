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
    ///  * 得到在线好友列表的应答包，格式为
    /// * 1. 头部
    /// * 2. 在线好友是否已经全部得到，1字节
    /// * 3. 31字节的FriendStatus结构
    /// * 4. 2个未知字节
    /// * 5. 1个字节扩展标志
    /// * 6. 1个字节通用标志
    /// * 7. 2个未知字节
    /// * 8. 1个未知字节
    /// * 9. 如果有更多在线好友，重复2-8部分
    /// * 10. 尾部
    /// * 
    /// * 这个回复包最多返回30个在线好友，如果有更多，需要继续请求
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class GetOnlineOpReplyPacket : BasicInPacket
    {
        /// <summary>true表示没有更多在线好友了
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public bool Finished { get; set; }
        /// <summary>
        /// 下一个请求包的起始位置，仅当finished为true时有效
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public int Position { get; set; }
        // 在线好友bean列表
        public List<FriendOnlineEntry> OnlineFriends { get; set; }
        public GetOnlineOpReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Get Friend Online Reply Packet";
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            // 当前好友列表位置
            Finished = buf.Get() == QQGlobal.QQ_POSITION_ONLINE_LIST_END;
            Position = 0;
            //只要还有数据就继续读取下一个friend结构
            OnlineFriends = new List<FriendOnlineEntry>();
            while (buf.HasRemaining())
            {
                FriendOnlineEntry entry = new FriendOnlineEntry();
                entry.Read(buf);
                // 添加到List
                OnlineFriends.Add(entry);
                // 如果还有更多好友，计算position
                if (!Finished)
                    Position = Math.Max(Position, (int)entry.Status.QQ);
            }
            Position++;
        }
    }
}
