/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol 
{
    //删除定时器
    public class C2S_timer_del_data
    {
        /// <summary>
        /// 要删除的定时器ID列表, 空则全部删除
        /// </summary>
        public List<string> ids;
    }
}