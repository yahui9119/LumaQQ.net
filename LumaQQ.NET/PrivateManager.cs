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
    /// 个人资料管理
    /// </summary>
    public class PrivateManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateManager"/> class.
        /// </summary>
        internal PrivateManager() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateManager"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        internal PrivateManager(QQClient client)
        {
            this.QQClient = client;
        }
        /// <summary>
        /// Gets or sets the QQ client.
        /// </summary>
        /// <value>The QQ client.</value>
        public QQClient QQClient { get; private set; }
        /// <summary>
        /// Gets or sets the QQ user.
        /// </summary>
        /// <value>The QQ user.</value>
        public QQUser QQUser { get { return QQClient.QQUser; } }
        /// <summary>请求自己这里的天气预报
        /// Gets the weather.
        /// </summary>
        public void GetWeather()
        {
            WeatherOpPacket packet = new WeatherOpPacket(QQUser);
            packet.IP = QQUser.IP;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>修改QQ密码
        /// Modifies the password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        public void ModifyPassword(string oldPassword, string newPassword)
        {
            ModifyInfo(oldPassword, newPassword, QQUser.ContactInfo);
        }

        /// <summary>修改个人信息
        /// Modifies the info.
        /// </summary>
        /// <param name="contactInfo">The contact info.</param>
        public void ModifyInfo(ContactInfo contactInfo)
        {
            ModifyInfo(null, null, contactInfo);
        }

        /// <summary>修改个人信息或密码
        /// Modifies the info.
        /// </summary>
        /// <param name="oldPassword">The old password.老密码，如果不修改密码，设成null</param>
        /// <param name="newPassword">The new password.新密码，如果不修改密码，设成null</param>
        /// <param name="contactInfo">The contact info.</param>
        private void ModifyInfo(string oldPassword, string newPassword, ContactInfo contactInfo)
        {
            ModifyInfoPacket packet = new ModifyInfoPacket(QQUser);
            packet.OldPassword = oldPassword;
            packet.NewPassword = newPassword;
            string[] infos = contactInfo.GetInfoArray();
            for (int i = 0; i < QQGlobal.QQ_COUNT_MODIFY_USER_INFO_FIELD; i++)
            {
                if (infos[i] == "-")
                {
                    infos[i] = "";
                }
            }
            packet.ContactInfo = contactInfo;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>修改个性签名
        /// Modifies the signature.
        /// </summary>
        /// <param name="sig">The sig.</param>
        public void ModifySignature(string sig)
        {
            SignatureOpPacket packet = new SignatureOpPacket(QQUser);
            packet.SubCommand = SignatureSubCmd.MODIFY;
            packet.Signature = sig;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>删除个性签名
        /// Deletes the signature.
        /// </summary>
        public void DeleteSignature()
        {
            SignatureOpPacket packet = new SignatureOpPacket(QQUser);
            packet.SubCommand = SignatureSubCmd.DELETE;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }

        /// <summary>设置是否只能通过QQ号码找到我
        /// Searches me by QQ only.
        /// </summary>
        /// <param name="only">if set to <c>true</c> [only].</param>
        public void SetSearchMeByQQOnly(bool only)
        {
            PrivacyDataOpPacket packet = new PrivacyDataOpPacket(QQUser);
            if (only)
            {
                packet.OpCode = ValueSet.Set;
            }
            else
                packet.OpCode = ValueSet.UnSet;
            packet.SubCommand = PrivacySubCmd.SearchMeByOnly;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        /// <summary>共享我的地址位置
        /// Shares the geography.
        /// </summary>
        /// <param name="shar">if set to <c>true</c> [shar].</param>
        public void ShareGeography(bool share)
        {
            PrivacyDataOpPacket packet = new PrivacyDataOpPacket(QQUser);
            if (share)
            {
                packet.OpCode = ValueSet.Set;
            }
            else
                packet.OpCode = ValueSet.UnSet;
            packet.SubCommand = PrivacySubCmd.ShareGeography;
            QQClient.PacketManager.SendPacket(packet, QQPort.Main.Name);
        }
        #region events

        #region 天气预报
        /// <summary>请求天气预报成功
        /// Occurs when [get weather successed].
        /// </summary>
        public event EventHandler<QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket>> GetWeatherSuccessed;
        /// <summary>
        /// Raises the <see cref="E:WeatherSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.WeatherOpReplyPacket,LumaQQ.NET.Packets.Out.WeatherOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetWeatherSuccessed(QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket> e)
        {
            if (GetWeatherSuccessed != null)
            {
                GetWeatherSuccessed(this, e);
            }
        }
        /// <summary>请求天气预报失败
        /// Occurs when [get weather failed].
        /// </summary>
        public event EventHandler<QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket>> GetWeatherFailed;
        /// <summary>
        /// Raises the <see cref="E:GetWeatherFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.WeatherOpReplyPacket,LumaQQ.NET.Packets.Out.WeatherOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnGetWeatherFailed(QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket> e)
        {
            if (GetWeatherFailed != null)
            {
                GetWeatherFailed(this, e);
            }
        }
        #endregion

        /// <summary>个人信息修改成功事件
        /// Occurs when [modify info successed].
        /// </summary>
        public event EventHandler<QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket>> ModifyInfoSuccessed;
        /// <summary>
        /// Raises the <see cref="E:ModifyInfoSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ModifyInfoReplyPacket,LumaQQ.NET.Packets.Out.ModifyInfoPacket&gt;"/> instance containing the event data.</param>
        internal void OnModifyInfoSuccessed(QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket> e)
        {
            //个人信息修改成功后更新QQUser对象
            QQUser.ContactInfo = e.OutPacket.ContactInfo;
            if (ModifyInfoSuccessed != null)
            {
                ModifyInfoSuccessed(this, e);
            }
        }

        /// <summary>个人信息修改失败事件
        /// Occurs when [modify info failed].
        /// </summary>
        public event EventHandler<QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket>> ModifyInfoFailed;
        /// <summary>
        /// Raises the <see cref="E:ModifyInfoFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.ModifyInfoReplyPacket,LumaQQ.NET.Packets.Out.ModifyInfoPacket&gt;"/> instance containing the event data.</param>
        internal void OnModifyInfoFailed(QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket> e)
        {
            if (ModifyInfoFailed != null)
            {
                ModifyInfoFailed(this, e);
            }
        }

        /// <summary>修改个性签名成功
        /// Occurs when [modify signature successed].
        /// </summary>
        public event EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>> ModifySignatureSuccessed;
        /// <summary>
        /// Raises the <see cref="E:ModifySignatureSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SignatureOpReplyPacket,LumaQQ.NET.Packets.Out.SignatureOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnModifySignatureSuccessed(QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            //个性签名修改成功修改对象
            QQUser.Signature = e.OutPacket.Signature;

            if (ModifySignatureSuccessed != null)
            {
                ModifySignatureSuccessed(this, e);
            }
        }

        /// <summary>修改个性签名失败
        /// Occurs when [modify signature failed].
        /// </summary>
        public event EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>> ModifySignatureFailed;
        /// <summary>
        /// Raises the <see cref="E:ModifySignatureFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SignatureOpReplyPacket,LumaQQ.NET.Packets.Out.SignatureOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnModifySignatureFailed(QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            if (ModifySignatureFailed != null)
            {
                ModifySignatureFailed(this, e);
            }
        }
        /// <summary>删除个性签名成功
        /// Occurs when [delete signature successed].
        /// </summary>
        public event EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>> DeleteSignatureSuccessed;
        /// <summary>
        /// Raises the <see cref="E:DeleteSignatureSuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SignatureOpReplyPacket,LumaQQ.NET.Packets.Out.SignatureOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnDeleteSignatureSuccessed(QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            if (DeleteSignatureSuccessed != null)
            {
                DeleteSignatureSuccessed(this, e);
            }
        }

        /// <summary>删除个性签名失败
        /// Occurs when [delete signature failed].
        /// </summary>
        public event EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>> DeleteSignatureFailed;
        /// <summary>
        /// Raises the <see cref="E:DeleteSignatureFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.SignatureOpReplyPacket,LumaQQ.NET.Packets.Out.SignatureOpPacket&gt;"/> instance containing the event data.</param>
        internal void OnDeleteSignatureFailed(QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            if (DeleteSignatureFailed != null)
            {
                DeleteSignatureFailed(this, e);
            }
        }


        /// <summary>成功设置只能通过好友找到选项
        /// Occurs when [set search me by QQ only successed].
        /// </summary>
        public event EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>> SetSearchMeByQQOnlySuccessed;
        /// <summary>
        /// Raises the <see cref="E:SetSearchMeByQQOnlySuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket,LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnSetSearchMeByQQOnlySuccessed(QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            if (SetSearchMeByQQOnlySuccessed != null)
            {
                SetSearchMeByQQOnlySuccessed(this, e);
            }
        }
        /// <summary>设置只能通过好友找到选项不成功
        /// Occurs when [set search me by QQ only failed].
        /// </summary>
        public event EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>> SetSearchMeByQQOnlyFailed;
        /// <summary>
        /// Raises the <see cref="E:SetSearchMeByQQOnlyFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket,LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnSetSearchMeByQQOnlyFailed(QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            if (SetSearchMeByQQOnlyFailed != null)
            {
                SetSearchMeByQQOnlyFailed(this, e);
            }
        }
        /// <summary>设置共享地理位置选项成功
        /// Occurs when [set share geography successed].
        /// </summary>
        public event EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>> SetShareGeographySuccessed;
        /// <summary>
        /// Raises the <see cref="E:SetShareGeographySuccessed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket,LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnSetShareGeographySuccessed(QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            if (SetShareGeographySuccessed != null)
            {
                SetShareGeographySuccessed(this, e);
            }
        }
        /// <summary>设置共享地理位置选项失败
        /// Occurs when [set share geography failed].
        /// </summary>
        public event EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>> SetShareGeographyFailed;
        /// <summary>
        /// Raises the <see cref="E:SetShareGeographyFailed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="LumaQQ.NET.Events.QQEventArgs&lt;LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket,LumaQQ.NET.Packets.In.PrivacyDataOpReplyPacket&gt;"/> instance containing the event data.</param>
        internal void OnSetShareGeographyFailed(QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            if (SetShareGeographyFailed != null)
            {
                SetShareGeographyFailed(this, e);
            }
        }
        #endregion
    }
}
