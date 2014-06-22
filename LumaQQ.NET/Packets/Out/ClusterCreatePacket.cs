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
    ///  * 创建群请求包，格式为：
    /// * 1. 头部
    /// * 2. 群命令类型，1字节，创建是0x01
    /// * 3. 群的类型，固定还是临时，1字节
    /// * 4. 是否需要认证，1字节
    /// * 5. 2004群分类，4字节
    /// * 6. 2005群分类，4字节
    /// * 7. 群名称长度，1字节
    /// * 8. 群名称
    /// * 9. 未知的2字节，0x0000
    /// * 10. 群声明长度，1字节
    /// * 11. 群声明
    /// * 12. 群简介长度，1字节
    /// * 13. 群简介
    /// * 14. 群现有成员的QQ号列表，每个QQ号4字节
    /// * 15. 尾部 
    /// 	<remark>abu 2008-02-28 </remark>
    /// </summary>
    public class ClusterCreatePacket : ClusterCommandPacket
    {
        public ClusterType Type { get; set; }
        public AuthType AuthType { get; set; }
        public int OldCategory { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string Notice { get; set; }
        public string Description { get; set; }
        public List<int> Members { get; set; }
        public ClusterCreatePacket(QQUser user)
            : base(user)
        {
            this.SubCommand = ClusterCommand.CREATE_CLUSTER;
            this.Type = ClusterType.PERMANENT;
            this.AuthType = AuthType.Need;
            this.OldCategory = 0;
        }
        public ClusterCreatePacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Cluster Create Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 群命令类型
            buf.Put((byte)SubCommand);
            // 群类型
            buf.Put((byte)Type);
            // 认证类型
            buf.Put((byte)AuthType);
            // 2004群分类
            buf.PutInt(OldCategory);
            // 群的分类
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
            // 群中的好友
            foreach (int i in Members)
                buf.PutInt(i);
        }
    }
}
