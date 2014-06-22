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

using LumaQQ.NET.Utils;

namespace LumaQQ.NET.Packets
{
    /// <summary>
    /// QQ所有包对象的基类
    /// 	<remark>abu 2008-02-15 11:33 </remark>
    /// </summary>
    public abstract class Packet
    {
        /// <summary>输入包使用的连接名称
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <value></value>
        public string PortName { get; set; }

        /// <summary>
        /// 加密解密对象
        /// </summary>
        protected static Crypter crypter = new Crypter();

        /// <summary>
        /// 包体缓冲区，有back array，用来存放未加密时的包体，子类应该在putBody方法中
        /// 使用这个缓冲区。使用之前先执行clear() 
        /// </summary>
        protected static ByteBuffer bodyBuf = new ByteBuffer(QQGlobal.QQ_MAX_PACKET_SIZE);


        /// <summary>
        /// 包命令, 0x03~0x04.
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public QQCommand Command { get; set; }

        /// <summary>源标志, 0x01~0x02.
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public char Source { get; set; }

        /// <summary>包序号, 0x05~0x06.
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public char Sequence { get; set; }
        /// <summary>
        /// 包头字节 
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte Header { get; set; }
        /// <summary>
        /// true表示这个包是一个重复包，重复包本来是不需要处理的，但是由于LumaQQ较常发生
        ///  消息确认包丢失的问题，所以，这里加一个字段来表示到来的消息包是重复的。目前这个
        ///  字段只对消息有效，姑且算个解决办法吧，虽然不是太好看
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public bool IsDuplicated { get; set; }

        /// <summary>QQUser
        /// 为了支持一个JVM中创建多个QQClient，包中需要保持一个QQUser的引用以
        /// 确定包的用户相关字段如何填写
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        protected QQUser user;

        /// <summary>
        /// 明文包体
        /// </summary>
        protected byte[] bodyDecrypted;

        /// <summary>构造一个指定参数的包
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="header">包头</param>
        /// <param name="source">包源</param>
        /// <param name="command">包命令 </param>
        /// <param name="sequence">包序号 </param>
        /// <param name="user">QQ用户对象</param>
        public Packet(byte header, char source, QQCommand command, char sequence, QQUser user)
        {
            this.user = user;
            this.Source = source;
            this.Command = command;
            this.Sequence = sequence;
            this.IsDuplicated = false;
            this.Header = header;
            this.DateTime = DateTime.Now;
        }
        /// <summary>从buf中构造一个OutPacket，用于调试。这个buf里面可能包含了抓包软件抓来的数据
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="user">The user.</param>
        protected Packet(ByteBuffer buf, QQUser user)
            : this(buf, buf.Length - buf.Position, user)
        {
        }
        /// <summary>从buf中构造一个OutPacket，用于调试。这个buf里面可能包含了抓包软件抓来的数据
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">要解析的内容长度</param>
        /// <param name="user">The user.</param>
        protected Packet(ByteBuffer buf, int length, QQUser user)
        {
            this.user = user;
            ParseHeader(buf);
            if (!ValidateHeader())
                throw new PacketParseException("包头有误，抛弃该包: " + ToString());
            // 得到包体
            byte[] body = GetBodyBytes(buf, length);
            bodyDecrypted = DecryptBody(body, 0, body.Length);
            if (bodyDecrypted == null)
                throw new PacketParseException("包内容解析出错，抛弃该包: " + ToString());
            // 包装到ByteBuffer
            ByteBuffer tempBuf = new ByteBuffer(bodyDecrypted);
            try
            {
                ParseBody(tempBuf);
            }
            catch (Exception e)
            {
                throw new PacketParseException(e.Message, e);
            }
            ParseTail(buf);
            this.DateTime = DateTime.Now;
        }

        /// <summary> 构造一个包对象，什么字段也不填，仅限于子类使用
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        protected Packet()
        {
            this.DateTime = DateTime.Now;
        }
        /// <summary>得到UDP形式包的总长度，不考虑TCP形式
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="bodyLength">包体长度.</param>
        /// <returns>包长度</returns>
        protected abstract int GetLength(int bodyLength);
        /// <summary>从buf的当前未知解析包尾
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected abstract void ParseTail(ByteBuffer buf);

        /// <summary>解析包体，从buf的开头位置解析起
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected abstract void ParseBody(ByteBuffer buf);
        /// <summary>解密包体
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="body">包体字节数组.</param>
        /// <param name="offset">包体开始偏移.</param>
        /// <param name="length">包体长度.</param>
        /// <returns>解密的包体字节数组</returns>
        protected abstract byte[] DecryptBody(byte[] body, int offset, int length);
        /// <summary>得到包体的字节数组
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">包总长度</param>
        /// <returns>包体字节数组</returns>
        protected abstract byte[] GetBodyBytes(ByteBuffer buf, int length);
        /// <summary>校验头部
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        protected abstract bool ValidateHeader();
        /// <summary>从buf的当前位置解析包头
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected abstract void ParseHeader(ByteBuffer buf);
        /// <summary>包头长度
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>包头长度</returns>
        protected abstract int GetHeaderLength();
        /// <summary>
        /// 包尾长度
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>包尾长度</returns>
        protected abstract int GetTailLength();
        /// <summary>
        /// 将包头部转化为字节流, 写入指定的ByteBuffer对象.
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected abstract void PutHeader(ByteBuffer buf);
        /// <summary>初始化包体
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected abstract void PutBody(ByteBuffer buf);
        /// <summary>
        /// 标识这个包属于哪个协议族
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public abstract ProtocolFamily GetFamily();
        /// <summary>
        /// 将包尾部转化为字节流, 写入指定的ByteBuffer对象.
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected abstract void PutTail(ByteBuffer buf);
        /// <summary>
        /// 加密包体
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">未加密的字节数组.</param>
        /// <param name="offset">包体开始的偏移.</param>
        /// <param name="length">包体长度.</param>
        /// <returns>加密的包体</returns>
        protected abstract byte[] EncryptBody(byte[] buf, int offset, int length);

        /// <summary>
        /// 密文的起始位置，这个位置是相对于包体的第一个字节来说的，如果这个包是未知包，
        /// 返回-1，这个方法只对某些协议族有意义
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        protected abstract int GetCryptographStart();

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        public override bool Equals(object obj)
        {
            if (obj is Packet)
            {
                Packet packet = (Packet)obj;
                return Header == packet.Header && Command == packet.Command && Sequence == packet.Sequence;
            }
            return base.Equals(obj);
        }
        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Hash(Sequence, Command);
        }


        /// <summary>
        /// 得到hash值
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public static int Hash(char sequence, QQCommand command)
        {
            return (sequence << 16) | (ushort)command;
        }
        /// <summary>包的描述性名称
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public virtual string GetPacketName()
        {
            return "Unknown Packet";
        }

        /// <summary>
        /// 包的接收时间或发送时间
        /// 	<remark>abu 2008-03-13 </remark>
        /// </summary>
        /// <value></value>
        public DateTime DateTime { get;  set; }
    }
}
