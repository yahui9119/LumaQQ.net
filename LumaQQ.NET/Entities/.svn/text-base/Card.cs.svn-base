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
    /// <summary>群名片
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class Card
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public string Email { get; set; }
        public Gender GenderIndex { get; set; }
        /// <summary>
        /// 从字节缓冲区中解析一个群名片结构
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            int len = (int)buf.Get();
            Name = Utils.Util.GetString(buf.GetByteArray(len));
            GenderIndex = (Gender)buf.Get();

            len = (int)buf.Get();
            Phone = Utils.Util.GetString(buf.GetByteArray(len));

            len = (int)buf.Get();
            Email = Utils.Util.GetString(buf.GetByteArray(len));

            len = (int)buf.Get();
            Remark = Utils.Util.GetString(buf.GetByteArray(len));
        }
        /// <summary>
        /// 写入bean的内容到缓冲区中
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Write(ByteBuffer buf)
        {
            byte[] b = Utils.Util.GetBytes(Name);
            buf.Put((byte)b.Length);
            buf.Put(b);

            buf.Put((byte)GenderIndex);

            b = Utils.Util.GetBytes(Phone);
            buf.Put((byte)b.Length);
            buf.Put(b);

            b = Utils.Util.GetBytes(Email);
            buf.Put((byte)b.Length);
            buf.Put(b);

            b = Utils.Util.GetBytes(Remark);
            buf.Put((byte)b.Length);
            buf.Put(b);
        }
    }
}
