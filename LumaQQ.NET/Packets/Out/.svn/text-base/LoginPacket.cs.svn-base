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
    ///  * QQ登录请求包，格式为
    /// * 1. 头部
    /// * 2. 初始密钥，16字节
    /// * 3. 用户的密码密钥加密一个空串得到的16字节
    /// * 4. 36字节的固定内容，未知含义
    /// * 5. 登录状态，隐身登录还是什么，1字节
    /// * 6. 16字节固定内容，未知含义
    /// * 7. 登录令牌长度，1字节
    /// * 8. 登录令牌
    /// * 9. 登录模式，1字节，目前只支持普通模式
    /// * 10. 未知1字节，0x40
    /// * 11. 后面段的个数，1字节，1个段9字节(猜测)
    /// * 12. 段，每次基本都是固定内容，未知含义
    /// * 13. 长度不足则全部填0知道符合登录包长度
    /// * 14. 尾部
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class LoginPacket : BasicOutPacket
    {
        public LoginPacket(QQUser user) : base(QQCommand.Login, true, user) { }
        public LoginPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Login Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 初始密钥
            buf.Put(user.InitKey);
            // 创建登陆信息数组
            byte[] login = new byte[QQGlobal.QQ_LENGTH_LOGIN_DATA];
            // 开始填充登陆信息
            // 头16个字节用md5处理的密码加密一个空字符串，这用来在服务器端校验密码
            // 其实不一定非要空串，任意均可，只要保证密文是16个字节就行，服务器端
            // 只是看看能不能用密码密钥解密，他不管解密出来的是什么
            Array.Copy(crypter.Encrypt(Utils.Util.GetBytes(""), user.PasswordKey), 0, login, 0, 16);
            // 36字节的固定内容
            Array.Copy(QQGlobal.QQ_LOGIN_16_51, 0, login, 16, QQGlobal.QQ_LOGIN_16_51.Length);
            // 登录状态，隐身登录还是什么，1字节
            login[52] = (byte)user.LoginMode;
            // 16字节固定内容
            Array.Copy(QQGlobal.QQ_LOGIN_53_68, 0, login, 53, QQGlobal.QQ_LOGIN_53_68.Length);
            // 登录令牌长度，1字节
            int pos = 69;
            int len = user.LoginToken.Length;
            login[pos++] = (byte)len;
            // 登录令牌
            Array.Copy(user.LoginToken, 0, login, 70, len);
            pos += len;
            // 未知1字节，0x40
            login[pos++] = 0x40;
            // 后面段的个数，1字节，1个段9字节(猜测)
            // 段，每次基本都是固定内容，未知含义
            Array.Copy(QQGlobal.QQ_LOGIN_SEGMENTS, 0, login, pos, QQGlobal.QQ_LOGIN_SEGMENTS.Length);
            pos += QQGlobal.QQ_LOGIN_SEGMENTS.Length;
            // 长度不足则全部填0知道符合登录包长度
            for (; pos < QQGlobal.QQ_LENGTH_LOGIN_DATA; pos++)
                login[pos] = 0;
            // 加密登录信息
            login = crypter.Encrypt(login, user.InitKey);
            // 写入登录信息
            buf.Put(login);
        }
    }
}
