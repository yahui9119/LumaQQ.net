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
    ///  * 上传分组中好友列表的消息包，格式为
    /// * 1. 头部
    /// * 2. 好友的QQ号，4字节
    /// * 3. 好友所在的组序号，0表示我的好友组，自己添加的组从1开始
    /// * 4. 如果有更多好友，重复2，3部分
    /// * 5. 尾部
    /// * 
    /// * 并不需要每次都上传所有的好友，比如如果在使用的过程中添加了一个好友，那么
    /// * 可以只上传这个好友即可
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class UploadGroupFriendPacket : BasicOutPacket
    {
        public Dictionary<int, List<int>> Friends { get; set; }
        public UploadGroupFriendPacket(QQUser user)
            : base(QQCommand.Upload_Group_Friend, true, user)
        {
            Friends = new Dictionary<int, List<int>>();
        }
        public UploadGroupFriendPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Upload Group Friend Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            int i = 0;
            // 写入每一个好友的QQ号和组序号
            foreach (List<int> list in Friends.Values)
            {
                // 等于null说明这是一个空组，不用处理了			
                if (list != null)
                {
                    foreach (int qq in list)
                    {
                        buf.PutInt(qq);
                        buf.Put((byte)i);
                    }
                }
                i++;
            }
        }
        /// <summary>添加好友信息
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="gIndex">Index of the g.</param>
        /// <param name="qqNum">The qq num.</param>
        public void addFriend(int gIndex, int qqNum)
        {
            List<int> gList = null;
            if (Friends.ContainsKey(gIndex))
                gList = Friends[gIndex];
            else
            {
                gList = new List<int>();
                Friends.Add(gIndex, gList);
            }
            gList.Add(qqNum);
        }
    }
}
