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
    /// <summary>在线用户的结构表示
    /// 	<remark>abu 2008-02-26 </remark>
    /// </summary>
    public class UserInfo
    {
        public int QQ { get; set; }
        public string Nick { get; set; }
        public string Province { get; set; }
        public int Face { get; set; }
        /// <summary>
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            ByteBuffer temp = new ByteBuffer();
            int i = 0;
            while (true)
            {
                byte b = buf.Get();
                if (b != 0x1F)
                {
                    if (b != 0x1E)
                    {
                        temp.Put(b);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            QQ = Utils.Util.GetInt(Utils.Util.GetString(temp.ToByteArray()), 0000);
                        }
                        else if (i == 1)
                            Nick = Utils.Util.GetString(temp.ToByteArray());
                        else if (i == 2)
                            Province = Utils.Util.GetString(temp.ToByteArray());
                        i++;
                        temp.Initialize();
                    }
                }
                else
                {
                    Face = Utils.Util.GetInt(Utils.Util.GetString(temp.ToByteArray()), 0);
                    break;
                }
            }
        }
    }
}
