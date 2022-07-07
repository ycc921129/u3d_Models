/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using FutureCore;
using FutureCore.Data;
using Newtonsoft.Json.Linq;
using ProjectApp.Data;
using ProjectApp.Protocol;
using UnityEngine;

namespace ProjectApp
{
    /// <summary>
    /// SDK内购返回码
    /// </summary>
    public static class IAPBillingResponseCode
    {
        public const int SERVICE_TIMEOUT = -3;
        public const int FEATURE_NOT_SUPPORTED = -2;
        public const int SERVICE_DISCONNECTED = -1;
        public const int OK = 0;
        public const int USER_CANCELED = 1;
        public const int SERVICE_UNAVAILABLE = 2;
        public const int BILLING_UNAVAILABLE = 3;
        public const int ITEM_UNAVAILABLE = 4;
        public const int DEVELOPER_ERROR = 5;
        public const int ERROR = 6;
        public const int ITEM_ALREADY_OWNED = 7;
        public const int ITEM_NOT_OWNED = 8;
    }

    public class IAPCtrl : BaseCtrl
    {
        public static IAPCtrl Instance { get; private set; }

        private const string TAG = "[IAPCtrl] ";
        private bool isInitFirst = true;
        private string curProductId;
        private ProductDetailsData productDetailsData;

        /// <summary>  
        /// 订阅相关
        /// </summary>
        private Dictionary<string, int> subIdsDic = new Dictionary<string, int>();
        // 存在的订阅字典
        private Dictionary<PurchaseData, History_goods_subscribe> existSubscriptionDict;
        // 缓存过期需要校验的订阅数据
        private List<PurchaseData> cacheVerifyPurchaseDatas = new List<PurchaseData>();
        // 订阅信息
        private SubscribeIAPItem subscribeIAPItem;

        /// <summary>
        /// 内购相关
        /// </summary>
        private Dictionary<string, int> goodsIdsDic = new Dictionary<string, int>();
        // SDK已消耗订单, 但游戏层未验证的商品订单字典  
        private Dictionary<string, IapPurchaseData> uncheckedSdkGoodOrderDict;
        // 已经发货的商品订单列表
        private List<string> shippedIapGoodOrders;

        #region 生命周期
        protected override void OnInit()
        {
            Instance = this;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            CtrlDispatcher.Instance.AddListener(CtrlMsg.Game_Start, OnInitQueryBilling);
            CtrlDispatcher.Instance.AddListener(CtrlMsg.Login_ReloginSucceed, OnReInitQueryBilling);
            CtrlDispatcher.Instance.AddListener(CtrlMsg.HeartBeat_UpdateServerCurrTime, OnHeartBeat_UpdateServerCurrTime);

            ChannelDispatcher.Instance.AddListener(ChannelRawMsg.OnBillingPurchasesResult, OnBillingPurchasesResult);
            ChannelDispatcher.Instance.AddListener(ChannelMsg.OnBillingSubscribePurchased, OnBillingSubscribePurchased);
            ChannelDispatcher.Instance.AddListener(ChannelMsg.OnBillingIapPurchased, OnBillingIapPurchased);
        }

        protected override void RemoveListener()
        {
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Game_Start, OnInitQueryBilling);
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.Login_ReloginSucceed, OnReInitQueryBilling);
            CtrlDispatcher.Instance.RemoveListener(CtrlMsg.HeartBeat_UpdateServerCurrTime, OnHeartBeat_UpdateServerCurrTime);

            ChannelDispatcher.Instance.RemoveListener(ChannelRawMsg.OnBillingPurchasesResult, OnBillingPurchasesResult);
            ChannelDispatcher.Instance.RemoveListener(ChannelMsg.OnBillingSubscribePurchased, OnBillingSubscribePurchased);
            ChannelDispatcher.Instance.RemoveListener(ChannelMsg.OnBillingIapPurchased, OnBillingIapPurchased);
        }

        protected override void AddServerListener()
        {
            wsNetDispatcher.AddListener(WSNetMsg.S2C_goodsList, OnS2C_goodsList);
            //订阅
            wsNetDispatcher.AddListener(WSNetMsg.S2C_subscribeOrder, OnSubscribeOrder);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_subscribeCallback, OnS2C_subscribeCallback);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_InvokeSubscribe, OnSubscribe);
            //内购
            wsNetDispatcher.AddListener(WSNetMsg.S2C_iapOrder, OnS2C_IapOrder);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_iapCallback, OnS2C_iapCallback);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_iapNotifyAck, OnS2C_notifyAck);
            wsNetDispatcher.AddListener(WSNetMsg.S2C_InvokeIAP, OnS2C_IAP);
        }

        protected override void RemoveServerListener()
        {
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_goodsList, OnS2C_goodsList);
            //订阅
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_subscribeOrder, OnSubscribeOrder);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_InvokeSubscribe, OnSubscribe);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_subscribeCallback, OnS2C_subscribeCallback);
            //内购
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_iapOrder, OnS2C_IapOrder);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_iapCallback, OnS2C_iapCallback);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_iapNotifyAck, OnS2C_notifyAck);
            wsNetDispatcher.RemoveListener(WSNetMsg.S2C_InvokeIAP, OnS2C_IAP);
        }
        #endregion

        #region 外部接口       
        /// <summary>
        /// 是否初始化成功
        /// </summary>
        public bool IsBillingInitSuccess()
        {
            return Channel.Current.isBillingInitSuccess();
        }

        /// <summary>
        /// 获取原来的价格
        /// </summary>
        public string GetBillingProductOriginalPrice(string productId)
        {
            return Channel.Current.getBillingProductOriginalPrice(productId);
        }

        /// <summary>
        /// 获取现在的价格
        /// </summary>
        public string GetBillingProductPrice(string productId)
        {
            return Channel.Current.getBillingProductPrice(productId);
        }

        /// <summary>
        /// 拉起内购
        /// </summary>
        public bool LaunchBillingGoodsFlow(string _productId)
        {
            if (!IsBillingInitSuccess()) return false;

            // 显示等待UI
            App.DisplayWaitUI();

            curProductId = _productId;
            int goods_id = GetIapIDByIap(curProductId);
            SendC2S_IapOrder(goods_id);

            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPGoods_click);
            return true;
        }

        /// <summary>
        /// 拉起订阅
        /// </summary>
        public bool LaunchBillingSubscribeFlow(string _productId)
        {
            if (!IsBillingInitSuccess())
            {
                LogUtil.LogError(TAG + "IsBilling Fai");
                return false;
            }

            // 显示等待UI
            App.DisplayWaitUI();

            curProductId = _productId;
            int goods_id = GetIapIDBySubscribe(curProductId);
            SendC2S_subscribeOrder(goods_id);

            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPSubscribe_click);
            return true;
        }

        /// <summary>
        /// 是否订阅中
        /// </summary>
        public bool IsSubscribe(string productId)
        {
            if (subscribeIAPItem == null || subscribeIAPItem.history_goods == null) return false;
            History_goods_subscribe subscribeItem = subscribeIAPItem.history_goods.Find((item) => item.product_id == productId);
            if (subscribeItem == null) return false;

            //查询订阅 增加订阅的缓冲时间
            long expireTime = subscribeItem.expire_time + DateTimeMgr.Instance.GetHeartBeatTime();
            if (expireTime < DateTimeMgr.Instance.GetServerTickTimestamp())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查询订阅内购的属性配置id列表 
        /// </summary>
        public ProductDetailsData GetProductDetailsData()
        {
            if (productDetailsData == null)
            {
                LogUtil.LogError(TAG + "productDetailsData is null.");
                return null;
            }

            return productDetailsData;
        }

        /// <summary>
        /// 获取当前订阅剩余的时间戳（毫秒）
        /// </summary>
        public long GetCurSubscribeTimes(string productId)
        {
            if (subscribeIAPItem == null || subscribeIAPItem.history_goods == null) return 0;
            History_goods_subscribe subscribeItem = subscribeIAPItem.history_goods.Find((item) => item.product_id == productId);
            if (subscribeItem == null) return 0;

            long expireTime = subscribeItem.expire_time - DateTimeMgr.Instance.GetServerCurrTimestamp();
            return expireTime > 0 ? expireTime : 0;
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 响应初始化查询IAP配置
        /// </summary>
        private void OnInitQueryBilling(object args)
        {
            SendGoods();
        }

        /// <summary>
        /// 获取订阅内购配置
        /// </summary>
        public void SendGoods()
        {
            if (productDetailsData != null
                && productDetailsData.subs_list.Count > 0
                && productDetailsData.iap_list.Count > 0) return;

            C2S_goodsList exchangeList = new C2S_goodsList();
            exchangeList.data = new C2S_goodsList_data();
            exchangeList.Send();
        }

        private void OnS2C_goodsList(BaseS2CJsonProto obj)
        {
            try
            {
                S2C_goodsList goodsList = obj as S2C_goodsList;
                // 异常处理
                if (goodsList == null
                    || !string.IsNullOrEmpty(goodsList.err))
                {
                    LogUtil.LogError(goodsList.err);
                    return;
                }
                if (goodsList.data == null)
                {
                    LogUtil.LogError(TAG + "S2C_goodsList is null.");
                    return;
                }

                InitQueryBilling(goodsList.data);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        private void OnReInitQueryBilling(object args)
        {
            SendGoods();
        }

        private void InitQueryBilling(S2C_goodsList_data _goodsDataList)
        {
            if (productDetailsData == null)
                productDetailsData = new ProductDetailsData();

            //订阅有配置
            if (_goodsDataList.subscribe != null)
            {
                for (int i = 0; i < _goodsDataList.subscribe.Count; i++)
                {
                    Iap subscribeIap = _goodsDataList.subscribe[i];
                    if (subscribeIap.extend == null || subscribeIap.extend.product_id == null) continue;

                    string product_id = subscribeIap.extend.product_id;

                    if (!productDetailsData.subs_list.Contains(product_id))
                        productDetailsData.subs_list.Add(product_id);

                    if (!subIdsDic.ContainsKey(product_id))
                        subIdsDic.Add(product_id, subscribeIap.id);
                }
            }

            //内购有配置
            if (_goodsDataList.iap != null)
            {
                for (int i = 0; i < _goodsDataList.iap.Count; i++)
                {
                    Iap iap = _goodsDataList.iap[i];
                    if (iap.extend == null || iap.extend.product_id == null) continue;

                    string product_id = iap.extend.product_id;

                    if (!productDetailsData.iap_list.Contains(product_id))
                        productDetailsData.iap_list.Add(product_id);

                    if (!goodsIdsDic.ContainsKey(product_id))
                        goodsIdsDic.Add(product_id, iap.id);
                }
            }

            // 内购配置表无数据
            if (goodsIdsDic.Count == 0 && subIdsDic.Count == 0) return;
            if (productDetailsData.iap_list.Count == 0 && productDetailsData.subs_list.Count == 0) return;

            if (PrefsUtil.HasKey(PrefsKeyConst.IAPCtrl_uncheckedSdkGoodOrderDict))
            {
                uncheckedSdkGoodOrderDict = PrefsUtil.ReadObject<Dictionary<string, IapPurchaseData>>(PrefsKeyConst.IAPCtrl_uncheckedSdkGoodOrderDict) as Dictionary<string, IapPurchaseData>;
            }
            if (PrefsUtil.HasKey(PrefsKeyConst.IAPCtrl_shippedIapGoodOrders))
            {
                shippedIapGoodOrders = PrefsUtil.ReadObject<List<string>>(PrefsKeyConst.IAPCtrl_shippedIapGoodOrders) as List<string>;
            }

            // 派发商品id
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.IAP_ProductDetailsData, productDetailsData);

            QueryBilling();
        }

        private void QueryBilling()
        {
            //订阅: 初始化IAP配置与校验  
            Channel.Current.queryBillingProductDetails(productDetailsData);
            Channel.Current.queryBillingPurchaseHistory();

            // 内购: 重新校验SDK已经消耗但是游戏层没校验的订单
            AfreshUncheckedSdkGoodOrderDict();
        }
        #endregion

        #region 内购
        /// <summary>
        /// 响应SDK内购回调
        /// </summary>
        private void OnBillingIapPurchased(object args)
        {
            try
            {
                UserSessionCtrl.Instance.StatisticEvent(StatisticConst.newbyear_iap_game_verify_start);

                IapPurchaseData iapPurchaseData = (IapPurchaseData)args;
                if (uncheckedSdkGoodOrderDict == null)
                {
                    uncheckedSdkGoodOrderDict = new Dictionary<string, IapPurchaseData>();
                }
                Dictionary<string, string> purchaseDataDict = SerializeUtil.ToObject<Dictionary<string, string>>(iapPurchaseData.purchaseData);
                if (purchaseDataDict != null && purchaseDataDict.Count != 0)
                {
                    string key = "purchaseToken";
                    if (purchaseDataDict.ContainsKey(key))
                    {
                        string purchaseToken = purchaseDataDict[key];
                        uncheckedSdkGoodOrderDict[purchaseToken] = iapPurchaseData;
                        PrefsUtil.WriteObject(PrefsKeyConst.IAPCtrl_uncheckedSdkGoodOrderDict, uncheckedSdkGoodOrderDict);
                    }
                }

                SendC2S_iapCallback(iapPurchaseData);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        /// <summary>
        /// 响应IAP结果回调
        /// </summary>
        private void OnBillingPurchasesResult(object args)
        {
            int resultCode = (int)args;
            // 购买错误
            if (resultCode != IAPBillingResponseCode.OK)
            {
                // 关闭等待UI
                App.HideWaitUI();
            }
        }

        /// <summary>
        /// 重新校验SDK已经消耗但是游戏层没校验的订单
        /// </summary>
        private void AfreshUncheckedSdkGoodOrderDict()
        {
            if (uncheckedSdkGoodOrderDict == null) return;
            if (uncheckedSdkGoodOrderDict.Count == 0) return;

            foreach (string purchaseToken in uncheckedSdkGoodOrderDict.Keys)
            {
                IapPurchaseData iapPurchaseData = uncheckedSdkGoodOrderDict[purchaseToken];
                SendC2S_iapCallback(iapPurchaseData);
            }
        }

        /// <summary>
        /// 内购广播
        /// </summary>
        /// <param name="respMsg"></param>
        private void OnS2C_IAP(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_InvokeIAP iapResp = respMsg as S2C_InvokeIAP;
                if (!string.IsNullOrEmpty(iapResp.err))
                {
                    LogUtil.LogError("[IAPCtrl] OnS2C_IAP is error, error = " + iapResp.err);
                    return;
                }

                if (iapResp.data == null)
                {
                    LogUtil.LogError("[IAPCtrl] iapOrderResp.data is null,");
                    return;
                }

                IAPItem iapItem = SerializeUtil.ToObject<IAPItem>(iapResp.data.ToString());
                if (iapItem == null)
                {
                    LogUtil.LogError("[IAPCtrl] iapItem is null.");
                    return;
                }

                if (iapItem.status == 0) { LogUtil.Log("内购：未支付."); return; }
                else if (iapItem.status == 2) { LogUtil.Log("内购：已消耗."); return; }
                else if (iapItem.status == 3) { LogUtil.Log("内购：重复验证."); return; }

                if (iapItem == null
                    || iapItem.goods == null
                    || iapItem.goods.receipt == null)
                {
                    LogUtil.LogError("[IAPCtrl] iapItem is null or iapItem.goods is null or iapItem.goods.receipt is null.");
                    return;
                }

                //当前单已支付， 0未支付，1已支付，2已发货, 3重复验证
                if (iapItem.status == 1)
                {
                    IapPurchaseData _iapPurchaseData = SerializeUtil.ToObject<IapPurchaseData>(iapItem.goods.receipt.ToString());
                    if (_iapPurchaseData == null)
                    {
                        LogUtil.LogError("[IAPCtrl] _iapPurchaseData is null.");
                        return;
                    }
                    if (_iapPurchaseData.purchaseData == null)
                    {
                        LogUtil.LogError("[IAPCtrl] _iapPurchaseData.purchaseData is null.");
                        return;
                    }

                    JObject jObject = SerializeUtil.GetJObjectByJson(_iapPurchaseData.purchaseData);
                    if (jObject == null)
                    {
                        LogUtil.LogError("[IAPCtrl] jObject is null.");
                        return;
                    }
                    if (jObject["purchaseToken"] == null)
                    {
                        LogUtil.LogError("[IAPCtrl] jObject.purchaseToken is null.");
                        return;
                    }

                    string purchaseToken = jObject["purchaseToken"].ToString();

                    if (uncheckedSdkGoodOrderDict != null && uncheckedSdkGoodOrderDict.Count != 0)
                    {
                        if (uncheckedSdkGoodOrderDict.ContainsKey(purchaseToken))
                        {
                            uncheckedSdkGoodOrderDict.Remove(purchaseToken);
                            PrefsUtil.WriteObject(PrefsKeyConst.IAPCtrl_uncheckedSdkGoodOrderDict, uncheckedSdkGoodOrderDict);
                        }
                    }
                    // 内购商品下单消耗
                    ConsumeIAPGoodsItem(iapItem);
                    return;
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        /// <summary>
        /// 内购校验
        /// </summary>
        private void SendC2S_iapCallback(IapPurchaseData iapPurchaseData)
        {
            try
            {
                if (iapPurchaseData == null)
                {
                    LogUtil.Log("[IAPCtrl]OnBillingIapPurchased: iapPurchaseData is null");
                    return;
                }

                C2S_iapCallback msg = new C2S_iapCallback();
                msg.data = new C2S_iapCallback_data();
                msg.data.receipt = iapPurchaseData;
                bool isSend = WSNetMgr.Instance.Send(msg);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        private void OnS2C_iapCallback(BaseS2CJsonProto respMsg)
        {
            // 关闭等待UI
            App.HideWaitUI();

            if (!string.IsNullOrEmpty(respMsg.err))
            {
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPSubscribe_err);
                LogUtil.LogError(TAG + respMsg.err);
                return;
            }

            UserSessionCtrl.Instance.StatisticEvent(StatisticConst.newbyear_iap_game_verify_respond_ok);
        }

        /// <summary>
        /// 内购下单
        /// </summary>
        private void SendC2S_IapOrder(int _goods_id)
        {
            C2S_iapOrder iapOrder = new C2S_iapOrder();
            iapOrder.data = new C2S_iapOrder_data();
            iapOrder.data.goods_id = _goods_id;
            iapOrder.Send();
        }

        /// <summary>
        /// 内购下单回调
        /// </summary>
        private void OnS2C_IapOrder(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_iapOrder iapOrderResp = respMsg as S2C_iapOrder;
                if (!string.IsNullOrEmpty(iapOrderResp.err))
                {
                    App.HideWaitUI();
                    LogUtil.LogError("[IAPCtrl] OnS2C_IapOrder is error, error = " + iapOrderResp.err);
                    return;
                }
                if (iapOrderResp.data == null)
                {
                    App.HideWaitUI();
                    LogUtil.LogError("[IAPCtrl] iapOrderResp.data is null,");
                    return;
                }

                Channel.Current.launchBillingFlow(curProductId);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        /// <summary>
        /// 消耗内购商品
        /// </summary>  
        private void ConsumeIAPGoodsItem(IAPItem iapItem)
        {
            // 关闭等待UI
            App.HideWaitUI();

            if (iapItem == null
                || iapItem.goods == null
                || iapItem.goods.product_id == null)
            {
                LogUtil.Log(TAG + "ConsumeIAPGoodsItem iapItem is null.");
                return;
            }

            if (iapItem.goods.product_id.StartsWith("iap_"))
            {
                // 已发货
                ShippedGood(iapItem);
                // 消耗
                SendC2S_notifyAck(iapItem);
            }
        }

        /// <summary>
        /// 发货内购商品
        /// </summary>
        private void ShippedGood(IAPItem iapItem)
        {
            if (shippedIapGoodOrders == null)
            {
                shippedIapGoodOrders = new List<string>();
            }

            string order_id = iapItem.goods.order_id;
            if (shippedIapGoodOrders.Contains(order_id)) return;

            shippedIapGoodOrders.Add(order_id);
            PrefsUtil.WriteObject(PrefsKeyConst.IAPCtrl_shippedIapGoodOrders, shippedIapGoodOrders);

            // 派发新增内购商品发货
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.IAP_PurchaseGood, iapItem);
        }

        /// <summary>
        /// 内购发货通知
        /// </summary>
        private void SendC2S_notifyAck(IAPItem iapItem)
        {
            C2S_iapNotifyAck iapNotifyAck = new C2S_iapNotifyAck();
            iapNotifyAck.data = new C2S_iapNotifyAck_data();
            iapNotifyAck.data.id = iapItem.id;
            iapNotifyAck.Send();
        }

        /// <summary>
        /// 内购发货通知确认回调
        /// </summary>
        /// <param name="respMsg"></param>
        private void OnS2C_notifyAck(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_iapNotifyAck iapNotifyAckResp = respMsg as S2C_iapNotifyAck;
                if (!string.IsNullOrEmpty(iapNotifyAckResp.err))
                {
                    App.HideWaitUI();
                    LogUtil.LogError("[IAPCtrl] OnS2C_notifyAck is error, error = " + iapNotifyAckResp.err);
                    //消耗失败
                    ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPGoods_err);
                    return;
                }

                //消耗成功
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPGoods_succeed);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        /// <summary>
        /// 通过商品id获取订单id
        /// </summary>
        private int GetIapIDByIap(string _productId)
        {
            if (goodsIdsDic.ContainsKey(_productId)) return goodsIdsDic[_productId];

            LogUtil.LogError(TAG + "GetIapIDByIap is error, _productId: " + _productId);
            return 0;
        }
        #endregion

        #region 订阅
        /// <summary>
        /// 响应SDK订阅回调
        /// </summary>
        private void OnBillingSubscribePurchased(object args)
        {
            try
            {
                PurchaseData purchaseData = (PurchaseData)args;
                if (purchaseData == null)
                {
                    LogUtil.Log("[IAPCtrl]OnBillingSubscribePurchased: purchaseData is null");
                    return;
                }

                // 订阅新单与补单处理
                if (subscribeIAPItem == null
                    || subscribeIAPItem.history_goods == null
                    || subscribeIAPItem.history_goods.Count == 0)
                {
                    SendC2S_subscribeCallback(purchaseData);
                }
                else
                {
                    History_goods_subscribe history_subscribe_item = subscribeIAPItem.history_goods.Find((item) => item.product_id == purchaseData.productId);
                    if (history_subscribe_item == null)
                    {
                        SendC2S_subscribeCallback(purchaseData);
                    }
                    else if (history_subscribe_item.expire_time < DateTimeMgr.Instance.GetServerTickTimestamp())
                    {
                        SendC2S_subscribeCallback(purchaseData);
                    }
                    else
                    {
                        if (existSubscriptionDict == null)
                        {
                            existSubscriptionDict = new Dictionary<PurchaseData, History_goods_subscribe>();
                        }
                        AddExistSubscription(purchaseData, history_subscribe_item);
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        /// <summary>
        /// 订阅下单
        /// </summary>
        private void SendC2S_subscribeOrder(int _goods_id)
        {
            C2S_subscribeOrder subscribeOrder = new C2S_subscribeOrder();
            subscribeOrder.data = new C2S_subscribeOrder_data();
            subscribeOrder.data.goods_id = _goods_id;
            subscribeOrder.Send();
        }

        /// <summary>
        /// 订阅下单回调
        /// </summary>
        private void OnSubscribeOrder(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_subscribeOrder subscribeOrderResp = respMsg as S2C_subscribeOrder;
                if (!string.IsNullOrEmpty(subscribeOrderResp.err))
                {
                    App.HideWaitUI();
                    LogUtil.LogError("[IAPCtrl] OnSubscribeOrder is error, error = " + subscribeOrderResp.err);
                    return;
                }
                if (subscribeOrderResp.data == null)
                {
                    App.HideWaitUI();
                    LogUtil.LogError("[IAPCtrl] subscribeOrderResp.data is null,");
                    return;
                }

                Channel.Current.launchBillingFlow(curProductId);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        /// <summary>
        /// 通过商品id获取订单id
        /// </summary>
        private int GetIapIDBySubscribe(string _productId)
        {
            if (subIdsDic.ContainsKey(_productId)) return subIdsDic[_productId];

            LogUtil.LogError(TAG + "GetIapIDBySubscribe is error, _productId: " + _productId);
            return 0;
        }

        /// <summary>
        /// 发送服务器校验订阅
        /// </summary>
        private void SendC2S_subscribeCallback(PurchaseData purchaseData)
        {
            try
            {
                UserSessionCtrl.Instance.StatisticEvent(StatisticConst.newbyear_subs_game_verify_start);

                if (existSubscriptionDict == null)
                {
                    existSubscriptionDict = new Dictionary<PurchaseData, History_goods_subscribe>();
                }
                AddExistSubscription(purchaseData, null);

                C2S_subscribeCallback msg = new C2S_subscribeCallback();
                msg.data = new C2S_subscribeCallback_data();
                msg.data.receipt.packageName = purchaseData.packageName;
                msg.data.receipt.orderId = purchaseData.orderId ?? string.Empty;
                msg.data.receipt.productId = purchaseData.productId ?? string.Empty;
                msg.data.receipt.purchaseToken = purchaseData.purchaseToken ?? string.Empty;
                bool isSend = WSNetMgr.Instance.Send(msg);

                if (isSend)
                {
                    UserSessionCtrl.Instance.StatisticEvent(StatisticConst.newbyear_subs_game_verify_request);
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(StringUtil.Concat(TAG, e.ToString()));
            }
        }

        /// <summary>
        /// 响应服务器校验订阅
        /// </summary>
        private void OnS2C_subscribeCallback(BaseS2CJsonProto protoMsg)
        {
            // 关闭等待UI
            App.HideWaitUI();

            if (!string.IsNullOrEmpty(protoMsg.err))
            {
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPSubscribe_err);
                LogUtil.LogError(TAG + protoMsg.err);
                return;
            }

            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.IAPSubscribe_succeed);
            UserSessionCtrl.Instance.StatisticEvent(StatisticConst.newbyear_subs_game_verify_respond_ok);
        }

        /// <summary>
        /// 订阅广播
        /// </summary>
        private void OnSubscribe(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_InvokeSubscribe subscibeResp = respMsg as S2C_InvokeSubscribe;
                if (!string.IsNullOrEmpty(subscibeResp.err))
                {
                    LogUtil.LogError("[IAPCtrl] OnS2C_info is error, error = " + subscibeResp.err);
                    return;
                }
                if (subscibeResp.data == null)
                {
                    LogUtil.LogError("[IAPCtrl] subscibeResp.data is null,");
                    return;
                }

                subscribeIAPItem = SerializeUtil.ToObject<SubscribeIAPItem>(subscibeResp.data.ToString());
                if (subscribeIAPItem == null || subscribeIAPItem.goods == null)
                {
                    LogUtil.Log("[IAPCtrl] subscribeIAPItem is null or subscribeIAPItem.goods is null.");
                    return;
                }

                // 0未支付，1已支付, 3重复验证
                if (subscribeIAPItem.goods.status == 1) LogUtil.Log("订阅：已支付.");
                else if (subscribeIAPItem.goods.status == 0) LogUtil.Log("订阅：未支付.");
                else if (subscribeIAPItem.goods.status == 3) LogUtil.Log("订阅：重复验证.");

                PurchaseData purchaseData = null;
                // subscribeIAPItem为空，不进行检查.
                if (subscribeIAPItem != null)
                {
                    if (existSubscriptionDict != null)
                    {
                        foreach (PurchaseData purchaseDataItem in existSubscriptionDict.Keys)
                        {
                            if (purchaseDataItem.productId == subscribeIAPItem.goods.product_id)
                            {
                                purchaseData = purchaseDataItem;
                            }
                        }
                    }
                }

                if (purchaseData == null) return;

                History_goods_subscribe historyItem = subscribeIAPItem.history_goods.Find((item) => item.product_id == purchaseData.productId);
                if (historyItem == null)
                {
                    LogUtil.LogError("[IAPCtrl] historyItem is null.");
                    return;
                }
                AddExistSubscription(purchaseData, historyItem);

                // 派发新增订阅
                CtrlDispatcher.Instance.Dispatch(CtrlMsg.IAP_PurchaseSubscribe, subscribeIAPItem.goods);
            }
            catch (Exception e)
            {
                LogUtil.LogError(TAG + e.ToString());
            }
        }

        private void AddExistSubscription(PurchaseData purchaseData, History_goods_subscribe history_subscribe_item)
        {
            if (existSubscriptionDict == null)
            {
                existSubscriptionDict = new Dictionary<PurchaseData, History_goods_subscribe>();
            }
            if (purchaseData != null)
            {
                PurchaseData prePurchaseData = null;
                foreach (PurchaseData purchaseDataItem in existSubscriptionDict.Keys)
                {
                    if (purchaseDataItem.productId == purchaseData.productId)
                    {
                        prePurchaseData = purchaseDataItem;
                    }
                }

                if (prePurchaseData != null)
                {
                    existSubscriptionDict.Remove(prePurchaseData);
                }

                existSubscriptionDict.Add(purchaseData, history_subscribe_item);
            }
        }

        private void OnHeartBeat_UpdateServerCurrTime(object obj)
        {
            if (!isInitFirst) return;

            if (existSubscriptionDict == null || existSubscriptionDict.Count == 0) return;

            if (subscribeIAPItem == null
                || subscribeIAPItem.history_goods == null
                || subscribeIAPItem.history_goods.Count == 0) return;

            // 订阅在线过期检查
            foreach (PurchaseData purchaseDataItem in existSubscriptionDict.Keys)
            {
                PurchaseData purchaseData = purchaseDataItem;
                if (existSubscriptionDict.ContainsKey(purchaseData))
                {
                    History_goods_subscribe historyItem = existSubscriptionDict[purchaseData];
                    if (historyItem != null)
                    {
                        if (historyItem.expire_time < DateTimeMgr.Instance.GetServerTickTimestamp())
                        {
                            cacheVerifyPurchaseDatas.Add(purchaseData);
                        }
                    }
                }
            }
            // 订阅在线过期校验
            for (int i = 0; i < cacheVerifyPurchaseDatas.Count; i++)
            {
                SendC2S_subscribeCallback(cacheVerifyPurchaseDatas[i]);
            }
            cacheVerifyPurchaseDatas.Clear();
        }
        #endregion
    }
}