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
    /// 所有输出包基类，这个基类定义了输出包的基本框架
    /// <remark>abu 2008-02-18 </remark>
    /// </summary>
    public abstract class OutPacket : Packet
    {
        /// <summary>
        /// 包起始序列号
        /// </summary>
        protected static char seq = (char)Util.Random.Next();
        /// <summary>
        /// 是否需要回应
        /// </summary>
        protected bool ack;
        /// <summary>
        /// 重发计数器
        /// </summary>
        protected int resendCountDown;
        /// <summary>
        /// 超时截止时间，单位ms
        /// </summary>
        public long TimeOut { get; set; }
        /// <summary>
        /// 发送次数，只在包是不需要ack时有效，比如logout包是发4次，但是其他可能只发一次
        /// </summary>
        public int SendCount { get; set; }
        /// <summary>
        /// 加密/解密密钥，只有有些包可能需要一个特定的密钥，如果为null，使用缺省的
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>创建一个基本输出包
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="command">包命令.</param>
        /// <param name="ack">包是否需要回复.</param>
        /// <param name="user">QQ用户对象.</param>
        public OutPacket(byte header, QQCommand command, bool ack, QQUser user)
            : base(header, QQGlobal.QQ_CLIENT_VERSION, command, GetNextSeq(), user)
        {
            this.ack = ack;
            this.resendCountDown = QQGlobal.QQ_SEND_TIME_NOACK_PACKET;
            this.SendCount = 1;
        }
        /// <summary>从buf中构造一个OutPacket，用于调试。这个buf里面可能包含了抓包软件抓来的数据
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="user">The user.</param>
        protected OutPacket(ByteBuffer buf, QQUser user) : base(buf, user) { }
        /// <summary>从buf中构造一个OutPacket，用于调试。这个buf里面可能包含了抓包软件抓来的数据
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="length">The length.</param>
        /// <param name="user">The user.</param>
        protected OutPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }

        /// <summary>
        /// 解析包体，从buf的开头位置解析起
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void ParseBody(ByteBuffer buf)
        {
        }

        /// <summary>
        /// 回填，有些字段必须填完整个包才能确定其内容，比如长度字段，那么这个方法将在
        /// 尾部填充之后调用
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="startPos">The start pos.</param>
        protected abstract void PostFill(ByteBuffer buf, int startPos);
        /// <summary>
        ///  将整个包转化为字节流, 并写入指定的ByteBuffer对象.
        ///  一般而言, 前后分别需要写入包头部和包尾部.
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Fill(ByteBuffer buf)
        {
            //保存当前pos
            int pos = buf.Position;
            // 填充头部
            PutHeader(buf);
            // 填充包体
            bodyBuf.Initialize();
            PutBody(bodyBuf);
            // 加密包体
            bodyDecrypted = bodyBuf.ToByteArray();
            byte[] enc = EncryptBody(bodyDecrypted, 0, bodyDecrypted.Length);
            // 加密内容写入最终buf
            buf.Put(enc);
            // 填充尾部
            PutTail(buf);
            // 回填
            PostFill(buf, pos);
        }
        protected static char GetNextSeq()
        {
            seq++;
            // 为了兼容iQQ
            // iQQ把序列号的高位都为0，如果为1，它可能会拒绝，wqfox称是因为TX是这样做的
            seq &= (char)0x7FFF;
            if (seq == 0)
            {
                seq++;
            }
            return seq;
        }
        /// <summary>
        /// 包的描述性名称
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public override string GetPacketName()
        {
            return "Unknown Outcoming Packet";
        }
        /// <summary>
        /// 是否需要重发.
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>需要重发返回true, 否则返回false.</returns>
        public bool NeedResend()
        {
            return (resendCountDown--) > 0;
        }
        /// <summary>
        /// 是否需要回复
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns>true表示包需要回复</returns>
        public bool NeedAck()
        {
            return ack;
        }

    }
}
