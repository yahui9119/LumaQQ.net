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
using System.Net;
using System.Text;
using System.Collections.Generic;

using LumaQQ.NET.Packets;
namespace LumaQQ.NET.Net
{
    /// <summary>
    /// 连接池接口，用于管理所有连接
    /// 	<remark>abu 2008-03-07 </remark>
    /// </summary>
    public interface IConnectionPool
    {
        /// <summary>立刻发送所有包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        void Flush();
        /// <summary>启动连接池
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        void Start();

        /// <summary>
        /// 释放连接
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="conn">The conn.</param>
        void Release(IConnection conn);

        /// <summary>
        /// 释放指定id的连接
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="id">The id.</param>
        void Release(string id);

        /// <summary>
        /// 发送一个包
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="keepSent">if set to <c>true</c> [keep sent].</param>
        void Send(string id, OutPacket packet, bool keepSent);

        /// <summary>新建一个UDP连接
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <param name="server">The server.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        /// <returns></returns>
        IConnection NewUDPConnection(ConnectionPolicy policy,EndPoint server , bool start);

        /// <summary>新建一个TCP连接
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <param name="server">The server.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        /// <returns></returns>
        IConnection NewTCPConnection(ConnectionPolicy policy, EndPoint server, bool start);

        /// <summary>根据id得到连接
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        IConnection GetConnection(string id);

        /// <summary>
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="server">The server.</param>
        /// <returns></returns>
        IConnection GetConnection(EndPoint server);

        /// <summary>关闭这个连接池，释放所有资源。一个释放掉的连接池不可继续使用，必须新建一个新的连接池对象
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        void Dispose();
        /// <summary>检测是否存在某个id的连接
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        bool HasConnection(string id);
        /// <summary>连接对象列表
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <returns></returns>
        List<IConnection> GetConnections();
 
    }
}
