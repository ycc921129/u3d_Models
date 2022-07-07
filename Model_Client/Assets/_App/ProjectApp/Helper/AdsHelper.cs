using FutureCore;
using System;
using ProjectApp.Data;

namespace ProjectApp
{
    public static partial class AdsHelper
    {
        /// <summary>
        /// ��ʾ�ر����������
        /// </summary>
        public static void ShowClosePanelInterstitialAd(Action<bool> callBack = null)
        {
            ChannelMgr.Instance.ShowInterstitialAd(AdsConst.ClosePanel_InterstitialAdId, callBack);
        }
    }
}