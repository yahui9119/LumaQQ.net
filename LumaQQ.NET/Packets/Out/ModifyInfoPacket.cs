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
namespace LumaQQ.NET.Packets.Out
{
    /// <summary>
    ///  * 修改用户个人信息的请求包，格式是:
    /// * 1. 头部
    /// * 2. 旧密码，新密码以及ContactInfo里面的域，但是不包括第一项QQ号，用0x1F分隔，依次往下排，最后要用
    /// *    一个0x1F结尾。但是开头不需要0x1F，如果哪个字段没有，就是空
    /// * 3. 尾部
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class ModifyInfoPacket : BasicOutPacket
    {
        /// <summary>标识是否有修改密码 阿不添加
        /// Gets or sets a value indicating whether [modify password].
        /// </summary>
        /// <value><c>true</c> if [modify password]; otherwise, <c>false</c>.</value>
        public bool ModifyPassword { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public ContactInfo ContactInfo { get; set; }
        private const byte DELIMIT = 0x1F;
        public ModifyInfoPacket(QQUser user) : base(QQCommand.Modify_Info, true, user) { }
        public ModifyInfoPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Modify Info Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 组装内容，首先是旧密码和新密码
            if (!string.IsNullOrEmpty(OldPassword) && !string.IsNullOrEmpty(NewPassword))
            {
                buf.Put(Utils.Util.GetBytes(OldPassword));
                buf.Put(DELIMIT);
                buf.Put(Utils.Util.GetBytes(NewPassword));
                ModifyPassword = true;
            }
            else
            {
                ModifyPassword = false;
                buf.Put(DELIMIT);
            }
            buf.Put(DELIMIT);
            // 写入contactInfo，除了QQ号
            String[] infos = ContactInfo.GetInfoArray();
            for (int i = 1; i < QQGlobal.QQ_COUNT_MODIFY_USER_INFO_FIELD; i++)
            {
                byte[] b = Utils.Util.GetBytes(infos[i]);
                buf.Put(b);
                buf.Put(DELIMIT);
            }
        }
    }
}
