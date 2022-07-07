using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
     /// <summary>
     /// 内购打点
     /// </summary>
    public class C2S_billing_state_data
     {

         /// <summary>
         /// iap ,subs
         /// </summary>
         public string type;
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
        /// <summary>
        /// 错误码
        /// </summary>
        public int err_code;

        /// <summary>
        /// 商品id
        /// </summary>
        public string product_id;

        /// <summary>
        /// 客户端服务器时间戳
        /// </summary>
        public int ts;
     }

    
}