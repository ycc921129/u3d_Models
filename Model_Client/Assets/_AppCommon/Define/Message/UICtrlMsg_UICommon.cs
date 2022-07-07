/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectApp
{
    public static partial class UICtrlMsg
    {
        public const uint BASE_CommonUI = 10000;

        // 公告
        public const uint C_OpenBroadcastUI = BASE_CommonUI + 1;
        public const uint C_CloseBroadcastUI = BASE_CommonUI + 2;

        // CheckIn
        public const uint C_OpenCheckInUI = BASE_CommonUI + 3;
        public const uint C_CloseCheckInUI = BASE_CommonUI + 4;

        // Reconnect
        public const uint C_OpenReconnectUI = BASE_CommonUI + 5;
        public const uint C_CloseReconnectUI = BASE_CommonUI + 6;

        // Updata
        public const uint C_OpenUpdataUI = BASE_CommonUI + 7;
        public const uint C_CloseUpdateUI = BASE_CommonUI + 8;

        // FeedBack
        public const uint C_OpenFeedBackUI = BASE_CommonUI + 9;
        public const uint C_CloseFeedBackUI = BASE_CommonUI + 10;

        // RateUs
        public const uint C_OpenRateUsUI = BASE_CommonUI + 11;
        public const uint C_CloseRateUsUI = BASE_CommonUI + 12;

        // FullRate
        public const uint C_OpenFullRateUI = BASE_CommonUI + 13;
        public const uint C_CloseFullRateUI = BASE_CommonUI + 14;

        // Invite
        public const uint C_OpenInviteUI = BASE_CommonUI + 15;
        public const uint C_CloseInviteUI = BASE_CommonUI + 16;

        // ReferralCode
        public const uint C_OpenReferralCodeUI = BASE_CommonUI + 17;
        public const uint C_CloseReferralCodeUI = BASE_CommonUI + 18;

        // Raffle
        public const uint C_OpenRaffleUI = BASE_CommonUI + 19;
        public const uint C_CloseRaffleUI = BASE_CommonUI + 20;

        // LotteryRank
        public const uint C_OpenLotteryRankUI = BASE_CommonUI + 21;
        public const uint C_CloseLotteryRankUI = BASE_CommonUI + 22;

        // RaffleReward
        public const uint C_OpenRaffleRewardUI = BASE_CommonUI + 23;
        public const uint C_CloseRaffleRewardUI = BASE_CommonUI + 24;

        // Login
        public const uint C_OpenLoginUI = BASE_CommonUI + 25;
        public const uint C_CloseLoginUI = BASE_CommonUI + 26;

        // Task
        public const uint C_OpenTaskAchievementUI = BASE_CommonUI + 27;
        public const uint C_CloseTaskAchievementUI = BASE_CommonUI + 28;

        // RewardTrasition
        public const uint C_OpenRewardTrasitionUI = BASE_CommonUI + 29;
        public const uint C_CloseRewardTrasitionUI = BASE_CommonUI + 30;

        // WakeUp
        public const uint C_OpenWakeUpUI = BASE_CommonUI + 31;
        public const uint C_CloseWakeUpUI = BASE_CommonUI + 32;

        // WakeUpRule
        public const uint C_OpenWakeUpRuleUI = BASE_CommonUI + 33;
        public const uint C_CloseWakeUpRuleUI = BASE_CommonUI + 34;

        // BasicInfo
        public const uint BasicInfoUI_Open = BASE_CommonUI + 35;
        public const uint BasicInfoUI_Close = BASE_CommonUI + 36;

        // loading
        public const uint GameLoadingUI_Open = BASE_CommonUI + 37;
        public const uint GameLoadingUI_Close = BASE_CommonUI + 38;

        // Activity
        public const uint ActivityUI_Open = BASE_CommonUI + 39;
        public const uint ActivityUI_Close = BASE_CommonUI + 40;

        // Congratulation
        public const uint CongratulationUI_Open = 10141;
        public const uint CongratulationUI_Close = 10142;

        //Redeem
        public const uint C_OpenRedeemUI = BASE_CommonUI + 43;
        public const uint C_CloseRedeemUI = BASE_CommonUI + 44;

        // RedeemLogin
        public const uint C_OpenCashLoginUI = BASE_CommonUI + 45;
        public const uint C_CloseCashLoginUI = BASE_CommonUI + 46;

        // GiftWallet
        public const uint C_OpenGiftWalletUI = BASE_CommonUI + 47;
        public const uint C_CloseGiftWalletUI = BASE_CommonUI + 48;

        // Loading
        public const uint Ctrl_LoadingOpen = BASE_CommonUI + 49;
        public const uint Ctrl_LoadingClose = BASE_CommonUI + 50;

        // tips
        public const uint C_OpenTipsUI = BASE_CommonUI + 51;
        public const uint C_CloseTipsUI = BASE_CommonUI + 52;

        // WaterMask
        public const uint WaterMaskUI_Open = BASE_CommonUI + 53;
        public const uint WaterMaskUI_Close = BASE_CommonUI + 54;

        // TicketBanner
        public const uint TicketBannerUI_Open = BASE_CommonUI + 55;
        public const uint TicketBannerUI_Close = BASE_CommonUI + 56;

        // RaffleRules
        public const uint RaffleRulesUI_Open = BASE_CommonUI + 57;
        public const uint RaffleRulesUI_Close = BASE_CommonUI + 58;

        // PayPalTrialUI
        public const uint PayPalTrialUI_Open = BASE_CommonUI + 59;
        public const uint PayPalTrialUI_Close = BASE_CommonUI + 60;

        // PayPalTrialFriendsUI
        public const uint PayPalTrialFriendsUI_Open = BASE_CommonUI + 61;
        public const uint PayPalTrialFriendsUI_Close = BASE_CommonUI + 62;

        // GPCardCongratulationUI
        public const uint GPCardCongratulationUI_Open = BASE_CommonUI + 63;
        public const uint GPCardCongratulationUI_Close = BASE_CommonUI + 64;

        // GPCardExchangeUI
        public const uint GPCardExchangeUI_Open = BASE_CommonUI + 65;
        public const uint GPCardExchangeUI_Close = BASE_CommonUI + 66;
        
        // VerifySMS
        public const uint GetVerifyCodeUI_Open = BASE_CommonUI + 67;
        public const uint GetVerifyCodeUI_Close = BASE_CommonUI + 68;
        public const uint CountryListDialogUI_Open = BASE_CommonUI + 69;
        public const uint CountryListDialogUI_Close = BASE_CommonUI + 70;
        public const uint GetVerifyCodeUI_Refresh = BASE_CommonUI + 71;
        public const uint VerifyCodeUI_Open = BASE_CommonUI + 72;
        public const uint VerifyCodeUI_Close = BASE_CommonUI + 73;
        
        
        // PP 兑换
        public const uint PPRedeemUI_Open = BASE_CommonUI + 74;
        public const uint PPRedeemUI_Close = BASE_CommonUI + 75;
        
    }
}
