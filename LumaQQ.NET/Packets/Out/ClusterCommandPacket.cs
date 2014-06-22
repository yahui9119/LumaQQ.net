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
    public class ClusterCommandPacket : BasicOutPacket
    {
        public ClusterCommand SubCommand { get; set; }
        public int ClusterId { get; set; }

        /** 字体属性 */
        protected const byte NONE = 0x00;
        /// <summary>
        /// 
        /// </summary>
        protected const byte BOLD = 0x20;
        /// <summary>
        /// 
        /// </summary>
        protected const byte ITALIC = 0x40;
        /// <summary>
        /// 
        /// </summary>
        protected const byte UNDERLINE = (byte)0x80;

        public ClusterCommandPacket(QQUser user) : base(QQCommand.Cluster_Cmd, true, user) { }
        public ClusterCommandPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        protected override void ParseBody(ByteBuffer buf)
        {
            SubCommand = (ClusterCommand)buf.Get();
        }
        public override string GetPacketName()
        {
            switch (SubCommand)
            {
                case ClusterCommand.CREATE_CLUSTER:
                    return "Cluster Create Packet";
                case ClusterCommand.MODIFY_MEMBER:
                    return "Cluster Modify Member Packet";
                case ClusterCommand.MODIFY_CLUSTER_INFO:
                    return "Cluster Modify Info Packet";
                case ClusterCommand.GET_CLUSTER_INFO:
                    return "Cluster Get Info Packet";
                case ClusterCommand.ACTIVATE_CLUSTER:
                    return "Cluster Activate Packet";
                case ClusterCommand.SEARCH_CLUSTER:
                    return "Cluster Search Packet";
                case ClusterCommand.JOIN_CLUSTER:
                    return "Cluster Join Packet";
                case ClusterCommand.JOIN_CLUSTER_AUTH:
                    return "Cluster Auth Packet";
                case ClusterCommand.EXIT_CLUSTER:
                    return "Cluster Exit Packet";
                case ClusterCommand.GET_ONLINE_MEMBER:
                    return "Cluster Get Online Member Packet";
                case ClusterCommand.GET_MEMBER_INFO:
                    return "Cluster Get Member Info Packet";

                case ClusterCommand.SEND_IM_EX:
                    return "Cluster Send IM Ex Packet";
                case ClusterCommand.CREATE_TEMP:
                    return "Cluster Create Temp Cluster Packet";
                case ClusterCommand.MODIFY_TEMP_MEMBER:
                    return "Cluster Modify Temp Cluster Member Packet";
                case ClusterCommand.EXIT_TEMP:
                    return "Cluster Exit Temp Cluster Packet";
                case ClusterCommand.GET_TEMP_INFO:
                    return "Cluster Get Temp Cluster Info Packet";
                case ClusterCommand.ACTIVATE_TEMP:
                    return "Cluster Get Temp Cluster Member Packet";
                case ClusterCommand.MODIFY_CARD:

                case ClusterCommand.GET_CARD_BATCH:

                case ClusterCommand.GET_CARD:

                case ClusterCommand.COMMIT_ORGANIZATION:

                case ClusterCommand.UPDATE_ORGANIZATION:

                case ClusterCommand.COMMIT_MEMBER_ORGANIZATION:

                case ClusterCommand.GET_VERSION_ID:

                case ClusterCommand.SET_ROLE:

                case ClusterCommand.TRANSFER_ROLE:

                case ClusterCommand.DISMISS_CLUSTER:

                case ClusterCommand.MODIFY_TEMP_INFO:

                case ClusterCommand.SEND_TEMP_IM:

                case ClusterCommand.SUB_CLUSTER_OP:

                default:
                    return "Unknown Cluster Command Packet";
            }
        }
        protected override void PutBody(ByteBuffer buf)
        {
            
        }
    }
}
