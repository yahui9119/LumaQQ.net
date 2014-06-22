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
    /// <summary>
    /// 短消息封装类
    /// 	<remark>abu 2008-02-23 </remark>
    /// </summary>
    public class SMS
    {
        public string Message { get; set; }
        public int Sender { get; set; }
        public int Header { get; set; }
        public long Time { get; set; }
        // 如果sender是10000，则senderName为手机号码
        public string SenderName { get; set; }

        /// <summary>给定一个输入流，解析SMS结构
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadBindUserSMS(ByteBuffer buf)
        {
            // 未知1字节，0x0
            buf.Get();
            // 发送者QQ号，4字节
            Sender = buf.GetInt();
            // 发送者头像
            Header = (int)buf.GetUShort();
            // 发送者名称，最多13字节，不足后面补0
            SenderName = Utils.Util.GetString(buf, (byte)0, QQGlobal.QQ_MAX_SMS_SENDER_NAME);
            // 未知的1字节，0x4D
            buf.Get();
            // 消息内容
            Message = Utils.Util.GetString(buf, (byte)0);

            Time = DateTime.Now.Millisecond;
        }

        /// <summary>读取移动QQ用户的短信
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadMobileQQSMS(ByteBuffer buf)
        {
            // 未知1字节
            buf.Get();
            // 发送者QQ号，4字节
            Sender = buf.GetInt();
            // 发送者头像
            Header = (int)buf.GetUShort();
            // 发送者名称，最多13字节，不足后面补0
            SenderName = Utils.Util.GetString(buf, (byte)0, QQGlobal.QQ_MAX_SMS_SENDER_NAME);
            // 未知的1字节，0x4D
            buf.Get();
            // 发送时间
            Time = (long)buf.GetInt() * 1000L;
            // 未知的1字节，0x03
            buf.Get();
            // 消息内容
            Message = Utils.Util.GetString(buf, (byte)0);
        }

        /// <summary>读取移动QQ用户消息（通过手机号描述）
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadMobileQQ2SMS(ByteBuffer buf)
        {
            // 未知1字节
            buf.Get();
            // 发送者，这种情况下都置为10000
            Sender = 10000;
            // 手机号码
            SenderName = Utils.Util.GetString(buf, (byte)0, 18);
            // 未知2字节
            buf.GetChar();
            // 时间
            Time = (long)buf.GetInt() * 1000L;
            // 未知的1字节，0x03
            buf.Get();
            // 消息内容
            Message = Utils.Util.GetString(buf, (byte)0);
        }
        /// <summary>读取普通手机的短信
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void ReadMobileSMS(ByteBuffer buf)
        {
            // 未知1字节，0x0
            buf.Get();
            // 发送者
            Sender = 10000;
            // 手机号码
            SenderName = Utils.Util.GetString(buf, (byte)0, 20);
            // 消息内容
            Message = Utils.Util.GetString(buf, (byte)0);

            Time = DateTime.Now.Ticks;
        }
    }
}
