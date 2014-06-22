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

using LumaQQ.NET.Utils;
using LumaQQ.NET.Entities;
namespace LumaQQ.NET
{
    /// <summary>
    /// 封装QQ用户的信息
    /// 	<remark>abu 2008-02-16 </remark>
    /// </summary>
    public class QQUser
    {

        /// <summary>
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="qqNum">QQ号.</param>
        /// <param name="pwd">密码.</param>
        public QQUser(int qqNum, string pwd)
        {
            this.QQ = qqNum;
            SetPassword(pwd);
            Initialize();
        }

        private void Initialize()
        {
            IP = new byte[4];
            ServerIp = new byte[4];
            LastLoginIp = new byte[4];
            IsLoggedIn = false;
            LoginMode = LoginMode.Normal;
            IsUdp = true;
            ContactInfo = new ContactInfo();
            IsShowFakeCam = false;
            InitKey = Util.RandomKey();

            Friends = new FriendList();
        }

        public QQUser(int qqNum, byte[] md5pwd)
        {
            this.QQ = qqNum;
            this.PasswordKey = md5pwd;
            this.Initialize();
        }
        private byte[] sessionKey;
        /// <summary>
        /// 设置用户的密码，不会保存明文形式的密码，立刻用Double MD5算法加密
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <param name="pwd">明文形式的密码</param>
        public void SetPassword(string pwd)
        {
            PasswordKey = Crypter.MD5(Crypter.MD5(Util.GetBytes(pwd)));
        }

        /// <summary>QQ号
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public int QQ { get; set; }
        /// <summary>会话密钥
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] SessionKey
        {
            get { return this.sessionKey; }
            set
            {
                this.sessionKey = value;
                byte[] b = new byte[4 + QQGlobal.QQ_LENGTH_KEY];
                b[0] = (byte)((QQ >> 24) & 0xFF);
                b[1] = (byte)((QQ >> 16) & 0xFF);
                b[2] = (byte)((QQ >> 8) & 0xFF);
                b[3] = (byte)(QQ & 0xFF);
                Array.Copy(this.sessionKey, 0, b, 4, QQGlobal.QQ_LENGTH_KEY);
                this.FileSessionKey = Utils.Crypter.MD5(b);
            }
        }
        /// <summary>MD5处理的用户密码
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] PasswordKey { get; private set; }
        /// <summary>本地IP
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] IP { get; set; }
        /// <summary>上一次登陆IP
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] LastLoginIp { get; set; }
        /// <summary>本地端口，在QQ中其实只有两字节
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public int Port { get; set; }
        /// <summary>服务器IP
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] ServerIp { get; set; }
        /// <summary>服务器端口，在QQ中其实只有两字节
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public int ServerPort { get; set; }
        /// <summary>上一次登陆时间，在QQ中其实只有4字节
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public long LastLoginTime { get; set; }
        /// <summary>本次登陆时间
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public long LoginTime { get; set; }
        /// <summary>当前登陆状态，为true表示已经登陆
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public bool IsLoggedIn { get; set; }
        /// <summary>登陆模式，隐身还是非隐身
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public LoginMode LoginMode { get; set; }
        /// <summary>设置登陆服务器的方式是UDP还是TCP
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public bool IsUdp { get; set; }
        /// <summary>当前的状态，比如在线，隐身等等
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public QQStatus Status { get; set; }
        /// <summary>ContactInfo
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public ContactInfo ContactInfo { get; set; }
        /// <summary>文件传输会话密钥
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] FileSessionKey { get; set; }
        /// <summary>文件中转服务器通讯密钥，来自0x001D - 0x4
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] FileAgentKey { get; set; }

        /// <summary>文件中转认证令牌
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] FileAgentToken { get; set; }
        /// <summary>未知令牌
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] Unknown03Token { get; set; }
        /// <summary>是否显示虚拟摄像头
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public bool IsShowFakeCam { get; set; }
        /// <summary>客户端key
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] ClientKey { get; set; }
        /// <summary>初始密钥
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] InitKey { get; private set; }
        /// <summary>登录令牌
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] LoginToken { get; set; }
        /// <summary>未知用途密钥，来自0x001D
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] Unknown03Key { get; set; }
        public byte[] Unknown06Key { get; set; }
        public byte[] Unknown07Key { get; set; }
        public byte[] Unknown08Key { get; set; }
        /// <summary>未知令牌
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] Unknown06Token { get; set; }
        public byte[] Unknown07Token { get; set; }
        public byte[] Unknown08Token { get; set; }
        /// <summary> 认证令牌
        /// 	<remark>abu 2008-02-18 </remark>
        /// </summary>
        /// <value></value>
        public byte[] AuthToken { get; set; }

        /// <summary>个性签名
        /// Gets or sets the signature.
        /// </summary>
        /// <value>The signature.</value>
        public string Signature { get; set; }
        /// <summary>
        /// QQ好友列表
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public FriendList Friends { get; private set; }
    }
}
