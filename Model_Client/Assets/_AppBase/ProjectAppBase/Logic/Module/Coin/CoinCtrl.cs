/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp.Data;
using ProjectApp.Protocol;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectApp
{
    public class CoinCtrl : BaseCtrl
    {
        public const string TAG = "[CoinCtrl] ";

        public static CoinCtrl Instance { get; private set; }

        public int lastCoin { get; private set; } = 0;

        private LoginModel userModel = null;

        protected override void OnInit()
        {
            Instance = this;
            userModel = ModuleMgr.Instance.GetModel(ModelConst.LoginModel) as LoginModel;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }

        protected override void AddListener()
        {
        }

        protected override void RemoveListener()
        {
        }

        protected override void AddServerListener()
        {


        }

        protected override void RemoveServerListener()
        {


        }

        /// <summary>
        /// 金币当前额度
        /// </summary>  
        public int CurCoin
        {
            get
            {
                if (userModel != null && userModel.loginData != null && userModel.loginData.acct != null)
                {
                    return userModel.loginData.acct.coin;
                }

                return 0;
            }
        }

        public float CurMoney
        {
            get
            {
                float tempMoney = CommonGlobal.Instance.Coefficient * CurCoin;
                tempMoney = Mathf.FloorToInt(tempMoney * 1000) * 1.0f / 1000;
                return tempMoney;
            }
        }

        public string CurMoneyStr
        {
            get
            {
                return StringUtil.Concat(CommonGlobal.Instance.Currency, CurMoney.ToString());
            }
        }

        public string MoneyStr(float _money)
        {
            return StringUtil.Concat(CommonGlobal.Instance.Currency, _money.ToString());
        }


        private void OnUpdateCoin(BaseS2CJsonProto respMsg)
        {
            try
            {
                S2C_InvokeUpdateCoin updateCoinResp = respMsg as S2C_InvokeUpdateCoin;
                if (!string.IsNullOrEmpty(updateCoinResp.err))
                {
                    LogUtil.LogError("[InfoCtrl] OnS2C_info is error, error = " + updateCoinResp.err);
                    return;
                }
                if (updateCoinResp.data == null)
                {
                    LogUtil.LogError("[InfoCtrl] updateCoinResp.data is null,");
                    return;
                }
                JObject jObject = updateCoinResp.data as JObject;
                if (jObject == null)
                {
                    LogUtil.LogError("[InfoCtrl] jObject is null,");  
                    return;
                }

                if (jObject["coin"] != null)
                {
                    userModel.loginData.acct.coin = int.Parse(jObject["coin"].ToString());
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(StringUtil.Concat("[CoinCtrl] ", e.ToString()));
            }
            CtrlDispatcher.Instance.Dispatch(CtrlMsg.UpdateCoin);
        }

        /// <summary>
        /// 保存金币-默认区间配表，区间增加货币
        /// </summary>  
        public void SaveCommonCoin()
        {
            var curLevel = CurLevelConfData();
            int nextCoin = NextCoin();
            UpdateCoinByServer(nextCoin, curLevel, "rewardRange");
        }

        /// <summary>
        /// 保存金币-自定义区间配表，区间增加货币
        /// </summary>  
        public void SaveCusCoin(int coin, BaseVO vo, string fileName)
        {
            UpdateCoinByServer(coin, vo, fileName);
        }

        /// <summary>  
        /// 保存金币-默认静态区间,自定义增加金币
        /// </summary>
        public void SaveStaticCoin(int coin)
        {
            AppCommonVO vo = AppCommonVOModel.Instance.GetVOByStaticKey(AppCommonVOStaticKey.coinStaticRange);
            UpdateCoinByServer(coin, vo, "staticValue");
        }

        /// <summary>
        /// 当前等级配置数据
        /// </summary>
        public MMCoinVO CurLevelConfData()
        {
            var list = MMCoinVOModel.Instance.GetVOList();
            if (list == null || list.Count <= 0)
            {
                return null;
            }

            float curCoin = CurCoin;
            for (int i = 0; i < list.Count; i++)
            {
                if (curCoin <= list[i].level)
                {
                    return list[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 金币奖励值
        /// </summary>
        /// <returns></returns>
        public int NextCoin()
        {
            var curLevelConf = this.CurLevelConfData();
            if (curLevelConf == null)
            {
                return 0;
            }

            // 通过期望计算值
            var average = curLevelConf.rewardAverage;
            var min = curLevelConf.rewardRange[0];
            var max = curLevelConf.rewardRange[1];

            if (average > min && average <= max && lastCoin != 0)
            {
                if (lastCoin > min && lastCoin <= average)
                    lastCoin = Mathf.RoundToInt(Random.Range(lastCoin, average));
                else if (lastCoin > average && lastCoin <= max)
                    lastCoin = Mathf.RoundToInt(Random.Range(average, lastCoin));
                else
                    lastCoin = Mathf.RoundToInt(Random.Range(min, max));
            }
            else
            {
                lastCoin = Mathf.RoundToInt(Random.Range(min, max));
            }

            return lastCoin;
        }

        /// <summary>
        /// 更新金币到服务器
        /// </summary>  
        public void UpdateCoinByServer(long coin, BaseVO vo, string fieldName)
        {
            if (vo == null)
            {
                LogUtil.LogError(StringUtil.Concat(TAG, " vo is null."));
                return;
            }

            ArrayList configPos = GetConfigPos(vo, fieldName);
            C2S_update_spec_coin req = new C2S_update_spec_coin();
            req.data = new C2S_update_spec_coin_data();
            req.data.type = "coin";
            req.data.value = coin;
            req.data.checkpos = configPos;

            string formInfo = string.Empty;
            for (int i = 0; i < configPos.Count; i++)
            {
                formInfo = StringUtil.Concat(formInfo, configPos[i].ToString());
                if (i != configPos.Count - 1)
                {
                    formInfo += "_";
                }
            }
             
            req.Send();
        }

        /// <summary>
        /// 获取配置表位置
        /// </summary>
        private ArrayList GetConfigPos(BaseVO vo, string fieldName)
        {
            ArrayList configPos = new ArrayList();
            configPos.Add(vo.GetVOModel().VOName);
            configPos.Add(vo.index);
            configPos.Add(fieldName);
            return configPos;
        }
    }
}