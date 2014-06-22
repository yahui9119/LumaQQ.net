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

namespace LumaQQ.NET.Threading
{
    /// <summary>
    /// 保持登录状态触发器
    /// 	<remark>abu 2008-03-11 </remark>
    /// </summary>
    public class KeepAliveTrigger : IRunable
    {
        private QQClient client;
        public KeepAliveTrigger(QQClient client)
        {
            this.client = client;
            ThreadExcutor.RegisterIntervalObject(this, this, QQGlobal.QQ_INTERVAL_KEEP_ALIVE, false);
        }
        #region IRunable Members

        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// 	<remark>abu 2008-03-07 </remark>
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="timedOut">if set to <c>true</c> [timed out].</param>
        public void Run(object state, bool timedOut)
        {
            if (!IsRunning)
            {
                lock (this)
                {
                    if (!IsRunning)
                    {
                        IsRunning = true;
                        this.client.KeepAlive();
                        IsRunning = false;
                    }
                }
            }

        }

        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public System.Threading.WaitHandle WaitHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 	<remark>abu 2008-03-11 </remark>
        /// </summary>
        /// <value></value>
        public System.Threading.RegisteredWaitHandle RegisterdHandler
        {
            get;
            set;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.WaitHandler != null && this.RegisterdHandler != null)
            {
                RegisterdHandler.Unregister(this.WaitHandler);
            }
        }

        #endregion
    }
}
