/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
    //用户-兑换下单
    public class C2S_exchangeOrder_data
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int goods_id;
        /// <summary>
        /// 收款账户，aws传空对象即可
        /// </summary>
        public Acct recv_acct;
        /// <summary>
        /// 付款方式  
        /// </summary>
        public string pay_method;
        /// <summary>
        /// 任务id
        /// </summary>
        public string task_id;
        /// <summary>
        /// 扩展参数集，可自定义参数，下单后跟随订单每个生命周期
        /// </summary>  
        public object extend;
    }

    public class Acct
    {
        public string name;
        public string account;
        public string pay_method;
        public string email;
    }
}