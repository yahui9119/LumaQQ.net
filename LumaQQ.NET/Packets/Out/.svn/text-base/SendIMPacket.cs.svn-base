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

using LumaQQ.NET.Entities;
namespace LumaQQ.NET.Packets.Out
{
    /// <summary>
    ///  * 发送消息的包，格式为
    /// * 1. 头部
    /// * 2. 发送者QQ号，4个字节
    /// * 3. 接收者的QQ号，4个字节
    /// * 4. 发送者QQ版本，2字节
    /// * 5. 发送者QQ号，4字节
    /// * 6. 接收者QQ号，4个字节（奇怪，为什么要搞两个在里面）
    /// * 7. 发送者QQ号和session key合在一起用md5处理一次的结果，16字节
    /// * 8. 消息类型，2字节
    /// * 9. 会话ID，2字节，如果是一个操作需要发送多个包才能完成，则这个id必须一致
    /// * 10. 发送时间，4字节
    /// * 11. 发送者头像，2字节
    /// * 12. 字体信息，4字节，设成0x00000001吧，不懂具体意思
    /// * 13. 消息分片数，1字节，如果消息比较长，这里要置一个分片值，QQ缺省是700字节一个分片，这个700字节是纯消息，
    /// *     不包含其他部分
    /// * 14. 分片序号，1字节，从0开始
    /// * 15. 消息的id，2字节，同一条消息的不同分片id相同
    /// * 16. 消息方式，是发送的，还是自动回复的，1字节
    /// * 17. 消息内容，最后一个分片的结尾需要追加一个空格。
    /// * Note: 结尾处的空格是必须的，如果不追加空格，会导致有些缺省表情显示为乱码
    /// * 18. 消息的尾部，包含一些消息的参数，比如字体颜色啦，等等等等，顺序是
    /// *     1. 字体修饰属性，bold，italic之类的，2字节，已知的位是
    /// *         i.   bit0-bit4用来表示字体大小，所以最大是32
    /// *         ii.  bit5表示是否bold
    /// *         iii. bit6表示是否italic
    /// *         iv.  bit7表示是否underline
    /// *     2. 颜色Red，1字节
    /// *     3. 颜色Green，1字节
    /// *     4. 颜色Blue，1字节
    /// *     5. 1个未知字节，置0先
    /// *     6. 消息编码，2字节，0x8602为GB，0x0000为EN，其他未知，好像可以自定义，因为服务器好像不干涉
    /// *     7. 字体名，比如0xcb, 0xce, 0xcc, 0xe5表示宋体 
    /// * 19. 1字节，表示18和19部分的字节长度
    /// * 20. 包尾部
    /// *
    /// * 请求传送文件的包，这是这个包的另一种用法，其格式为
    /// * 1  - 14. 1到14部分均与发送消息包相同，只有第8部分不同，对于UDP的请求，8部分是0x0035，对于TCP，是0x0001
    /// * 15 - 17. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
    /// * 18. 传输类型，1字节，表示是传文件还是传表情
    /// * 19. 连接方式字节，UDP是0， TCP是3
    /// * 20. 4个字节的发送者外部ip地址（也就是可能为代理地址）
    /// * 21. 2个字节的发送者端口
    /// * 22. 2个字节的端口，第一个监听端口，TCP没有这个部分
    /// * 23. 4个字节的地址，真实IP
    /// * 24. 2个字节的端口，第二个而监听端口
    /// * 25. 空格符号做为上述信息的结束，一个字节，0x20
    /// * 26. 分隔符0x1F
    /// * 27. 要传送的文件名
    /// * 28. 分隔符0x1F
    /// * 29. 字节数的字符串形式后跟" 字节"，比如文件大小3字节的话，就是"3 字节"这个字符串的编码形式
    /// * 30. 尾部 
    /// * 
    /// * 同意传送文件的包，格式为
    /// * 1  - 24. 除了8部分，其他均与发送消息包相同。对于UDP的情况，8部分是0x0037，TCP是0x0003。
    /// *          UDP时，最后的本地ip和端口都是0；TCP时没有22部分
    /// * 25. 尾部
    /// * 
    /// * 拒绝接收文件的包，格式为
    /// * 1 - 19. 除了8部分，均与同意传送文件包相同。对于UDP的情况，8部分是0x0039，对于TCP，是0x0005
    /// * 20. 尾部
    /// * 
    /// * 通知我的IP信息，格式为
    /// * 1 - 24. 除了8部分，均与请求传送文件包相同。8部分是0x003B
    /// * 25. 尾部
    /// * 
    /// * 取消传送文件，格式为
    /// * 1 - 18. 除了8部分，均与请求传送文件包相同。8部分是0x0049
    /// * 19. 尾部
    /// * 
    /// * 要求别人主动连接我的包，格式为
    /// * 1 - 18. 除了8部分，均与请求传送文件包相同。8部分是0x003F
    /// * 19. 尾部
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class SendIMPacket : BasicOutPacket
    {
        // 下面为发送普通消息需要设置的变量
        public FontStyle FontStyle { get; set; }
        public int Receiver { get; set; }
        public byte[] Message { get; set; }
        public NormalIMType MessageType { get; set; }
        public ReplyType ReplyType { get; set; }
        /// <summary>消息分片数
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public int TotalFragments { get; set; }
        /// <summary>消息分片序号
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public int FragmentSequence { get; set; }
        public ushort MessageId { get; set; }

        // 下面为发送文件时需要设置的变量
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public ushort DirectPort { get; set; }
        public ushort LocalPort { get; set; }
        public byte[] LocalIp { get; set; }
        public ushort SessionId { get; set; }
        public TransferType TransferType { get; set; }

        /// <summary>true时表示发送一个假IP，用在如来神掌中，免得泄漏自己的IP
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <value></value>
        public bool FakeIp { get; set; }
        private const byte DELIMIT = 0x1F;

        public SendIMPacket(QQUser user)
            : base(QQCommand.Send_IM, true, user)
        {
            FontStyle = new FontStyle();
            Message = null;
            MessageType = NormalIMType.TEXT;
            ReplyType = ReplyType.NORMAL;
            TransferType = TransferType.FILE;
            FakeIp = false;
            TotalFragments = 1;
            FragmentSequence = 0;
        }

        public SendIMPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Send IM Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 发送者QQ号
            buf.PutInt(user.QQ);
            // 接收者QQ号
            buf.PutInt(Receiver);
            // 发送者QQ版本
            buf.PutChar(Source);
            // 发送者QQ号
            buf.PutInt(user.QQ);
            // 接收者QQ号
            buf.PutInt(Receiver);
            // 文件传输会话密钥
            buf.Put(user.SessionKey);
            // 消息类型
            buf.PutUShort((ushort)MessageType);
            // 顺序号
            if (SessionId == 0)
                buf.PutChar(Sequence);
            else
                buf.PutUShort(SessionId);
            // 发送时间
            int time = (int)(Utils.Util.GetTimeMillis(DateTime.Now) / 1000);
            buf.PutInt(time);
            // 发送者头像
            char face = (char)user.ContactInfo.Head;
            buf.PutChar(face);
            // 字体信息，设成1
            buf.PutInt(1);
            // 暂时为如来神掌做的设置
            if (FakeIp)
                buf.PutInt(0);
            else
            {
                // 分片数
                buf.Put((byte)TotalFragments);
                // 分片序号
                buf.Put((byte)FragmentSequence);
                // 消息id
                buf.PutUShort(MessageId);
            }

            // 判断消息类型
            switch (MessageType)
            {
                case NormalIMType.TEXT:
                    InitTextContent(buf);
                    break;
                case NormalIMType.UDP_REQUEST:
                    InitSendFileContent(buf);
                    break;
                case NormalIMType.ACCEPT_UDP_REQUEST:
                    InitSendFileAcceptContent(buf);
                    break;
                case NormalIMType.REJECT_UDP_REQUEST:
                case NormalIMType.REJECT_TCP_REQUEST:
                    InitSendFileRejectContent(buf);
                    break;
                case NormalIMType.NOTIFY_IP:
                    InitNotifyFilePortUDP(buf);
                    break;
                case NormalIMType.REQUEST_CANCELED:
                    InitConnectionCanceled(buf);
                    break;
                case NormalIMType.ARE_YOU_BEHIND_FIREWALL:
                    InitPleaseConnectMe(buf);
                    break;
            }
        }

        /// <summary>初始化请求对方主动连接的包
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitPleaseConnectMe(ByteBuffer buf)
        {
            // 17 - 19. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
            buf.PutLong(0);
            buf.PutChar((char)0);
            buf.Put((byte)0);
            // 我们先尝试UDP方式
            buf.Put((byte)TransferType);
            buf.Put((byte)0x0);
            // 四个字节的发送者IP，这是外部IP
            buf.Put(user.IP);
            // 发送者端口
            buf.PutChar((char)user.Port);
            // 监听端口，含义未知，为连接服务器的端口，先随便写一个值
            buf.PutUShort(DirectPort);
            // 后面全0
            buf.PutInt(0);
            buf.PutChar((char)0);
        }

        /// <summary>
        /// 初始化取消发送文件包
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitConnectionCanceled(ByteBuffer buf)
        {
            // 17 - 19. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
            buf.PutLong(0);
            buf.PutChar((char)0);
            buf.Put((byte)0);
            // 传输类型
            buf.Put((byte)TransferType);
        }

        /// <summary>
        /// 初始化IP信息通知包
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitNotifyFilePortUDP(ByteBuffer buf)
        {
            // 17 - 19. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
            buf.PutLong(0);
            buf.PutChar((char)0);
            buf.Put((byte)0);
            // 我们先尝试UDP方式
            buf.Put((byte)TransferType);
            buf.Put((byte)0x0);
            // 四个字节的发送者IP，这是外部IP
            buf.Put(user.IP);
            // 发送者端口
            buf.PutChar((char)user.Port);
            // 监听端口，含义未知，为连接服务器的端口，先随便写一个值
            buf.PutUShort(DirectPort);
            // 真实IP和第二个端口
            buf.Put(LocalIp);
            buf.PutUShort(LocalPort);
        }

        /// <summary>初始化拒绝接收文件包的其余部分
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitSendFileRejectContent(ByteBuffer buf)
        {
            // 17 - 19. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
            buf.PutLong(0);
            buf.PutChar((char)0);
            buf.Put((byte)0);
            // 我们先尝试UDP方式
            buf.Put((byte)TransferType);
        }

        /// <summary>初始化同意接收文件包的其余部分
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitSendFileAcceptContent(ByteBuffer buf)
        {
            // 17 - 19. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
            buf.PutLong(0);
            buf.PutChar((char)0);
            buf.Put((byte)0);
            // 我们先尝试UDP方式
            buf.Put((byte)TransferType);
            buf.Put((byte)0x0);
            // 四个字节的发送者IP，这是外部IP
            buf.Put(user.IP);
            // 发送者端口
            buf.PutChar((char)user.Port);
            // 监听端口，含义未知，为连接服务器的端口，先随便写一个值
            buf.PutUShort(DirectPort);
            // 后面全0
            buf.PutInt(0);
            buf.PutChar((char)0);
        }

        /// <summary>初始化请求发送文件包的其余部分
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitSendFileContent(ByteBuffer buf)
        {
            // 17 - 19. 怀疑也和发送消息包相同，但是在这种情况中，这部分没有使用，为全0，一共11个0字节
            buf.PutLong(0);
            buf.PutChar((char)0);
            buf.Put((byte)0);
            // 我们先尝试UDP方式
            buf.Put((byte)TransferType);
            buf.Put((byte)0x0);
            if (FakeIp)
            {
                buf.PutInt(0);
                buf.PutChar((char)0);
            }
            else
            {
                // 四个字节的发送者IP，这是外部IP
                buf.Put(user.IP);
                // 发送者端口
                buf.PutChar((char)user.Port);
            }
            // 直接端口
            buf.PutUShort(DirectPort);
            buf.PutInt(0);
            buf.PutChar((char)0);
            buf.Put((byte)0x20);
            buf.Put(DELIMIT);
            buf.Put(Utils.Util.GetBytes(FileName));
            buf.Put(DELIMIT);
            buf.Put(Utils.Util.GetBytes(FileSize));
        }
        /// <summary> 初始化普通消息包的其余部分
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void InitTextContent(ByteBuffer buf)
        {
            // 消息方式，是发送的，还是自动回复的，1字节
            buf.Put((byte)ReplyType);
            // 写入消息正文字节数组
            if (Message != null)
                buf.Put(Message);
            // 最后一个分片时追加空格
            if (FragmentSequence == TotalFragments - 1)
                buf.Put((byte)0x20);
            // 消息尾部，字体修饰属性
            FontStyle.Write(buf);
        }
    }
}
