/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore.Data
{
    #region 订阅
    /// <summary>
    /// 订阅购买项
    /// </summary>
    public class SubscribeIAPItem
    {
        public Goods_subscribe goods;
        public List<History_goods_subscribe> history_goods;
    }

    public class Goods_subscribe
    {
        /// <summary>
        /// 商品Id，唯一标识
        /// </summary>
        public int id;
        /// <summary>
        /// 0未支付，1已支付, 3重复验证
        /// </summary>
        public int status;
        /// <summary>
        /// 第三方order_id
        /// </summary>
        public string order_id;
        /// <summary>
        /// 商品id
        /// </summary>
        public string product_id;
        /// <summary>
        /// 商品id
        /// </summary>
        public long expire_time;
        /// <summary>
        /// 凭证（json字符串）
        /// </summary>
        public C2S_subscribeCallback_info receipt;
    }

    public class History_goods_subscribe
    {
        /// <summary>
        /// 第三方order_id
        /// </summary>
        public string order_id;
        /// <summary>
        /// 商品id
        /// </summary>
        public string product_id;
        /// <summary>
        /// 过期时间 （时间戳-毫秒）
        /// </summary>
        public long expire_time;
    }

    /// <summary>
    /// 购买数据 (订阅使用)
    /// </summary>
    public class PurchaseData
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public string orderId;
        /// <summary>
        /// 包名
        /// </summary>
        public string packageName;
        /// <summary>
        /// 商品Id
        /// </summary>
        public string productId;
        /// <summary>
        /// 购买时间
        /// </summary>
        public long purchaseTime;
        /// <summary>
        /// 购买状态 (可能的值为 0（已购买）、1（已取消）或者 2（已退款）)
        /// </summary>
        public int purchaseState;
        /// <summary>
        /// google订单Token
        /// </summary>
        public string purchaseToken;
        /// <summary>
        /// 自动续订服务
        /// </summary>
        public bool autoRenewing;
    }

    /// <summary>
    /// 凭证（json字符串）
    /// </summary>
    public class C2S_subscribeCallback_info
    {
        public string orderId;
        public string packageName;
        public string productId;
        public string purchaseToken;
    }
    #endregion

    #region 内购
    /// <summary>
    /// 内购购买项
    /// </summary>
    public class IAPItem
    {
        public int id;
        /// <summary>
        /// 0未支付，1已支付，2已发货, 3重复验证
        /// </summary>
        public int status;
        public IAPGoods goods;
    }

    public class IAPGoods
    {
        /// <summary>
        /// 第三方order_id
        /// </summary>
        public string order_id;
        /// <summary>
        /// 第三方商品id
        /// </summary>
        public string product_id;
        /// <summary>
        /// 数量
        /// </summary>
        public int count;
        /// <summary>
        /// 凭证（对象）
        /// </summary>
        public object receipt;
        /// <summary>
        /// 商品id
        /// </summary>
        public int goods_id;
    }

    /// <summary>  
    /// 内购购买数据
    /// </summary>
    public class IapPurchaseData
    {
        /// <summary>
        /// 数据签名
        /// </summary>
        public string dataSignature;
        /// <summary>
        /// 包名
        /// </summary>
        public string packageName;
        /// <summary>
        /// 订单数据
        /// </summary>
        public string purchaseData;
    }
    #endregion

}