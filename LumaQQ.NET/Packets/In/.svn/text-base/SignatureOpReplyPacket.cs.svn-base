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
namespace LumaQQ.NET.Packets.In
{
    /// <summary>
    ///  * 个性签名操作的回复包
    /// * 1. 头部
    /// * 2. 子命令，1字节
    /// * 3. 回复码，1字节
    /// * 
    /// * 如果2部分为0x00, 0x01，则
    /// * 4. 尾部
    /// * 
    /// * 如果2部分为0x02，即得到个性签名，则还有
    /// * 4. 下一个起始的QQ号，4字节。为这个回复包中所有QQ号的最大值加1
    /// * 5. QQ号，4字节
    /// * 6. 个性签名最后修改时间，4字节。这个修改时间的用处在于减少网络I/O，只有第一次我们需要
    /// *    得到所有的个性签名，以后我们只要送出个性签名，然后服务器会比较最后修改时间，修改过的
    /// *    才发回来
    /// * 7. 个性签名字节长度，1字节
    /// * 8. 个性签名
    /// * 9. 如果有更多，重复5-8部分
    /// * 10. 尾部
    /// 	<remark>abu 2008-02-26 </remark>
    /// </summary>
    public class SignatureOpReplyPacket : BasicInPacket
    {
        public SignatureSubCmd SubCommand { get; set; }
        public ReplyCode ReplyCode { get; set; }
        public int NextQQ { get; set; }
        public List<Signature> Signatures { get; set; }

        public SignatureOpReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        protected override void ParseBody(ByteBuffer buf)
        {
            SubCommand = (SignatureSubCmd)buf.Get();
            ReplyCode = (ReplyCode)buf.Get();
            if (SubCommand == SignatureSubCmd.GET)
            {
                NextQQ = buf.GetInt();
                Signatures = new List<Signature>();
                while (buf.HasRemaining())
                {
                    Signature sig = new Signature();
                    sig.Read(buf);
                    Signatures.Add(sig);
                }
            }
        }
        public override string GetPacketName()
        {
            return "Signature Op Reply Packet"; 
        }
    }
}
