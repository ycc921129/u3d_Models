using FutureCore;
using ProjectApp;
using ProjectApp.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using ProjectApp.Data;
using Spine.Unity;

public class ProtobufExample : MonoBehaviour
{
    public SkeletonAnimation spinGO; 
    void Start()
    {
        //AddListenEvents();        
    }  

    private void AddListenEvents()
    {

    }

    private void OnS2C_exchangeOrder(BaseS2CJsonProto obj)
    {

    }

    private void OnS2C_goodsList(BaseS2CJsonProto obj)
    {
        if(obj == null)
        {
            LogUtil.LogError("S2C_goodsList is null");
            return;
        }
        S2C_goodsList goodsList = obj as S2C_goodsList;
        RedeemGoods(goodsList);
    }

    private void RedeemGoods(S2C_goodsList goodsList)
    {
        C2S_exchangeOrder exchangeOrder = new C2S_exchangeOrder();
        exchangeOrder.data = new C2S_exchangeOrder_data();  
        exchangeOrder.data.goods_id = 17;
        exchangeOrder.data.recv_acct = new Acct
        {
            name = "test",
            account = "123"
        };         
        exchangeOrder.Send();  
    }

    private void SendGoods()
    {
        C2S_goodsList goodsList = new C2S_goodsList();
        goodsList.data = new C2S_goodsList_data();
        goodsList.data.types = new string[] { "exchange" };
        goodsList.Send();  
    }


#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ///SpineHelper.PlayAnim(spinGO, "broken");
            //SendGoods();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //SpineHelper.PlayAnim(spinGO, "recover");
            //CoinCtrl.Instance.SaveStaticCoin(-100);  
        }  
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //SpineHelper.PlayAnim(spinGO, "idle");
            //CoinCtrl.Instance.SaveCommonCoin();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //CoinCtrl.Instance.SaveCusCoin(-100, CoinCtrl.Instance.CurLevelConfData(), "rewardRange");         
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //LogUtil.LogError(AppCommonVOStatic.CoinsLimitValue);
        }
    }
#endif
}
