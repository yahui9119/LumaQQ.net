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

using LumaQQ.NET.Entities;
namespace LumaQQ.NET.Packets.Out
{
    /// <summary> * 提交成员分组情况到服务器
    /// * 1. 头部
    /// * 2. 命令，1字节，0x13
    /// * 3. 群内部id，4字节
    /// * 4. 未知1字节，0x00
    /// * 5. 成员QQ号，4字节
    /// * 6. 成员所属组织序号，1字节，没有组织时是0x00
    /// * 7. 如果有更多成员，重复5-6部分
    /// * 8. 尾部
    /// * 
    /// * 注意：不需要一次提交所有成员分组情况，如果只有个别成员的分组变动了（比如拖动操作），
    /// * 那么只需要提交改变的成员。所以这个操作不象修改临时群成员那样，又有添加又有删除的，
    /// * 可以一个包搞定了
    /// 	<remark>abu 2008-02-28 </remark>
    /// </summary>
    public class ClusterCommitMemberOrganizationPacket : ClusterCommandPacket
    {
        public List<Member> Members { get; set; }
        public ClusterCommitMemberOrganizationPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public ClusterCommitMemberOrganizationPacket(QQUser user)
            : base(user)
        {
            SubCommand = ClusterCommand.COMMIT_MEMBER_ORGANIZATION;
        }
        public override string GetPacketName()
        {
            return "Cluster Commit Member Organization Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            buf.Put((byte)SubCommand);
            buf.PutInt(ClusterId);
            buf.Put((byte)0);
            foreach (Member m in Members)
            {
                buf.PutInt(m.QQ);
                buf.Put((byte)m.Organization);
            }
        }
    }
}
