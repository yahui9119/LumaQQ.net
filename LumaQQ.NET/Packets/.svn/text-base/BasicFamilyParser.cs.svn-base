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

using LumaQQ.NET.Packets;
using LumaQQ.NET.Packets.In;
namespace LumaQQ.NET.Packets
{
    /// <summary> 基本协议族解析器   未完，等所有的协议包定义完后再来补充2008-02-19
    /// 	<remark>abu 2008-02-20 </remark>
    /// </summary>
    public class BasicFamilyParser : IParser
    {
        private int offset;
        private int length;
        private PacketHistory history;
        public BasicFamilyParser()
        {
            history = new PacketHistory();
        }
        #region IParser Members

        public bool Accept(ByteBuffer buf)
        {
            //保存偏移
            offset = buf.Position;
            int bufferLength = buf.Remaining();
            if (bufferLength <= 0)
                return false;
            bool accept = CheckTcp(buf);
            if (!accept)
                accept = CheckUdp(buf);
            return accept;
        }

        public int GetLength(ByteBuffer buf)
        {
            return length;
        }

        public InPacket ParseIncoming(ByteBuffer buf, int len, QQUser user)
        {
            try
            {
                switch (GetCommand(buf, user))
                {
                    case QQCommand.Request_Login_Token:
                        return new RequestLoginTokenReplyPacket(buf, len, user);
                    case QQCommand.Keep_Alive:
                        return new KeepAliveReplyPacket(buf, len, user);
                    case QQCommand.Modify_Info:
                        return new ModifyInfoReplyPacket(buf, len, user);
                    case QQCommand.Add_Friend_Ex:
                        return new AddFriendExReplyPacket(buf, len, user);
                    case QQCommand.Search_User:
                        return new SearchUserReplyPacket(buf, len, user);
                    case QQCommand.Delete_Friend:
                        return new DeleteFriendReplyPacket(buf, len, user);
                    case QQCommand.Remove_Self:
                        return new RemoveSelfReplyPacket(buf, len, user);
                    case QQCommand.Add_Friend_Auth:
                        return new AddFriendAuthResponseReplyPacket(buf, len, user);
                    case QQCommand.Get_UserInfo:
                        return new GetUserInfoReplyPacket(buf, len, user);
                    case QQCommand.Change_Status:
                        return new ChangeStatusReplyPacket(buf, len, user);
                    case QQCommand.Send_IM:
                        return new SendIMReplyPacket(buf, len, user);
                    case QQCommand.Recv_IM:
                        return new ReceiveIMPacket(buf, len, user);
                    case QQCommand.Login:
                        return new LoginReplyPacket(buf, len, user);
                    case QQCommand.Get_Friend_List:
                        return new GetFriendListReplyPacket(buf, len, user);
                    case QQCommand.Get_Online_OP:
                        return new GetOnlineOpReplyPacket(buf, len, user);
                    case QQCommand.Recv_Msg_Sys:
                        return new SystemNotificationPacket(buf, len, user);
                    case QQCommand.Recv_Msg_Friend_Change_Status:
                        return new FriendChangeStatusPacket(buf, len, user);
                    case QQCommand.Upload_Group_Friend:
                        return new UploadGroupFriendReplyPacket(buf, len, user);
                    case QQCommand.Download_Group_Friend:
                        return new DownloadGroupFriendReplyPacket(buf, len, user);
                    case QQCommand.Group_Data_OP:
                        return new GroupDataOpReplyPacket(buf, len, user);
                    case QQCommand.Friend_Data_OP:
                        return new FriendDataOpReplyPacket(buf, len, user);
                    case QQCommand.Cluster_Cmd:
                        return new ClusterCommandReplyPacket(buf, len, user);
                    case QQCommand.Request_Key:
                        return new RequestKeyReplyPacket(buf, len, user);
                    case QQCommand.Advanced_Search:
                        return new AdvancedSearchUserReplyPacket(buf, len, user);
                    case QQCommand.Cluster_Data_OP:
                        return new GetTempClusterOnlineMemberReplyPacket(buf, len, user);
                    case QQCommand.Authorize:
                        return new AuthorizeReplyPacket(buf, len, user);
                    case QQCommand.Signature_OP:
                        return new SignatureOpReplyPacket(buf, len, user);
                    case QQCommand.Weather_OP:
                        return new WeatherOpReplyPacket(buf, len, user);
                    case QQCommand.User_Property_OP:
                        return new UserPropertyOpReplyPacket(buf, len, user);
                    case QQCommand.Friend_Level_OP:
                        return new FriendLevelOpReplyPacket(buf, len, user);
                    case QQCommand.Send_SMS:
                        return new SendSMSReplyPacket(buf, len, user);
                    case QQCommand.Temp_Session_OP:
                        return new TempSessionOpReplyPacket(buf, len, user);
                    case QQCommand.Privacy_Data_OP:
                        return new PrivacyDataOpReplyPacket(buf, len, user);
                    default:
                        return new UnknownInPacket(buf, len, user);

                }
            }
            catch (Exception e)
            {
                return new ErrorPacket(ErrorPacketType.RUNTIME_ERROR, user, e);
            }
            //// 如果解析失败，返回null
            //buf.Position = offset;
            //return new UnknownInPacket(buf, len, user);
        }

        public OutPacket ParseOutcoming(ByteBuffer buf, int length, QQUser user)
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicate(InPacket packet)
        {
            return history.Check(packet, true);
        }

        public bool IsDuplicatedNeedReply(InPacket packet)
        {
            return packet.Command == QQCommand.Recv_IM;
        }

        public int Relocate(ByteBuffer buf)
        {
            int offset = buf.Position;
            if (buf.Remaining() < 2)
                return offset;
            int len = buf.GetUShort(offset);
            if (len <= 0 || offset + len > buf.Length)
                return offset;
            else
                return offset + len;
        }

        public PacketHistory GetHistory()
        {
            return history;
        }

        #endregion

        /// <summary>
        /// 得到包的命令和序号  
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private QQCommand GetCommand(ByteBuffer buf, QQUser user)
        {
            if (!user.IsUdp)
            {
                return (QQCommand)buf.GetUShort(offset + 5);
            }
            else
            {
                return (QQCommand)buf.GetUShort(offset + 3);
            }
        }
        /// <summary>检查一个包是否是tcp包
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// <returns>true表示是</returns>
        private bool CheckTcp(ByteBuffer buf)
        {
            //buffer length不大于2则连个长度字段都没有
            int bufferLength = buf.Length - buf.Position;
            if (bufferLength < 2) return false;
            // 如果可读内容小于包长，则这个包还没收完
            length = buf.GetChar(offset);
            if (length <= 0 || length > bufferLength)
                return false;
            // 再检查包头包尾
            if (buf.Get(offset + 2) == QQGlobal.QQ_HEADER_BASIC_FAMILY)
                if (buf.Get(offset + length - 1) == QQGlobal.QQ_TAIL_BASIC_FAMILY)
                    return true;
            return false;
        }
        private bool CheckUdp(ByteBuffer buf)
        {
            if (buf.Get(offset) == QQGlobal.QQ_HEADER_BASIC_FAMILY)
            {
                //首先检查是否UDP方式
                length = buf.Length - buf.Position;
                if (buf.Get(offset + length - 1) == QQGlobal.QQ_TAIL_BASIC_FAMILY)
                    return true;
            }
            return false;
        }
    }
}
