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
    /// 传送文件的ip，端口信息封装类
    /// 	<remark>abu 2008-02-23 </remark>
    /// </summary>
    public class FileTransferArgs
    {
        // 传输类型
        public TransferType TransferType { get; set; }
        // 连接方式
        public FileConnectMode ConnectMode { get; set; }
        // 发送者外部ip
        public byte[] InternetIP { get; set; }
        /// <summary>
        /// 发送者外部端口
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <value></value>
        public int InternetPort { get; set; }
        /// <summary>
        /// 第一个监听端口
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <value></value>
        public int MajorPort { get; set; }
        /// <summary>
        /// 发送者真实ip
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <value></value>
        public byte[] LocalIP { get; set; }
        /// <summary>
        /// 第二个监听端口
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <value></value>
        public int MinorPort { get; set; }

        /// <summary>
        /// 给定一个输入流，解析FileTransferArgs结构
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 跳过19个无用字节
            buf.Position = buf.Position + 19;
            // 读取传输类型
            TransferType = (TransferType)buf.Get();
            // 读取连接方式
            ConnectMode = (FileConnectMode)buf.Get();
            // 读取发送者外部ip
            InternetIP = buf.GetByteArray(4);
            // 读取发送者外部端口
            InternetPort = (int)buf.GetUShort();
            // 读取文件传送端口
            if (ConnectMode != FileConnectMode.DIRECT_TCP)
                MajorPort = (int)buf.GetUShort();
            else
                MajorPort = InternetPort;
            // 读取发送者真实ip
            LocalIP = buf.GetByteArray(4);
            // 读取发送者真实端口
            MinorPort = (int)buf.GetUShort();
        }
    }
}
