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
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LumaQQ.NET.Utils
{
    public static class Util
    {
        static Encoding DefaultEncoding = Encoding.GetEncoding(QQGlobal.QQ_CHARSET_DEFAULT);
        static DateTime baseDateTime = DateTime.Parse("1970-1-01 00:00:00.000");
        public static Random Random = new Random();
        /// <summary>
        /// 把字节数组从offset开始的len个字节转换成一个unsigned int，
        /// 	<remark>abu 2008-02-15 14:47 </remark>
        /// </summary>
        /// <param name="inData">字节数组</param>
        /// <param name="offset">从哪里开始转换.</param>
        /// <param name="len">转换长度, 如果len超过8则忽略后面的.</param>
        /// <returns></returns>
        public static uint GetUInt(byte[] inData, int offset, int len)
        {
            uint ret = 0;
            int end = 0;
            if (len > 8)
                end = offset + 8;
            else
                end = offset + len;
            for (int i = 0; i < end; i++)
            {
                ret <<= 8;
                ret |= (uint)inData[i];
            }
            return ret;
        }

        /// <summary>
        /// 根据某种编码方式将字节数组转换成字符串
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <param name="encoding">encoding 编码方式</param>
        /// <returns> 如果encoding不支持，返回一个缺省编码的字符串</returns>
        public static string GetString(byte[] b, string encoding)
        {
            try
            {
                return Encoding.GetEncoding(encoding).GetString(b);
            }
            catch
            {
                return Encoding.Default.GetString(b);
            }
        }

        /// <summary>
        /// 根据缺省编码将字节数组转换成字符串
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="b">字节数组</param>
        /// <returns>字符串</returns>
        public static string GetString(byte[] b)
        {
            return GetString(b, QQGlobal.QQ_CHARSET_DEFAULT);
        }
        /// <summary>
        /// * 从buf的当前位置解析出一个字符串，直到碰到了buf的结尾
        /// * <p>
        /// * 此方法不负责调整buf位置，调用之前务必使buf当前位置处于字符串开头。在读取完成
        /// * 后，buf当前位置将位于buf最后之后
        /// * </p>
        /// * <p>
        /// * 返回的字符串将使用QQ缺省编码，一般来说就是GBK编码
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns></returns>
        public static string GetString(ByteBuffer buf)
        {
            ByteBuffer temp = new ByteBuffer();
            while (buf.HasRemaining())
            {
                temp.Put(buf.Get());
            }
            return GetString(temp.ToByteArray());
        }
        /// <summary>从buf的当前位置解析出一个字符串，直到碰到了buf的结尾或者读取了len个byte之后停止
        /// 此方法不负责调整buf位置，调用之前务必使buf当前位置处于字符串开头。在读取完成
        /// * 后，buf当前位置将位于len字节之后或者最后之后
        /// 	<remark>abu 2008-02-25 </remark>
        /// </summary>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static string GetString(ByteBuffer buf, int len)
        {
            ByteBuffer temp = new ByteBuffer();
            while (buf.HasRemaining() && len-- > 0)
            {
                temp.Put(buf.Get());
            }
            return GetString(temp.ToByteArray());
        }

        /// <summary>
        /// * 从buf的当前位置解析出一个字符串，直到碰到了delimit或者读取了maxLen个byte或者
        /// * 碰到结尾之后停止
        /// *此方法不负责调整buf位置，调用之前务必使buf当前位置处于字符串开头。在读取完成
        /// *后，buf当前位置将位于maxLen之后
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="delimit">The delimit.</param>
        /// <param name="maxLen">The max len.</param>
        /// <returns></returns>
        public static String GetString(ByteBuffer buf, byte delimit, int maxLen)
        {
            ByteBuffer temp = new ByteBuffer();
            while (buf.HasRemaining() && maxLen-- > 0)
            {
                byte b = buf.Get();
                if (b == delimit)
                    break;
                else
                    temp.Put(b);
            }
            while (buf.HasRemaining() && maxLen-- > 0)
                buf.Get();
            return GetString(temp.ToByteArray());
        }
        /// <summary>根据某种编码方式将字节数组转换成字符串
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="len">The len.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string GetString(byte[] b, int offset, int len)
        {
            byte[] temp = new byte[len];
            Array.Copy(b, offset, temp, 0, len);
            return GetString(temp);
        }

        /// <summary>
        /// 从buf的当前位置解析出一个字符串，直到碰到一个分隔符为止，或者到了buf的结尾
        /// 此方法不负责调整buf位置，调用之前务必使buf当前位置处于字符串开头。在读取完成
        /// * 后，buf当前位置将位于分隔符之后
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="delimit">The delimit.</param>
        /// <returns></returns>
        public static string GetString(ByteBuffer buf, byte delimit)
        {
            ByteBuffer temp = new ByteBuffer();
            while (buf.HasRemaining())
            {
                byte b = buf.Get();
                if (b == delimit)
                    return GetString(temp.ToByteArray());
                else
                    buf.Put(b);
            }
            return GetString(temp.ToByteArray());
        }

        /// <summary>
        /// 把字符串转换成int
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="defaultValue">如果转换失败，返回这个值</param>
        /// <returns></returns>
        public static int GetInt(string s, int defaultValue)
        {
            int value;
            if (int.TryParse(s, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 字符串转二进制字数组
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static byte[] GetBytes(string s)
        {
            return DefaultEncoding.GetBytes(s);
        }

        /// <summary>一个随机产生的密钥字节数组
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public static byte[] RandomKey()
        {
            byte[] key = new byte[QQGlobal.QQ_LENGTH_KEY];
            (new Random()).NextBytes(key);
            return key;
        }


        /// <summary>这个不是用于调试的，真正要用的方法
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="encoding">编码方式.</param>
        /// <returns>编码方式的字符串表示形式</returns>
        public static String GetEncodingString(Charset encoding)
        {
            switch (encoding)
            {
                case Charset.GB:
                    return "GBK";
                case Charset.EN:
                    return "ISO-8859-1";
                case Charset.BIG5:
                    return "BIG5";
                default:
                    return "GBK";
            }
        }

        /// <summary>
        /// 用于代替 System.currentTimeMillis()
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long GetTimeMillis(DateTime dateTime)
        {
            return (long)(dateTime - baseDateTime).TotalMilliseconds;
        }
        /// <summary>
        /// 根据服务器返回的毫秒表示的日期，获得实际的日期
        /// Gets the date time from millis.
        /// 似乎服务器返回的日期要加上8个小时才能得到正确的 +8 时区的登录时间
        /// </summary>
        /// <param name="millis">The millis.</param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromMillis(long millis)
        {
            return baseDateTime.AddTicks(millis * TimeSpan.TicksPerMillisecond).AddHours(8);
        }

        /// <summary>判断IP是否全0
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public static bool IsIPZero(byte[] ip)
        {
            for (int i = 0; i < ip.Length; i++)
            {
                if (ip[i] != 0)
                    return false;
            }
            return true;
        }
        /// <summary>ip的字节数组形式转为字符串形式的ip
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <returns></returns>
        public static String GetIpStringFromBytes(byte[] ip)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ip[0] & 0xFF);
            sb.Append('.');
            sb.Append(ip[1] & 0xFF);
            sb.Append('.');
            sb.Append(ip[2] & 0xFF);
            sb.Append('.');
            sb.Append(ip[3] & 0xFF);
            return sb.ToString();
        }
        public static string ToHex(byte[] bs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x") + " ");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
