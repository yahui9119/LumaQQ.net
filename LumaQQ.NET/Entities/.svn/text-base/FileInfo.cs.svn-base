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

namespace LumaQQ.NET.Entities
{
    /// <summary>请求传送文件包的数据封装类，传送文件包是发送消息包的变种格式
    /// 	<remark>abu 2008-02-23 </remark>
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// 文件名
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <value></value>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <value></value>
        public int FileSize { get; set; }
        /// <summary>
        /// 给定一个输入流，解析SendFileRequest结构
        /// 	<remark>abu 2008-02-23 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 跳过空格符和分隔符
            buf.GetChar();
            // 获取后面的所有内容
            byte[] b = buf.GetByteArray(buf.Remaining());
            // 找到分隔符
            int i = Array.IndexOf<byte>(b, 0, (byte)0x1F);
            // 得到文件名
            FileName = Utils.Util.GetString(b, 0, i);
            // 得到文件大小的字符串形式
            String sizeStr = Utils.Util.GetString(b, i + 1, b.Length - 6 - i);
            FileSize = Utils.Util.GetInt(sizeStr, 0);
        }
    }
}
