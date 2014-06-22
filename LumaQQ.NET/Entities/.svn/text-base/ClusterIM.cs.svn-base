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

namespace LumaQQ.NET.Entities
{
    /// <summary> * 群消息的信息封装bean，具体内容可以参见ReceiveIMPacket
    ///* 
    ///* 关于自定义表情的格式，参见NormalIM注释
    /// 	<remark>abu 2008-02-23 </remark>
    /// </summary>
    public class ClusterIM
    {
        public RecvSource Source { get; set; }
        // 这个字段在收到临时群消息时表示父群ID，在固定群消息时表示群外部ID
        public int ExternalId { get; set; }
        public byte Type { get; set; }
        public int Sender { get; set; }
        public char Unknown1 { get; set; }
        public char Sequence { get; set; }
        public long SendTime { get; set; }
        public int VersionId { get; set; }
        public char ContentType { get; set; }
        public int FragmentSequence { get; set; }
        public int FragmentCount { get; set; }
        public int MessageId { get; set; }
        // 下面这些都是消息的属性，比如字体啦颜色啦，都是在fontAttribute里面的
        public bool hasFontAttribute { get; set; }
        public FontStyle FontStyle { get; set; }

        // 临时群内部ID，仅用于临时群消息时
        public int ClusterId { get; set; }

        // 消息内容，在解析的时候只用byte[]，正式要显示到界面上时才会转为String，上层程序
        // 要负责这个事，这个类只负责把内容读入byte[]
        public byte[] MessageBytes { get; set; }
        public string Message { get; set; }

        // true表示这个消息中的自定义表情已经全部得到
        public bool FaceResolved { get; set; }
        public ClusterIM(RecvSource source)
        {
            this.Source = source;
            FaceResolved = false;
            FontStyle = new FontStyle();
        }

        /// <summary>给定一个输入流，解析ClusterIM结构
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        public void Read(ByteBuffer buf)
        {
            // 群外部ID或者父群ID
            ExternalId = buf.GetInt();
            // 群类型，1字节
            Type = buf.Get();
            // 临时群内部ID
            if (Source == RecvSource.TEMP_CLUSTER)
                ClusterId = buf.GetInt();
            // 发送者
            Sender = buf.GetInt();
            // 未知1
            Unknown1 = buf.GetChar();
            // 消息序号
            Sequence = buf.GetChar();
            // 发送时间，记得乘1000才对
            SendTime = buf.GetInt() * 1000L;
            // Member Version ID
            VersionId = buf.GetInt();
            // 后面的内容长度
            buf.GetChar();
            // 一些扩展信息 
            if (Source != RecvSource.UNKNOWN_CLUSTER)
            {
                // content type
                ContentType = buf.GetChar();
                // 分片数
                FragmentCount = buf.Get() & 0xFF;
                // 分片序号
                FragmentSequence = buf.Get() & 0xFF;
                // 2字节未知
                MessageId = (int)buf.GetChar();
                // 4字节未知
                buf.GetInt();
            }
            // 消息正文，只有最后一个分片有字体属性
            int remain = buf.Remaining();
            int fontAttributeLength = (FragmentSequence == FragmentCount - 1) ? (buf.Get(buf.Position + remain - 1) & 0xFF) : 0;
            MessageBytes = buf.GetByteArray(remain - fontAttributeLength);
            // 只有最后一个分片有字体属性
            hasFontAttribute = FragmentSequence == FragmentCount - 1;
            // 这后面都是字体属性，这个和SendIMPacket里面的是一样的
            if (hasFontAttribute)
                FontStyle.Read(buf);
        }
    }
}
