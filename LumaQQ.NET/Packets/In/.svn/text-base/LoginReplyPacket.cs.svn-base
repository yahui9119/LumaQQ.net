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
    ///  * QQ登陆应答包
    /// *1. 头部
    /// *2. 回复码, 1字节
    /// *2部分如果是0x00
    /// *3. session key, 16字节
    /// *4. 用户QQ号，4字节
    /// *5. 我的外部IP，4字节
    /// *6. 我的外部端口，2字节
    /// *7. 服务器IP，4字节
    /// *8. 服务器端口，2字节
    /// *9. 本次登录时间，4字节，为从1970-1-1开始的毫秒数除1000
    /// *10. 未知的2字节
    /// *11. 用户认证令牌,24字节
    /// *12. 一个未知服务器1的ip，4字节
    /// *13. 一个未知服务器1的端口，2字节
    /// *14. 一个未知服务器2的ip，4字节
    /// *15. 一个未知服务器2的端口，2字节
    /// *16. 两个未知字节
    /// *17. 两个未知字节
    /// *18. client key，32字节，这个key用在比如登录QQ家园之类的地方
    /// *19. 12个未知字节
    /// *20. 上次登陆的ip，4字节
    /// *21. 上次登陆的时间，4字节
    /// *22. 39个未知字节
    /// *2部分如果是0x01，表示重定向
    /// *3. 用户QQ号，4字节
    /// *4. 重定向到的服务器IP，4字节
    /// *5. 重定向到的服务器的端口，2字节
    /// *2部分如果是0x05，表示登录失败
    /// *3. 一个错误消息
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class LoginReplyPacket : BasicInPacket
    {
        public byte[] SessionKey { get; set; }
        public byte[] IP { get; set; }
        public byte[] ServerIP { get; set; }
        public byte[] LastLoginIP { get; set; }
        public byte[] RedirectIP { get; set; }
        public int Port { get; set; }
        public int ServerPort { get; set; }
        public int RedirectPort { get; set; }
        public long LoginTime { get; set; }
        public long LastLoginTime { get; set; }
        public ReplyCode ReplyCode { get; set; }
        public string ReplyMessage { get; set; }
        public byte[] ClientKey { get; set; }
        /// <summary>认证令牌，用在一些需要认证身份的地方，比如网络硬盘
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public byte[] AuthToken { get; set; }
        public LoginReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Login Reply Packet";
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            ReplyCode = (ReplyCode)buf.Get();
            switch (ReplyCode)
            {
                case ReplyCode.OK:
                    // 001-016字节是session key
                    SessionKey = buf.GetByteArray(QQGlobal.QQ_LENGTH_KEY);
                    // 017-020字节是用户QQ号
                    buf.GetUInt();
                    // 021-024字节是服务器探测到的用户IP
                    IP = buf.GetByteArray(4);
                    // 025-026字节是服务器探测到的用户端口
                    Port = buf.GetUShort();
                    // 027-030字节是服务器自己的IP
                    ServerIP = buf.GetByteArray(4);
                    // 031-032字节是服务器的端口
                    ServerPort = buf.GetUShort();
                    // 033-036字节是本次登陆时间，为什么要乘1000？因为这个时间乘以1000才对，-_-!...
                    LoginTime = (long)buf.GetUInt() * 1000L;                    
                    // 037-038, 未知的2字节
                    buf.GetUShort();
                    // 039-062, 认证令牌
                    AuthToken = buf.GetByteArray(24);
                    // 063-066字节是一个未知服务器1的ip
                    // 067-068字节是一个未知服务器1的端口
                    // 069-072是一个未知服务器2的ip
                    // 073-074是一个未知服务器2的端口
                    // 075-076是两个未知字节
                    // 077-078是两个未知字节
                    buf.GetByteArray(buf.Position + 16);
                    // 079-110是client key，这个key用在比如登录QQ家园之类的地方
                    ClientKey = buf.GetByteArray(32);
                    // 111-122是12个未知字节
                    buf.GetByteArray(buf.Position + 12);
                    // 123-126是上次登陆的ip
                    LastLoginIP = buf.GetByteArray(4);
                    // 127-130是上次登陆的时间
                    LastLoginTime = (long)buf.GetUInt() * 1000L;                   
                    // 39个未知字节
                    // do nothing
                    break;

                case ReplyCode.LOGIN_REDIRECT:
                    // 登陆重定向，可能是为了负载平衡
                    // 001-004字节是用户QQ号
                    buf.GetUInt();
                    // 005-008字节是重定向到的服务器IP
                    RedirectIP = buf.GetByteArray(4);
                    // 009-010字节是重定向到的服务器的端口
                    RedirectPort = buf.GetUShort();
                    break;
                case ReplyCode.LOGIN_FAIL:
                    // 登录失败，我们得到服务器发回来的消息
                    byte[] b = buf.ToByteArray();
                    ReplyMessage = Utils.Util.GetString(b, 1, b.Length - 1);
                    break;
            }
        }

        /// <summary>重定向服务器地址的字符串
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <value></value>
        public string RedirectIPString
        {
            get { return Utils.Util.GetIpStringFromBytes(RedirectIP); }
        }
    }
}
