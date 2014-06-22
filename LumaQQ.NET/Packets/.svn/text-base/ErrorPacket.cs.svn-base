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
    /// 错误代码
    /// </summary>
    public enum ErrorPacketType
    {
        /// <summary>
        /// 远端已经关闭连接 
        /// </summary>
        ERROR_CONNECTION_BROKEN = 0,
        /// <summary>
        ///  操作超时
        /// </summary>
        ERROR_TIMEOUT = 1,
        /// <summary>
        /// 代理服务器错误
        /// </summary>
        ERROR_PROXY = 2,
        /// <summary>
        /// 网络错误
        /// </summary>
        ERROR_NETWORK = 3,
        /// <summary>
        /// 运行时错误，调试用
        /// </summary>
        RUNTIME_ERROR = 4
    }
    /// <summary>这个包和协议无关，它用来通知上层，有些错误发生了，上层应该检查errorCode字段
    /// 来获得更具体的信息
    /// 	<remark>abu 2008-02-20 </remark>
    /// </summary>
    public class ErrorPacket : BasicInPacket
    {
        public ProtocolFamily Family { get; set; }
        public ErrorPacketType ErrorType;
        public string ConnectionId { get; set; }
        public string ErrorMessage { get; set; }
        /// <summary>在运行时错误的异常
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public Exception e { get; private set; }
        /// <summary>
        /// 用在超时错误中
        /// </summary>
        public OutPacket TimeOutPacket { get; set; }
        public ErrorPacket(ErrorPacketType errorType, QQUser user, Exception e)
            : this(errorType, user)
        {
            this.e = e;
        }
        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <param name="user">The user.</param>
        public ErrorPacket(ErrorPacketType errorType, QQUser user)
            : base(QQCommand.Unknown, user)
        {
            this.ErrorType = errorType;
            this.Family = ProtocolFamily.All;
            ErrorMessage = "";
        }
        protected override void ParseBody(ByteBuffer buf)
        {

        }
        public override ProtocolFamily GetFamily()
        {
            return this.Family;
        }
    }
}
