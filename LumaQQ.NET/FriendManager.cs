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
using LumaQQ.NET.Events;
using LumaQQ.NET.Packets;
using LumaQQ.NET.Entities;
using LumaQQ.NET.Threading;
using LumaQQ.NET.Packets.In;
using LumaQQ.NET.Packets.Out;
namespace LumaQQ.NET
{

    /// <summary>
    /// 好友管理
    /// <remark>abu 2008-03-11 </remark>
    /// </summary>
    public class FriendManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendManager"/> class.
        /// </summary>
        private FriendManager() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendManager"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        internal FriendManager(QQClient client)
        {
            this.QQClient = client;
        }
        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public QQClient QQClient { get; private set; }
        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public QQUser QQUser { get { return QQClient.QQUser; } }

        /// <summary>搜索所有的在线用户
        /// Searches the user.
        /// </summary>
        /// <param name="page">The page.</param>
        public void SearchUser(int page)
        {
            SearchUser(page, "", "", "");
        }
        /// <summary>自定义搜索用户
        /// Searches the user.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="qq">The qq.</param>
        /// <param name="nick">The nick.</param>
        /// <param name="email">The email.</param>
        public void SearchUser(int page, string qq, string nick, string email)
        {
            SearchUserPacket packet = new SearchUserPacket(QQUser);
            packet.Page = page.ToString();
            packet.QQStr = qq;
            packet.Nick = nick;
            packet.Email = email;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>下载好友分组
        /// Downloads the group friend.
        /// </summary>
        /// <param name="beginFrom">The begin from.起始好友号 如果是第一个包，起始好友号为0</param>
        public void DownloadGroupFriends(int beginFrom)
        {
            DownloadGroupFriendPacket packet = new DownloadGroupFriendPacket(QQUser);
            packet.BeginFrom = beginFrom;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>上传分组名称
        /// Uploads the group.
        /// </summary>
        /// <param name="groups">The groups.</param>
        public void UploadGroupName(List<string> groups)
        {
            GroupDataOpPacket packet = new GroupDataOpPacket(QQUser);
            packet.Type = GroupSubCmd.UPLOAD;
            packet.Groups = groups;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>下载分组名称
        /// Downloads the group.
        /// </summary>
        public void DownloadGroupName()
        {
            GroupDataOpPacket packet = new GroupDataOpPacket(QQUser);
            packet.Type = GroupSubCmd.DOWNLOAD;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>得到用户属性
        /// Gets the user property.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        public void GetUserProperty(ushort startPosition)
        {
            UserPropertyOpPacket packet = new UserPropertyOpPacket(QQUser);
            packet.StartPosition = startPosition;
            packet.SubCommand = UserPropertySubCmd.GET;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>得到好友等级
        /// Gets the friend level.
        /// </summary>
        /// <param name="friends">The friends.</param>
        public void GetFriendLevel(List<int> friends)
        {
            FriendLevelOpPacket packet = new FriendLevelOpPacket(QQUser);
            packet.Friends = friends;
            packet.SubCommand = FriendLevelSubCmd.GET;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>根据QQ号码读取个性签名
        /// Gets the signature.
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void GetSignature(int qq)
        {
            List<Signature> list = new List<Signature>();
            Signature sig = new Signature();
            sig.QQ = qq;
            list.Add(sig);
            GetSignature(list);
        }
        /// <summary>读取个性签名
        /// <remarks>在得到好友的个性签名时，QQ的做法是对所有的QQ号排个序，每次最多请求33个</remarks>
        /// Gets the signature.
        /// </summary>
        /// <param name="sigs">The sigs.</param>
        public void GetSignature(List<Signature> sigs)
        {
            SignatureOpPacket packet = new SignatureOpPacket(QQUser);
            packet.SubCommand = SignatureSubCmd.GET;
            packet.Signatures = sigs;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>下载好友备注信息
        /// Downloads the friend remark.
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void DownloadFriendRemark(int qq)
        {
            FriendDataOpPacket packet = new FriendDataOpPacket(QQUser);
            packet.SubCommand = FriendOpSubCmd.DOWNLOAD_FRIEND_REMARK;
            packet.QQ = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>批量下载好友备注信息
        /// Batches the download friend remark.
        /// </summary>
        /// <param name="page">The page.页号</param>
        public void BatchDownloadFriendRemark(int page)
        {
            FriendDataOpPacket packet = new FriendDataOpPacket(QQUser);
            packet.SubCommand = FriendOpSubCmd.BATCH_DOWNLOAD_FRIEND_REMARK;
            packet.Page = page;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>上传好友备信息
        /// Uploads the friend remark.
        /// </summary>
        /// <param name="qq">The qq.</param>
        /// <param name="remark">The remark.</param>
        public void UploadFriendRemark(int qq, FriendRemark remark)
        {
            FriendDataOpPacket packet = new FriendDataOpPacket(QQUser);
            packet.Remark = remark;
            packet.QQ = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary> 把好友从服务器端的好友列表中删除
        /// Removes the friend from list.
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void RemoveFriendFromList(int qq)
        {
            FriendDataOpPacket packet = new FriendDataOpPacket(QQUser);
            packet.SubCommand = FriendOpSubCmd.REMOVE_FRIEND_FROM_LIST;
            packet.QQ = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>
        /// 添加好友到服务器端的好友列表中
        /// </summary>
        /// <param name="group">The group.好友的组号，我的好友组是0，然后是1，2，...</param>
        /// <param name="qq">The qq.</param>
        public void AddFriendToList(int group, int qq)
        {
            UploadGroupFriendPacket packet = new UploadGroupFriendPacket(QQUser);
            packet.addFriend(group, qq);
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>添加一个好友
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void AddFriend(int qq)
        {
            AddFriendExPacket packet = new AddFriendExPacket(QQUser);
            packet.To = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name, false);
        }
        /// <summary>删除一个好友
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void DeleteFriend(int qq)
        {
            DeleteFriendPacket packet = new DeleteFriendPacket(QQUser);
            packet.To = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary> 把某人的好友列表中的自己删除
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void RemoveSelfFrom(int qq)
        {
            RemoveSelfPacket packet = new RemoveSelfPacket(QQUser);
            packet.RemoveFrom = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary> 如果要加的人需要认证，用这个方法发送验证请求
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        /// <param name="message">The message.</param>
        public void SendAddFriendAuth(int qq, string message)
        {
            AuthorizePacket packet = new AuthorizePacket(QQUser);
            packet.To = qq;
            packet.Message = message;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>改变自身状态
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="showFakeCam">if set to <c>true</c> [show fake cam].</param>
        public void ChangeStatus(QQStatus status, bool showFakeCam)
        {
            QQUser.Status = status;
            ChangeStatusPacket packet = new ChangeStatusPacket(QQUser);
            packet.ShowFakeCam = showFakeCam;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>如果我要同意一个人加我为好友的请求，用这个方法发送同意消息
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void ApprovedAddMe(int qq)
        {
            AddFriendAuthResponsePacket packet = new AddFriendAuthResponsePacket(QQUser);
            packet.To = qq;
            packet.Action = AuthAction.Approve;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>如果我要拒绝一个人加我为好友的请求，用这个方法发送拒绝消息
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        /// <param name="message">The message.</param>
        public void RejectAddMe(int qq, string message)
        {
            AddFriendAuthResponsePacket packet = new AddFriendAuthResponsePacket(QQUser);
            packet.To = qq;
            packet.Message = message;
            packet.Action = AuthAction.Reject;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>得到一个用户的详细信息
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void GetUserInfo(int qq)
        {
            GetUserInfoPacket packet = new GetUserInfoPacket(QQUser);
            packet.QQ = qq;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>请求在线好友列表
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        public void GetOnlineFriend()
        {
            GetOnlineFriend(0);
        }

        /// <summary>请求在线好友列表
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        private void GetOnlineFriend(int startPosition)
        {
            GetOnlineOpPacket packet = new GetOnlineOpPacket(QQUser);
            packet.StartPosition = startPosition;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>请求取得好友名单
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        public void GetFriendList()
        {
            GetFriendList(0);
        }

        /// <summary>请求取得好友名单
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="position">The position.</param>
        private void GetFriendList(int startPosition)
        {
            GetFriendListPacket packet = new GetFriendListPacket(QQUser);
            packet.StartPosition = (ushort)startPosition;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        #region events
        #region 搜索QQ用户事件

        /// <summary>搜索好友回复事件
        /// Occurs when [search user successed].
        /// </summary>
        public event EventHandler<QQEventArgs<SearchUserReplyPacket, SearchUserPacket>> SearchUserSuccessed;
        /// <summary>
        /// Raises the <see cref="E:SearchUserSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SearchUserReplyPacket,LumaQQ.NET.Packets.Out.SearchUserPacket&gt;"/> instance containing the event data.</param>
        internal void OnSearchUserSuccessed(QQEventArgs<SearchUserReplyPacket, SearchUserPacket> e)
        {
            if (SearchUserSuccessed != null)
            {
                SearchUserSuccessed(this, e);
            }
        }

        #endregion

        #region 分组名称事件
        /// <summary>上传分组名称成功
        /// Occurs when [upload group names successed].
        /// </summary>
        public event EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>> UploadGroupNamesSuccessed;
        /// <summary>
        /// Raises the <see cref="E:UploadGroupNamesSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.GroupDataOpReplyPacket,LumaQQ.NET.Packets.Out.GroupDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnUploadGroupNamesSuccessed(QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            if (UploadGroupNamesSuccessed != null)
            {
                UploadGroupNamesSuccessed(this, e);
            }
        }

        /// <summary>上传分组名称失败
        /// Occurs when [upload group names failed].
        /// </summary>
        public event EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>> UploadGroupNamesFailed;
        internal void OnUploadGroupNamesFailed(QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            if (UploadGroupNamesFailed != null)
            {
                UploadGroupNamesFailed(this, e);
            }
        }

        /// <summary>下载分组名称成功
        /// Occurs when [download group names successed].
        /// </summary>
        public event EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>> DownloadGroupNamesSuccessed;
        /// <summary>
        /// Raises the <see cref="E:DownloadGroupNamesSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.GroupDataOpReplyPacket,LumaQQ.NET.Packets.Out.GroupDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnDownloadGroupNamesSuccessed(QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            if (DownloadGroupNamesSuccessed != null)
            {
                DownloadGroupNamesSuccessed(this, e);
            }
        }

        /// <summary>下载分名称失败
        /// Occurs when [download group names failed].
        /// </summary>
        public event EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>> DownloadGroupNamesFailed;
        internal void OnDownloadGroupNamesFailed(QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            if (DownloadGroupNamesFailed != null)
            {
                DownloadGroupNamesFailed(this, e);
            }
        }
        #endregion

        #region 下载分组好友
        /// <summary>下载分组好友成功
        /// Occurs when [download group friend successed].
        /// </summary>
        public event EventHandler<QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket>> DownloadGroupFriendSuccessed;
        /// <summary>
        /// Raises the <see cref="E:DownloadGroupFriendSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.DownloadGroupFriendReplyPacket,LumaQQ.NET.Packets.Out.DownloadGroupFriendPacket&gt;"/> instance containing the event data.</param>
        internal void OnDownloadGroupFriendSuccessed(QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket> e)
        {
            if (DownloadGroupFriendSuccessed != null)
            {
                DownloadGroupFriendSuccessed(this, e);
            }
        }
        /// <summary>下载分组好友失败
        /// Occurs when [download group friend failed].
        /// </summary>
        public event EventHandler<QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket>> DownloadGroupFriendFailed;
        /// <summary>
        /// Raises the <see cref="E:DownloadGroupFriendFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.DownloadGroupFriendReplyPacket,LumaQQ.NET.Packets.Out.DownloadGroupFriendPacket&gt;"/> instance containing the event data.</param>
        internal void OnDownloadGroupFriendFailed(QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket> e)
        {
            if (DownloadGroupFriendFailed != null)
            {
                DownloadGroupFriendFailed(this, e);
            }
        }
        #endregion

        #region 读取用户属性
        /// <summary>读取用户属性成功
        /// Occurs when [get user property successed].
        /// </summary>
        public event EventHandler<QQEventArgs<UserPropertyOpReplyPacket, UserPropertyOpPacket>> GetUserPropertySuccessed;
        /// <summary>
        /// Raises the <see cref="E:GetUserPropertySuccess"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.UserPropertyOpReplyPacket,LumaQQ.NET.Packets.Out.UserPropertyOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetUserPropertySuccess(QQEventArgs<UserPropertyOpReplyPacket, UserPropertyOpPacket> e)
        {
            if (GetUserPropertySuccessed != null)
            {
                GetUserPropertySuccessed(this, e);
            }
        }
        #endregion

        #region 读取好友等级
        /// <summary>读取好友等级成功
        /// Occurs when [get friend level successed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendLevelOpReplyPacket, FriendLevelOpPacket>> GetFriendLevelSuccessed;
        /// <summary>
        /// Raises the <see cref="E:GetFriendLevelSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendLevelOpReplyPacket,LumaQQ.NET.Packets.Out.FriendLevelOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetFriendLevelSuccessed(QQEventArgs<FriendLevelOpReplyPacket, FriendLevelOpPacket> e)
        {
            if (GetFriendLevelSuccessed != null)
            {
                GetFriendLevelSuccessed(this, e);
            }
        }

        #endregion

        #region 好友个性签名信息
        /// <summary>读取个性签名成功
        /// Occurs when [get signature successed].
        /// </summary>
        public event EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>> GetSignatureSuccessed;
        /// <summary>
        /// Raises the <see cref="E:GetSignatureSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SignatureOpReplyPacket,LumaQQ.NET.Packets.Out.SignatureOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetSignatureSuccessed(QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            if (GetSignatureSuccessed != null)
            {
                GetSignatureSuccessed(this, e);
            }
        }

        /// <summary>读取个性签名失败
        /// Occurs when [get signature failed].
        /// </summary>
        public event EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>> GetSignatureFailed;
        /// <summary>
        /// Raises the <see cref="E:GetSignatureFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SignatureOpReplyPacket,LumaQQ.NET.Packets.Out.SignatureOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetSignatureFailed(QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            if (GetSignatureFailed != null)
            {
                GetSignatureFailed(this, e);
            }
        }
        #endregion

        #region 好友备注信息操作
        /// <summary>上传好友备注信息成功
        /// Occurs when [upload friend remark successed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> UploadFriendRemarkSuccessed;
        /// <summary>
        /// Raises the <see cref="E:UploadFriendRemarkSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnUploadFriendRemarkSuccessed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (UploadFriendRemarkSuccessed != null)
            {
                UploadFriendRemarkSuccessed(this, e);
            }
        }

        /// <summary>上传好友备注信息失败
        /// Occurs when [upload friend remark failed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> UploadFriendRemarkFailed;
        /// <summary>
        /// Raises the <see cref="E:UploadFriendRemarkFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnUploadFriendRemarkFailed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (UploadFriendRemarkFailed != null)
            {
                UploadFriendRemarkFailed(this, e);
            }
        }

        /// <summary>下载好友备注信息成功
        /// Occurs when [download friend remark successed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> DownloadFriendRemarkSuccessed;
        /// <summary>
        /// Raises the <see cref="E:DownloadFriendRemarkSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnDownloadFriendRemarkSuccessed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (DownloadFriendRemarkSuccessed != null)
            {
                DownloadFriendRemarkSuccessed(this, e);
            }
        }

        /// <summary>下载好友备注信息失败
        /// Occurs when [download friend remark failed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> DownloadFriendRemarkFailed;
        /// <summary>
        /// Raises the <see cref="E:DownloadFriendRemarkFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnDownloadFriendRemarkFailed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (DownloadFriendRemarkFailed != null)
            {
                DownloadFriendRemarkFailed(this, e);
            }
        }

        /// <summary>成批下载好友信息成功
        /// Occurs when [batch download friend remark successed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> BatchDownloadFriendRemarkSuccessed;
        /// <summary>
        /// Raises the <see cref="E:BatchDownloadFriendRemarkSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnBatchDownloadFriendRemarkSuccessed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (BatchDownloadFriendRemarkSuccessed != null)
            {
                BatchDownloadFriendRemarkSuccessed(this, e);
            }
        }

        /// <summary>成批下载好友信息失败
        /// Occurs when [batch download friend remark failed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> BatchDownloadFriendRemarkFailed;
        /// <summary>
        /// Raises the <see cref="E:BatchDownloadFriendRemarkFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnBatchDownloadFriendRemarkFailed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (BatchDownloadFriendRemarkFailed != null)
            {
                BatchDownloadFriendRemarkFailed(this, e);
            }
        }

        /// <summary>从服务器端好友列表中移除好友成功
        /// Occurs when [remove friend from list successed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> RemoveFriendFromListSuccessed;
        /// <summary>
        /// Raises the <see cref="E:RemoveFriendFromListSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnRemoveFriendFromListSuccessed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (RemoveFriendFromListSuccessed != null)
            {
                RemoveFriendFromListSuccessed(this, e);
            }
        }

        /// <summary>从服务器端好友列表中移除好友失败
        /// Occurs when [remove friend from list failed].
        /// </summary>
        public event EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>> RemoveFriendFromListFailed;
        /// <summary>
        /// Raises the <see cref="E:RemoveFriendFromListFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendDataOpReplyPacket,LumaQQ.NET.Packets.Out.FriendDataOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnRemoveFriendFromListFailed(QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (RemoveFriendFromListFailed != null)
            {
                RemoveFriendFromListFailed(this, e);
            }
        }

        #endregion

        /// <summary>取得在线好友列表
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<GetOnlineOpReplyPacket, GetOnlineOpPacket>> GetOnlineFriendSuccessed;
        /// <summary>
        /// Called when [get online friend successed].
        /// </summary>
        /// <param name="e">The e.</param>
        internal void OnGetOnlineFriendSuccessed(QQEventArgs<GetOnlineOpReplyPacket, GetOnlineOpPacket> e)
        {
            foreach (FriendOnlineEntry online in e.InPacket.OnlineFriends)
            {
                QQUser.Friends.SetFriendOnline(online.Status.QQ, online);
            }
            if (!e.InPacket.Finished)
            {
                GetOnlineFriend(e.InPacket.Position + 1);
            }
            if (GetOnlineFriendSuccessed != null)
            {
                GetOnlineFriendSuccessed(this, e);
            }
        }

        /// <summary>得到好友列表事件
        /// <remarks>获得好友列表及在线状态的顺序是：
        /// 先得到所有的好友列表，根据TheEnd判断是否已经得到所有的好友。
        /// 得到所有的好友列表后，才能去获取在线好友列表。</remarks>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<GetFriendListReplyPacket, GetFriendListPacket>> GetFriendListSuccessed;
        /// <summary>
        /// Raises the <see cref="E:GetFriendListSuccess"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.GetFriendListReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetFriendListSuccessed(QQEventArgs<GetFriendListReplyPacket, GetFriendListPacket> e)
        {
            foreach (QQFriend friend in e.InPacket.Friends)
            {
                QQUser.Friends.Add(friend.QQ, new FriendInfo(friend));
            }
            if (!e.InPacket.Finished)
            {
                GetFriendList(e.InPacket.Position + 1);
            }
            if (GetFriendListSuccessed != null)
            {
                GetFriendListSuccessed(this, e);
            }
        }

        /// <summary>得到用户详细信息回复事件 
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<GetUserInfoReplyPacket, GetUserInfoPacket>> GetUserInfoSuccessed;
        /// <summary>
        /// Raises the <see cref="E:GetUserInfoSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.GetUserInfoReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetUserInfoSuccessed(QQEventArgs<GetUserInfoReplyPacket, GetUserInfoPacket> e)
        {
            //如果QQ号码等于自己则更新自己的详细信息
            if (e.QQClient.QQUser.QQ == e.InPacket.ContactInfo.QQ)
            {
                e.QQClient.QQUser.ContactInfo = e.InPacket.ContactInfo;
            }
            if (GetUserInfoSuccessed != null)
            {
                GetUserInfoSuccessed(this, e);
            }
        }

        /// <summary>收到好友的状态发生变化
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<FriendChangeStatusPacket, OutPacket>> FriendChangeStatus;
        /// <summary>
        /// Raises the <see cref="E:FriendChangeStatus"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.FriendChangeStatusPacket&gt;"/> instance containing the event data.</param>
        internal void OnFriendChangeStatus(QQEventArgs<FriendChangeStatusPacket, OutPacket> e)
        {
            if (FriendChangeStatus != null)
            {
                FriendChangeStatus(this, e);
            }
        }

        #region 改变自身状态的回复事件

        /// <summary>改变自身状态成功
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket>> ChangeStatusSuccessed;
        /// <summary>
        /// Raises the <see cref="E:ChangeStatusSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ChangeStatusReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnChangeStatusSuccessed(QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket> e)
        {
            if (ChangeStatusSuccessed != null)
            {
                ChangeStatusSuccessed(this, e);
            }
        }

        /// <summary>
        /// 改变自身状态失败
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket>> ChangeStatusFailed;
        /// <summary>
        /// Raises the <see cref="E:ChangeStatusFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ChangeStatusReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnChangeStatusFailed(QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket> e)
        {
            if (ChangeStatusFailed != null)
            {
                ChangeStatusFailed(this, e);
            }
        }
        #endregion

        #region 添加好友的回复事件

        /// <summary>好友添加成功
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>> AddFriendSuccessed;
        /// <summary>
        /// Raises the <see cref="E:AddFriendSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendExReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnAddFriendSuccessed(QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            if (AddFriendSuccessed != null)
            {
                AddFriendSuccessed(this, e);
            }
        }
        /// <summary>
        /// 添加好友时，需要发送验证信息
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>> AddFriendNeedAuth;
        /// <summary>
        /// Raises the <see cref="E:AddFriendNeedAuth"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendExReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnAddFriendNeedAuth(QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            if (AddFriendNeedAuth != null)
            {
                AddFriendNeedAuth(this, e);
            }
        }

        /// <summary>
        /// 对方拒绝让你加为好友
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>> AddFriendDeny;
        /// <summary>
        /// Raises the <see cref="E:AddFriendDeny"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendExReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnAddFriendDeny(QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            if (AddFriendDeny != null)
            {
                AddFriendDeny(this, e);
            }
        }

        /// <summary>对方已经是好友
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>> FriendAlready;
        /// <summary>
        /// Raises the <see cref="E:FriendAlready"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendExReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnFriendAlready(QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            if (FriendAlready != null)
            {
                FriendAlready(this, e);
            }
        }

        /// <summary>添加好友失败
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>> AddFriendFailed;
        /// <summary>
        /// Raises the <see cref="E:AddFriendFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendExReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnAddFriendFailed(QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            if (AddFriendFailed != null)
            {
                AddFriendFailed(this, e);
            }
        }

        /// <summary>认证信息发送成功
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AuthorizeReplyPacket, AuthorizePacket>> SendAuthSuccessed;
        /// <summary>
        /// Raises the <see cref="E:SendAuthSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendAuthResponseReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnSendAuthSuccessed(QQEventArgs<AuthorizeReplyPacket, AuthorizePacket> e)
        {
            if (SendAuthSuccessed != null)
            {
                SendAuthSuccessed(this, e);
            }
        }

        /// <summary>发送认证信息失败
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AuthorizeReplyPacket, AuthorizePacket>> SendAuthFailed;
        /// <summary>
        /// Raises the <see cref="E:SendAuthFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendAuthResponseReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnSendAuthFailed(QQEventArgs<AuthorizeReplyPacket, AuthorizePacket> e)
        {
            if (SendAuthFailed != null)
            {
                SendAuthFailed(this, e);
            }
        }
        #endregion

        #region 把自己从好友的好友列表中删除的回复事件
        /// <summary>把自己从好友的好友列表中删除成功
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<RemoveSelfReplyPacket, RemoveSelfPacket>> RemoveSelfSuccessed;
        /// <summary>
        /// Raises the <see cref="E:RemoveSelfSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.RemoveSelfReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnRemoveSelfSuccessed(QQEventArgs<RemoveSelfReplyPacket, RemoveSelfPacket> e)
        {
            if (RemoveSelfSuccessed != null)
            {
                RemoveSelfSuccessed(this, e);
            }
        }

        /// <summary>把自己从好友的好友列表中删除失败
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<RemoveSelfReplyPacket, RemoveSelfPacket>> RemoveSelfFailed;
        /// <summary>
        /// Raises the <see cref="E:RemoveSelfFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.RemoveSelfReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnRemoveSelfFailed(QQEventArgs<RemoveSelfReplyPacket, RemoveSelfPacket> e)
        {
            if (RemoveSelfFailed != null)
            {
                RemoveSelfFailed(this, e);
            }
        }

        #endregion

        #region 删除好友回复事件
        /// <summary>删除好友成功
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket>> DeleteFriendSuccessed;
        /// <summary>
        /// Raises the <see cref="E:DeleteFriendSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.DeleteFriendReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnDeleteFriendSuccessed(QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket> e)
        {
            if (DeleteFriendSuccessed != null)
            {
                DeleteFriendSuccessed(this, e);
            }
        }
        /// <summary>删除好友失败
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket>> DeleteFriendFailed;
        /// <summary>
        /// Raises the <see cref="E:DeleteFriendFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.DeleteFriendReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnDeleteFriendFailed(QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket> e)
        {
            if (DeleteFriendFailed != null)
            {
                DeleteFriendFailed(this, e);
            }
        }
        #endregion

        #region 处理对方发送过来的认证信息回复事件
        /// <summary>处理认证信息成功
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket>> ResponseAuthSuccessed;
        /// <summary>
        /// Raises the <see cref="E:ResponseAuthSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.Out.AddFriendAuthResponsePacket&gt;"/> instance containing the event data.</param>
        internal void OnResponseAuthSuccessed(QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket> e)
        {
            if (ResponseAuthSuccessed != null)
            {
                ResponseAuthSuccessed(this, e);
            }
        }

        /// <summary>处理认证信息失败
        /// 	<remark>abu 2008-03-12 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket>> ResponseAuthFailed;
        /// <summary>
        /// Raises the <see cref="E:ResponseAuthFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.AddFriendAuthResponseReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnResponseAuthFailed(QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket> e)
        {
            if (ResponseAuthFailed != null)
            {
                ResponseAuthFailed(this, e);
            }
        }
        #endregion

        #region 处理上传分组好友列表回复事件
        /// <summary>
        /// Occurs when [upload group friend successed].事件在上传分组中的好友列表成功时发生
        /// </summary>
        public event EventHandler<QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket>> UploadGroupFriendSuccessed;
        /// <summary>
        /// Raises the <see cref="E:UploadGroupFriendSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.UploadGroupFriendReplyPacket,LumaQQ.NET.Packets.Out.UploadGroupFriendPacket&gt;"/> instance containing the event data.</param>
        internal void OnUploadGroupFriendSuccessed(QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket> e)
        {
            if (UploadGroupFriendSuccessed != null)
            {
                UploadGroupFriendSuccessed(this, e);
            }
        }

        /// <summary>
        /// Occurs when [upload group friend failed].事件在下载分组中的好友列表成功时发生
        /// </summary>
        public event EventHandler<QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket>> UploadGroupFriendFailed;
        /// <summary>
        /// Raises the <see cref="E:UploadGroupFriendFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.UploadGroupFriendReplyPacket,LumaQQ.NET.Packets.Out.UploadGroupFriendPacket&gt;"/> instance containing the event data.</param>
        internal void OnUploadGroupFriendFailed(QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket> e)
        {
            if (UploadGroupFriendFailed != null)
            {
                UploadGroupFriendFailed(this, e);
            }
        }
        #endregion

        /// <summary>
        /// 好友个性签名改变
        /// 	<remark>abu 2008-03-15 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> SignatureChanged;
        /// <summary>
        /// Raises the <see cref="E:SignatureChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket,LumaQQ.NET.Packets.OutPacket&gt;"/> instance containing the event data.</param>
        internal void OnSignatureChanged(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (SignatureChanged != null)
            {
                SignatureChanged(this, e);
            }
        }

        /// <summary>
        /// 收到好友属性变化通知
        /// 	<remark>abu 2008-03-15 </remark>
        /// </summary>
        public event EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>> UserPropertyChanged;
        /// <summary>
        /// Raises the <see cref="E:UserPropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ReceiveIMPacket,LumaQQ.NET.Packets.OutPacket&gt;"/> instance containing the event data.</param>
        internal void OnUserPropertyChanged(QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            if (UserPropertyChanged != null)
            {
                UserPropertyChanged(this, e);
            }
        }
        #endregion
    }
}
