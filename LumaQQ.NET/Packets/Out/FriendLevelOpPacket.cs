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

namespace LumaQQ.NET.Packets.Out
{
    /// <summary>
    ///  * 这个查询QQ号等级的包，格式是
    /// * 1. 头部
    /// * 2. 子命令，1字节
    /// * 3. 查询的号码，4字节
    /// * 4. 如果有更多好友，重复3部分
    /// * 5. 尾部
    /// * 
    /// * QQ的做法是一次最多请求70个。号码必须按照大小排序，本来之前不排序也可以，后来腾讯可能在服务器端动了些手脚，必须
    /// * 得排序了。这种顺序并没有在这个类中维护，所以是否排序目前是上层的责任，这个类假设收到的是一个排好序的用户QQ号
    /// * 列表
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class FriendLevelOpPacket : BasicOutPacket
    {
        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>The friends.</value>
        public List<int> Friends { get; set; }
        /// <summary>
        /// Gets or sets the sub command.
        /// </summary>
        /// <value>The sub command.</value>
        public FriendLevelSubCmd SubCommand { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendLevelOpPacket"/> class.
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">The length.</param>
        /// <param name="user">The user.</param>
        public FriendLevelOpPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendLevelOpPacket"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public FriendLevelOpPacket(QQUser user)
            : base(QQCommand.Friend_Level_OP, true, user)
        {
            SubCommand = FriendLevelSubCmd.GET;
        }
        /// <summary>
        /// 初始化包体
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void PutBody(ByteBuffer buf)
        {
            buf.Put((byte)SubCommand);
            foreach (int friend in Friends)
                buf.PutInt(friend);
        }
        /// <summary>
        /// Gets the name of the packet.
        /// </summary>
        /// <returns></returns>
        public override string GetPacketName()
        {
            return "Get Friends Level Packet";
        }
    }
}
