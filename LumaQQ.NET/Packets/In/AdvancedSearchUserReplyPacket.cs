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
    ///  * 高级搜索的回复包
    ///  * 1. 头部
    ///  * 2. 回复码，1字节，0x00表示还有数据，0x01表示没有更多数据了，当为0x01时，后面没有内容了
    ///  *    当为0x00时，后面才有内容
    ///  * 3. 页号，从1开始，2字节，如果页号后面没有内容了，那也说明是搜索结束了
    ///  * 4. QQ号，4字节
    ///  * 5. 性别，1字节，表示下拉框索引
    ///  * 6. 年龄，2字节
    ///  * 7. 在线，1字节，0x01表示在线，0x00表示离线
    ///  * 8. 昵称长度，1字节
    ///  * 9. 昵称
    ///  * 10. 省份索引，2字节
    ///  * 11. 城市索引，2字节，这个索引是以"不限"为0开始算的，shit
    ///  * 13. 头像索引，2字节
    ///  * 14. 如果有更多结果，重复4 - 13部分
    ///  * 15. 尾部
    /// 	<remark>abu 2008-02-20 </remark>
    /// </summary>
    public class AdvancedSearchUserReplyPacket : BasicInPacket
    {
        public ReplyCode ReplyCode { get; set; }
        public uint Page { get; set; }
        public List<AdvancedUserInfo> Users { get; set; }
        public bool Finished { get; set; }
        /// <summary>
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">The length.</param>
        /// <param name="user">The user.</param>
        public AdvancedSearchUserReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        /// <summary>
        /// 包的描述性名称
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public override string GetPacketName()
        {
            return "Advanced Search User Reply Packet";
        }
        /// <summary>
        /// 解析包体，从buf的开头位置解析起
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void ParseBody(ByteBuffer buf)
        {
            ReplyCode = (ReplyCode)buf.Get();
            if (ReplyCode == ReplyCode.OK)
            {
                Page = buf.GetUInt();
                Users = new List<AdvancedUserInfo>();
                while (buf.HasRemaining())
                {
                    AdvancedUserInfo aui = new AdvancedUserInfo();
                    aui.ReadBean(buf);
                    Users.Add(aui);
                }
                Finished = Users.Count == 0;
            }
            else
                Finished = true;
        }
    }
}
