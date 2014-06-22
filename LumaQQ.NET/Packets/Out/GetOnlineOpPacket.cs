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
    ///  * 获取在线好友列表的请求包，格式为
    /// * 1. 头部
    /// * 2. 1个字节，只有值为0x02或者0x03时服务器才有反应，不然都是返回0xFF
    /// *    经过初步的试验，发现3得到的好友都是一些系统服务，号码比如72000001到72000013，
    /// *    就是那些移动QQ，会员服务之类的；而2是用来得到好友的
    /// * 3. 起始位置，4字节。这个起始位置的含义与得到好友列表中的字段完全不同。估计是两拨人
    /// *    设计的，-_-!...
    /// *    这个起始位置需要有回复包得到，我们已经知道，在线好友的回复包一次最多返回30个好友，
    /// *    那么如果你的在线好友超过30个，就需要计算这个值。第一个请求包，这个字段肯定是0，后面
    /// *    的请求包和前一个回复包就是相关的了。具体的规则是这样的，在前一个回复包中的30个好友里面，
    /// *    找到QQ号最大的那个，然后把他的QQ号加1，就是下一个请求包的起始位置了！
    /// * 6. 尾部
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class GetOnlineOpPacket : BasicOutPacket
    {
        public int StartPosition { get; set; }
        public GetOnlineSubCmd SubCommand { get; set; }
        public GetOnlineOpPacket(QQUser user)
            : base(QQCommand.Get_Online_OP, true, user)
        {
            StartPosition = QQGlobal.QQ_POSITION_ONLINE_LIST_START;
            SubCommand = GetOnlineSubCmd.GET_ONLINE_FRIEND;
        }
        public GetOnlineOpPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Get Friend Online Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            buf.Put((byte)SubCommand);
            buf.PutInt(StartPosition); 
        }
    }
}
