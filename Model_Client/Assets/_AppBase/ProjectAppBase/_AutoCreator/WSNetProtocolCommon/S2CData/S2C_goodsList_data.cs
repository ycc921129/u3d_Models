/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    /// <summary>
    /// 用户-商品列表
    /// </summary>
    public class S2C_goodsList_data
    {
        public List<Exchange> exchange;
        public List<Iap> iap;
        public List<Iap> subscribe;  
    }

    public class Exchange
    {
        public int id;
        public int type;    //1:Payermax，2:AWS
        public string title;   
        public string method;   //比如：OVO/DANA
        public float amount;
        public object conditions;
        public List<Sdk_params> sdk_params;
        public List<Pay_methods> pay_methods;
    }

    public class Iap
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int id;
        /// <summary>
        /// 扩展参数
        /// </summary>
        public Extend extend;
    }
    
    public class Sdk_params
    {
        public string field;
        public string name;
        public string regexp;
        public string comment;
    }

    public class Pay_methods
    {
        public float amount;  
        public string type; 
    }

    public class Extend
    {
        /// <summary>
        /// iap_1
        /// </summary>
        public string product_id;
    }
}