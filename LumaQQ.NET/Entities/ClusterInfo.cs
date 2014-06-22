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

using LumaQQ.NET.Utils;

namespace LumaQQ.NET.Entities
{
    /// <summary>
    /// 封装群信息
    /// 	<remark>abu 2008-02-20 </remark>
    /// </summary>
    public class ClusterInfo
    {
        /// <summary>
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint ClusterId { get; set; }
        /// <summary>
        /// // 如果是固定群，这个表示外部ID，如果是临时群，这个表示父群ID
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint ExternalId { get; set; }
        /// <summary>
        /// type字段表示固定群或者临时群的群类型
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public ClusterType Type { get; set; }
        /// <summary>
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint Unknown1 { get; set; }
        public uint Creator { get; set; }
        public AuthType AuthType { get; set; }
        /// <summary>
        /// 2004的群分类
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint OldCategory { get; set; }
        public char Unknown2 { get; set; }
        /// <summary>
        /// 2005采用的分类
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint Category { get; set; }
        public char Unknown3 { get; set; }
        public byte Unknown4 { get; set; }
        public uint VersionId { get; set; }
        public string Name { get; set; }
        public char Unknown5 { get; set; }
        public string Description { get; set; }
        public string Notice { get; set; }
        /// <summary>
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        public ClusterInfo()
        {
            Type = ClusterType.PERMANENT;
            AuthType = AuthType.Need;
            Description = Notice = Name = string.Empty;
        }
        /// <summary>
        /// 读取临时群信息
        /// <remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadTempClusterInfo(ByteBuffer buf) {
            Type = (ClusterType)buf.Get();
            // 父群内部ID
            ExternalId = buf.GetUInt();
            // 临时群内部ID
            ClusterId = buf.GetUInt();
            Creator = buf.GetUInt();
            AuthType = (AuthType)buf.Get();
            // 未知的1字节
            buf.Get();
            Category = buf.GetChar();
            // 群组名称的长度
            int len = (int)buf.Get();
            byte[] b1 = buf.GetByteArray(len);
            Name = Util.GetString(b1);
        }

        /// <summary>
        /// 给定一个输入流，解析ClusterInfo结构，这个方法适合于得到群信息的回复包
        /// <remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadClusterInfo(ByteBuffer buf)
        {
            ClusterId = buf.GetUInt();
            ExternalId = buf.GetUInt();
            Type = (ClusterType)buf.Get();
            Unknown1 = buf.GetUInt();
            Creator = buf.GetUInt();
            AuthType = (AuthType)buf.Get();
            OldCategory = buf.GetUInt();
            Unknown2 = buf.GetChar();
            Category = buf.GetUInt();
            Unknown3 = buf.GetChar();
            Unknown4 = buf.Get();
            VersionId = buf.GetUInt();
            // 群组名称的长度
            int len = (int)buf.Get();
            byte[] b1 = buf.GetByteArray(len);
            Unknown5 = buf.GetChar();
            // 群声明长度
            len = (int)buf.Get();
            byte[] b2 = buf.GetByteArray(len);
            // 群描述长度
            len = (int)buf.Get();
            byte[] b3 = buf.GetByteArray(len);
            // 转换成字符串
            Name = Util.GetString(b1);
            Notice = Util.GetString(b2);
            Description = Util.GetString(b3);
        }

        /// <summary>
        /// 从搜索群的回复中生成一个ClusterInfo结构
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadClusterInfoFromSearchReply(ByteBuffer buf)
        {
            ClusterId = buf.GetUInt();
            ExternalId = buf.GetUInt();
            Type = (ClusterType)buf.Get();
            // 未知的4字节
            buf.GetUInt();
            Creator = buf.GetUInt();
            OldCategory = buf.GetUInt();
            // 未知的2字节
            buf.GetChar();
            // 群名称长度和群名称
            int len = (int)buf.Get();
            byte[] b1 = buf.GetByteArray(len);
            // 两个未知字节
            buf.GetChar();
            // 认证类型
            AuthType = (AuthType)buf.Get();
            // 群描述长度和群描述
            len = (int)buf.Get();
            byte[] b2 = buf.GetByteArray(len);

            Name = Util.GetString(b1);
            Description = Util.GetString(b2);
        }
    }
}
