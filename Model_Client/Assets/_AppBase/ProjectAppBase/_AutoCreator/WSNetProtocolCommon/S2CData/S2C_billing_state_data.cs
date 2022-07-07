using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
    // 大喇叭
    public class S2C_billing_state_data
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string state;
        /// <summary>
        /// 客户端id
        /// </summary>
        public string client_order_id;
        
        /// <summary>
        /// 第三方订单Id
        /// </summary>
        public string order_id;
 
    }

    
}