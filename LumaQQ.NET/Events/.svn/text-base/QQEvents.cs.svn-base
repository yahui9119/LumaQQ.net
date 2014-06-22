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

using LumaQQ.NET.Packets;
using LumaQQ.NET.Packets.In;
namespace LumaQQ.NET.Events
{
    /// <summary>
    /// 	<remark>abu 2008-03-12 </remark>
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="O"></typeparam>
    public class QQEventArgs<I, O> : EventArgs
        where I : InPacket
        where O : OutPacket
    {
        /// <summary>
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        public QQEventArgs(QQClient client, I inPacket, O outPacket)
        {
            this.QQClient = client;
            this.InPacket = inPacket;
            this.OutPacket = outPacket;
        }
        /// <summary>回复包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <value></value>
        public I InPacket { get; private set; }
        /// <summary>对应的发送包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <value></value>
        public O OutPacket { get; private set; }
        /// <summary>
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <value></value>
        public QQClient QQClient { get; private set; }
    }
}
