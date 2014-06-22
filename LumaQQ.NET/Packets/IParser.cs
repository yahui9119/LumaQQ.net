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

namespace LumaQQ.NET.Packets
{
    /// <summary>
    /// 包解析器
    /// 	<remark>abu 2008-02-15 11:20 </remark>
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// 判断此parser是否可以处理这个包，判断不能影响到buf的指针位置
        /// 	<remark>abu 2008-02-15 11:27</remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns>true表示这个parser可以处理这个包</returns>
        bool Accept(ByteBuffer buf);
        /// <summary>包的总长度
        /// 	<remark>abu 2008-02-15 11:31 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns>包的总长度</returns>
        int GetLength(ByteBuffer buf);
        /// <summary>从buf当前位置解析出一个输入包对象，解析完毕后指针位于length之后
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">包长度.</param>
        /// <param name="user">The user.</param>
        /// <returns>InPacket子类，如果解析不了返回null</returns>
        InPacket ParseIncoming(ByteBuffer buf, int length, QQUser user);
        /// <summary>从buf当前位置解析出一个输出包对象，解析完毕后指针位于length之后
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">包长度.</param>
        /// <param name="user">QQ用户对象.</param>
        /// <returns>OutPacket子类，如果解析不了，返回null</returns>
        OutPacket ParseOutcoming(ByteBuffer buf, int length, QQUser user);
        /// <summary>
        /// 检查这个输入包是否重复
        /// <remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>true表示重复</returns>
        bool IsDuplicate(InPacket packet);
        /// <summary>检查这个包是重复包是否也要回复
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>true表示即使这个包是重复包也要回复</returns>
        bool IsDuplicatedNeedReply(InPacket packet);
        /// <summary>假设buf的当前位置处是一个包，返回下一个包的起始位置。这个方法
        /// 用来重新调整buf指针。如果无法重定位，返回当前位置
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns>下一个包的起始位置</returns>
        int Relocate(ByteBuffer buf);

        /// <summary>
        /// PacketHistory类
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        PacketHistory GetHistory();
    }
}
