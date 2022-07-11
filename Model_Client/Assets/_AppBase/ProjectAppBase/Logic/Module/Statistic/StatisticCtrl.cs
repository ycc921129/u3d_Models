/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using CodeStage.AntiCheat.ObscuredTypes;
using FutureCore;
using UnityEngine;

namespace ProjectApp
{
    public class StatisticCtrl : BaseCtrl
    {
        public static StatisticCtrl Instance { get; private set; }

        public LoginModel loginModel;

        /// <summary>
        /// 兑换开关
        /// </summary>
        private bool giftsSwitch
        {
            get
            {
                if (loginModel == null)
                {
                    loginModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
                }
                if (loginModel != null && loginModel.loginData != null && loginModel.loginData.info != null)
                {
                    return loginModel.loginData.info.is_open_exchange;
                }

                return false;
            }
        }

        protected override void OnInit()
        {
            Instance = this;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        protected override void AddListener()
        {
            ctrlDispatcher.AddListener(CtrlMsg.Game_Start, DoStatistic);
            ctrlDispatcher.AddListener(CtrlMsg.Module_GiftSwitchChange, DoStatistic);
        }

        protected override void RemoveListener()
        {
            ctrlDispatcher.RemoveListener(CtrlMsg.Game_Start, DoStatistic);
            ctrlDispatcher.RemoveListener(CtrlMsg.Module_GiftSwitchChange, DoStatistic);
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }

        private void OnCoinChange(ChangeValue<ObscuredDouble> param)
        {
            double change = param.newValue - param.oldValue;
            if (change < 0)
            {
                FBStatisticUtil.SpendCreditsEvent(FBMoneyType.Coin, "Coin_Null", change);
            }
        }

        private void DoStatistic(object param)
        {
            ServerRedeemEvent();
        }

        // ############## 运营 Start ##############
        /// <summary>
        /// 后台事件上报
        /// </summary>
        private void ServerRedeemEvent()
        {
            PayPal_turn_on();
            if (giftsSwitch)
            {
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.gifts_switch_on);
                Paypal_homepage();  
                Gifts_switch_open_duration();
            }
        }

        /// <summary>
        /// 兑换开启时间
        /// </summary>
        public void Gifts_switch_open_duration()
        {
            ChannelMgr.Instance.SendStatisticEventWithTime(StatisticConst.gifts_switch_open_duration, (int)Time.realtimeSinceStartup);
        }

        /// <summary>
        /// 兑换开启状态
        /// </summary>
        public void PayPal_turn_on()
        {
            UserSessionCtrl.Instance.StatisticState(StatisticConst.PayPal_turn_on, giftsSwitch ? 1 : 0);
            ChannelMgr.Instance.SendStatisticEventWithParam(StatisticConst.PayPal_turn_on, giftsSwitch ? "1" : "0");
        }

        /// <summary>
        /// PP卡首页展示
        /// </summary>
        public void Paypal_homepage()
        {
            UserSessionCtrl.Instance.StatisticEvent(StatisticConst.Paypal_homepage);
            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.Paypal_homepage);
        }

        /// <summary>
        /// PP卡领取页面展示数
        /// </summary>
        public void PayPal_impression(bool isFree)
        {
            if (isFree)
            {
                UserSessionCtrl.Instance.StatisticEvent(StatisticConst.stuff_impression_free);
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.stuff_impression_free);
                return;
            }

            UserSessionCtrl.Instance.StatisticEvent(StatisticConst.PayPal_impression);
            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.PayPal_impression);
        }

        /// <summary>
        /// 领取按钮点击数
        /// </summary>
        public void PayPal_click(bool isFree)
        {
            if (isFree)
            {
                UserSessionCtrl.Instance.StatisticEvent(StatisticConst.stuff_click_free);
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.stuff_click_free);
                return;
            }

            UserSessionCtrl.Instance.StatisticEvent(StatisticConst.PayPal_click);
            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.PayPal_click);
        }

        /// <summary>
        /// 成功领取PP卡次数(或者PP卡增加次数)
        /// </summary>
        public void PayPal_reward_success(bool isFree)
        {
            if (isFree)
            {
                UserSessionCtrl.Instance.StatisticEvent(StatisticConst.stuff_get_success_free);
                ChannelMgr.Instance.SendStatisticEvent(StatisticConst.stuff_get_success_free);
                return;
            }

            UserSessionCtrl.Instance.StatisticEvent(StatisticConst.PayPal_reward_success);
            ChannelMgr.Instance.SendStatisticEvent(StatisticConst.PayPal_reward_success);
        }
        // ############## 运营 End ##############
    }
}