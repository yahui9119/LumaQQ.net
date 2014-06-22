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
    /// QQLive描述信息 ，找不到StringTokenizer的替代类，暂时还没有实现
    /// 	<remark>abu 2008-02-25 </remark>
    /// </summary>
    public class QQLive
    {
        public ushort Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public void Read(ByteBuffer buf)
        {
            Type = buf.GetUShort();
            int len = buf.GetUShort() & 0xFF;
            switch (Type)
            {
                case QQGlobal.QQ_LIVE_IM_TYPE_DISK:
                    String s = Utils.Util.GetString(buf, len);
                    //还没实现
                    //StringTokenizer st = new StringTokenizer(s, "\u0002");
                    //if(st.hasMoreTokens())
                    //    title = st.nextToken();
                    //if(st.hasMoreTokens())
                    //    description = st.nextToken();
                    //if(st.hasMoreTokens())
                    //    url = st.nextToken();
                    break;
            }
        }
    }
}
