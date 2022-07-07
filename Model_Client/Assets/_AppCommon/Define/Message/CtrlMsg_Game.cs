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
    public static partial class CtrlMsg
    {
        public const uint CtrlMsg_Default = 190000;

        public const uint CtrlMsg_Raffle = 191000;

        public const uint CtrlMsg_Redeem = 192000;

        public const uint CtrlMsg_UI = 195000;

        #region default
        /// <summary>
        /// 新的一天
        /// </summary>
        public const uint NewDays = CtrlMsg_Default + 1;

        /// <summary>
        /// 新的一个月
        /// </summary>
        public const uint NewMonth = CtrlMsg_Default + 2;

        /// <summary>
        /// Claim 任务
        /// </summary>
        public const uint ClaimTaskAchievement = CtrlMsg_Default + 3;

        /// <summary>
        /// 更新任务UI
        /// </summary>
        public const uint UpdataTaskAchivenmentUI = CtrlMsg_Default + 4;

        /// <summary>
        /// 绑定登陆信息完成
        /// </summary>
        public const uint BindLoginTokenFinish = CtrlMsg_Default + 5;

        /// <summary>
        /// 绑定手机信息完成
        /// </summary>
        public const uint BindMobileFinish = CtrlMsg_Default + 6;

        /// <summary>
        /// 被邀请成功
        /// </summary>
        public const uint InvitedSuccess = CtrlMsg_Default + 7;

        /// <summary>
        /// 每日挑战开始任务
        /// </summary>
        public const uint DailyChallengeStart = CtrlMsg_Default + 8;

        /// <summary>
        /// 每日挑战结束任务
        /// </summary>
        public const uint DailyChallengeFinish = CtrlMsg_Default + 9;

        /// <summary>
        /// 每日挑战胜利
        /// </summary>
        public const uint DailyChallengeWin = CtrlMsg_Default + 10;

        /// <summary>
        /// 每日挑战失败
        /// </summary>
        public const uint DailyChallengeLose = CtrlMsg_Default + 11;

        /// <summary>
        /// 添加一条公告
        /// </summary>
        public const uint AddBroadcast = CtrlMsg_Default + 12;

        /// <summary>
        /// 关闭更新界面
        /// </summary>
        public const uint EnableReconect = CtrlMsg_Default + 13;

        /// <summary>
        /// 激活 大喇叭
        /// </summary>
        public const uint EnableBroadcast = CtrlMsg_Default + 14;

        /// <summary>
        /// 执行下一个登陆弹窗流程
        /// </summary>
        public const uint NextGameStartProcess = CtrlMsg_Default + 15;

        /// <summary>
        /// 完成登陆弹窗流程
        /// </summary>
        public const uint GameStartProcessFinish = CtrlMsg_Default + 16;

        /// <summary>
        /// 打开其他的Normal界面
        /// </summary>
        public const uint OpenNormalView = CtrlMsg_Default + 17;

        /// <summary>
        /// 修改钻石动画的默认位置
        /// </summary>
        public const uint SetRewardConst = CtrlMsg_Default + 18;

        /// <summary>
        /// 激活状态栏信息
        /// </summary>
        public const uint EnableBasicInfo = CtrlMsg_Default + 19;

        /// <summary>
        /// 开始每日弹窗流程
        /// </summary>
        public const uint GameStartProcess = CtrlMsg_Default + 20;

        /// <summary>
        /// 清理每日Preferences数据
        /// </summary>
        public const uint DisposeDailyPerferences = CtrlMsg_Default + 21;

        /// <summary>
        /// 服务器时区新的一天
        /// </summary>
        public const uint ServerNewDays = CtrlMsg_Default + 22;

        /// <summary>
        /// 更新任务列表
        /// </summary>
        public const uint UpdateTaskDic = CtrlMsg_Default + 23;

        /// <summary>
        /// 更新金币
        /// </summary>
        public const uint UpdateCoin = CtrlMsg_Default + 24;
        #endregion

        #region UI相关
        /// <summary>
        /// 打开签到
        /// </summary>
        public const uint OpenCheckIn = CtrlMsg_UI + 1;

        /// <summary>
        ///打开离线
        /// </summary>
        public const uint OpenOffline = CtrlMsg_UI + 2;

        /// <summary>
        /// 打开订阅
        /// </summary>
        public const uint OpenSubscribe = CtrlMsg_UI + 3;

        /// <summary>
        /// 打开邀请界面弹窗
        /// </summary>
        public const uint OpenReferralCode = CtrlMsg_UI + 4;

        /// <summary>
        /// 打开转盘界面
        /// </summary>
        public const uint OpenSpin = CtrlMsg_UI + 5;

        /// <summary>
        /// 打开设置界面
        /// </summary>
        public const uint OpenSetting = CtrlMsg_UI + 6;

        /// <summary>
        /// 打开抽奖
        /// </summary>
        public const uint OpenRaffle = CtrlMsg_UI + 7;

        /// <summary>
        /// 打开抽奖规则
        /// </summary>
        public const uint OpenRaffleRules = CtrlMsg_UI + 8;

        /// <summary>
        /// 打开开奖奖励
        /// </summary>
        public const uint OpenRaffleReward = CtrlMsg_UI + 9;
        #endregion
    }
}
