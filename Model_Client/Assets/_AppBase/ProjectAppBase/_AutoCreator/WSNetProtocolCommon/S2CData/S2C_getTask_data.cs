/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 客户端接口：获取(或者更新)任务列表
    /// </summary>
    public class S2C_getTask_data
    {
        public int id;
        public int status;
        /// <summary>  
        /// 进度，结构客户端自己定义
        /// </summary>
        public object schedule; 
        public object arguments;  
    }
}