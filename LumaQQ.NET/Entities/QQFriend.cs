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
namespace LumaQQ.NET.Entities
{
    /// <summary>
    /// 好友的信息
    /// 	<remark>abu 2008-02-20 </remark>
    /// </summary>
    public class QQFriend
    {
        /// <summary>
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public int QQ { get; set; }
        /// <summary>
        /// 头像，参看ContactInfo的头像注释
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public Char Header { get; set; }
        /// <summary>
        /// 年龄
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public byte Age { get; set; }
        /// <summary>
        /// 性别
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public Gender Gender { get; set; }
        /// <summary>
        /// 昵称
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public string Nick { get; set; }

        /// <summary>
        /// // 用户属性标志
        /// bit1 => 会员
        /// bit5 => 移动QQ
        /// bit6 => 绑定到手机
        /// bit7 => 是否有摄像头
        /// bit18 => 是否TM登录
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint UserFlag { get; set; }

        /// <summary>
        /// true如果好友是会员，否则为false
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool IsMember()
        {
            return (UserFlag & QQGlobal.QQ_FLAG_MEMBER) != 0;
        }
        /// <summary>
        /// 是否绑定手机
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool IsBind()
        {
            return (UserFlag & QQGlobal.QQ_FLAG_BIND) != 0;
        }
        /// <summary>是否移动QQ
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool IsMobile()
        {
            return (UserFlag & QQGlobal.QQ_FLAG_MOBILE) != 0;
        }
        /// <summary>
        /// 用户是否有摄像头
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool HasCam()
        {
            return (UserFlag & QQGlobal.QQ_FLAG_CAM) != 0;
        }
        /// <summary>
        /// 用户是否使用TM登录
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool IsTM()
        {
            return (UserFlag & QQGlobal.QQ_FLAG_TM) != 0;
        }
        /// <summary>
        /// 是否是男性
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <returns></returns>
        public bool IsGG()
        {
            return Gender == Gender.GG;
        }
        /// <summary>
        /// 给定一个输入流，解析QQFriend结构
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 000-003: 好友QQ号
            QQ = buf.GetInt();
            // 004-005: 头像
            Header = buf.GetChar();
            // 006: 年龄
            Age = buf.Get();
            // 007: 性别
            Gender = (Gender)buf.Get();
            // 008: 昵称长度
            int len = (int)buf.Get();
            byte[] b = buf.GetByteArray(len);
            Nick = Util.GetString(b);
            // 用户属性
            UserFlag = buf.GetUInt();
        }
    }
}
