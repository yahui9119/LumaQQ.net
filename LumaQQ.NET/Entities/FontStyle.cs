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
using System.Drawing;

namespace LumaQQ.NET.Entities
{
    /// <summary>
    /// 字体
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class FontStyle
    {
        /** 字体属性 */
        private const byte NONE = 0x00;
        private const byte BOLD = 0x20;
        private const byte ITALIC = 0x40;
        private const byte UNDERLINE = (byte)0x80;

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public string FontName { get; set; }
        private bool bold;
        public bool Bold
        {
            get { return this.bold; }
            set
            {
                this.bold = value;
                fontFlag &= 0xDF;
                fontFlag |= bold ? BOLD : NONE;
            }
        }
        private bool italic;
        public bool Italic
        {
            get
            { return this.italic; }
            set
            {
                this.italic = value;
                fontFlag &= 0xBF;
                fontFlag |= italic ? ITALIC : NONE;
            }
        }
        private bool underline;
        public bool Underline
        {
            get
            {
                return this.underline;
            }
            set
            {
                this.underline = value;
                fontFlag &= 0x7F;
                fontFlag |= underline ? UNDERLINE : NONE;
            }
        }
        public int FontSize { get; set; }
        private ushort fontFlag; // 用来表示bold, italic, underline, fontSize的组合结果
        public string Encoding { get; set; }
        public Charset EncodingCode { get; set; }

        public FontStyle()
        {
            EncodingCode = Charset.GB;
            Encoding = "GBK";
            FontName = "宋体";
            Red = Green = Blue = 0;
            bold = italic = underline = false;
            FontSize = 0x9;
            fontFlag = 0x9;
        }
        /// <summary>
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Write(ByteBuffer buf)
        {
            buf.PutUShort(fontFlag);
            // 字体颜色红绿篮
            buf.Put((byte)Red);
            buf.Put((byte)Green);
            buf.Put((byte)Blue);
            // 一个未知字节
            buf.Put((byte)0);
            // 消息编码
            buf.PutUShort((ushort)EncodingCode);
            // 字体
            byte[] fontBytes = Utils.Util.GetBytes(FontName);
            buf.Put(fontBytes);
            // 字体属性长度（包括本字节）
            buf.Put((byte)(fontBytes.Length + 9));
        }

        /// <summary>
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            fontFlag = buf.GetChar();
            // 分析字体属性到具体的变量
            // 字体大小
            FontSize = fontFlag & 0x1F;
            // 组体，斜体，下画线
            bold = (fontFlag & 0x20) != 0;
            italic = (fontFlag & 0x40) != 0;
            underline = (fontFlag & 0x80) != 0;
            // 字体颜色rgb
            Red = (int)buf.Get();
            Green = (int)buf.Get();
            Blue = (int)buf.Get();
            // 1个未知字节
            buf.Get();
            // 消息编码，这个据Gaim QQ的注释，这个字段用处不大，说是如果在一个英文windows
            // 里面输入了中文，那么编码是英文的，按照这个encoding来解码就不行了
            // 不过我们还是得到这个字段吧，后面我们采用先缺省GBK解码，不行就这个encoding
            // 解码，再不行就ISO-8859-1的方式
            EncodingCode = (Charset)buf.GetChar();
            Encoding = Utils.Util.GetEncodingString(EncodingCode);
            // 字体名称，字体名称也有中文的也有英文的，所以。。先来试试缺省的
            FontName = Utils.Util.GetString(buf.GetByteArray(buf.Length - buf.Position - 1));
        }

        /// <summary>字体颜色
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public Color FontColor
        {
            get { return Color.FromArgb(Red, Green, Blue); }
            set
            {
                Red = value.R;
                Green = value.G;
                Blue = value.B;
            }
        }

    }
}
