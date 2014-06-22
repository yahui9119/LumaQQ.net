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
using System.Net;
using System.Net.Sockets;
namespace LumaQQ.NET.Net
{
    /// <summary>一些缺省的QQ端口定义
    /// 	<remark>abu 2008-03-07 </remark>
    /// </summary>
    public class QQPort
    {
        public static QQPort Main = new QQPort("Main");
        public static QQPort CLUSTER_CUSTOM_FACE = new QQPort("CLUSTER_CUSTOM_FACE");
        public static QQPort CUSTOM_HEAD_INFO = new QQPort("CUSTOM_HEAD_INFO");
        public static QQPort CUSTOM_HEAD_DATA = new QQPort("CUSTOM_HEAD_DATA");
        public static QQPort DISK = new QQPort("DISK");
        static Dictionary<string, QQPort> ports;
        static QQPort()
        {
            ports = new Dictionary<string, QQPort>();
            ports.Add(Main.Name, Main);
            ports.Add(CLUSTER_CUSTOM_FACE.Name, CLUSTER_CUSTOM_FACE);
            ports.Add(CUSTOM_HEAD_INFO.Name, CUSTOM_HEAD_INFO);
            ports.Add(CUSTOM_HEAD_DATA.Name, CUSTOM_HEAD_DATA);
            ports.Add(DISK.Name, DISK);
        }
        /// <summary>根据名称得到QQPort对象
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static QQPort GetPort(string name)
        {
            return ports[name];
        }
        public string Name { get; set; }
        QQPort(string name)
        {
            this.Name = name;
        }
        public IConnection Create(QQClient client, string serverHost, int port, bool start)
        {
            IConnection conn = null;
            ConnectionPolicy policy = null;
            EndPoint server = GetEndPoint(serverHost, port);
            switch (Name)
            {
                case "Main":
                    policy = new ConnectionPolicy(client, Name, ProtocolFamily.Basic, ProtocolFamily.Basic);
                    if (client.QQUser.IsUdp)
                    {
                        conn = client.ConnectionManager.ConnectionPool.NewUDPConnection(policy, server, start);
                    }
                    else
                    {
                        conn = client.ConnectionManager.ConnectionPool.NewTCPConnection(policy, server, start);
                    }
                    break;
                case "CLUSTER_CUSTOM_FACE": break;
                case "CUSTOM_HEAD_INFO": break;
                case "CUSTOM_HEAD_DATA": break;
                case "DISK": break;
                default:
                    break;
            }
            return conn;
        }
        public static IPEndPoint GetEndPoint(string host, int port)
        {
            IPAddress ipAddress;
            IPAddress.TryParse(host, out ipAddress);
            if (ipAddress == null)
            {
                try
                {

                    System.Net.IPHostEntry ipHostEntry = System.Net.Dns.GetHostEntry(host);
                    ipAddress = ipHostEntry.AddressList[0];
                }
                catch { }
            }
            Check.Require(ipAddress != null, "获取:" + host + " IP失败！");
            return new IPEndPoint(ipAddress, port);
        }
    }
}
