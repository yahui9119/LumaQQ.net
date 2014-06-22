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
    ///  * 搜索在线用户的包，格式为
    /// * 1. 头部
    /// * 2. 1个字节，表示搜索类型，比如搜索全部在线用户是0x31，自定义搜索是0x30
    /// * 3. 1字节分隔符: 0x1F
    /// * 4. 搜索参数
    /// * 	  i.  对于搜索全部在线用户的请求，是一个页号，用字符串表示，从0开始
    /// *    ii. 对于自定义搜索类型，是4个域，用0x1F分隔，依次是
    /// * 		   a. 要搜索的用户的QQ号的字符串形式
    /// * 		   b. 要搜索的用户的昵称
    /// * 		   c. 要搜索的用户的email
    /// *         d. 页号的字符串形式，这后面没有分隔符了，是用0x0结尾的         
    /// * 5. 尾部
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class SearchUserPacket : BasicOutPacket
    {
        public FriendSearchType SearchType { get; set; }
        public string Page { get; set; }
        public string QQStr { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        /** 分隔符 */
        private const byte DELIMIT = 0x1F;
        /** 如果字段为空，用0x2D替代，即'-'字符 */
        private const byte NULL = 0x2D;

        public SearchUserPacket(QQUser user)
            : base(QQCommand.Search_User, true, user)
        {
            Page = "0";
            SearchType = FriendSearchType.SEARCH_ALL;
            QQStr = Nick = Email = string.Empty;
        }
        public SearchUserPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Search User Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 开始组装内容
            if (SearchType == FriendSearchType.SEARCH_ALL)
            {
                buf.Put((byte)SearchType);
                buf.Put(DELIMIT);
                buf.Put(Utils.Util.GetBytes(Page));
            }
            else if (SearchType == FriendSearchType.SEARCH_CUSTOM)
            {
                buf.Put((byte)SearchType);
                buf.Put(DELIMIT);
                // QQ号
                if (string.IsNullOrEmpty(QQStr)) buf.Put(NULL);
                else buf.Put(Utils.Util.GetBytes(QQStr));
                buf.Put(DELIMIT);
                // 昵称
                if (string.IsNullOrEmpty(Nick)) buf.Put(NULL);
                else
                    buf.Put(Utils.Util.GetBytes(Nick));
                buf.Put(DELIMIT);
                // email
                if (string.IsNullOrEmpty(Email)) buf.Put(NULL);
                else
                    buf.Put(Utils.Util.GetBytes(Email));
                buf.Put(DELIMIT);
                // 结尾
                buf.Put(Utils.Util.GetBytes(Page));
                buf.Put((byte)0x0);
            }
        }
    }
}
