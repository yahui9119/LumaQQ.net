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

namespace LumaQQ.NET.Packets.In
{
    /**
 * <pre>
 * 群命令的回复包，根据不同的子命令类型格式有所不同：
 * 创建群的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，1字节，创建群是0x1
 * 3. 回复码，1字节，成功是0x0，如果成功则后面为
 *    1. 群内部ID，4字节，如果为0，表示创建失败
 *    2. 群外部ID，4字节，如果为0，表示创建失败
 * 4. 如果回复码不为0，则后面为出错信息
 * 5. 尾部
 * 
 * 创建临时群的回复包
 * 1. 头部
 * 2. 命令类型，1字节，0x30
 * 3. 回复码，1字节
 * 4. 临时群类型，1字节，0x01是多人对话，0x02是讨论组
 * 5. 父群内部ID，4字节
 * 6. 创建的临时群的内部ID，4字节
 * 7. 尾部
 * 
 * 激活群的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，激活是0x5
 * 3. 回复码，1字节，成功是0x0
 * 4. 群的内部ID
 * 5. 尾部
 * 
 * 得到群信息的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，1字节，得到群信息是0x4
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节
 * 5. 群外部ID，4字节
 * 6. 群类型，1字节
 * 7. 未知的4字节
 * 8. 创建者QQ号，4字节
 * 9. 认证类型，1字节
 * 10. 群分类，4字节，这是2004的分类法。2004只有4个分类
 * 11. 未知的2字节
 * 12. 群分类ID，4字节，这是2005的分类法，2005的分类最多有三层。每个分类有一个唯一的id
 * 13. 未知的2字节
 * 14. 未知的1字节
 * 15. 群version id, 4字节
 * 16. 群名称长度，1字节
 * 17. 群名称
 * 18. 未知的两字节，全0
 * 19. 群声明长度，1字节
 * 20. 群声明
 * 21. 群描述长度，1字节
 * 22. 群描述
 * 23. 群中成员的QQ号，4字节
 * 24. 成员所属组织的序号，1字节，组织序号从1开始，如果为0，表示其不在某个组织中，
 *    一个成员只能在一个组织里面，组织和讨论组不同，讨论组可以看成是群中群，组织
 *     只是群成员的分类而已，它并不是一个群
 * 25. 群成员的类型，是不是管理员之类的，1字节
 * 26. 如果存在更多成员，重复23-25部分
 * 27. 尾部
 * 
 * 得到临时群信息的回复包，格式为
 * 1. 头部
 * 2. 命令类型，1字节，0x33
 * 3. 回复码，1字节，成功是0x00
 * 4. 群类型，1字节
 * 5. 父群内部ID，4字节
 * 6. 临时群内部ID，4字节
 * 7. 创建者QQ号，4字节
 * 8. 未知的4字节，全0
 * 9. 群名称长度，1字节
 * 10. 群名称
 * 11. 群中成员的QQ号，4字节
 * 12. 成员所属组织的序号，1字节。对于临时群来说，没有群内组织的概念，所以这个字段无用
 * 13. 如果有更多成员，重复11-12部分
 * 14. 尾部
 * 
 * 退出群的回复，格式为：
 * 1. 头部
 * 2. 命令类型，1字节，退出是0x9
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节，应该是个非0值
 * 5. 尾部
 * 
 * 解散群的回复
 * 1. 头部
 * 2. 子命令，1字节，0x1D
 * 3. 回复码，1字节
 * 4. 群内部id，4字节
 * 5. 尾部
 * 
 * 请求成员信息的回复包，格式为
 * 1. 头部
 * 2. 命令类型，1字节，请求成员信息是0x0C
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节
 * 5. 成员QQ号，4字节
 * 6. 头像号，2字节
 * 7. 年龄，1字节
 * 8. 性别，1字节
 * 9. 昵称长度，1字节
 * 10. 昵称
 * 11. 未知的2字节
 * 12. 扩展标志，1字节
 * 13. 通用标志，1字节
 * 14. 如果有更多成员，重复5-13部分，5-13部分其实也就是QQFriend结构
 * 15. 尾部
 * 
 * 得到在线成员的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，1字节，请求成员信息是0x0B
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节
 * 5. 未知字节，0x3C
 * 6. 在线成员的qq号
 * 7. 如果有更多在线成员，重复6
 * 8. 尾部
 * 
 * 激活临时群的回复包
 * 1. 头部
 * 2. 命令类型，1字节，0x37
 * 3. 回复码，1字节
 * 4. 临时群类型，1字节
 * 5. 父群内部ID，4字节
 * 6. 临时群内部ID，4字节
 * 7. 成员QQ号，4字节
 * 8. 如果有更多成员，重复7部分
 * 9. 尾部
 * 
 * 请求加入群的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，1字节，请求成员信息是0x07
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节
 * 5. 回复码，这个回复码是比较细的特定于join请求的回复，1字节
 * 6. 尾部
 * 
 * 请求加入群的认证信息回复包，没什么太大用处，就是表示服务器收到了，格式为：
 * 1. 头部
 * 2. 命令类型，这里是0x8
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节，如果为0表示出错
 * 5. 尾部
 * 
 * 修改群信息的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，这里是0x03
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节
 * 5. 尾部
 * 
 * 修改群成员的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，这里是0x03
 * 3. 回复码，1字节，成功是0x0
 * 4. 群内部ID，4字节
 * 5. 尾部
 * 
 * 搜索群的回复包，格式为：
 * 1. 头部
 * 2. 命令类型，这里是0x06
 * 3. 回复码，1字节，成功是0x0
 * 4. 搜索方式，1字节
 * 5. 群内部ID，4字节
 * 6. 群外部ID，4字节
 * 7. 群类型，1字节
 * 8. 未知的4字节
 * 9. 群创建者，4字节
 * 10. 群分类，4字节，这是2004的分类法。2004只有4个分类
 * 11. 群分类，4字节，这是2005分类法
 * 12. 未知的2字节
 * 13. 群名称长度，1字节
 * 14. 群名称
 * 15. 未知的两字节
 * 16. 认证类型，1字节
 * 17. 群简介长度，1字节
 * 18. 群简介
 * 19. 尾部
 * 
 * 发送群信息和发送扩展群信息的回复包
 * 1. 头部
 * 2. 命令类型，1字节，0x0A
 * 3. 回复码，1字节
 * 4. 群内部id，4字节
 * 5. 尾部
 * 
 * 发送临时群信息的回复包
 * 1. 头部
 * 2. 命令类型，1字节，0x35
 * 3. 回复码，1字节
 * 4. 临时群类型，1字节
 * 5. 父群内部ID，4字节
 * 6. 临时群内部ID，4字节
 * 7. 尾部
 * 
 * 退出临时群的回复包
 * 1. 头部
 * 2. 命令类型，1字节，0x32
 * 3. 回复码，1字节
 * 4. 临时群类型，1字节
 * 5. 父群内部ID，4字节
 * 6. 临时群内部ID，4字节
 * 7. 尾部
 * 
 * 修改临时群信息的回复包
 * 1. 头部
 * 2. 命令类型，这里是0x34
 * 3. 回复码，1字节，成功是0x00
 * 4. 临时群类型，1字节
 * 5. 父群内部ID，4字节
 * 6. 临时群内部ID，4字节
 * 7. 尾部
 * 
 * 修改临时群成员的回复包
 * 1. 头部
 * 2. 命令类型，这里是0x31
 * 3. 回复码，1字节，成功是0x00
 * 4. 临时群类型，1字节
 * 5. 父群内部ID，4字节
 * 6. 临时群内部ID，4字节
 * 7. 操作方式，1字节，0x01是添加，0x02是删除
 * 8. 操作的成员QQ号，4字节
 * 9. 如果有更多成员，重复8部分
 * 10. 尾部
 * 
 * 讨论组操作的回复包，子命令类型为0x02时（得到讨论组列表）
 * 1. 头部
 * 2. 命令类型，0x36
 * 3. 回复码，1字节
 * 4. 子命令类型，1字节，0x02
 * 5. 群内部id，4字节
 * 6. 群外部id，4字节
 * 7. 讨论组id，4字节
 * 8. 讨论组名称字节长度，1字节
 * 9. 讨论组名称
 * 10. 如果有更多讨论组，重复7-9部分
 * 11. 尾部 
 * 
 * 讨论组操作的回复包，子命令类型为0x01时（得到多人对话列表）
 * 1. 头部
 * 2. 命令类型，0x36
 * 3. 回复码，1字节
 * 4. 子命令类型，1字节，0x01
 * 5. 群内部id，4字节，为0
 * 6. 群外部id，4字节，为0
 * 7. 讨论组id，4字节
 * 8. 讨论组名称字节长度，1字节
 * 9. 讨论组名称
 * 10. 如果有更多讨论组，重复7-9部分
 * 11. 尾部 
 * 
 * 从服务器更新组织架构的回复包
 * 1. 头部
 * 2. 命令，1字节，0x12
 * 3. 回复码，1字节，0x00为成功 
 * 4. 群内部ID，4字节
 * 5. 未知1字节，0x00
 * 6. 组织Version ID，4字节，意义和群的Version ID相同。
 * 	  如果这个字段为0，表示没有组织，并且7-12部分不存在
 * 7. 组织个数，1字节
 * 8. 组织序号，1字节，从1开始
 * 9. 组织的层次关系，4字节。QQ的组织最多支持4层，4个字节一共32bit，第一层用了8位，
 *    后面的层用了6位，所以还有6位是保留未用的。举个例子说明一下这个字段的具体格式，
 *    假如这个字段的二进制表示为
 *    0000 0001 0000 1100 0101 0010 0100 0000
 *    那么得知，前8位0000 0001，值为1
 *    然后是0000 11，值为3
 *    然后是00 0101，值为5
 *    然后后0010 01，值为9，
 *    最后6位保留未用，
 *    所以这个组织位于第四层，它是父节点的第9个子组织，它的父节点是祖父节点的第5个子组织，
 *    它的祖父节点是曾祖父节点的第3个组织，它的曾祖父节点是群的第一个组织。
 *    我们要分清楚的是，组织的序号和层次号并不是一样的，也不是有对应关系的。所以目前来看，
 *    这个关系需要我们自己维护，以便查找组织
 * 10. 组织名称字节长度，1字节
 * 11. 组织名称
 * 12. 如果有更多组织，重复8-11部分
 * 13. 尾部 
 * 
 * 提交组织架构的回复包
 * 1. 头部
 * 2. 命令，1字节，0x11
 * 3. 回复码，1字节，0x00为成功 
 * 4. 群内部ID，4字节
 * 5. 组织Version ID，4字节
 * 6. 组织个数，2字节
 * 7. 组织序号，1字节
 * 8. 如果有更多组织，重复7部分
 * 9. 尾部
 * 
 * 提交成员分组情况的回复包
 * 1. 头部
 * 2. 命令，1字节，0x13
 * 3. 回复码，1字节，0x00为成功 
 * 4. 群内部ID，4字节
 * 5. 成员分组情况version id，4字节
 * 6. 尾部
 * 
 * 修改群名片回复包
 * 1. 头部
 * 2. 命令, 1字节，0x0E
 * 3. 回复码，1字节，0x00为成功 
 * 4. 群内部ID，4字节
 * 5. 自己的QQ号，4字节
 * 
 * 批量得到群名片真实姓名的回复包
 * 1. 头部
 * 2. 命令, 1字节，0x0F
 * 3. 回复码，1字节，0x00为成功 
 * 4. 群内部ID，4字节
 * 5. 群名片Version id， 4字节
 * 6. 下一个请求包的起始位置，4字节。这个字段如果为0，表示所有名片都已经得到
 *    如果不为0，表示起始记录数，比如一共有10条名片信息，这次得到了6条，还剩
 *    4条，那么这个字段就是0x00000006，因为下一条的序号是6(从0开始)
 * 7. 成员QQ号，4字节
 * 8. 真实姓名长度，1字节
 * 9. 真实姓名
 * 10. 如果有更多成员，重复7-9部分
 * 11. 尾部
 * 
 * 得到单个成员全部群名片信息的回复包
 * 1. 头部
 * 2. 命令, 1字节，0x0F
 * 3. 回复码，1字节，0x00为成功 
 * 4. 群内部ID，4字节
 * 5. 成员QQ号，4字节
 * 6. 真实姓名长度，1字节
 * 7. 真实姓名
 * 8. 性别索引，1字节，性别的顺序是'男', '女', '-'，所以男是0x00，等等
 * 9. 电话字符串长度，1字节
 * 10. 电话的字符串表示
 * 11. 电子邮件长度，1字节
 * 12. 电子邮件
 * 13. 备注长度，1字节
 * 14. 备注内容
 * 15. 尾部
 * 
 * 设置角色的回复包
 * 1. 头部
 * 2. 命令，1字节，0x1B
 * 3. 回复码，1字节
 * 4. 群内部ID，4字节
 * 5. 群version id, 4字节
 * 6. 被设置的QQ号，4字节
 * 7. 操作之后成员的角色，1字节
 * 8. 尾部
 * 
 * 转让角色的回复包
 * 1. 头部
 * 2. 命令，1字节，0x1B
 * 3. 回复码，1字节
 * 4. 群内部ID，4字节
 * 5. 转让到的QQ号，4字节
 * 6. 根据回复码不同，有:
 *    i. 3部分为0x00时，为群version id，4字节
 *    ii. 3部分为其他值时，为错误信息
 * 7. 尾部
 * </pre>
 * 
 * @author luma
 */
    public class ClusterCommandReplyPacket : BasicInPacket
    {
        /// <summary>
        /// 子命令 
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public ClusterCommand SubCommand { get; set; }
        /// <summary>
        /// 回复码 
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public ReplyCode ReplyCode { get; private set; }
        /// <summary>
        /// 群内部id
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint ClusterId { get; set; }
        /// <summary>
        /// 群外部id 
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint ExternalId { get; set; }
        /// <summary>
        /// 群类型
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public ClusterType Type { get; set; }
        /// <summary>
        /// 父群内部ID 
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint ParentClusterId { get; set; }
        /// <summary>
        /// 群版本号
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint VersionId { get; set; }

        /// <summary>
        /// 如果某个包是对单个群成员进行操作，则使用这个字段保存QQ号 
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public uint MemberQQ { get; set; }
        /// <summary>
        /// 如果ReplyCode不是ok，那么这个字段有效，表示出错信息
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 群信息
        /// 仅用于得到群信息的回复包，list的元素类型为ClusterInfo
        /// <remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public ClusterInfo Info { get; set; }

        /// <summary>
        /// 群成员列表，元素类型为Integer，包含了成员的QQ号
        /// 仅用于得到群信息和得到临时群成员列表的回复包
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public List<Member> Members { get; set; }

        /// <summary>
        /// 包含了群成员信息的列表，元素类型是QQFriend
        /// 仅用于得到群成员信息的回复包，list的元素类型是QQFriend
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public List<QQFriend> MemberInfos { get; set; }

        /// <summary>
        /// 包含了在线成员列表，元素类型是Integer，表示成员的QQ号
        /// 仅用于得到在线成员的回复包，list的元素类型是Integer
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public List<uint> OnlineMembers { get; set; }

        /// <summary>
        /// 子群列表，可能是讨论组也可能是多人对话
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public List<SimpleClusterInfo> SubClusters { get; set; }
        /// <summary>
        /// 子群操作子类型
        /// 	<remark>abu 2008-02-20 </remark>
        /// </summary>
        /// <value></value>
        public byte SubClusterOpByte { get; set; }

        /// <summary>
        /// 加入群的回复
        /// 	<remark>仅用于加入群的回复包 abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public byte JoinReply { get; set; }

        /// <summary>
        /// 搜索类型
        /// 仅用于搜索群的回复包，元素类型是ClusterInfo
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public byte SearchType { get; set; }

        /// <summary>
        /// 搜索到的群，类型是ClusterInfo 
        /// 	<remark> abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public List<ClusterInfo> Clusters { get; set; }

        /// <summary>
        /// 用于更新组织架构的回复包和提交组织架构的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public uint OrganizationVersionId { get; set; }
        /// <summary>
        /// 用于更新组织架构的回复包和提交组织架构的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public uint OrganizationCount { get; set; }
        public List<QQOrganization> Organizations { get; set; }

        /// <summary>
        /// 用于提交成员分组的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public uint MemberOrganizationVersionId { get; set; }

        /// <summary>
        /// 用于批量得到群名片真实姓名的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public List<CardStub> CardStubs { get; set; }
        public uint CardVersionId { get; set; }
        public uint NextStart { get; set; }

        /// <summary>
        /// 用于得到单个成员群名片的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public Card Card { get; set; }

        /// <summary>用于设置角色回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <value></value>
        public byte Role { get; set; }
        //未完，待续

        public ClusterCommandReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Cluster Command Reply";
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            // 得到群操作命令和回复码
            SubCommand = (ClusterCommand)buf.Get();
            ReplyCode = (ReplyCode)buf.Get();
            // 判断命令类型
            switch (SubCommand)
            {
                case ClusterCommand.SEND_IM_EX:
                    ParseSendIMReply(buf);
                    break;
                case ClusterCommand.SEND_TEMP_IM:
                    ParseSendTempClusterIMReply(buf);
                    break;
                case ClusterCommand.CREATE_CLUSTER:
                    ParseCreateReply(buf);
                    break;
                case ClusterCommand.CREATE_TEMP:
                    ParseCreateTempCluster(buf);
                    break;
                case ClusterCommand.ACTIVATE_CLUSTER:
                    ParseActivateReply(buf);
                    break;
                case ClusterCommand.MODIFY_MEMBER:
                    ParseModifyMemberReply(buf);
                    break;
                case ClusterCommand.GET_CLUSTER_INFO:
                    ParseGetInfoReply(buf);
                    break;
                case ClusterCommand.EXIT_CLUSTER:
                    ParseExitReply(buf);
                    break;
                case ClusterCommand.GET_MEMBER_INFO:
                    ParseGetMemberInfoReply(buf);
                    break;
                case ClusterCommand.GET_ONLINE_MEMBER:
                    ParseGetOnlineMemberReply(buf);
                    break;
                case ClusterCommand.JOIN_CLUSTER:
                    ParseJoinReply(buf);
                    break;
                case ClusterCommand.JOIN_CLUSTER_AUTH:
                    ParseJoinAuthReply(buf);
                    break;
                case ClusterCommand.MODIFY_CLUSTER_INFO:
                    ParseModifyInfoReply(buf);
                    break;
                case ClusterCommand.SEARCH_CLUSTER:
                    ParseSearchReply(buf);
                    break;
                case ClusterCommand.GET_TEMP_INFO:
                    ParseGetTempClusterInfoReply(buf);
                    break;
                case ClusterCommand.EXIT_TEMP:
                    ParseExitTempClusterReply(buf);
                    break;
                case ClusterCommand.ACTIVATE_TEMP:
                    ParseActivateTempCluster(buf);
                    break;
                case ClusterCommand.SUB_CLUSTER_OP:
                    ParseSubClusterOp(buf);
                    break;
                case ClusterCommand.UPDATE_ORGANIZATION:
                    ParseUpdateOrganization(buf);
                    break;
                case ClusterCommand.COMMIT_ORGANIZATION:
                    ParseCommitOrganization(buf);
                    break;
                case ClusterCommand.COMMIT_MEMBER_ORGANIZATION:
                    ParseCommitMemberOrganization(buf);
                    break;
                case ClusterCommand.MODIFY_TEMP_INFO:
                    ParseModifyTempClusterInfo(buf);
                    break;
                case ClusterCommand.MODIFY_CARD:
                    ParseModifyCard(buf);
                    break;
                case ClusterCommand.GET_CARD_BATCH:
                    ParseGetCardBatch(buf);
                    break;
                case ClusterCommand.GET_CARD:
                    ParseGetCard(buf);
                    break;
                case ClusterCommand.SET_ROLE:
                    ParseSetRole(buf);
                    break;
                case ClusterCommand.TRANSFER_ROLE:
                    ParseTransferRole(buf);
                    break;
                case ClusterCommand.DISMISS_CLUSTER:
                    ParseDismissCluster(buf);
                    break;
            }
            // 如果操作失败
            if (ReplyCode != ReplyCode.OK)
            {
                switch (SubCommand)
                {
                    case ClusterCommand.TRANSFER_ROLE:
                        ClusterId = buf.GetUInt();
                        MemberQQ = buf.GetUInt();
                        ErrorMessage = Util.GetString(buf);
                        break;
                    case ClusterCommand.SET_ROLE:
                        ClusterId = buf.GetUInt();
                        ErrorMessage = Util.GetString(buf);
                        break;
                    default:
                        /* 操作失败 */
                        ErrorMessage = Util.GetString(buf);
                        break;
                }
            }
        }


        /// <summary>
        /// 处理解散群的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseDismissCluster(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 处理转让角色的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseTransferRole(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                MemberQQ = buf.GetUInt();
                VersionId = buf.GetUInt();
            }
        }
        /// <summary>
        /// 处理设置群成员角色的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseSetRole(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                VersionId = buf.GetUInt();
                MemberQQ = buf.GetUInt();
                Role = buf.Get();
            }
        }

        /// <summary>
        /// 处理得到单个成员群名片回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseGetCard(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                MemberQQ = buf.GetUInt();
                Card = new Card();
                Card.Read(buf);
            }
        }

        /// <summary>处理批量得到群名片真实姓名回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseGetCardBatch(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                CardVersionId = buf.GetUInt();
                CardStubs = new List<CardStub>();
                NextStart = buf.GetUInt();
                while (buf.HasRemaining())
                {
                    CardStub stub = new CardStub();
                    stub.Read(buf);
                    CardStubs.Add(stub);
                }
            }
        }

        /// <summary>解析修改群名片回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        /// 
        private void ParseModifyCard(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 处理修改临时群信息回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseModifyTempClusterInfo(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                Type = (ClusterType)buf.Get();
                ParentClusterId = buf.GetUInt();
                ClusterId = buf.GetUInt();
            }
        }


        /// <summary>
        /// 处理提交成员分组情况的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseCommitMemberOrganization(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                MemberOrganizationVersionId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 处理提交组织架构的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseCommitOrganization(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                OrganizationVersionId = buf.GetUInt();
                OrganizationCount = (uint)buf.GetUShort();
            }
        }

        /// <summary>
        /// 解析更新组织架构回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseUpdateOrganization(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                Organizations = new List<QQOrganization>();
                ClusterId = buf.GetUInt();
                buf.Get();
                OrganizationVersionId = buf.GetUInt();
                if (OrganizationVersionId != 0)
                {
                    OrganizationCount = (uint)buf.Get();
                    while (buf.HasRemaining())
                    {
                        QQOrganization org = new QQOrganization();
                        org.Read(buf);
                        Organizations.Add(org);
                    }
                }
            }
        }

        /// <summary>
        /// 解析子群操作回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseSubClusterOp(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                SubClusterOpByte = buf.Get();
                ClusterId = buf.GetUInt();
                ExternalId = buf.GetUInt();
                SubClusters = new List<SimpleClusterInfo>();
                while (buf.HasRemaining())
                {
                    SimpleClusterInfo s = new SimpleClusterInfo();
                    s.Read(buf);
                    SubClusters.Add(s);
                }
            }
        }

        /// <summary>
        /// 解析创建临时群回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseCreateTempCluster(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                Type = (ClusterType)buf.Get();
                ParentClusterId = buf.GetUInt();
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseExitTempClusterReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                Type = (ClusterType)buf.Get();
                ParentClusterId = buf.GetUInt();
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 处理发送临时群信息的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseSendTempClusterIMReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                Type = (ClusterType)buf.Get();
                ParentClusterId = buf.GetUInt();
                ClusterId = buf.GetUInt();
            }
        }
        /// <summary>
        /// 处理修改群成员的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseModifyMemberReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
                ClusterId = buf.GetUInt();
        }
        /// <summary>处理发送消息的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseSendIMReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>处理搜索群的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseSearchReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                SearchType = buf.Get();
                Clusters = new List<ClusterInfo>();
                while (buf.HasRemaining())
                {
                    ClusterInfo ci = new ClusterInfo();
                    ci.ReadClusterInfoFromSearchReply(buf);
                    Clusters.Add(ci);
                }
            }
        }

        /// <summary>
        /// 处理修改群信息的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseModifyInfoReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 处理认证信息的回复包，这个回复包只是个简单的回复，没什么用
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseJoinAuthReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
            }
        }

        /// <summary>
        /// 处理加入群的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseJoinReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                JoinReply = buf.Get();
            }
        }

        /// <summary>
        /// 处理得到在线成员的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseGetOnlineMemberReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                // 内部ID
                ClusterId = buf.GetUInt();
                // 未知字节，0x3C
                buf.Get();
                // 成员信息
                OnlineMembers = new List<uint>();
                while (buf.HasRemaining())
                    OnlineMembers.Add(buf.GetUInt());
            }
        }

        /// <summary>
        /// 处理激活临时群回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseActivateTempCluster(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                // 临时群类型
                Type = (ClusterType)buf.Get();
                // 父群内部ID
                ParentClusterId = buf.GetUInt();
                // 临时群内部ID
                ClusterId = buf.GetUInt();
                // 成员信息
                Members = new List<Member>();
                while (buf.HasRemaining())
                {
                    Member member = new Member();
                    member.QQ = buf.GetUInt();
                    Members.Add(member);
                }
            }
        }

        /// <summary>
        /// 处理得到群成员信息的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseGetMemberInfoReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                // 成员信息
                MemberInfos = new List<QQFriend>();
                while (buf.HasRemaining())
                {
                    QQFriend friend = new QQFriend();
                    friend.Read(buf);
                    MemberInfos.Add(friend);
                }
            }
        }
        /// <summary>处理退出群的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseExitReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
                ClusterId = buf.GetUInt();
        }

        /// <summary>
        /// 处理得到群信息的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseGetInfoReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                // 群信息
                Info = new ClusterInfo();
                Info.ReadClusterInfo(buf);
                ClusterId = Info.ClusterId;
                ExternalId = Info.ExternalId;
                // 读取成员列表
                Members = new List<Member>();
                while (buf.HasRemaining())
                {
                    Member member = new Member();
                    member.Read(buf);
                    Members.Add(member);
                }
            }
        }

        /// <summary>
        /// 处理得到临时群信息的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseGetTempClusterInfoReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                // 群信息
                Info = new ClusterInfo();
                Info.ReadTempClusterInfo(buf);
                Type = Info.Type;
                ClusterId = Info.ClusterId;
                ParentClusterId = Info.ExternalId;
                // 读取成员列表
                Members = new List<Member>();
                while (buf.HasRemaining())
                {
                    Member member = new Member();
                    member.ReadTemp(buf);
                    Members.Add(member);
                }
            }
        }

        /// <summary>
        /// 处理激活群的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseActivateReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
                ClusterId = buf.GetUInt();
        }

        /// <summary>
        /// 解析创建群的回复包
        /// 	<remark>abu 2008-02-22 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        private void ParseCreateReply(ByteBuffer buf)
        {
            if (ReplyCode == ReplyCode.OK)
            {
                ClusterId = buf.GetUInt();
                ExternalId = buf.GetUInt();
            }
        }

    }
}
