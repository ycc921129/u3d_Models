/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 用户-兑换列表
    /// </summary>
    public class S2C_exchangeList_data
    {
        public int id;
        /// <summary>
        /// 1待审核，2待发货，3成功，4失败，5拒绝，9等待回调
        /// </summary>
        public int status;
        public string status_desc;
        public Goods goods;
        public float amount;
        /// <summary>
        /// 收款账户，aws传空对象即可
        /// </summary>
        public object recv_acct;  
        public Pay_method pay_method;
        public ASWCode extend;
        public string create_time;  
        public string delivery_time;  
    }   

    public class Goods
    {
        public int id;
        public object pgs;
        /// <summary>
        /// 1payermax，2aws
        /// </summary>
        public int type;
        public string title;
        public string country;
        public string remark;
        public float amount;
        public object conditions;
        public List<Pay_methods> pay_methods;
        public int need_review;
        public string method;
        public List<Sdk_params> sdk_params;
    }

    public class Pay_method
    {
        public float amount;
        public string type; 
    }

    public class ASWCode
    {
        public string code;  
    }
}