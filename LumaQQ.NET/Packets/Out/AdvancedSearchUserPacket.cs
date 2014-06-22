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
    ///  * 高级搜索用户的请求包：
    /// * 1. 头部
    /// * 2. 页数，从0开始，2字节
    /// * 3. 在线与否，1字节，0x01表示在线，0x00表示不在线
    /// * 4. 是否有摄像头，1字节，0x01表示有，0x00表示无，TX QQ 2004中的处理是如果要查找
    /// *    有摄像头的用户，则必须查找在线用户，不知道不这样行不行
    /// * 5. 年龄，1字节，表示在下拉框中的索引
    /// * 6. 性别，1字节，表示在下拉框中的索引
    /// * 7. 省份，2字节，表示在下拉框中的索引
    /// * 8. 城市，2字节，表示在下拉框中的索引
    /// * 9. 尾部
    /// 	<remark>abu 2008-02-27 </remark>
    /// </summary>
    public class AdvancedSearchUserPacket : BasicOutPacket
    {
        public bool SearchOnline { get; set; }
        public bool HasCam { get; set; }
        public ushort Page { get; set; }
        public byte AgeIndex { get; set; }
        public byte GenderIndex { get; set; }
        public ushort ProvinceIndex { get; set; }
        public ushort CityIndex { get; set; }

        public AdvancedSearchUserPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public AdvancedSearchUserPacket(QQUser user)
            : base(QQCommand.Advanced_Search, true, user)
        {
            SearchOnline = true;
            HasCam = false;
            Page = ProvinceIndex = CityIndex = 0;
            AgeIndex = GenderIndex = 0;
        }
        public override string GetPacketName()
        {
            return "Advanced Search Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 2. 页数，从0开始
            buf.PutUShort(Page);
            // 3. 在线与否，1字节，0x01表示在线，0x00表示不在线
            buf.Put(SearchOnline ? (byte)0x01 : (byte)0x00);
            // 4. 是否有摄像头，1字节，0x01表示有，0x00表示无，TX QQ 2004中的处理是如果要查找
            //   有摄像头的用户，则必须查找在线用户，不知道不这样行不行
            buf.Put(HasCam ? (byte)0x01 : (byte)0x00);
            // 5. 年龄，1字节，表示在下拉框中的索引
            buf.Put(AgeIndex);
            // 6. 性别，1字节，表示在下拉框中的索引
            buf.Put(GenderIndex);
            // 7. 省份，2字节，表示在下拉框中的索引
            buf.PutUShort(ProvinceIndex);
            // 8. 城市，2字节，表示在下拉框中的索引
            buf.PutUShort(CityIndex);
        }
    }
}
