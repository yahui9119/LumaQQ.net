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

namespace LumaQQ.NET.Entities
{
    /// <summary>
    /// 得到群信息中的成员信息
    /// 	<remark>abu 2008-02-20 </remark>
    /// </summary>
    public class Member
    {
        public uint QQ { get; set; }
        public uint Organization { get; set; }
        public byte Role { get; set; }
        public void Read(ByteBuffer buf)
        {
            QQ = buf.GetUInt();
            Organization = buf.Get();
            Role = buf.Get();
        }
        public void ReadTemp(ByteBuffer buf)
        {
            QQ = buf.GetUInt();
            Organization = buf.Get();
            Role = 0;
        }
        /// <summary>
        /// 是否为管理员
        /// 
        /// 根据Role的值，最好改为枚举
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            return (Role & QQGlobal.QQ_ROLE_ADMIN) != 0;
        }
        /// <summary>
        /// 是否为股东
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        public bool IsStockHolder()
        {
            return (Role & QQGlobal.QQ_ROLE_STOCKHOLDER) != 0;
        }
    }
}
