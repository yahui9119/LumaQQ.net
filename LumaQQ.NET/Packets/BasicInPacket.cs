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

namespace LumaQQ.NET.Packets
{
    /// <summary>
    /// 基本协议族的输入包基类:
    ///  1. 包头标志，1字节，0x02
    ///  2. 服务器端版本代码, 2字节
    ///  3. 命令，2字节
    ///  4. 包序号，2字节
    ///  5. 包体
    ///  6. 包尾标志，1字节，0x03
    /// 	<remark>abu 2008-02-18 </remark>
    /// 	在LumaQQ中，这边还定义了元数据。
    /// </summary>
    public abstract class BasicInPacket : InPacket
    {
       
        /// <summary>
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="user">The user.</param>
        public BasicInPacket(QQCommand command, QQUser user) : base(QQGlobal.QQ_HEADER_BASIC_FAMILY, QQGlobal.QQ_SERVER_VERSION_0100, command, user) { }
        /// <summary>
        /// 构造一个指定参数的包.从buf的当前位置开始解析直到limit
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="user">The user.</param>
        public BasicInPacket(ByteBuffer buf, QQUser user) : base(buf, user) { }
        /// <summary>
        /// 构造一个InPacket，从buf的当前位置解析length个字节
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">The length.</param>
        /// <param name="user">The user.</param>
        public BasicInPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        /// <summary>
        /// 从buf的当前位置解析包头
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void ParseHeader(ByteBuffer buf)
        {
            if (!user.IsUdp)
                buf.GetChar();
            Header = buf.Get();
            Source = buf.GetChar();
            Command = (QQCommand)buf.GetUShort();
            Sequence = buf.GetChar();
        }
        protected override void PutHeader(ByteBuffer buf)
        {
            if (!user.IsUdp)
                buf.PutUShort(0);
            buf.Put(Header);
            buf.PutChar(Source);
            buf.PutUShort((ushort)Command);
            buf.PutChar(Sequence);
        }
        /// <summary>
        /// 从buf的当前未知解析包尾
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void ParseTail(ByteBuffer buf)
        {
            buf.Get();
        }
        /// <summary>
        /// 初始化包体
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void PutBody(ByteBuffer buf)
        {

        }
        /// <summary>
        /// 将包尾部转化为字节流, 写入指定的ByteBuffer对象.
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void PutTail(ByteBuffer buf)
        {
            buf.Put(QQGlobal.QQ_TAIL_BASIC_FAMILY);
        }
        /// <summary>
        /// 包的描述性名称
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public override string GetPacketName()
        {
            return "Unknown Incoming Packet - 0x" + Command.ToString("X");
        }
        /// <summary>
        /// 解密包体
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="body">包体字节数组.</param>
        /// <param name="offset">包体开始偏移.</param>
        /// <param name="length">包体长度.</param>
        /// <returns>解密的包体字节数组</returns>
        protected override byte[] DecryptBody(byte[] body, int offset, int length)
        {
            byte[] temp = null;
            switch (Command)
            {
                case QQCommand.Request_Login_Token:
                    byte[] undecrypted = new byte[length];
                    Array.Copy(body, offset, undecrypted, 0, length);
                    return undecrypted;
                case QQCommand.Login:
                    temp = crypter.Decrypt(body, offset, length, user.PasswordKey);
                    if (temp == null)
                    {
                        temp = crypter.Decrypt(body, offset, length, user.InitKey);
                    }
                    return temp;
                default:
                    if (user.SessionKey != null)
                    {
                        temp = crypter.Decrypt(body, offset, length, user.SessionKey);
                    }
                    if (temp == null)
                    {
                        temp = crypter.Decrypt(body, offset, length, user.PasswordKey);
                    }
                    return temp;
            }
        }
        /// <summary>
        /// 加密包体
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">未加密的字节数组.</param>
        /// <param name="offset">包体开始的偏移.</param>
        /// <param name="length">包体长度.</param>
        /// <returns>加密的包体</returns>
        protected override byte[] EncryptBody(byte[] buf, int offset, int length)
        {
            return null;
        }
        /// <summary>
        /// 得到包体的字节数组
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">包总长度</param>
        /// <returns>包体字节数组</returns>
        protected override byte[] GetBodyBytes(ByteBuffer buf, int length)
        {
            // 得到包体长度
            int bodyLen = length - QQGlobal.QQ_LENGTH_BASIC_FAMILY_IN_HEADER - QQGlobal.QQ_LENGTH_BASIC_FAMILY_TAIL;
            if (!user.IsUdp) bodyLen -= 2;
            // 得到加密的包体内容
            byte[] body = buf.GetByteArray(bodyLen);
            return body;
        }
        /// <summary>
        /// 得到UDP形式包的总长度，不考虑TCP形式
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="bodyLength">包体长度.</param>
        /// <returns>包长度</returns>
        protected override int GetLength(int bodyLength)
        {
            return QQGlobal.QQ_LENGTH_BASIC_FAMILY_IN_HEADER + QQGlobal.QQ_LENGTH_BASIC_FAMILY_TAIL + bodyLength + (user.IsUdp ? 0 : 2);
        }
        /// <summary>
        /// 包头长度
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>包头长度</returns>
        protected override int GetHeaderLength()
        {
            return QQGlobal.QQ_LENGTH_BASIC_FAMILY_IN_HEADER + (user.IsUdp ? 0 : 2);
        }
        /// <summary>
        /// 包尾长度
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>包尾长度</returns>
        protected override int GetTailLength()
        {
            return QQGlobal.QQ_LENGTH_BASIC_FAMILY_TAIL;
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return "包类型: " + Command.ToString() + " 序号: " + (int)Sequence;
        }
        /// <summary>
        /// 密文的起始位置，这个位置是相对于包体的第一个字节来说的，如果这个包是未知包，
        /// 返回-1，这个方法只对某些协议族有意义
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        protected override int GetCryptographStart()
        {
            return -1;
        }
        /// <summary>
        /// 标识这个包属于哪个协议族
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public override ProtocolFamily GetFamily()
        {
            return ProtocolFamily.Basic;
        }
    }
}
