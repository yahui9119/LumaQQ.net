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
    ///  * 05系列的输入包基类
    /// * 1. 包头标识，1字节
    /// * 2. source，2字节
    /// * 3. 包长度，2字节
    /// * 4. 包命令，2字节
    /// * 5. 包序号，2字节
    /// * 6. 用户QQ号，4字节
    /// * 7. 包体
    /// * 8. 包尾，1字节
    /// * 
    /// * 值得注意的是：这种包的包体并非完全加密型，而是部分加密型
    /// 	<remark>abu 2008-02-26 </remark>
    /// </summary>
    public abstract class _05InPacket : InPacket
    {
        public int QQ { get; set; }
        public _05InPacket(QQCommand command, QQUser user)
            : base(QQGlobal.QQ_HEADER_05_FAMILY, QQGlobal.QQ_CLIENT_VERSION, command, user) { }
        public _05InPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public _05InPacket(ByteBuffer buf, QQUser user) : base(buf, user) { }
        protected override int GetLength(int bodyLength)
        {
            return QQGlobal.QQ_LENGTH_05_FAMILY_HEADER + QQGlobal.QQ_LENGTH_05_FAMILY_TAIL + bodyLength;
        }
        protected override int GetHeaderLength()
        {
            return QQGlobal.QQ_LENGTH_05_FAMILY_HEADER;
        }
        protected override int GetTailLength()
        {
            return QQGlobal.QQ_LENGTH_05_FAMILY_TAIL;
        }
        protected override void PutHeader(ByteBuffer buf)
        {
            buf.Put(QQGlobal.QQ_HEADER_05_FAMILY);
            buf.PutChar(Source);
            buf.PutChar((char)0);
            buf.PutUShort((ushort)Command);
            buf.PutChar(Sequence);
            buf.PutInt(user.QQ);
        }
        protected override void PutBody(ByteBuffer buf)
        {

        }
        protected override byte[] GetBodyBytes(ByteBuffer buf, int length)
        {
            // 得到包体长度
            int bodyLen = length - QQGlobal.QQ_LENGTH_05_FAMILY_HEADER - QQGlobal.QQ_LENGTH_05_FAMILY_TAIL;
            // 得到包体内容
            byte[] body = buf.GetByteArray(bodyLen);
            return body;
        }
        protected override void PutTail(ByteBuffer buf)
        {
            buf.Get(QQGlobal.QQ_TAIL_05_FAMILY);
        }
        protected override byte[] EncryptBody(byte[] buf, int offset, int length)
        {
            return null;
        }
        protected override byte[] DecryptBody(byte[] body, int offset, int length)
        {
            // 解密密文部分
            int start = GetCryptographStart();
            if (start == -1)
            {
                byte[] ret = new byte[length];
                Array.Copy(body, offset, ret, 0, length);
                return ret;
            }
            byte[] decrypted = crypter.Decrypt(body, start + offset, length - start, user.FileAgentKey);

            // 创建返回数组
            byte[] ret1 = new byte[start + decrypted.Length];
            // 拷贝前面的明文部分
            Array.Copy(body, offset, ret1, 0, start);
            // 拷贝已解密部分
            Array.Copy(decrypted, 0, ret1, start, decrypted.Length);
            return ret1;
        }
        protected override void ParseHeader(ByteBuffer buf)
        {
            Header = buf.Get();
            Source = buf.GetChar();
            buf.GetChar();
            Command = (QQCommand)buf.GetUShort();
            Sequence = buf.GetChar();
            QQ = buf.GetInt();
        }
        protected override void ParseTail(ByteBuffer buf)
        {
            buf.Get();
        }
        public override string GetPacketName()
        {
            return "Unknown Incoming Packet - 05 Family";
        }
        public override string ToString()
        {
            return "包类型: " + Command.ToString() + " 序号: " + (int)Sequence;
        }
        public override ProtocolFamily GetFamily()
        {
            return ProtocolFamily._05;
        }
    }
}
