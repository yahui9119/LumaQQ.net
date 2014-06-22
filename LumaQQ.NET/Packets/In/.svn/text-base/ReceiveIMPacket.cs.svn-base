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

namespace LumaQQ.NET.Packets.In
{
    /// <summary>
    ///  * 别人发来的消息包，如果是普通消息，格式为
    /// *1. 头部
    /// *2. 发送者QQ号，4字节
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，是好友发的，还是陌生人发的，还是系统消息等等， 2字节
    /// *8. 发送者QQ版本，2字节
    /// *9. 发送者的QQ号，4字节
    /// *10. 接受者的QQ号，4字节
    /// *11. md5处理的发送方的uid和session key，16字节
    /// *12. 普通消息类型，比如是文本消息还是其他什么消息，2字节
    /// *13. 会话ID，2字节，如果是一个操作需要发送多个包才能完成，则这个id必须一致
    /// *14. 发送时间，4字节
    /// *15. 发送者头像，2字节
    /// *16. 是否有字体属性，4字节，有一般是0x00000001
    /// *17. 消息的分片数，1字节
    /// *18. 分片序号，1字节，从0开始
    /// *19. 消息id，2字节，同一条消息的不同分片id相同
    /// *20. 消息类型，这里的类型表示是正常回复还是自动回复之类的信息， 1字节
    /// *21. 消息正文，长度 = 剩余字节数 - 包尾字体属性长度
    /// *22. 字体属性，和SendIMPacket中的相同
    /// *23. 尾部
    /// *
    /// *如果是临时会话消息
    /// *2. 发送者QQ号，4字节
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，2字节，这里是0x001F
    /// *8. 发送者QQ号，4字节
    /// *9. 未知的4字节
    /// *10. 发送者昵称长度，1字节
    /// *11. 发送者昵称
    /// *12. Site名称长度，1字节
    /// *13. Site名称
    /// *14. 未知的1字节
    /// *15. 发送时间，4字节
    /// *16. 后面实际内容的长度，2字节
    /// *Note: 包的结尾有4个未知字节，所以16部分的长度是实际内容长度，不包括那4个字节
    /// *17. 消息内容，长度 = 16部分 - 字体属性长度
    /// *18. 字体属性，参见SendIMPacket
    /// *19. 未知的4字节
    /// *20. 尾部
    /// *
    /// *如果是群通知
    /// *1. 头部
    /// *2. 群的内部ID，4字节
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，是好友发的，还是陌生人发的，还是系统消息等等， 2字节，0x002C
    /// *8. 群的外部ID，4字节
    /// *9. 群类型，1字节
    /// *10. 操作码，1字节
    /// *11. 接受者QQ号，4字节
    /// *12. 接受者当前角色，1字节
    /// *13. 尾部
    /// *
    /// *如果是个性签名改变通知
    /// *1. 头部
    /// *2. 发送者QQ号，在这里是10000，4字节
    /// *3. 接受者QQ号，4字节
    /// *4. 服务器端包序号，4字节
    /// *5. 发送者IP，4字节
    /// *6. 发送者端口，2字节
    /// *7. 消息类型，2字节，在这里是0x0041
    /// *8. 个性签名改变的QQ号，4字节
    /// *9. 个性签名改变的时间，4字节
    /// *10. 新个性签名的字节长度，1字节
    /// *11. 新个性签名
    /// *12. 尾部
    /// *
    /// *如果是系统消息：
    /// *1-7. 与普通消息相同，只不过7是0x0030，表示是系统消息
    /// *8. 系统消息类型，1字节
    /// *9. 系统消息长度，1字节
    /// *10. 系统消息
    /// *
    /// *如果是来自绑定手机的手机短消息，格式为：
    /// *1. 头部
    /// *2. 发送者QQ号，4字节
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，是好友发的，还是陌生人发的，还是系统消息等等， 2字节，0x000B
    /// *8. 未知1字节，0x0
    /// *9. 发送者QQ号，4字节
    /// *10. 发送者头像，2字节
    /// *11. 发送者名称，最多13字节，不足后面补0
    /// *12. 未知的1字节，0x4D
    /// *13. 消息内容，160字节，如果不足，填0
    /// *注：在接收长消息时，13部分前两个字节为固定内容，似乎是用来标识同一消息的分片，但是
    /// *QQ本身却没有处理这个字段，而是把这个字段也显示出来了，似乎是个bug
    /// *14. 尾部
    /// *
    /// *如果是来自移动QQ用户的手机短消息，格式为：
    /// *1. 头部
    /// *2. 发送者QQ号，4字节
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，是好友发的，还是陌生人发的，还是系统消息等等， 2字节，0x0013
    /// *8. 未知1字节
    /// *9. 发送者QQ号，4字节
    /// *10. 发送者头像，2字节
    /// *11. 发送者名称，最多13字节，不足后面补0
    /// *12. 未知的1字节，0x4D
    /// *13. 短信发送时间，4字节
    /// *14. 未知1字节，0x03
    /// *15. 短信内容，160字节，不足填0
    /// *注：在接收长消息时，15部分前两个字节为固定内容，似乎是用来标识同一消息的分片，但是
    /// *QQ本身却没有处理这个字段，而是把这个字段也显示出来了，似乎是个bug
    /// *16. 尾部
    /// *
    /// *如果是来自移动QQ用户的手机短消息(使用的是手机号码)，格式为：
    /// *1. 头部
    /// *2. 发送者QQ号，4字节
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，是好友发的，还是陌生人发的，还是系统消息等等， 2字节，0x0014
    /// *8. 未知1字节
    /// *9. 移动QQ的手机号，18字节，不足补0
    /// *10. 未知的2字节
    /// *11. 短信发送时间，4字节
    /// *12. 未知1字节，0x03
    /// *13. 短信内容，160字节，不足填0
    /// *注：在接收长消息时，13部分前两个字节为固定内容，似乎是用来标识同一消息的分片，但是
    /// *QQ本身却没有处理这个字段，而是把这个字段也显示出来了，似乎是个bug
    /// *14. 尾部
    /// *
    /// *如果是来自普通手机的消息
    /// *1. 头部
    /// *2. 发送者QQ号，4字节，在这里是10000
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，是好友发的，还是陌生人发的，还是系统消息等等， 2字节，0x000C
    /// *8. 未知1字节，0x0
    /// *9. 发送者手机号码，20字节，不足填0
    /// *10. 短信内容，160字节，不足填0
    /// *注：在接收长消息时，15部分前两个字节为固定内容，似乎是用来标识同一消息的分片，但是
    /// *QQ本身却没有处理这个字段，而是把这个字段也显示出来了，似乎是个bug
    /// *11. 尾部
    /// *
    /// *如果是请求传送文件，格式为
    /// *1 - 18. 与普通消息格式相同，差别只有12部分，为0x0035，表示是请求传送文件
    /// *19.     未用部分，全0，15字节
    /// *20.     传输类型，1字节
    /// *21.     连接方式，1字节
    /// *22.     请求者外部ip，4字节
    /// *23.     请求者QQ端口，2字节，如果连接方式为0x3，则这个部分没有
    /// *24.     第一个监听端口，2字节
    /// *25.     请求者真实ip，4字节
    /// *26.     第二个监听端口，2字节
    /// *27.     空格符0x20
    /// *28.     分隔符0x1F
    /// *29.     文件名，不定长，不包含路径名
    /// *30.     分隔符0x1F
    /// *31.     文件字节数的字符串形式加“ 字节”
    /// *32.     尾部
    /// *
    /// *如果是接受传送文件的请求，格式为
    /// *1 - 18. 与普通消息格式相同，差别只有12部分，对于UDP请求，为0x0037
    /// *19 - 26 和0x0035时相同
    /// *27.     尾部 
    /// *
    /// *如果是通知文件传送的端口，格式为
    /// *1 - 18. 与普通消息格式相同，差别只有12部分，为0x003B，表示是通知文件传送端口
    /// *19 - 26 和0x0035时相同
    /// *27. 尾部
    /// 
    /// *未知类型的群消息
    /// *1-7. 与普通消息相同，只不过发送者QQ号相当于是群的内部ID,7部分为0x0020
    /// *8.   群外部ID，4字节
    /// *9.   群类型，1字节
    /// *10.  发送者QQ号，4字节
    /// *11.  未知的两字节，全0
    /// *12.  消息序号，2字节
    /// *13.  消息发送时间，4字节
    /// *14.  未知的4字节
    /// *15.  后面的数据的长度，2字节
    /// *16.  以0结尾的消息内容
    /// *17.  字体属性，和SendIMPacket中的相同
    /// *18.  尾部
    /// * 
    /// *如果是讨论组或者多人对话消息，讨论组和多人对话属于临时群，格式为
    /// *1 - 7 部分与普通消息相同，只不过7部分是0x002A
    /// *8.   父群内部ID，4字节
    /// *9.   群类型，1字节
    /// *10.  讨论组内部ID，4字节
    /// *11.  发送者QQ号，4字节
    /// *12.  未知的两字节，全0
    /// *13.  消息序号，2字节
    /// *14.  消息发送时间，4字节
    /// *15.  Version ID, 4字节，所谓version id，是我这里根据这个字段的意思乱编的。
    /// *     其作用主要是标识群信息的版本，比如一开始，群内有两个人，这个时候版本是0，然后我删
    /// *     除一个人，那么群的版本就要加1，于是就变成了1。然后我又加了2个人，于是版本再加2变成3，
    /// *     假如我再删一个人又加一个人，那么版本就要加2变成5了。为什么要加2呢，因为我做了两次操作，
    /// *     一次删，一次加，所以版本号加了2，你可能奇怪：为什么我不一次搞定，然后版本加1啊？因为
    /// *     QQ的协议就是这样，它的协议不能同时做删除和添加成员的操作，晕吧？所以我要做两次操作，
    /// *     也就是要发两个包才能又加人又删人，所以版本号加了2。客户端对每一个群都要保存这么一个version ID，
    /// *     一旦收到的消息大于我本地的version id，于是就发个包过去请求得到目前的成员列表。所以你
    /// *     会看到，QQ在发临时群消息的时候，成员如果变化了，会即时的反映出来，就是如此实现的了。
    /// *     这个id不光是在修改成员的时候才变，修改基本信息也照样变，用来表示这个群的信息修改过了。
    /// *16.  后面内容的长度
    /// *17.  Content Type, 2字节，0x0001表示纯文件，0x0002表示有自定义表情
    /// *18.  消息的分片数，1字节
    /// *19.  分片序号，1字节，从0开始
    /// *20.  消息id，2字节，同一条消息的不同分片id相同
    /// *21.  4字节，未知
    /// *22.  消息正文，长度 = 剩余字节数 - 包尾字体属性长度
    /// *23.  字体属性，和SendIMPacket中的相同
    /// *24.  尾部
    /// *
    /// *如果是示范群或会员创建的群发来的消息，这些群都是固定群，格式为
    /// *1 - 7 部分与普通消息相同，只不过7部分是0x002B
    /// *8.   群外部ID，4字节
    /// *9.   群类型，1字节
    /// *10.  发送者QQ号，4字节
    /// *11.  未知的两字节
    /// *12.  消息序号，2字节
    /// *13.  消息发送时间，4字节
    /// *14.  Version ID, 4字节
    /// *15.  后面内容的长度
    /// *16.  Content Type, 2字节，0x0001表示纯文件，0x0002表示有自定义表情
    /// *17.  消息的分片数，1字节
    /// *18.  分片序号，1字节，从0开始
    /// *19.  消息id，2字节，同一条消息的不同分片id相同
    /// *20.  4字节，未知
    /// *21.  消息正文，长度 = 剩余字节数 - 包尾字体属性长度
    /// *22.  字体属性，和SendIMPacket中的相同
    /// *23.  尾部
    /// *
    /// *如果是0x0041，可能是向服务器报告最后连接情况的包，格式为
    /// *1 - 27. 和0x003B时相同，差别只有命令不同，为0x0041
    /// *
    /// *如果是取消传送文件请求，格式为：
    /// *1 - 18. 与普通消息格式相同，差别只有12部分，为0x0049，表示是取消传送文件
    /// *19.     未用部分，全0，15字节
    /// *20.     固定字节0x65
    /// *21.     尾部
    /// *
    /// *如果是QQ直播消息：
    /// *1. 头部
    /// *2. 发送者QQ号，4字节，一般是10000
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，2字节，0x0018
    /// *8. 直播消息类型，2字节
    /// *9. 后面的内容长度，2字节，exclusive
    /// *如果8部分是0x0100，表示普通直播消息
    /// *10. 直播标题长度，1字节
    /// *11. 标题
    /// *12. 直播描述长度，1字节
    /// *13. 直播描述
    /// *14. 直播URL长度，1字节
    /// *15. 直播URL
    /// *16. 如果10-15部分加起来的长度没有超过9部分的值，则后面都填0
    /// *17. 尾部
    /// *如果8部分是0x0400，表示网络硬盘通知
    /// *10. 标题
    /// *11. 分隔符，1字节，0x02
    /// *12. 描述
    /// *13. 分隔符，1字节，0x02
    /// *14. 好友的QQ号的字符串形式
    /// *15. 尾部
    /// *
    /// *如果是会员登录提示
    /// *1. 头部
    /// *2. 发送者QQ号，4字节，一般是10000
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，2字节，0x0012
    /// *8. 未知1字节
    /// *9. 尾部
    /// *
    /// *如果是自定义头像变化通知
    /// *1. 头部
    /// *2. 发送者QQ号，4字节，一般是10000
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，2字节，0x0049
    /// *8. 头像变化的好友个数，1字节
    /// *9. 好友QQ号，4字节
    /// *10. 自定义头像最后改变的时间戳，4字节，为从1970-1-1到现在的秒数
    /// *11. 自定义头像的MD5值，16字节
    /// *12. 如果有更多好友，重复10-11部分
    /// *13. 尾部
    /// *
    /// *如果是好友属性改变通知
    /// *1. 头部
    /// *2. 发送者QQ号，4字节，一般是10000
    /// *3. 接收者QQ号，4字节
    /// *4. 包序号（并非我们发送时候的序号，因为这个是4字节，可能是服务器端得总序号）
    /// *5. 发送者IP，如果是服务器转发的，那么ip就是服务器ip， 4字节
    /// *6. 发送者端口，如果是服务器转发的，那么就是服务器的端口，2字节
    /// *7. 消息类型，2字节，0x001E
    /// *8. 好友QQ号，4字节
    /// *9. 好友属性，4字节
    /// *10. 未知的16字节
    /// *11. 尾部
    /// 	<remark>abu 2008-02-22 </remark>
    /// </summary>
    public class ReceiveIMPacket : BasicInPacket
    {
        // 用于发送确认
        public byte[] Reply { get; set; }
        // 为true时表示收到的消息是空的
        public bool Empty { get; set; }
        // 整个包的头
        public new ReceiveIMHeader Header { get; set; }

        //仅用于普通消息时
        public NormalIMHeader NormalHeader { get; set; }
        public NormalIM NormalIM { get; set; }

        // 仅用于系统通知时
        public SystemIMType SystemMessageType { get; set; }
        public string SysMessage { get; set; }

        // 仅用于文件传输时
        public FileInfo FileInfo { get; set; }
        public FileTransferArgs FileArgs { get; set; }
        public TransferType TransferType { get; set; }

        // 仅用于群普通消息时
        public ClusterIM ClusterIM { get; set; }

        // 仅用于其他类型群消息
        public int ExternalId { get; set; }
        public ClusterType ClusterType { get; set; }
        public int Sender { get; set; }
        public string Message { get; set; }

        // 用于群通知时
        public byte Role { get; set; }
        public byte OpCode { get; set; }
        public int MemberQQ { get; set; }

        // 用户个性签名通知
        public int SignatureOwner { get; set; }
        public int ModifiedTime { get; set; }
        public string Signature { get; set; }

        // 用于手机短信消息
        public SMS SMS { get; set; }
        // 临时会话消息
        public TempSessionIM TempSessionIM { get; set; }

        // QQ直播消息
        public QQLive QQLive { get; set; }
        // 自定义头像变化通知
        public List<CustomHead> HeadChanges { get; set; }
        // 用户属性变化
        public UserPropertyChange PropertyChange { get; set; }

        public ReceiveIMPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }

        /// <summary>
        /// 包的描述性名称
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <returns></returns>
        public override string GetPacketName()
        {
            return "Receive IM Packet";
        }
        /// <summary>
        /// 解析包体，从buf的开头位置解析起
        /// <remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void ParseBody(ByteBuffer buf)
        {
            Empty = false;
            // 检查消息长度，至少要有16字节，因为我们需要前16字节做为确认发回
            if (buf.Remaining() < 16)
            {
                throw new PacketParseException("收到的消息太短，抛弃该消息");
            }
            // 得到前16个字节用作回复
            Reply = buf.GetByteArray(16);
            // 读取消息头
            buf.Position = 0;
            Header = new ReceiveIMHeader();
            Header.Read(buf);
            // 检查输入流可用字节
            if (!buf.HasRemaining())
            {
                Empty = true;
                return;
            }
            // 判断消息类型
            int len = 0;
            switch (Header.Type)
            {
                case RecvSource.FRIEND:
                case RecvSource.STRANGER:
                    /* 是从好友或者陌生人处发来的消息 */
                    ParseNormalIM(buf);
                    break;

                case RecvSource.TEMP_SESSION:
                    TempSessionIM = new TempSessionIM();
                    TempSessionIM.Read(buf);
                    break;
                case RecvSource.SYS_MESSAGE:
                    /* 是系统消息 */
                    ParseSystemMessage(buf);
                    break;
                case RecvSource.CLUSTER:
                    /* 群消息 */
                    ParseClusterIM(buf);
                    break;
                case RecvSource.TEMP_CLUSTER:
                    /* 临时群消息 */
                    ParseTempClusterIM(buf);
                    break;
                case RecvSource.UNKNOWN_CLUSTER:
                    ParseUnknownClusterIM(buf);
                    break;
                case RecvSource.BIND_USER:
                    SMS = new SMS();
                    SMS.ReadBindUserSMS(buf);
                    break;
                case RecvSource.MOBILE_QQ:
                    SMS = new SMS();
                    SMS.ReadMobileQQSMS(buf);
                    break;
                case RecvSource.MOBILE_QQ_2 :
                    SMS = new SMS();
                    SMS.ReadMobileQQ2SMS(buf);
                    break;
                case RecvSource.MOBILE:
                    SMS = new SMS();
                    SMS.ReadMobileSMS(buf);
                    break;
                case RecvSource.CREATE_CLUSTER:
                case RecvSource.ADDED_TO_CLUSTER:
                case RecvSource.DELETED_FROM_CLUSTER:
                    ExternalId = buf.GetInt();
                    ClusterType = (ClusterType)buf.Get();
                    Sender = buf.GetInt();
                    break;
                case RecvSource.APPROVE_JOIN_CLUSTER:
                case RecvSource.REJECT_JOIN_CLUSTER:
                case RecvSource.REQUEST_JOIN_CLUSTER:
                    ExternalId = buf.GetInt();
                    ClusterType = (ClusterType)buf.Get();
                    Sender = buf.GetInt();
                    len = buf.Get() & 0xFF;
                    byte[] b = buf.GetByteArray(len);
                    Message = Utils.Util.GetString(b);
                    break;
                case RecvSource.CLUSTER_NOTIFICATION:
                    ExternalId = buf.GetInt();
                    ClusterType = (ClusterType)buf.Get();
                    OpCode = buf.Get();
                    MemberQQ = buf.GetInt();
                    Role = buf.Get();
                    Sender = ExternalId;
                    break;
                case RecvSource.SIGNATURE_CHANGE:
                    SignatureOwner = buf.GetInt();
                    ModifiedTime = buf.GetInt();
                    len = buf.Get() & 0xFF;
                    Signature = Utils.Util.GetString(buf, len);
                    break;
                case RecvSource.QQLIVE:
                    QQLive = new QQLive();
                    QQLive.Read(buf);
                    break;
                case RecvSource.MEMBER_LOGIN_HINT:
                    buf.Get();
                    break;
                case RecvSource.CUSTOM_HEAD_CHANGE:
                    int count = buf.Get() & 0xFF;
                    if (count > 0)
                    {
                        HeadChanges = new List<CustomHead>();
                        while (buf.HasRemaining())
                        {
                            CustomHead change = new CustomHead();
                            change.Read(buf);
                            HeadChanges.Add(change);
                        }
                    }
                    break;
                case RecvSource.PROPERTY_CHANGE :
                    PropertyChange = new UserPropertyChange();
                    PropertyChange.Read(buf);
                    break;
                default:
                    // 其他类型的消息我们现在没有办法处理，忽略
                    break;
            }
        }
        private void ParseUnknownClusterIM(ByteBuffer buf)
        {
            ClusterIM = new ClusterIM(RecvSource.UNKNOWN_CLUSTER);
            ClusterIM.Read(buf);
        }
        /// <summary>解析临时组消息
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseTempClusterIM(ByteBuffer buf)
        {
            ClusterIM = new ClusterIM(RecvSource.TEMP_CLUSTER);
            ClusterIM.Read(buf);
        }
        /// <summary>解析群普通消息
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseClusterIM(ByteBuffer buf)
        {
            ClusterIM = new ClusterIM(RecvSource.CLUSTER);
            ClusterIM.Read(buf);
        }
        /// <summary>
        /// 解析系统消息
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseSystemMessage(ByteBuffer buf)
        {
            // 系统消息类型
            SystemMessageType = (SystemIMType)buf.Get();
            // 系统消息长度
            int len = buf.Get() & 0xFF;
            // 系统消息
            SysMessage = Utils.Util.GetString(buf, len);
        }
        /// <summary>解析普通消息
        /// 	<remark>abu 2008-02-26 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseNormalIM(ByteBuffer buf)
        {
            // 读入普通消息头
            NormalHeader = new NormalIMHeader();
            NormalHeader.Read(buf);
            // 判断普通消息类型
            if (NormalHeader.Type == NormalIMType.TEXT)
            {
                NormalIM = new NormalIM();
                NormalIM.Read(buf);
            }
            else
            {
                if (NormalHeader.Type == NormalIMType.UDP_REQUEST || NormalHeader.Type == NormalIMType.TCP_REQUEST)
                {
                    FileArgs = new FileTransferArgs();
                    FileArgs.Read(buf);
                    FileInfo = new FileInfo();
                    FileInfo.Read(buf);
                }
                else
                {
                    if (NormalHeader.Type == NormalIMType.ACCEPT_UDP_REQUEST || NormalHeader.Type == NormalIMType.NOTIFY_IP)
                    {
                        FileArgs = new FileTransferArgs();
                        FileArgs.Read(buf);
                    }
                }

            }

        }

        public IMFrom GetMessageCategory()
        {
            switch (Header.Type)
            {
                case RecvSource.SYS_MESSAGE:
                    return IMFrom.SYS;
                case RecvSource.FRIEND:
                case RecvSource.STRANGER:
                    return IMFrom.USER;
                case RecvSource.TEMP_SESSION:
                    return IMFrom.TEMP_SESSION;
                case RecvSource.CLUSTER:
                case RecvSource.TEMP_CLUSTER:
                case RecvSource.UNKNOWN_CLUSTER:
                    return IMFrom.CLUSTER;
                case RecvSource.BIND_USER:
                case RecvSource.MOBILE:
                case RecvSource.MOBILE_QQ:
                case RecvSource.MOBILE_QQ_2:
                    return IMFrom.SMS;
                default:
                    return IMFrom.SYS;
            }
        }
    }
}
