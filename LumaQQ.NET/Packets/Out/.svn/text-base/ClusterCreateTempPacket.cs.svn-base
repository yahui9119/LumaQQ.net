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
    ///  * 创建临时群的请求包
    /// * 1. 头部
    /// * 2. 子命令类型，1字节，0x30
    /// * 3. 临时群类型，1字节
    /// * 4. 父群内部ID，4字节
    /// * 5. 名称长度，1字节
    /// * 6. 名称
    /// * 7. 成员QQ号，4字节
    /// * 8. 如果有更多成员，重复6部分
    /// * 10. 尾部
    /// 	<remark>abu 2008-02-28 </remark>
    /// </summary>
    public class ClusterCreateTempPacket : ClusterCommandPacket
    {
        public int ParentClusterId { get; set; }
        public string Name { get; set; }
        public List<int> Members { get; set; }
        public ClusterType Type { get; set; }

        public ClusterCreateTempPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public ClusterCreateTempPacket(QQUser user)
            : base(user)
        {
            SubCommand = ClusterCommand.CREATE_TEMP;
        }
        public override string GetPacketName()
        {
            return "Cluster - Create Temp Cluster Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 子命令类型，1字节，0x30
            buf.Put((byte)SubCommand);
            // 临时群类型，1字节
            buf.Put((byte)Type);
            // 父群内部ID，4字节
            buf.PutInt(ParentClusterId);
            // 名称长度，1字节
            byte[] b = Utils.Util.GetBytes(Name);
            buf.Put((byte)(b.Length & 0xFF));
            // 名称
            buf.Put(b);
            // 成员QQ号，4字节
            foreach (int i in Members)
                buf.PutInt(i);
        }
    }
}
