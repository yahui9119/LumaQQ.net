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

namespace LumaQQ.NET
{
    /// <summary>
    ///  * 定义一些QQ用到的常量，常量的命名方式经过调整，统一为
    ///  * QQ_[类别]_[名称]
    ///  * 
    ///  * 比如表示长度的常量，为QQ_LENGTH_XXXXX
    ///  * 表示最大值的常量，为QQ_MAX_XXXX
    /// 	<remark>abu 2008-02-16 </remark>
    /// </summary>
    public static class QQGlobal
    {
        /// <summary>
        /// 包最大大小
        /// </summary>
        public const int QQ_MAX_PACKET_SIZE = 65535;
        /// <summary>
        ///  QQ缺省编码方式
        /// </summary>
        public const string QQ_CHARSET_DEFAULT = "GBK";

        /// <summary>
        ///  密钥长度 
        /// </summary>
        public const int QQ_LENGTH_KEY = 16;

        /// <summary>
        /// 登陆信息长度
        /// </summary>
        public const int QQ_LENGTH_LOGIN_DATA = 416;

        /// <summary>
        /// 程序缺省使用的客户端版本号
        /// </summary>
        public const char QQ_CLIENT_VERSION = QQ_CLIENT_VERSION_0E1B;

        /// <summary>
        /// 客户端版本号标志 - QQ2005 
        /// </summary>
        public const char QQ_CLIENT_VERSION_0E1B = (char)0x0E1B;

        /// <summary>
        /// 不需要确认的包的发送次数，这个值应该是随便的，由于QQ Logout包发了4次，所以我选4 
        /// </summary>
        public const int QQ_SEND_TIME_NOACK_PACKET = 4;

        // QQ包类型定义
        /// <summary>
        /// QQ基本协议族包头 
        /// </summary>
        public const byte QQ_HEADER_BASIC_FAMILY = 0x02;
        /** QQ P2P协议族 */
        public const byte QQ_HEADER_P2P_FAMILY = 0x00;
        /** 03协议族包头 */
        public const byte QQ_HEADER_03_FAMILY = 0x03;
        /** 04开头的协议族，未知含义，文件中转包有用到过 */
        public const byte QQ_HEADER_04_FAMILY = 0x04;
        /** 05协议族包头 */
        public const byte QQ_HEADER_05_FAMILY = 0x05;
        /** QQ基本协议族包尾 */
        public const byte QQ_TAIL_BASIC_FAMILY = 0x03;
        /** 05系列协议族包尾 */
        public const byte QQ_TAIL_05_FAMILY = 0x03;

        /** 基本协议族输入包的包头长度 */
        public const int QQ_LENGTH_BASIC_FAMILY_IN_HEADER = 7;
        /** 基本协议族输出包的包头长度 */
        public const int QQ_LENGTH_BASIC_FAMILY_OUT_HEADER = 11;
        /** 基本协议族包尾长度 */
        public const int QQ_LENGTH_BASIC_FAMILY_TAIL = 1;
        /** FTP协议族包头长度 */
        public const int QQ_LENGTH_FTP_FAMILY_HEADER = 46;
        /** 05协议族包头长度 */
        public const int QQ_LENGTH_05_FAMILY_HEADER = 13;
        /** 05协议族包尾长度 */
        public const int QQ_LENGTH_05_FAMILY_TAIL = 1;
        /** 网络硬盘协议族输入包包头长度 */
        public const int QQ_LENGTH_DISK_FAMILY_IN_HEADER = 82;
        /** 网络硬盘协议族输出包包头长度 */
        public const int QQ_LENGTH_DISK_FAMILY_OUT_HEADER = 154;

        /// <summary>
        /// 服务器端版本号 (不一定)
        /// 不一定真的是表示服务器端版本号，似乎和发出的包不同，这个有其他的含义，
        /// 感觉像是包的类型标志
        /// </summary>
        public const char QQ_SERVER_VERSION_0100 = (char)0x0100;

        /** 群成员角色标志位 - 管理员 */
        public const int QQ_ROLE_ADMIN = 0x01;
        /** 群成员角色标志位 - 股东 */
        public const int QQ_ROLE_STOCKHOLDER = 0x02;

        // 用户标志，比如QQFriend类，好友状态改变包都包含这样的标志
        /** 有摄像头 */
        public const int QQ_FLAG_CAM = 0x80;
        /** 绑定了手机 */
        public const int QQ_FLAG_BIND = 0x40;
        /** 移动QQ用户 */
        public const int QQ_FLAG_MOBILE = 0x20;
        /** 会员 */
        public const int QQ_FLAG_MEMBER = 0x02;
        /** TM登录 */
        public const int QQ_FLAG_TM = 0x40000;

        /** 好友列表从第一个好友开始 */
        public const ushort QQ_POSITION_FRIEND_LIST_START = 0x0000;
        /** 好友列表已经全部得到 */
        public const ushort QQ_POSITION_FRIEND_LIST_END = 0xFFFF;
        /** 在线好友列表从第一个好友开始 */
        public const byte QQ_POSITION_ONLINE_LIST_START = 0x00;
        /** 在线好友列表已经全部得到 */
        public const byte QQ_POSITION_ONLINE_LIST_END = (byte)0xFF;
        /** 用户属性列表从第一个好友开始 */
        public const ushort QQ_POSITION_USER_PROPERTY_START = 0x0000;
        /** 用户属性列表结束 */
        public const ushort QQ_POSITION_USER_PROPERTY_END = 0xFFFF;


        // 以下常量用于QQ短信功能
        /** 短消息发送者最大名称字节长度 */
        public const int QQ_MAX_SMS_SENDER_NAME = 13;
        /** 接受者手机号最大长度 */
        public const int QQ_MAX_SMS_MOBILE_LENGTH = 18;
        /** 短信发送时，发送者名称和短信内容的字符数之和的最大值 */
        public const int QQ_MAX_SMS_LENGTH = 58;
        /** 发送模式 - 免提短信 */
        public const byte QQ_SMS_MODE_HAND_FREE = 0x20;
        /** 发送模式 - 普通 */
        public const byte QQ_SMS_MODE_NORMAL = 0x00;
        /** 短消息内容 - 普通短消息 */
        public const byte QQ_SMS_CONTENT_NORMAL = 0x00;
        /** 短消息内容 - 言语传情 */
        public const byte QQ_SMS_CONTENT_LOVE_WORD = 0x01;
        /** 短消息内容 - 精美图片 */
        public const byte QQ_SMS_CONTENT_PICTURE = 0x02;
        /** 短消息内容 - 悦耳铃声 */
        public const byte QQ_SMS_CONTENT_RING = 0x03;

        // QQ直播消息类型
        /** 网络硬盘通知 */
        public const ushort QQ_LIVE_IM_TYPE_DISK = 0x0400;

        // 用户属性，在UserProperty中，相关命令0x0065
        /** 有个性签名 */
        public const int QQ_FLAG_HAS_SIGNATURE = 0x40000000;
        /** 有自定义头像 */
        public const int QQ_FLAG_HAS_CUSTOM_HEAD = 0x100000;


        // 和虚拟摄像头有关系
        /** 显示虚拟摄像头 */
        public const int QQ_CAM_SHOW_FAKE = 1;
        /** 隐藏虚拟摄像头 */
        public const int QQ_CAM_DONT_SHOW_FAKE = 0;


        /** QQ分组的名称最大字节长度，注意一个汉字是两个字节 */
        public const int QQ_MAX_GROUP_NAME = 16;
        /** QQ昵称的最长长度 */
        public const int QQ_MAX_NAME_LENGTH = 250;
        /** QQ缺省表情个数 */
        public const int QQ_COUNT_DEFAULT_FACE = 96;
        /** 得到用户信息的回复包字段个数 */
        public const int QQ_COUNT_GET_USER_INFO_FIELD = 37;
        /// <summary>
        /// 修改用户信息的请求包字段个数，比实际的多1，最开始的QQ号不包括
        /// </summary>
        public const int QQ_COUNT_MODIFY_USER_INFO_FIELD = 35;
        /** 用户备注信息的字段个数 */
        public const int QQ_COUNT_REMARK_FIELD = 7;


        /** QQ登录包中16到51字节的固定内容 */
        public static byte[] QQ_LOGIN_16_51 = new byte[] {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x00, 0x00, (byte)0x86, (byte)0xCC, 0x4C, 0x35, 0x2C, 
            (byte)0xD3, 0x73, 0x6C, 0x14, (byte)0xF6, (byte)0xF6, (byte)0xAF, (byte)0xC3, 
            (byte)0xFA, 0x33, (byte)0xA4, 0x01
    };

        /** QQ登录包中53到68字节的固定内容 */
        public static byte[] QQ_LOGIN_53_68 = new byte[] {
            (byte)0x8D, (byte)0x8B, (byte)0xFA, (byte)0xEC, (byte)0xD5, 0x52, 0x17, 0x4A, 
            (byte)0x86, (byte)0xF9, (byte)0xA7, 0x75, (byte)0xE6, 0x32, (byte)0xD1, 0x6D
    };

        /** QQ登录包中的未知固定内容 */
        public static byte[] QQ_LOGIN_SEGMENTS = new byte[] {
            0x0B, 0x04, 0x02, 0x00, 0x01, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x03, 0x09, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x01, (byte)0xE9, 0x03,
            0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, (byte)0xF3,
            0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 
            (byte)0xED, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x01, (byte)0xEC, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 
            0x00, 0x03, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x03, 0x07, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x01, (byte)0xEE, 0x03, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x01, (byte)0xEF, 0x03, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x01, (byte)0xEB, 0x03,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00
    };
        /** QQ UDP缺省端口 */
        public const int QQ_PORT_UDP = 8000;
        /** QQ TCP缺省端口 */
        public const int QQ_PORT_TCP = 80;
        /** 使用HTTP代理时连接QQ服务器的端口 */
        public const int QQ_PORT_HTTP = 443;

        /** 单位: ms */
        public const long QQ_TIMEOUT_SEND = 5000;
        /** 最大重发次数 */
        public const int QQ_MAX_RESEND = 5;
        /** Keep Alive包发送间隔，单位: ms */
        public const long QQ_INTERVAL_KEEP_ALIVE = 100000;
    }

    /// <summary>
    /// 联系方法的可见类型
    /// </summary>
    public enum OpenContact
    {
        /// <summary>
        /// 完全公开 
        /// </summary>
        Open = 0,
        /// <summary>
        /// 仅好友可见
        /// </summary>
        Friends = 1,
        /// <summary>
        /// 完全保密 
        /// </summary>
        Close = 2
    }

    /// <summary>
    /// 认证类型，加一个人为好友时是否需要验证等等
    /// </summary>
    public enum AuthType : byte
    {
        /// <summary>
        /// 不需认证
        /// </summary>
        No = 0,
        /// <summary>
        /// 需要认证
        /// </summary>
        Need = 1,
        /// <summary>
        /// 对方拒绝加好友
        /// </summary>
        Reject = 2
    }

    /// <summary>
    /// 登录模式
    /// </summary>
    public enum LoginMode : byte
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0x0A,
        /// <summary>
        /// 隐身
        /// </summary>
        Hidden = 0x28
    }

    /// <summary>
    /// 协议族标识
    /// </summary>
    [Flags]
    public enum ProtocolFamily : uint
    {
        /// <summary>
        /// 基本协议族
        /// </summary>
        Basic = 0x01,
        /// <summary>
        /// 05开头的协议族，目前发现的用途
        /// </summary>
        _05 = 0x2,
        /// <summary>
        /// 03开头的协议族，目前发现的用途
        /// </summary>
        _03 = 0x4,
        /// <summary>
        /// Disk协议族，用来访问网络硬盘
        /// </summary>
        Disk = 0x8,
        /// <summary>
        /// 所有协议族
        /// </summary>
        All = 0xFFFFFFFF
    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public enum QQCommand : ushort
    {
        /// <summary>
        ///  登出
        /// </summary>
        Logout = 0x0001,
        /// <summary>
        /// 保持在线状态
        /// </summary>
        Keep_Alive = 0x0002,
        /// <summary>
        /// 修改自己的信息
        /// </summary>
        Modify_Info = 0x0004,
        /// <summary>
        /// 查找用户
        /// </summary>
        Search_User = 0x0005,
        /// <summary>
        ///  得到好友信息 
        /// </summary>
        Get_UserInfo = 0x0006,

        /// <summary>
        /// 删除一个好友
        /// </summary>
        Delete_Friend = 0x000A,
        /// <summary>
        /// 发送验证信息
        /// </summary>
        Add_Friend_Auth = 0x000B,
        /// <summary>
        /// 改变自己的在线状态
        /// </summary>
        Change_Status = 0x000D,
        /// <summary>
        /// 确认收到了系统消息
        /// </summary>
        Ack_Sys_Msg = 0x0012,
        /// <summary>
        /// 发送消息
        /// </summary>
        Send_IM = 0x0016,
        /// <summary>
        /// 接收消息
        /// </summary>
        Recv_IM = 0x0017,
        /// <summary>
        /// 把自己从对方好友名单中删除
        /// </summary>
        Remove_Self = 0x001c,
        /// <summary>
        /// 请求一些操作需要的密钥，比如文件中转，视频也有可能 
        /// </summary>
        Request_Key = 0x001d,
        /// <summary>
        /// 登陆
        /// </summary>
        Login = 0x0022,
        /// <summary>
        /// 得到好友列表 
        /// </summary>
        Get_Friend_List = 0x0026,
        /// <summary>
        /// 得到在线好友列表
        /// </summary>
        Get_Online_OP = 0x0027,
        /// <summary>
        /// 发送短消息
        /// </summary>
        Send_SMS = 0x002d,
        /// <summary>
        /// 群相关命令
        /// </summary>
        Cluster_Cmd = 0x0030,
        /// <summary>
        /// 测试连接
        /// </summary>
        Test = 0x0031,
        /// <summary>
        /// 分组数组操作
        /// </summary>
        Group_Data_OP = 0x003C,
        /// <summary>
        /// 上传分组中的好友QQ号列表 
        /// </summary>
        Upload_Group_Friend = 0x003D,
        /// <summary>
        /// 好友相关数据操作
        /// </summary>
        Friend_Data_OP = 0x003E,
        /// <summary>
        /// 下载分组中的好友QQ号列表 
        /// </summary>
        Download_Group_Friend = 0x0058,
        /// <summary>
        /// 好友等级信息相关操作
        /// </summary>
        Friend_Level_OP = 0x005C,
        /// <summary>
        /// 隐私数据操作 
        /// </summary>
        Privacy_Data_OP = 0x005E,
        /// <summary>
        /// 群数据操作命令
        /// </summary>
        Cluster_Data_OP = 0x005F,
        /// <summary>
        /// 好友高级查找 
        /// </summary>
        Advanced_Search = 0x0061,
        /// <summary>
        /// 请求登录令牌
        /// </summary>
        Request_Login_Token = 0x0062,
        /// <summary>
        /// 用户属性操作 
        /// </summary>
        User_Property_OP = 0x0065,
        /// <summary>
        /// 临时会话操作
        /// </summary>
        Temp_Session_OP = 0x0066,
        /// <summary>
        /// 个性签名的操作 
        /// </summary>
        Signature_OP = 0x0067,
        /// <summary>
        /// 接收到系统消息
        /// </summary>
        Recv_Msg_Sys = 0x0080,
        /// <summary>
        /// 好友改变状态 
        /// </summary>
        Recv_Msg_Friend_Change_Status = 0x0081,
        /// <summary>
        /// 天气操作
        /// </summary>
        Weather_OP = 0x00A6,
        /// <summary>
        /// QQ2005使用的添加好友命令
        /// </summary>
        Add_Friend_Ex = 0x00A7,
        /// <summary>
        /// 发送验证消息
        /// </summary>
        Authorize = 0x00A8,
        /// <summary>
        /// 未知命令，调试用途 
        /// </summary>
        Unknown = 0xFFFF,

    }

    /// <summary>
    ///  命令常量
    /// </summary>
    public enum _05Command : ushort
    {
        /// <summary>
        ///  请求中转
        /// </summary>
        _05_REQUEST_AGENT = 0x0021,
        /// <summary>
        /// 请求得到自定义表情
        /// </summary>
        _05_REQUEST_FACE = 0x0022,
        /// <summary>
        /// 开始传送
        /// </summary>
        _05_TRANSFER = 0x0023,
        /// <summary>
        /// 请求开始传送
        /// </summary>
        _05_REQUEST_BEGIN = 0x0026
    }
    /// <summary>
    /// 回复常量
    /// </summary>
    public enum ReplyCode : byte
    {
        /// <summary>
        /// 通用常量，操作成功
        /// </summary>
        OK = 0x00,
        /// <summary>
        /// 对方已经是我的好友
        /// </summary>
        ADD_FRIEND_ALREADY = (byte)0x99,
        /// <summary>
        /// 请求登录令牌成功
        /// </summary>
        REQUEST_LOGIN_TOKEN_OK = 0x00,
        /// <summary>
        /// 登录信息-重定向
        /// </summary>
        LOGIN_REDIRECT = 0x01,
        /// <summary>
        /// 登录信息-登录失败
        /// </summary>
        LOGIN_FAIL = 0x05,
        /// <summary>
        /// 改变在线状态成功
        /// </summary>
        CHANGE_STATUS_OK = 0x30,
        /// <summary>
        /// 发送认证消息成功
        /// </summary>
        ADD_FRIEND_AUTH_OK = 0x30,
        /// <summary>
        /// 高级搜索结束，没有更多数据
        /// </summary>
        ADVANCED_SEARCH_END = 1,
        /// <summary>
        /// 申请中转服务器，重定向
        /// </summary>
        REQUEST_AGENT_REDIRECT = 0x0001,
        /// <summary>
        /// 申请中转服务器成功
        /// </summary>
        REQUEST_AGENT_OK = 0x0000,
        /// <summary>
        /// 要发送的图片太大
        /// </summary>
        REQUEST_AGENT_TOO_LONG = 0x0003
    }

    /// <summary>
    /// 群操作命令
    /// </summary>
    public enum ClusterCommand : byte
    {
        /// <summary>
        /// 创建群
        /// </summary>
        CREATE_CLUSTER = 0x01,
        /// <summary>
        /// 修改群成员
        /// </summary>
        MODIFY_MEMBER = 0x02,
        /// <summary>
        /// 修改群资料
        /// </summary>
        MODIFY_CLUSTER_INFO = 0x03,
        /// <summary>
        /// 得到群资料
        /// </summary>
        GET_CLUSTER_INFO = 0x04,
        /// <summary>
        /// 激活群 
        /// </summary>
        ACTIVATE_CLUSTER = 0x05,
        /// <summary>
        /// 搜索群
        /// </summary>
        SEARCH_CLUSTER = 0x06,
        /// <summary>
        /// 加入群 
        /// </summary>
        JOIN_CLUSTER = 0x07,
        /// <summary>
        /// 加入群的验证消息 
        /// </summary>
        JOIN_CLUSTER_AUTH = 0x08,
        /// <summary>
        /// 退出群
        /// </summary>
        EXIT_CLUSTER = 0x09,
        /// <summary>
        /// 得到在线成员
        /// </summary>
        GET_ONLINE_MEMBER = 0x0B,
        /// <summary>
        /// 得到成员资料 
        /// </summary>
        GET_MEMBER_INFO = 0x0C,
        /// <summary>
        /// 修改群名片
        /// </summary>
        MODIFY_CARD = 0x0E,
        /// <summary>
        /// 批量得到成员群名片中的真实姓名
        /// </summary>
        GET_CARD_BATCH = 0x0F,
        /// <summary>
        /// 得到某个成员的群名片
        /// </summary>
        GET_CARD = 0x10,
        /// <summary>
        /// 提交组织架构到服务器 
        /// </summary>
        COMMIT_ORGANIZATION = 0x11,
        /// <summary>
        /// 从服务器获取组织架构
        /// </summary>
        UPDATE_ORGANIZATION = 0x12,
        /// <summary>
        /// 提交成员分组情况到服务器
        /// </summary>
        COMMIT_MEMBER_ORGANIZATION = 0x13,
        /// <summary>
        /// 得到各种version id
        /// </summary>
        GET_VERSION_ID = 0x19,
        /// <summary>
        /// 扩展格式的群消息
        /// </summary>
        SEND_IM_EX = 0x1A,
        /// <summary>
        /// 设置成员角色
        /// </summary>
        SET_ROLE = 0x1B,
        /// <summary>
        /// 转让自己的角色给他人
        /// </summary>
        TRANSFER_ROLE = 0x1C,
        /// <summary>
        /// 解散群，如果自己是群的创建者，则使用这个命令
        /// </summary>
        DISMISS_CLUSTER = 0x1D,
        /// <summary>
        /// 创建临时群 
        /// </summary>
        CREATE_TEMP = 0x30,
        /// <summary>
        /// 修改临时群成员列表 
        /// </summary>
        MODIFY_TEMP_MEMBER = 0x31,
        /// <summary>
        /// 退出临时群 
        /// </summary>
        EXIT_TEMP = 0x32,
        /// <summary>
        /// 得到临时群资料 
        /// </summary>
        GET_TEMP_INFO = 0x33,
        /// <summary>
        /// 修改临时群资料
        /// </summary>
        MODIFY_TEMP_INFO = 0x34,
        /// <summary>
        /// 发送临时群消息 
        /// </summary>
        SEND_TEMP_IM = 0x35,
        /// <summary>
        /// 子群操作
        /// </summary>
        SUB_CLUSTER_OP = 0x36,
        /// <summary>
        /// 激活临时群 
        /// </summary>
        ACTIVATE_TEMP = 0x37
    }

    /// <summary>
    /// 群类型常量
    /// </summary>
    public enum ClusterType : byte
    {
        /// <summary>
        /// 固定群
        /// </summary>
        PERMANENT = 0x01,
        /// <summary>
        /// 多人对话
        /// </summary>
        DIALOG = 0x01,
        /// <summary>
        /// 讨论组
        /// </summary>
        SUBJECT = 0x02
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender : byte
    {
        /// <summary>
        /// 男
        /// </summary>
        GG = 0,
        /// <summary>
        /// 女
        /// </summary>
        MM = 1,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = (byte)0xFF
    }

    /// <summary>
    /// 这两个常量用在下载好友分组时
    /// </summary>
    public enum FriendType : byte
    {
        /// <summary>
        ///  号码代表一个用户
        /// </summary>
        IS_FRIEND = 0x1,
        /// <summary>
        /// 号码是一个群
        /// </summary>
        IS_CLUSTER = 0x4
    }

    /// <summary>
    ///  好友操作子命令常量，服务于命令0x003E 。
    /// </summary>
    public enum FriendOpSubCmd : byte
    {
        /// <summary>
        /// 批量下载好友备注
        /// </summary>
        BATCH_DOWNLOAD_FRIEND_REMARK = 0x0,
        /// <summary>
        ///  上传好友备注
        /// </summary>
        UPLOAD_FRIEND_REMARK = 0x1,
        /// <summary>
        /// 添加好友到列表中
        /// </summary>
        REMOVE_FRIEND_FROM_LIST = 0x2,
        /// <summary>
        /// 下载好友备注
        /// </summary>
        DOWNLOAD_FRIEND_REMARK = 0x3
    }

    /// <summary>
    /// 在线状态
    /// </summary>
    public enum QQStatus : byte
    {
        /// <summary>
        /// 在线
        /// </summary>
        ONLINE = 0x0A,
        /// <summary>
        /// 离线
        /// </summary>
        OFFLINE = 0x14,
        /// <summary>
        /// 离开
        /// </summary>
        AWAY = 0x1E,
        /// <summary>
        /// 隐身
        /// </summary>
        HIDDEN = 0x28
    }

    /// <summary>
    /// 组操作子命令常量，服务于命令0x003C
    /// </summary>
    public enum GroupSubCmd : byte
    {
        /// <summary>
        /// 下载组名 
        /// </summary>
        DOWNLOAD = 0x1,
        /// <summary>
        /// 上传组名
        /// </summary>
        UPLOAD = 0x2
    }

    /// <summary>
    /// 消息编码，好像可以自己胡乱定义
    /// </summary>
    public enum Charset : ushort
    {
        /// <summary>
        /// 
        /// </summary>
        GB = 0x8602,
        /// <summary>
        /// 
        /// </summary>
        EN = 0x0000,
        /// <summary>
        /// 
        /// </summary>
        BIG5 = 0x8603
    }

    /// <summary>
    /// 请求传送文件消息中的一个标志字节，传输类型之后那个，意思不明，姑且这样
    /// </summary>
    public enum FileConnectMode : byte
    {
        /// <summary>
        /// UDP，可能不是这意思
        /// </summary>
        UDP = 0,
        /// <summary>
        /// 直接UDP，可能不是这意思
        /// </summary>
        DIRECT_UDP = 1,
        /// <summary>
        /// TCP，可能不是这意思
        /// </summary>
        TCP = 2,
        /// <summary>
        /// 直接TCP，可能不是这意思 
        /// </summary>
        DIRECT_TCP = 3
    }

    /// <summary>
    /// 传输类型
    /// </summary>
    public enum TransferType : byte
    {
        /// <summary>
        /// 传输文件
        /// </summary>
        FILE = 0x65,
        /// <summary>
        /// 传输自定义表情
        /// </summary>
        FACE = 0x6B
    }

    /// <summary>
    ///  消息类型，就是ReceiveIMHeader中的类型，对于有些类型，我们做为通知来处理
    ///  而不是显示在消息窗口中，比如请求加入，验证之类的消息
    /// </summary>
    public enum RecvSource : ushort
    {
        /// <summary>
        /// 来自好友的消息
        /// </summary>
        FRIEND = 0x0009,
        /// <summary>
        /// 来自陌生人的消息
        /// </summary>
        STRANGER = 0x000A,
        /// <summary>
        /// 手机短消息 - 普通绑定用户
        /// </summary>
        BIND_USER = 0x000B,
        /// <summary>
        /// 手机短消息 - 普通手机 
        /// </summary>
        MOBILE = 0x000C,
        /// <summary>
        /// 会员登录提示，这个消息基本没内容，就是用来提醒你是会员，可以显示一个窗口来告诉你上次登录时间和ip
        /// </summary>
        MEMBER_LOGIN_HINT = 0x0012,
        /// <summary>
        /// 手机短消息 - 移动QQ用户 
        /// </summary>
        MOBILE_QQ = 0x0013,
        /// <summary>
        /// 手机短消息 - 移动QQ用户(使用手机号描述)
        /// </summary>
        MOBILE_QQ_2 = 0x0014,
        /// <summary>
        /// QQ直播消息
        /// </summary>
        QQLIVE = 0x0018,
        /// <summary>
        /// 好友属性改变通知
        /// </summary>
        PROPERTY_CHANGE = 0x001E,
        /// <summary>
        /// 临时会话消息
        /// </summary>
        TEMP_SESSION = 0x001F,
        /// <summary>
        /// 未知类型的群消息，在2003时是普通群消息 
        /// </summary>
        UNKNOWN_CLUSTER = 0x0020,
        /// <summary>
        /// 通知我被加入到一个群，这个群先前已经建立，我是后来被加的
        /// </summary>
        ADDED_TO_CLUSTER = 0x0021,
        /// <summary>
        /// 我被踢出一个群 
        /// </summary>
        DELETED_FROM_CLUSTER = 0x0022,
        /// <summary>
        /// 有人请求加入群
        /// </summary>
        REQUEST_JOIN_CLUSTER = 0x0023,
        /// <summary>
        /// 同意对方加入群 
        /// </summary>
        APPROVE_JOIN_CLUSTER = 0x0024,
        /// <summary>
        /// 拒绝对方加入群 
        /// </summary>
        REJECT_JOIN_CLUSTER = 0x0025,
        /// <summary>
        /// 通知我被加入到一个群，我是在群被创建的时候就被加的 
        /// </summary>
        CREATE_CLUSTER = 0x0026,
        /// <summary>
        /// 临时群消息 
        /// </summary>
        TEMP_CLUSTER = 0x002A,
        /// <summary>
        /// 固定群消息 
        /// </summary>
        CLUSTER = 0x002B,
        /// <summary>
        /// 群通知 
        /// </summary>
        CLUSTER_NOTIFICATION = 0x002C,
        /// <summary>
        /// 收到的系统消息
        /// </summary>
        SYS_MESSAGE = 0x0030,
        /// <summary>
        /// 收到个性签名改变通知 
        /// </summary>
        SIGNATURE_CHANGE = 0x0041,
        /// <summary>
        /// 收到自定义头像变化通知
        /// </summary>
        CUSTOM_HEAD_CHANGE = 0x0049
    }

    /// <summary>
    /// 消息类型，这个类型比上面的类型又再低一级，他们基本从属于QQ_RECV_IM_FRIEND
    /// 所以他们是normalIMHeader中的类型
    /// </summary>
    public enum NormalIMType : ushort
    {
        /// <summary>
        /// 普通文件消息 
        /// </summary>
        TEXT = 0x000B,
        /// <summary>
        /// 一个TCP连接请求
        /// </summary>
        TCP_REQUEST = 0x0001,
        /// <summary>
        /// 接收TCP连接请求
        /// </summary>
        ACCEPT_TCP_REQUEST = 0x0003,
        /// <summary>
        /// 拒绝TCP连接请求
        /// </summary>
        REJECT_TCP_REQUEST = 0x0005,
        /// <summary>
        /// UDP连接请求
        /// </summary>
        UDP_REQUEST = 0x0035,
        /// <summary>
        /// 接受UDP连接请求
        /// </summary>
        ACCEPT_UDP_REQUEST = 0x0037,
        /// <summary>
        /// 拒绝UDP连接请求
        /// </summary>
        REJECT_UDP_REQUEST = 0x0039,
        /// <summary>
        /// 通知文件传输端口
        /// </summary>
        NOTIFY_IP = 0x003B,
        /// <summary>
        /// 请求对方主动连接
        /// </summary>
        ARE_YOU_BEHIND_FIREWALL = 0x003F,
        /// <summary>
        /// 未知含意
        /// </summary>
        ARE_YOU_BEHIND_PROXY = 0x0041,
        /// <summary>
        /// 未知含意，0x0041的回复
        /// </summary>
        YES_I_AM_BEHIND_PROXY = 0x0042,
        /// <summary>
        /// 通知文件中转服务器信息
        /// </summary>
        NOTIFY_FILE_AGENT_INFO = 0x004B,
        /// <summary>
        /// 取消TCP或者UDP连接请求
        /// </summary>
        REQUEST_CANCELED = 0x0049
    }

    /// <summary>
    /// 消息来源，主要在ReceiveIMPacket中使用，和协议关系不大
    /// </summary>
    public enum IMFrom
    {
        /// <summary>
        /// 来自好友
        /// </summary>
        USER = 0,
        /// <summary>
        /// 来自系统
        /// </summary>
        SYS = 1,
        /// <summary>
        /// 来自群
        /// </summary>
        CLUSTER = 2,
        /// <summary>
        /// 来自短消息
        /// </summary>
        SMS = 3,
        /// <summary>
        /// 来自临时会话
        /// </summary>
        TEMP_SESSION = 4
    }

    /// <summary>
    /// 子命令常量，用于子命令0x0067
    /// </summary>
    public enum SignatureSubCmd : byte
    {
        /// <summary>
        /// 修改个性签名
        /// </summary>
        MODIFY = 0x01,
        /// <summary>
        /// 删除个性签名
        /// </summary>
        DELETE = 0x02,
        /// <summary>
        /// 得到个性签名
        /// </summary>
        GET = 0x03,
    }

    /// <summary>
    /// 系统通知的类型
    /// </summary>
    public enum SystemMessageType : int
    {
        /// <summary>
        /// 自己被别人加为好友
        /// </summary>
        BEING_ADDED = 1,
        /// <summary>
        /// 对方请求加你为好友
        /// 当对方不使用0x00A8命令发送认证消息，才会收到此系统通知
        /// </summary>
        ADD_FRIEND_REQUEST = 2,
        /// <summary>
        /// 同意对方加自己为好友
        /// </summary>
        ADD_FRIEND_APPROVED = 3,
        /// <summary>
        /// 拒绝对方加自己为好友 
        /// </summary>
        ADD_FRIEND_REJECTED = 4,
        /// <summary>
        /// 广告 
        /// </summary>
        ADVERTISEMENT = 6,
        /// <summary>
        /// 未知含意 
        /// </summary>
        UPDATE_HINT = 9,
        /// <summary>
        /// 对方把你加为了好友 
        /// </summary>
        BEING_ADDED_EX = 40,
        /// <summary>
        /// 对方请求加你为好友
        /// 当对方使用0x00A8命令发送认证消息，才会收到此系统通知
        /// </summary>
        ADD_FRIEND_REQUEST_EX = 41,
        /// <summary>
        /// 同意对方加自己为好友，同时加对方为好友
        /// </summary>
        ADD_FRIEND_APPROVED_AND_ADD = 43
    }

    /// <summary>
    /// 子命令常量，用于命令0x0066
    /// </summary>
    public enum TempSessionSubCmd : byte
    {
        /// <summary>
        /// 临时会话操作 - 发送临时会话消息
        /// </summary>
        SendIM = 0x01
    }
    /// <summary>
    ///  子命令，用于0x0065
    /// </summary>
    public enum UserPropertySubCmd : byte
    {
        /// <summary>
        /// 得到用户属性
        /// </summary>
        GET = 0x1
    }

    /// <summary>
    /// 群的搜索方式 
    /// </summary>
    public enum ClusterSearchType : byte
    {
        /// <summary>
        /// 根据群号搜索 
        /// </summary>
        By_ID = 0x01,
        /// <summary>
        /// 搜索示范群
        /// </summary>
        Demo = 0x02
    }

    /// <summary>
    /// 以下常量用于消息中的表情，对于自定义表情的表示格式参考NormalIM.java的注释
    /// </summary>
    public enum FaceType : byte
    {
        /// <summary>
        /// 系统自带表情前导字节
        /// </summary>
        DEFAULT = 0x14,
        /// <summary>
        ///  自定义表情前导字节
        /// </summary>
        CUSTOM = 0x15,
        /// <summary>
        /// 新自定义表情，普通格式 
        /// </summary>
        NEW_CUSTOM = 0x33,
        /// <summary>
        /// 已经出现过的自定义表情 
        /// </summary>
        EXISTING_CUSTOM = 0x34,
        /// <summary>
        /// 新自定义表情，存储在服务器端
        /// </summary>
        NEW_SERVER_SIDE_CUSTOM = 0x36,
        /// <summary>
        /// 已经出现过的服务器端自定义表情 
        /// </summary>
        EXISTING_SERVER_SIDE_CUSTOM_SIDE = 0x37,
        /// <summary>
        /// 未知自定义表情格式描述1，未知含义
        /// </summary>
        UNKNOWN_1 = 0x38,
        /// <summary>
        /// 未知自定义表情格式描述2，未知含义 
        /// </summary>
        UNKNOWN_2 = 0x39
    }

    /// <summary>
    /// 群操作子命令
    /// </summary>
    public enum ClusterSubCmd : byte
    {
        /// <summary>
        /// 添加成员，用在修改成员列表命令中
        /// </summary>
        ADD_MEMBER = 0x01,
        /// <summary>
        /// 删除成员，用在修改成员列表命令中
        /// </summary>
        REMOVE_MEMBER = 0x02,
        /// <summary>
        /// 得到群内的讨论组列表
        /// </summary>
        GET_SUBJECT_LIST = 0x02,
        /// <summary>
        /// 得到多人对话列表
        /// </summary>
        GET_DIALOG_LIST = 0x01
    }

    /// <summary>
    /// 0x005C 好友等级的子命令
    /// </summary>
    public enum FriendLevelSubCmd : byte
    {
        /// <summary>
        /// 得到好友等级信息
        /// </summary>
        GET = 0x02
    }
    /// <summary>
    /// 好友列表排序
    /// </summary>
    public enum FriendListSort : byte
    {
        /// <summary>
        /// 不对得到的好友列表排序
        /// </summary>
        Unsorted = 0,
        /// <summary>
        /// 对得到的好友列表排序 
        /// </summary>
        Sorted = 1
    }
    /// <summary>
    /// 子命令常量，用于命令0x0027
    /// </summary>
    public enum GetOnlineSubCmd : byte
    {
        /// <summary>
        /// 得到在线好友
        /// </summary>
        GET_ONLINE_FRIEND = 0x2,
        /// <summary>
        /// 得到系统服务
        /// </summary>
        GET_ONLINE_SERVICE = 0x3
    }
    /// <summary>
    /// 这是搜索用户时指定的搜索类类型，比如是查看全部在线用户，还是自定义查找
    /// </summary>
    public enum FriendSearchType : byte
    {
        /// <summary>
        /// 看谁在线上
        /// </summary>
        SEARCH_ALL = 0x31,
        /// <summary>
        /// 自定义搜索
        /// </summary>
        SEARCH_CUSTOM = 0x30
    }

    /// <summary>
    /// 消息回复类型
    /// </summary>
    public enum ReplyType : byte
    {
        /// <summary>
        /// 正常回复
        /// </summary>
        NORMAL = 0x01,
        /// <summary>
        /// 自动回复
        /// </summary>
        AUTO = 0x02
    }

    /// <summary>
    /// 短消息内容
    /// </summary>
    public enum SMSContentType : byte
    {
        /// <summary>
        /// 普通短消息
        /// </summary>
        NORMAL = 0x00,
        /// <summary>
        /// 言语传情
        /// </summary>
        LOVE_WORD = 0x01,
        /// <summary>
        /// 精美图片
        /// </summary>
        PICTURE = 0x02,
        /// <summary>
        /// 悦耳铃声
        /// </summary>
        RING = 0x03
    }

    /// <summary>
    /// 发送模式
    /// </summary>
    public enum SMSSendMode : byte
    {
        /// <summary>
        ///  免提短信
        /// </summary>
        HAND_FREE = 0x20,
        /// <summary>
        /// 普通
        /// </summary>
        NORMAL = 0x00
    }

    /// <summary>
    /// 子命令，用于0x00A6
    /// </summary>
    public enum WeatherSubCmd : byte
    {
        /// <summary>
        /// 得到天气数据 
        /// </summary>
        Get = 0x01
    }

    /// <summary>
    /// 系统消息类别
    /// </summary>
    public enum SystemIMType : byte
    {
        /// <summary>
        /// 同一个QQ号在其他地方登录，我被踢出
        /// </summary>
        QQ_RECV_IM_KICK_OUT = 0x01
    }

    /// <summary>
    /// 这三个常量用在添加好友认证的包中，表示你是请求，或者你拒绝还是同意别人的请求
    /// </summary>
    public enum AuthAction : byte
    {
        /// <summary>
        ///  通过认证
        /// </summary>
        Approve = 0x30,
        /// <summary>
        /// 拒绝认证
        /// </summary>
        Reject = 0x31,
        /// <summary>
        /// 请求认证
        /// </summary>
        Request = 0x32
    }

    /// <summary>
    /// 是否允许对方也加自己为好友
    /// </summary>
    public enum RevenseAdd : byte
    {
        /// <summary>
        /// 允许对方也加自己为好友
        /// </summary>
        Allow = 0x01,
        /// <summary>
        /// 不允许对方加自己为好友
        /// </summary>
        NotAll = 0x02
    }

    /// <summary>
    /// 是否设置一个选项，用在如0x005E这样的命令中，其他地方如果类似也可使用
    /// </summary>
    public enum ValueSet : byte
    {
        /// <summary>
        /// 设置
        /// </summary>
        Set = 0x01,
        /// <summary>
        /// 取消设置
        /// </summary>
        UnSet = 0x00
    }

    /// <summary>
    /// 0x005E 私密设置的子命令
    /// </summary>
    public enum PrivacySubCmd : byte
    {
        /// <summary>
        /// 只能通过号码搜到我
        /// </summary>
        SearchMeByOnly = 0x03,
        /// <summary>
        /// 共享地理位置
        /// </summary>
        ShareGeography = 0x04,
    }
}
