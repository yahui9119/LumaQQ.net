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
namespace LumaQQ.NET
{
    /// <summary>
    /// QQ好友信息
    /// 	<remark>abu 2008-03-11 </remark>
    /// </summary>
    public class FriendInfo
    {
        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="basicInfo">The basic info.</param>
        public FriendInfo(QQFriend basicInfo)
        {
            this.BasicInfo = basicInfo;
        }
        /// <summary>
        /// 好友的基本信息
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public QQFriend BasicInfo { get; private set; }
        /// <summary>
        /// 好的状态信息
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public FriendStatus Status { get; set; }
        /// <summary>
        /// 用户是否在线
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <returns></returns>
        public QQStatus GetStatu()
        {
            if (Status == null)
            {
                return QQStatus.OFFLINE;
            }
            return Status.Status;
        }
    }

    public class FriendList : Dictionary<int, FriendInfo>
    {
        /// <summary>
        /// 在线好友数量
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public int Onlines { get; private set; }
        /// <summary>设置好友为上线状态
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        /// <param name="onlineEntry">The online entry.</param>
        public void SetFriendOnline(int qq, FriendOnlineEntry onlineEntry)
        {
            this[qq].Status = onlineEntry.Status;
            this.Onlines++;
        }
        /// <summary>设置好友为离线状态
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <param name="qq">The qq.</param>
        public void SetFriendOffline(int qq)
        {
            if (this[qq] != null)
            {
                if (this[qq].GetStatu() != QQStatus.OFFLINE)
                {
                    this.Onlines--;
                }
                if (this[qq].Status != null)
                {
                    this[qq].Status.Status = QQStatus.OFFLINE;
                }
            }

        }
    }
}
