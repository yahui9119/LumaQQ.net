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

namespace LumaQQ.NET.Packets.In._05
{
    /// <summary>
    ///  * 请求中转服务器的回复包，格式为
 /// * 1. 头部
 /// * 2. 未知的8字节，和请求包保持相同
 /// * ------ 加密开始 -------
 /// * 3. 回复码，2字节
 /// * 4. 被请求的服务器IP，4字节，little-endian
 /// * 5. 被请求的服务器端口，2字节
 /// * 6. 会话ID，重定向时为0
 /// * 7. 重定向IP，4字节，little-endian
 /// * 8. 重定向的port，2字节
 /// * 9. 后面的消息长度，2字节
 /// * 10. 消息内容
 /// * ------ 加密结束 -------
 /// * 11. 尾部
    /// 	<remark>abu 2008-02-26 </remark>
    /// </summary>
    public class RequestAgentReplyPacket : _05InPacket
    {
        public ReplyCode  ReplyCode { get; set; }
        public byte[] RedirectIp { get; set; }
        public int RedirectPort { get; set; }
        public int SessionId { get; set; }
        public string Message { get; set; }
        public RequestAgentReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Request Agent Reply Packet";
        }
        protected override int GetCryptographStart()
        {
            return 8;
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            buf.GetLong();

            ReplyCode = (ReplyCode)buf.GetUShort();
            // 原服务器IP
            buf.GetUInt();
            // 原服务器端口
            buf.GetUShort();
            // 会话ID
            SessionId = buf.GetInt();
            // 重定向IP
            RedirectIp = buf.GetByteArray(4);
            // 重定向端口
            RedirectPort = buf.GetUShort();
            // 消息长度
            int len = buf.GetUShort();
            Message = Utils.Util.GetString(buf, len);

            // swap ip bytes
            byte temp = RedirectIp[0];
            RedirectIp[0] = RedirectIp[3];
            RedirectIp[3] = temp;
            temp = RedirectIp[1];
            RedirectIp[1] = RedirectIp[2];
            RedirectIp[2] = temp;
        }
    }
}
