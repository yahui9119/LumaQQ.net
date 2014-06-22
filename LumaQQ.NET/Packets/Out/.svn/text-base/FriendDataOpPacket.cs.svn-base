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
    ///  * 上传下载好友备注的消息包，格式为
    /// * 1. 头部
    /// * 2. 子命令，1字节
    /// * 3. 页号，1字节，从1开始，如果为0，表示此字段未用
    /// * 4. 操作对象的QQ号，4字节
    /// * 5. 未知1字节，0x00
    /// * 6. 以下为备注信息，一共7个域，域的顺序依次次是
    /// *    姓名、手机、电话、地址、邮箱、邮编、备注
    /// *    每个域都有一个前导字节，这个字节表示了这个域的字节长度
    /// * 7. 尾部
    /// * 
    /// * Note: 如果子命令是0x00(批量下载备注)，只有2，3部分
    /// * 		 如果子命令是0x01(上传备注)，所有部分都要，3部分未用
    /// *       如果子命令是0x02(删除好友)，仅保留1,2,4,7部分
    /// *       如果子命令是0x03(下载备注)，仅保留1,2,4,7部分
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class FriendDataOpPacket : BasicOutPacket
    {
        /// <summary>操作类型，上传还是下载
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <value></value>
        public FriendOpSubCmd SubCommand { get; set; }
        /// <summary> 操作的对象的QQ号
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <value></value>
        public int QQ { get; set; }
        /// <summary>好友备注对象
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <value></value>
        public FriendRemark Remark { get; set; }
        /// <summary>页号
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <value></value>
        public int Page { get; set; }

        public FriendDataOpPacket(QQUser user)
            : base(QQCommand.Friend_Data_OP, true, user)
        {
            SubCommand = FriendOpSubCmd.UPLOAD_FRIEND_REMARK;
            Remark = new FriendRemark();
        }
        public FriendDataOpPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            switch (SubCommand)
            {
                case FriendOpSubCmd.BATCH_DOWNLOAD_FRIEND_REMARK:
                    return "Friend Data Packet - Batch Download Remark";
                case FriendOpSubCmd.UPLOAD_FRIEND_REMARK:
                    return "Friend Data Packet - Upload Remark";
                case FriendOpSubCmd.REMOVE_FRIEND_FROM_LIST:
                    return "Friend Data Packet - Remove Friend From List";
                case FriendOpSubCmd.DOWNLOAD_FRIEND_REMARK:
                    return "Friend Data Packet - Download Remark";
                default:
                    return "Friend Data Packet - Unknown Sub Command";
            }
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 操作类型
            buf.Put((byte)SubCommand);
            // 未知字节0x0，仅在上传时
            if (SubCommand == FriendOpSubCmd.UPLOAD_FRIEND_REMARK || SubCommand == FriendOpSubCmd.BATCH_DOWNLOAD_FRIEND_REMARK)
                buf.Put((byte)Page);
            // 操作对象的QQ号
            if (SubCommand != FriendOpSubCmd.BATCH_DOWNLOAD_FRIEND_REMARK)
                buf.PutInt(QQ);
            // 后面的内容为一个未知字节0和备注信息，仅在上传时
            if (SubCommand == FriendOpSubCmd.UPLOAD_FRIEND_REMARK)
            {
                buf.Put((byte)0);
                // 备注信息
                // 姓名
                if (string.IsNullOrEmpty(Remark.Name))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Name);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
                // 手机
                if (string.IsNullOrEmpty(Remark.Mobile))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Mobile);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
                // 电话
                if (string.IsNullOrEmpty(Remark.Telephone))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Telephone);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
                // 地址
                if (string.IsNullOrEmpty(Remark.Address))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Address);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
                // 邮箱
                if (string.IsNullOrEmpty(Remark.Email))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Email);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
                // 邮编
                if (string.IsNullOrEmpty(Remark.Zipcode))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Zipcode);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
                // 备注
                if (string.IsNullOrEmpty(Remark.Note))
                    buf.Put((byte)0);
                else
                {
                    byte[] b = Utils.Util.GetBytes(Remark.Note);
                    buf.Put((byte)b.Length);
                    buf.Put(b);
                }
            }
        }
    }
}
