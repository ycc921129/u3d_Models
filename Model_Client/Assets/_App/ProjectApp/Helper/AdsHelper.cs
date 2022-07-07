using FutureCore;
using System;
using ProjectApp.Data;

namespace ProjectApp
{
    public static partial class AdsHelper
    {
        /// <summary>
        /// 显示关闭面板插屏广告
        /// </summary>
        public static void ShowClosePanelInterstitialAd(Action<bool> callBack = null)
        {
            ChannelMgr.Instance.ShowInterstitialAd(AdsConst.ClosePanel_InterstitialAdId, callBack);
        }
    }
}