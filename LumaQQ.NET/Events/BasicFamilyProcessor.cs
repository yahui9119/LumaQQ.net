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

using LumaQQ.NET.Net;
using LumaQQ.NET.Packets;
using LumaQQ.NET.Packets.In;
using LumaQQ.NET.Packets.Out;

namespace LumaQQ.NET.Events
{
    public class BasicFamilyProcessor : IPacketListener
    {
        QQClient client;
        // QQ用户，我们将在收到登陆包后设置user不为null，所以如果user为null意味着尚未登陆
        QQUser user;
        public BasicFamilyProcessor(QQClient client)
        {
            this.client = client;
        }
        #region IPacketListener Members

        public void PacketArrived(LumaQQ.NET.Packets.InPacket inPacket)
        {
            // BasicInPacket packet = (BasicInPacket)inPacket;
            if (inPacket is UnknownInPacket)
            {
                client.PacketManager.OnReceivedUnknownPacket(new QQEventArgs<UnknownInPacket, OutPacket>(client, (UnknownInPacket)inPacket, null));
                return;
            }
            if (inPacket is ErrorPacket)
            {
                ErrorPacket error = (ErrorPacket)inPacket;
                if (error.ErrorType == ErrorPacketType.ERROR_TIMEOUT)
                {
                    client.PacketManager.OnSendPacketTimeOut(new QQEventArgs<InPacket, OutPacket>(client, null, error.TimeOutPacket));
                }
                else
                {
                    client.OnError(new QQEventArgs<ErrorPacket, OutPacket>(client, (ErrorPacket)inPacket, null));
                }
                return;
            }
            // 检查目前是否已经登录
            if (user == null || !user.IsLoggedIn)
            {
                //按理说应该把在登陆之前时收到的包缓存起来，但是在实际中观察发现，登录之前收到的
                //东西基本没用，所以在这里不做任何事情，简单的抛弃这个包
                if (!IsPreloginPacket(inPacket))
                {
                    return;
                }
            }
            // 现在开始判断包的类型，作出相应的处理
            ConnectionPolicy policy = client.ConnectionManager.GetConnection(inPacket.PortName).Policy;
            // 根据输入包，检索对应的输出包
            OutPacket outPacket = policy.RetrieveSent(inPacket);
            // 这里检查是不是服务器发回的确认包
            // 为什么要检查这个呢，因为有些包是服务器主动发出的，对于这些包我们是不需要
            // 从超时发送队列中移除的，如果是服务器的确认包，那么我们就需要把超时发送队列
            // 中包移除
            switch (inPacket.Command)
            {
                // 这三种包是服务器先发出的，我们要确认
                case QQCommand.Recv_IM:
                case QQCommand.Recv_Msg_Sys:
                case QQCommand.Recv_Msg_Friend_Change_Status:
                    break;
                // 其他情况我们删除超时队列中的包
                default:
                    client.PacketManager.RemoveResendPacket(inPacket);
                    client.PacketManager.OnSendPacketSuccessed(new QQEventArgs<InPacket, OutPacket>(client, inPacket, outPacket));
                    break;
            }
            switch (inPacket.Command)
            {
                case QQCommand.Request_Login_Token:
                    this.user = client.QQUser;
                    ProcessRequestLoginTokenReply((RequestLoginTokenReplyPacket)inPacket, outPacket as RequestLoginTokenPacket);
                    break;
                case QQCommand.Login:
                    ProcessLoginReply((LoginReplyPacket)inPacket, outPacket as LoginPacket);
                    break;
                case QQCommand.Keep_Alive:
                    ProcessKeepAliveReply((KeepAliveReplyPacket)inPacket, outPacket as KeepAlivePacket);
                    break;
                case QQCommand.Recv_IM:
                    ProcessReceiveIM((ReceiveIMPacket)inPacket);
                    break;
                case QQCommand.Get_Friend_List:
                    ProcessGetFriendListReply((GetFriendListReplyPacket)inPacket, outPacket as GetFriendListPacket);
                    break;
                case QQCommand.Get_Online_OP:
                    ProcessGetFriendOnlineReply((GetOnlineOpReplyPacket)inPacket, outPacket as GetOnlineOpPacket);
                    break;
                case QQCommand.Get_UserInfo:
                    ProcessGetUserInfoReply((GetUserInfoReplyPacket)inPacket, outPacket as GetUserInfoPacket);
                    break;
                case QQCommand.Change_Status:
                    ProcessChangeStatusReply((ChangeStatusReplyPacket)inPacket, outPacket as ChangeStatusPacket);
                    break;
                case QQCommand.Recv_Msg_Friend_Change_Status:
                    ProcessFriendChangeStatus((FriendChangeStatusPacket)inPacket);
                    break;
                case QQCommand.Recv_Msg_Sys:
                    ProcessSystemNotification((SystemNotificationPacket)inPacket);
                    break;
                case QQCommand.Add_Friend_Ex:
                    ProcessAddFriendExReply((AddFriendExReplyPacket)inPacket, outPacket as AddFriendExPacket);
                    break;
                case QQCommand.Add_Friend_Auth:
                    ProcessAddFriendAuthReply((AddFriendAuthResponseReplyPacket)inPacket, outPacket as AddFriendAuthResponsePacket);
                    break;
                case QQCommand.Remove_Self:
                    ProcessRemoveSelfReply((RemoveSelfReplyPacket)inPacket, outPacket as RemoveSelfPacket);
                    break;
                case QQCommand.Delete_Friend:
                    ProcessDeleteFriendReply((DeleteFriendReplyPacket)inPacket, outPacket as DeleteFriendPacket);
                    break;
                case QQCommand.Authorize:
                    ProcessAuthorizeReply((AuthorizeReplyPacket)inPacket, outPacket as AuthorizePacket);
                    break;
                case QQCommand.Upload_Group_Friend:
                    ProcessUploadGroupFriendReply((UploadGroupFriendReplyPacket)inPacket, (UploadGroupFriendPacket)outPacket);
                    break;
                case QQCommand.Modify_Info:
                    ProcessModifyInfoReply((ModifyInfoReplyPacket)inPacket, (ModifyInfoPacket)outPacket);
                    break;
                case QQCommand.Signature_OP:
                    ProcessSignatureOpReply((SignatureOpReplyPacket)inPacket, (SignatureOpPacket)outPacket);
                    break;
                case QQCommand.Privacy_Data_OP:
                    ProcessPrivacyDataOpReply((PrivacyDataOpReplyPacket)inPacket, (PrivacyDataOpPacket)outPacket);
                    break;
                case QQCommand.Friend_Data_OP:
                    ProcessFriendDataOpReply((FriendDataOpReplyPacket)inPacket, (FriendDataOpPacket)outPacket);
                    break;
                case QQCommand.Friend_Level_OP:
                    ProcessFriendLevelOpReply((FriendLevelOpReplyPacket)inPacket, (FriendLevelOpPacket)outPacket);
                    break;
                case QQCommand.User_Property_OP:
                    PocessUserPropertyOpReply((UserPropertyOpReplyPacket)inPacket, (UserPropertyOpPacket)outPacket);
                    break;
                case QQCommand.Download_Group_Friend:
                    ProcessDownloadGroupFriendReply((DownloadGroupFriendReplyPacket)inPacket, (DownloadGroupFriendPacket)outPacket);
                    break;
                case QQCommand.Group_Data_OP:
                    ProcessGroupNameOpReply((GroupDataOpReplyPacket)inPacket, (GroupDataOpPacket)outPacket);
                    break;
                case QQCommand.Search_User:
                    ProcessSearchUserReply((SearchUserReplyPacket)inPacket, (SearchUserPacket)outPacket);
                    break;
                case QQCommand.Weather_OP:
                    ProcessWeatherOpReply((WeatherOpReplyPacket)inPacket, (WeatherOpPacket)outPacket);
                    break;
            }
        }

        public bool Accept(LumaQQ.NET.Packets.InPacket inPacket)
        {
            return inPacket.GetFamily() == ProtocolFamily.Basic;
        }
        #endregion
        /// <summary>判断包是否时登录前可以出现的包，有些包是在登录前可以处理的，如果不是，应该缓存起来等
        /// 到登录成功后再处理，不过但是在实际中观察发现，登录之前收到的东西基本没用，可以不管
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <returns></returns>
        private bool IsPreloginPacket(InPacket inPacket)
        {
            switch (inPacket.Command)
            {
                case QQCommand.Request_Login_Token:
                case QQCommand.Login:
                    return true;
                case QQCommand.Unknown:
                    return inPacket is ErrorPacket;
                default:
                    return false;
            }
        }

        /// <summary>处理请求登录令牌的回复包
        /// 	<remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="basicInPacket">The basic in packet.</param>
        private void ProcessRequestLoginTokenReply(RequestLoginTokenReplyPacket inPacket, RequestLoginTokenPacket outPacket)
        {
            QQEventArgs<RequestLoginTokenReplyPacket, RequestLoginTokenPacket> e = new QQEventArgs<RequestLoginTokenReplyPacket, RequestLoginTokenPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                user.LoginToken = inPacket.LoginToken;
                client.LoginManager.OnRequestLoginTokenSuccessed(e);
            }
            else
            {
                client.LoginManager.OnRequestLoginTokenFailed(e);
            }
        }

        /// <summary>
        /// 处理登陆应答
        /// <remark>abu 2008-03-08 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessLoginReply(LoginReplyPacket inPacket, LoginPacket outPacket)
        {
            QQEventArgs<LoginReplyPacket, LoginPacket> e = new QQEventArgs<LoginReplyPacket, LoginPacket>(client, inPacket, outPacket);
            switch (inPacket.ReplyCode)
            {
                case ReplyCode.OK:
                    // 登陆成功的话我们就设置用户的一些信息，然后触发事件
                    user.SessionKey = inPacket.SessionKey;
                    user.IP = inPacket.IP;
                    user.ServerIp = inPacket.ServerIP;
                    user.LastLoginIp = inPacket.LastLoginIP;
                    user.Port = inPacket.Port;
                    user.ServerPort = inPacket.ServerPort;
                    user.LoginTime = inPacket.LoginTime;
                    user.LastLoginTime = inPacket.LastLoginTime;
                    user.ClientKey = inPacket.ClientKey;
                    user.AuthToken = inPacket.AuthToken;
                    // 得到文件传输密钥                  
                    client.LoginManager.OnLoginSuccessed(e);
                    break;
                case ReplyCode.LOGIN_FAIL:
                    //登录失败
                    client.LoginManager.OnLoginFailed(e);
                    break;
                case ReplyCode.LOGIN_REDIRECT:
                    if (Utils.Util.IsIPZero(inPacket.RedirectIP))
                    {
                        client.LoginManager.OnLoginRedirectNull(e);
                    }
                    else
                    {
                        client.LoginManager.OnLoginRedirect(e);
                    }
                    break;
                default:
                    client.LoginManager.OnLoginUnknownError(e);
                    break;
            }
        }
        /// <summary>处理保持登录应答
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessKeepAliveReply(KeepAliveReplyPacket inPacket, KeepAlivePacket outPacket)
        {
            client.ConnectionManager.OnReceivedKeepAlive(new QQEventArgs<KeepAliveReplyPacket, KeepAlivePacket>(client, inPacket, outPacket));
        }

        /// <summary>
        /// 处理接收信息
        /// 	<remark>abu 2008-03-10 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessReceiveIM(ReceiveIMPacket packet)
        {
            QQEventArgs<ReceiveIMPacket, OutPacket> e = new QQEventArgs<ReceiveIMPacket, OutPacket>(client, packet, null);
            //先发送接收确认
            client.MessageManager.SendReceiveReplyPacket(packet);
            if (packet.IsDuplicated)
            {
                client.MessageManager.OnReceiveDuplicatedIM(e);
                return;
            }
            switch (packet.Header.Type)
            {
                case RecvSource.FRIEND:
                case RecvSource.STRANGER:
                    switch (packet.NormalHeader.Type)
                    {
                        case NormalIMType.TEXT:
                            client.MessageManager.OnReceiveNormalIM(e);
                            break;
                        case NormalIMType.TCP_REQUEST:
                            break;
                        case NormalIMType.ACCEPT_TCP_REQUEST:
                            break;
                        case NormalIMType.REJECT_TCP_REQUEST:
                            break;
                        case NormalIMType.UDP_REQUEST:
                            break;
                        case NormalIMType.ACCEPT_UDP_REQUEST:
                            break;
                        case NormalIMType.REJECT_UDP_REQUEST:
                            break;
                        case NormalIMType.NOTIFY_IP:
                            break;
                        case NormalIMType.ARE_YOU_BEHIND_FIREWALL:
                            break;
                        case NormalIMType.ARE_YOU_BEHIND_PROXY:
                            break;
                        case NormalIMType.YES_I_AM_BEHIND_PROXY:
                            break;
                        case NormalIMType.NOTIFY_FILE_AGENT_INFO:
                            break;
                        case NormalIMType.REQUEST_CANCELED:
                            break;
                        default:
                            client.MessageManager.OnReceiveUnknownIM(e);
                            break;
                    }
                    break;
                case RecvSource.BIND_USER:
                    break;
                case RecvSource.MOBILE:
                    break;
                case RecvSource.MEMBER_LOGIN_HINT:
                    break;
                case RecvSource.MOBILE_QQ:
                    break;
                case RecvSource.MOBILE_QQ_2:
                    break;
                case RecvSource.QQLIVE:
                    break;
                case RecvSource.PROPERTY_CHANGE:
                    client.FriendManager.OnUserPropertyChanged(e);
                    break;
                case RecvSource.TEMP_SESSION:
                    client.MessageManager.OnReceiveTempSessionIM(e);
                    break;
                case RecvSource.UNKNOWN_CLUSTER:
                    break;
                case RecvSource.ADDED_TO_CLUSTER:
                    break;
                case RecvSource.DELETED_FROM_CLUSTER:
                    break;
                case RecvSource.REQUEST_JOIN_CLUSTER:
                    break;
                case RecvSource.APPROVE_JOIN_CLUSTER:
                    break;
                case RecvSource.REJECT_JOIN_CLUSTER:
                    break;
                case RecvSource.CREATE_CLUSTER:
                    break;
                case RecvSource.TEMP_CLUSTER:
                    break;
                case RecvSource.CLUSTER:
                    break;
                case RecvSource.CLUSTER_NOTIFICATION:
                    break;
                case RecvSource.SYS_MESSAGE:
                    if (packet.SystemMessageType == SystemIMType.QQ_RECV_IM_KICK_OUT)
                    {
                        client.MessageManager.OnReceiveKickOut(e);
                    }
                    else
                    {
                        client.MessageManager.OnReceiveSysMessage(e);
                    }
                    break;
                case RecvSource.SIGNATURE_CHANGE:
                    client.FriendManager.OnSignatureChanged(e);
                    break;
                case RecvSource.CUSTOM_HEAD_CHANGE:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理得到好友列表应答
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessGetFriendListReply(GetFriendListReplyPacket inPacket, GetFriendListPacket outPacket)
        {
            client.FriendManager.OnGetFriendListSuccessed(new QQEventArgs<GetFriendListReplyPacket, GetFriendListPacket>(client, inPacket, outPacket));
        }

        /// <summary>
        /// 处理得到在线好友应答
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessGetFriendOnlineReply(GetOnlineOpReplyPacket inPacket, GetOnlineOpPacket outPacket)
        {
            client.FriendManager.OnGetOnlineFriendSuccessed(new QQEventArgs<GetOnlineOpReplyPacket, GetOnlineOpPacket>(client, inPacket, outPacket));
        }

        /// <summary>processGetUserInfoReply
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessGetUserInfoReply(GetUserInfoReplyPacket inPacket, GetUserInfoPacket outPacket)
        {
            client.FriendManager.OnGetUserInfoSuccessed(new QQEventArgs<GetUserInfoReplyPacket, GetUserInfoPacket>(client, inPacket, outPacket));
        }

        /// <summary>处理改变状态回复事件
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessChangeStatusReply(ChangeStatusReplyPacket inPacket, ChangeStatusPacket outPacket)
        {
            QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket> e = new QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.CHANGE_STATUS_OK)
            {
                client.FriendManager.OnChangeStatusSuccessed(e);
            }
            else
            {
                client.FriendManager.OnChangeStatusFailed(e);
            }
        }

        /// <summary>处理好友状态改变事件
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessFriendChangeStatus(FriendChangeStatusPacket packet)
        {
            QQEventArgs<FriendChangeStatusPacket, OutPacket> e = new QQEventArgs<FriendChangeStatusPacket, OutPacket>(client, packet, null);
            client.FriendManager.OnFriendChangeStatus(e);
        }

        /// <summary>
        /// 处理系统消息，比如谁谁谁添加你为好友
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessSystemNotification(SystemNotificationPacket packet)
        {
            QQEventArgs<SystemNotificationPacket, OutPacket> e = new QQEventArgs<SystemNotificationPacket, OutPacket>(client, packet, null);
            switch (packet.Type)
            {
                case SystemMessageType.BEING_ADDED:
                    client.MessageManager.OnSysAddedByOthers(e);
                    break;
                case SystemMessageType.ADD_FRIEND_REQUEST:
                    client.MessageManager.OnSysRequestAddMe(e);
                    break;
                case SystemMessageType.ADD_FRIEND_APPROVED:
                    client.MessageManager.OnSysAddOtherApproved(e);
                    break;
                case SystemMessageType.ADD_FRIEND_REJECTED:
                    client.MessageManager.OnSysAddOtherRejected(e);
                    break;
                case SystemMessageType.ADVERTISEMENT:
                    client.MessageManager.OnSysAdvertisment(e);
                    break;
                case SystemMessageType.BEING_ADDED_EX:
                    client.MessageManager.OnAddedByOthersEx(e);
                    break;
                case SystemMessageType.ADD_FRIEND_REQUEST_EX:
                    client.MessageManager.OnSysRequestAddMeEx(e);
                    break;
                case SystemMessageType.ADD_FRIEND_APPROVED_AND_ADD:
                    client.MessageManager.OnSysApprovedAddOtherAndAddMe(e);
                    break;
                case SystemMessageType.UPDATE_HINT:
                default:
                    break;
            }
        }

        /// <summary>处理请求加一个好友的回复包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessAddFriendExReply(AddFriendExReplyPacket inPacket, AddFriendExPacket outPacket)
        {
            QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e = new QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                switch (inPacket.AuthCode)
                {
                    case AuthType.No:
                        client.FriendManager.OnAddFriendSuccessed(e);
                        break;
                    case AuthType.Need:
                        client.FriendManager.OnAddFriendNeedAuth(e);
                        break;
                    case AuthType.Reject:
                        client.FriendManager.OnAddFriendDeny(e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (inPacket.ReplyCode == ReplyCode.ADD_FRIEND_ALREADY)
                {
                    client.FriendManager.OnFriendAlready(e);
                }
                else
                {
                    client.FriendManager.OnAddFriendFailed(e);
                }
            }
        }

        /// <summary> 处理认证信息的回复包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessAddFriendAuthReply(AddFriendAuthResponseReplyPacket inPacket, AddFriendAuthResponsePacket outPacket)
        {
            QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket> e = new QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode != ReplyCode.ADD_FRIEND_AUTH_OK)
            {
                client.FriendManager.OnResponseAuthFailed(e);
            }
            else
            {
                client.FriendManager.OnResponseAuthSuccessed(e);
            }
        }

        /// <summary>处理删除自己的回复包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessRemoveSelfReply(RemoveSelfReplyPacket inPacket, RemoveSelfPacket outPacket)
        {
            QQEventArgs<RemoveSelfReplyPacket, RemoveSelfPacket> e = new QQEventArgs<RemoveSelfReplyPacket, RemoveSelfPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                client.FriendManager.OnRemoveSelfSuccessed(e);
            }
            else
            {
                client.FriendManager.OnRemoveSelfFailed(e);
            }
        }

        /// <summary>处理删除好友的回复包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessDeleteFriendReply(DeleteFriendReplyPacket inPacket, DeleteFriendPacket outPacket)
        {
            QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket> e = new QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode != ReplyCode.OK)
            {
                client.FriendManager.OnDeleteFriendFailed(e);
            }
            else
            {
                client.FriendManager.OnDeleteFriendSuccessed(e);
            }
        }

        /// <summary>处理认证消息发送包
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="packet">The packet.</param>
        private void ProcessAuthorizeReply(AuthorizeReplyPacket inPacket, AuthorizePacket outPacket)
        {
            QQEventArgs<AuthorizeReplyPacket, AuthorizePacket> e = new QQEventArgs<AuthorizeReplyPacket, AuthorizePacket>(client, inPacket, outPacket);
            switch (inPacket.ReplyCode)
            {
                case ReplyCode.OK:
                    client.FriendManager.OnSendAuthSuccessed(e);
                    break;
                default:
                    client.FriendManager.OnSendAuthFailed(e);
                    break;
            }
        }

        /// <summary>
        /// Processes the upload group friend reply.处理上传分组好友列表回复包
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessUploadGroupFriendReply(UploadGroupFriendReplyPacket inPacket, UploadGroupFriendPacket outPacket)
        {
            QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket> e = new QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                client.FriendManager.OnUploadGroupFriendSuccessed(e);
            }
            else
            {
                client.FriendManager.OnUploadGroupFriendFailed(e);
            }
        }

        /// <summary>处理修改个人信息的回复包
        /// Processes the modify info reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessModifyInfoReply(ModifyInfoReplyPacket inPacket, ModifyInfoPacket outPacket)
        {
            QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket> e = new QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket>(client, inPacket, outPacket);
            if (inPacket.Success)
            {
                client.PrivateManager.OnModifyInfoSuccessed(e);
            }
            else
            {
                client.PrivateManager.OnModifyInfoFailed(e);
            }
        }

        /// <summary>处理个性签名操作回复包
        /// Processes the signature op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessSignatureOpReply(SignatureOpReplyPacket inPacket, SignatureOpPacket outPacket)
        {
            QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e = new QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                switch (inPacket.SubCommand)
                {
                    case SignatureSubCmd.MODIFY:
                        client.PrivateManager.OnModifySignatureSuccessed(e);
                        break;
                    case SignatureSubCmd.DELETE:
                        client.PrivateManager.OnDeleteSignatureSuccessed(e);
                        break;
                    case SignatureSubCmd.GET:
                        client.FriendManager.OnGetSignatureSuccessed(e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (inPacket.SubCommand)
                {
                    case SignatureSubCmd.MODIFY:
                        client.PrivateManager.OnModifySignatureFailed(e);
                        break;
                    case SignatureSubCmd.DELETE:
                        client.PrivateManager.OnDeleteSignatureFailed(e);
                        break;
                    case SignatureSubCmd.GET:
                        client.FriendManager.OnGetSignatureFailed(e);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>处理隐私选项操作回复包
        /// Processes the privacy data op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessPrivacyDataOpReply(PrivacyDataOpReplyPacket inPacket, PrivacyDataOpPacket outPacket)
        {
            QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e = new QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                switch (inPacket.SubCommand)
                {
                    case PrivacySubCmd.SearchMeByOnly:
                        client.PrivateManager.OnSetSearchMeByQQOnlySuccessed(e);
                        break;
                    case PrivacySubCmd.ShareGeography:
                        client.PrivateManager.OnSetShareGeographySuccessed(e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (inPacket.SubCommand)
                {
                    case PrivacySubCmd.SearchMeByOnly:
                        client.PrivateManager.OnSetSearchMeByQQOnlyFailed(e);
                        break;
                    case PrivacySubCmd.ShareGeography:
                        client.PrivateManager.OnSetShareGeographyFailed(e);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>处理上传下载备注的回复包
        /// Processes the friend data op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessFriendDataOpReply(FriendDataOpReplyPacket inPacket, FriendDataOpPacket outPacket)
        {
            QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e = new QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                switch (inPacket.SubCommand)
                {
                    case FriendOpSubCmd.BATCH_DOWNLOAD_FRIEND_REMARK:
                        client.FriendManager.OnBatchDownloadFriendRemarkSuccessed(e);
                        break;
                    case FriendOpSubCmd.UPLOAD_FRIEND_REMARK:
                        client.FriendManager.OnUploadFriendRemarkSuccessed(e);
                        break;
                    case FriendOpSubCmd.REMOVE_FRIEND_FROM_LIST:
                        client.FriendManager.OnRemoveFriendFromListSuccessed(e);
                        break;
                    case FriendOpSubCmd.DOWNLOAD_FRIEND_REMARK:
                        client.FriendManager.OnDownloadFriendRemarkSuccessed(e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (inPacket.SubCommand)
                {
                    case FriendOpSubCmd.BATCH_DOWNLOAD_FRIEND_REMARK:
                        client.FriendManager.OnBatchDownloadFriendRemarkFailed(e);
                        break;
                    case FriendOpSubCmd.UPLOAD_FRIEND_REMARK:
                        client.FriendManager.OnUploadFriendRemarkFailed(e);
                        break;
                    case FriendOpSubCmd.REMOVE_FRIEND_FROM_LIST:
                        client.FriendManager.OnRemoveFriendFromListFailed(e);
                        break;
                    case FriendOpSubCmd.DOWNLOAD_FRIEND_REMARK:
                        client.FriendManager.OnDownloadFriendRemarkFailed(e);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>处理好友等级回复包
        /// Processes the friend level op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessFriendLevelOpReply(FriendLevelOpReplyPacket inPacket, FriendLevelOpPacket outPacket)
        {
            QQEventArgs<FriendLevelOpReplyPacket, FriendLevelOpPacket> e = new QQEventArgs<FriendLevelOpReplyPacket, FriendLevelOpPacket>(client, inPacket, outPacket);
            client.FriendManager.OnGetFriendLevelSuccessed(e);
        }

        /// <summary>处理用户属性回复包
        /// Pocesses the user property op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void PocessUserPropertyOpReply(UserPropertyOpReplyPacket inPacket, UserPropertyOpPacket outPacket)
        {
            QQEventArgs<UserPropertyOpReplyPacket, UserPropertyOpPacket> e = new QQEventArgs<UserPropertyOpReplyPacket, UserPropertyOpPacket>(client, inPacket, outPacket);
            client.FriendManager.OnGetUserPropertySuccess(e);
        }

        /// <summary>处理下载分组好友列表回复包
        /// Processes the download group friend reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessDownloadGroupFriendReply(DownloadGroupFriendReplyPacket inPacket, DownloadGroupFriendPacket outPacket)
        {
            QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket> e = new QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                client.FriendManager.OnDownloadGroupFriendSuccessed(e);
            }
            else
            {
                client.FriendManager.OnDownloadGroupFriendFailed(e);
            }
        }

        /// <summary>处理分组名称回复包
        /// Processes the group name op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessGroupNameOpReply(GroupDataOpReplyPacket inPacket, GroupDataOpPacket outPacket)
        {
            QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e = new QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                switch (inPacket.SubCommand)
                {
                    case GroupSubCmd.DOWNLOAD:
                        client.FriendManager.OnDownloadGroupNamesSuccessed(e);
                        break;
                    case GroupSubCmd.UPLOAD:
                        client.FriendManager.OnUploadGroupNamesSuccessed(e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (inPacket.SubCommand)
                {
                    case GroupSubCmd.DOWNLOAD:
                        client.FriendManager.OnDownloadGroupNamesFailed(e);
                        break;
                    case GroupSubCmd.UPLOAD:
                        client.FriendManager.OnUploadGroupNamesFailed(e);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>处理搜索用户的回复包
        /// Processes the search user reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessSearchUserReply(SearchUserReplyPacket inPacket, SearchUserPacket outPacket)
        {
            QQEventArgs<SearchUserReplyPacket, SearchUserPacket> e = new QQEventArgs<SearchUserReplyPacket, SearchUserPacket>(client, inPacket, outPacket);
            client.FriendManager.OnSearchUserSuccessed(e);
        }

        /// <summary>处理天气预报操作回复包
        /// Processes the weather op reply.
        /// </summary>
        /// <param name="inPacket">The in packet.</param>
        /// <param name="outPacket">The out packet.</param>
        private void ProcessWeatherOpReply(WeatherOpReplyPacket inPacket, WeatherOpPacket outPacket)
        {
            QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket> e = new QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket>(client, inPacket, outPacket);
            if (inPacket.ReplyCode == ReplyCode.OK)
            {
                if (!string.IsNullOrEmpty(inPacket.Province))
                {
                    client.PrivateManager.OnGetWeatherSuccessed(e);
                }
                else
                {
                    client.PrivateManager.OnGetWeatherFailed(e);
                }
            }
            else
            {
                client.PrivateManager.OnGetWeatherFailed(e);
            }
        }
    }
}
