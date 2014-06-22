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
    ///  * 发送短消息的请求包，格式为：
    /// * 1. 包头
    /// * 2. 消息序号，2字节，从0开始，用在拆分发送中
    /// * 3. 未知2字节，全0
    /// * 4. 未知4字节，全0
    /// * 5. 发送者昵称，最长13个字节，如果不足，则后面为0
    /// * 6. 未知的1字节，0x01
    /// * 7. 如果是免提短信，0x20，其他情况，0x00
    /// * 8. 短消息内容类型，1字节
    /// * 9. 短消息内容类型编号，4字节
    /// * 10. 未知的1字节，0x01
    /// * 11. 接受者中的手机号码个数，1字节
    /// * 12. 手机号码，18字节，不足的为0
    /// * 13. 未知的2字节
    /// * 14. 未知的1字节
    /// * 15. 如果有更多手机号，重复12-14部分
    /// * 注：12-15部分只在11部分不为0时存在
    /// * 16. 接受者中的QQ号码个数，1字节
    /// * 17. QQ号码，4字节
    /// * 18. 如果有更多QQ号码，重复17部分
    /// * 注：17-18部分只有在16部分不为0时存在
    /// * 19. 未知1字节，一般是0x03
    /// * 20. 短消息字节长度，2字节，如果8部分不为0，则此部分0x0000
    /// * 注：QQ的短信和发送者昵称加起来不能超过58个字符（英文和汉字都算是一个字符），
    /// * 昵称最长是13字节，所以最短也应该能发43个字符，所以可以考虑不按照QQ的做法，
    /// * 我们可以尽量发满86个字节。
    /// * 21. 短消息字节数组，消息的格式如下：
    /// * 		如果是普通的消息，则就是平常的字节数组而已
    /// *      如果有些字符有闪烁，则那些字节要用0x01括起来
    /// *      如果这条消息是一条长消息拆分而成的部分，则在消息字节数组前面要加一部分内容，这部分内容是
    /// *      [消息序号的字符串形式，从1开始] [0x2F] [总消息条数的字符串形式] [0x0A]
    /// * 注：21部分只有当20部分部位0时存在
    /// * 22. 尾部
    /// * 
    /// * 调用这个包时，message的内容必须是已经组装好的
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class SendSMSPacket : BasicOutPacket
    {
        public ushort MessageSequence { get; set; }
        public byte[] Message { get; set; }
        public SMSContentType ContentType { get; set; }
        public int ContentId { get; set; }
        public SMSSendMode SendMode { get; set; }
        public string SenderName { get; set; }
        public List<string> ReceiverMobile { get; set; }
        public List<int> ReceiverQQ { get; set; }

        public SendSMSPacket(QQUser user)
            : base(QQCommand.Send_SMS, true, user)
        {
            MessageSequence = 0;
            ContentType = SMSContentType.NORMAL;
            SendMode = SMSSendMode.NORMAL;
        }
        public SendSMSPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Send SMS Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 短消息序号
            buf.PutUShort(MessageSequence);
            // 未知2字节
            buf.PutChar((char)0);
            // 未知4字节
            buf.PutInt(0);
            // 发送者昵称
            byte[] b = Utils.Util.GetBytes(SenderName);
            if (b.Length > QQGlobal.QQ_MAX_SMS_SENDER_NAME)
            {
                buf.Put(b, 0, QQGlobal.QQ_MAX_SMS_SENDER_NAME);
            }
            else
            {
                buf.Put(b);
                int stuff = QQGlobal.QQ_MAX_SMS_SENDER_NAME - b.Length;
                while (stuff-- > 0)
                    buf.Put((byte)0);
            }
            // 未知1字节
            buf.Put((byte)0x01);
            // 发送模式
            buf.Put((byte)SendMode);
            // 内容类型
            buf.Put((byte)ContentType);
            // 内容编号
            buf.PutInt(ContentId);
            // 未知1字节
            buf.Put((byte)0x01);
            // 手机个数
            if (ReceiverMobile == null)
                buf.Put((byte)0);
            else
            {
                buf.Put((byte)ReceiverMobile.Count);
                foreach (String mobile in ReceiverMobile)
                {
                    b = Utils.Util.GetBytes(mobile);
                    if (b.Length > QQGlobal.QQ_MAX_SMS_MOBILE_LENGTH)
                    {
                        buf.Put(b, 0, QQGlobal.QQ_MAX_SMS_MOBILE_LENGTH);
                    }
                    else
                    {
                        buf.Put(b);
                        int stuff = QQGlobal.QQ_MAX_SMS_MOBILE_LENGTH - b.Length;
                        while (stuff-- > 0)
                            buf.Put((byte)0);
                    }
                    // 未知的2字节
                    buf.PutChar((char)0);
                    // 未知的1字节
                    buf.Put((byte)0);
                }
            }
            // QQ号码个数
            if (ReceiverQQ == null)
                buf.Put((byte)0);
            else
            {
                buf.Put((byte)ReceiverQQ.Count);
                foreach (int qq in ReceiverQQ)
                    buf.PutInt(qq);
            }
            // 未知1字节
            buf.Put((byte)0x03);
            // 消息
            if (Message == null)
                buf.PutChar((char)0);
            else
            {
                buf.PutChar((char)Message.Length);
                // 消息
                buf.Put(Message);
            }
        }
    }
}
