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

namespace LumaQQ.NET.Packets
{
    /// <summary>
    /// 所有输入包的基类
    /// 	<remark>abu 2008-02-18 </remark>
    /// </summary>
    public abstract class InPacket : Packet
    {       
        /// <summary>
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="source">The source.</param>
        /// <param name="command">The command.</param>
        /// <param name="user">The user.</param>
        public InPacket(byte header, char source, QQCommand command, QQUser user)
            : base(header, source, command, (char)0, user)
        {
        }
        /// <summary>
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">The length.</param>
        /// <param name="user">The user.</param>
        public InPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        /// <summary>
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="user">The user.</param>
        public InPacket(ByteBuffer buf, QQUser user) : base(buf, user) { }
        /// <summary>
        /// 校验头部, 默认返回 true
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>true</returns>
        protected override bool ValidateHeader()
        {
            return true;
        }    

    }
}
