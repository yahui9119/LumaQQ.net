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
    /// * 请求下载分组好友列表的回复包，格式为
    /// * 1. 头部
    /// * 2. 操作字节，下载为0x1
    /// * 3. 回复码，1字节
    /// * 4. 4个未知字节，全0
    /// * 5. 下一个下载包的起始好友号，4字节
    /// * 6. 好友的QQ号，4字节
    /// * 7. 好友类型，0x1表示普通好友，0x4表示群
    /// * 8. 分组序号，1字节，但是这个很奇怪，不是1，2，3那样的，而是用序号乘4，比如如果是属于第2个组，
    /// *    那么这个就是8，注意我的好友组是第0组，但是有可能这个数字不是4的倍数，那就不知道什么
    /// *    意思了，但是除以4得到组序号的方法仍然不受影响
    /// * 9. 如果还有更多好友，重复4，5，6部分
    /// * 10. 尾部
    /// * 
    /// * 这个包解析后产生的数据可以通过哈希表friends访问，每一个组为一个list，用组的索引为key，
    /// * 比如第0，第1组，分别可以得到一个List对象，list中包含了好友的qq号
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class DownloadGroupFriendReplyPacket : BasicInPacket
    {
        /// <summary>
        /// Gets or sets the friends.
        /// </summary>
        /// <value>The friends.</value>
        public List<DownloadFriendEntry> Friends { get; set; }
        /// <summary>分组好友是否已经下载完
        /// Gets the finished.
        /// </summary>
        /// <value>The finished.</value>
        public bool Finished { get { return BeginFrom == 0; } }
        /// <summary>起始好友号
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public uint BeginFrom { get; set; }
        /// <summary>
        /// Gets or sets the reply code.
        /// </summary>
        /// <value>The reply code.</value>
        public ReplyCode ReplyCode { get; set; }
        /// <summary>
        /// Gets or sets the sub command.
        /// </summary>
        /// <value>The sub command.</value>
        public byte SubCommand { get; set; }
        public DownloadGroupFriendReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Download Group Friend Reply Packet";
        }
        /// <summary>
        /// 解析包体，从buf的开头位置解析起
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void ParseBody(ByteBuffer buf)
        {
            // 操作字节，下载为0x1
            SubCommand = buf.Get();
            ReplyCode = (ReplyCode)buf.Get();
            // 4个未知字节，全0
            buf.GetUInt();
            // 起始好友号
            BeginFrom = buf.GetUInt();
            // 循环读取各好友信息，加入到list中
            Friends = new List<DownloadFriendEntry>();
            while (buf.HasRemaining())
            {
                DownloadFriendEntry dfe = new DownloadFriendEntry();
                dfe.Read(buf);
                Friends.Add(dfe);
            }
        }
    }
}
