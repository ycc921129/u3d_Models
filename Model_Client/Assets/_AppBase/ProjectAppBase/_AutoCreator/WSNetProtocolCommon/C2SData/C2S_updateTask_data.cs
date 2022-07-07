/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class C2S_updateTask_data
    {
        /// <summary>
        /// 任务ID
        /// </summary>  
        public int id;
        /// <summary>
        /// 0未开始，1进行中，2已完成，3已发奖励，9已提现
        /// </summary>
        public int status;
        /// <summary>
        /// 进度，结构客户端自己定义 - object
        /// </summary>
        public object schedule;
        /// <summary>
        /// updateTask : 更新任务状态 
        /// </summary>
        public string protocol;

        public C2S_updateTask_data()
        {
            protocol = "updateTask";
        }
    }
}