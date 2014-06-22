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

namespace LumaQQ.NET.Packets.In
{
    /// <summary>
    ///  * 下载分组名称的回复包，格式为
    /// *1. 头部
    /// *2. 子命令，1字节，下载是0x1
    /// *3. 回复码，1字节
    /// *5. 未知的4字节
    /// *6. 组序号，从1开始，0表示我的好友组，因为是缺省组，所以不包含在包中
    /// *7. 16字节的组信息，开始是组名，以0结尾，如果长度不足16字节，则其余部分可能为0，也可能
    /// *   为其他字节，含义不明
    /// *8. 若有多个组，重复6，7部分
    /// *9. 尾部
    /// *
    /// *上传分组名称的回复包，格式为
    /// *1. 头部
    /// *2. 子命令，1字节
    /// *3. 回复码，1字节
    /// *4. 组需要，从1开始，0表示我的好友组，因为是缺省组，所以不包含在包中
    /// *5. 如果有更多组，重复4部分
    /// *6. 尾部
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class GroupDataOpReplyPacket : BasicInPacket
    {
        public List<string> GroupNames { get; set; }
        public GroupSubCmd SubCommand { get; set; }
        public ReplyCode ReplyCode { get; set; }
        public GroupDataOpReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            switch (SubCommand)
            {
                case GroupSubCmd.UPLOAD:
                    return "Group Data Reply Packet - Upload Group";
                case GroupSubCmd.DOWNLOAD:
                    return "Group Data Reply Packet - Download Group";
                default:
                    return "Group Data Reply Packet - Unknown Sub Command";
            }
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            // 得到操作类型
            SubCommand = (GroupSubCmd)buf.Get();
            // 回复码
            ReplyCode = (ReplyCode)buf.Get();
            if (ReplyCode == ReplyCode.OK)
            {
                // 如果是下载包，继续解析内容
                if (SubCommand == GroupSubCmd.DOWNLOAD)
                {
                    // 创建list
                    GroupNames = new List<String>();
                    // 未知4个字节
                    buf.GetUInt();
                    // 读取每个组名
                    while (buf.HasRemaining())
                    {
                        buf.Get();
                        GroupNames.Add(Utils.Util.GetString(buf, (byte)0x00, 16));
                    }
                }
            }
        }
    }
}
