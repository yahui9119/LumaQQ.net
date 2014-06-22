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

namespace LumaQQ.NET.Packets.Out
{
    /// <summary>
    ///  * 修改群信息的请求包，格式为：
    /// * 1. 头部
    /// * 2. 命令类型，1字节，修改群信息是0x03
    /// * 3. 群的内部ID，4字节
    /// * 4. 群类型，1字节
    /// * 5. 群的认证类型，1字节
    /// * 6. 2004群分类，4字节
    /// * 7. 2005群分类，4字节
    /// * 8. 群名称长度，1字节
    /// * 9. 群名称
    /// * 10. 未知的两字节，全0
    /// * 11. 群声明长度，1字节
    /// * 12. 群声明
    /// * 13. 群简介长度，1字节
    /// * 14. 群简介
    /// * 16. 尾部
    /// 	<remark>abu 2008-02-28 </remark>
    /// </summary>
    public class ClusterModifyInfoPacket : ClusterCommandPacket
    {
        public AuthType AuthType { get; set; }
        public int Category { get; set; }
        public int OldCategory { get; set; }
        public string Name { get; set; }
        public string Notice { get; set; }
        public string Description { get; set; }
        public ClusterType Type { get; set; }
        public ClusterModifyInfoPacket(QQUser user)
            : base(user)
        {
            SubCommand = ClusterCommand.MODIFY_CLUSTER_INFO;
            AuthType = AuthType.Need;
            Name = Notice = Description = string.Empty;
            Type = ClusterType.PERMANENT;
        }
        public ClusterModifyInfoPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Cluster Modify Info Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 群命令类型
            buf.Put((byte)SubCommand);
            // 群内部ID
            buf.PutInt(ClusterId);
            // 未知1字节
            buf.Put((byte)Type);
            // 认证类型
            buf.Put((byte)AuthType);
            // 2004群分类
            buf.PutInt(OldCategory);
            // 群分类，同学，朋友，之类的
            buf.PutInt(Category);
            // 群名称长度和群名称
            byte[] b = Utils.Util.GetBytes(Name);
            buf.Put((byte)(b.Length & 0xFF));
            buf.Put(b);
            // 未知的2字节
            buf.PutChar((char)0);
            // 群声明长度和群声明
            b = Utils.Util.GetBytes(Notice);
            buf.Put((byte)(b.Length & 0xFF));
            buf.Put(b);
            // 群描述长度和群描述
            b = Utils.Util.GetBytes(Description);
            buf.Put((byte)(b.Length & 0xFF));
            buf.Put(b);
        }
    }
}
