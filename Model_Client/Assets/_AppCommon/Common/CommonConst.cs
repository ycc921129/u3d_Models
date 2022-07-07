/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectApp
{
    public static class CommonConst 
    {
        /// <summary>
        /// 默认头像
        /// </summary>
        public const string defaultHead = "icon_defaultHead";

        /// <summary>
        /// 默认国家
        /// </summary>
        public const string defaultCountry = "nation_default";

        /// <summary>
        /// 国企前缀
        /// </summary>
        public const string defaultCountry_prefix = "nation_";

        /// <summary>
        /// 恭喜图片前缀
        /// </summary>
        public const string congratulation_prefix = "congreatulation_";

        /// <summary>
        /// 金币前面的 X 符号
        /// </summary>
        public const string default_X = "×";

        /// <summary>
        /// 金币前面的 + 符号
        /// </summary>
        public const string default_add = "+";

        /// <summary>
        /// 本地图片加载前缀
        /// </summary>
        public const string Sprite_prefix = "Sprite/";

        #region 功能模块
        /// <summary>
        /// 是否开启大喇叭功能
        /// </summary>
        private static bool openBroadcastFunc = true;

        public static bool OpenBroadcastFunc
        {
            get
            {
                return openBroadcastFunc;
            }
            set
            {
                openBroadcastFunc = value;

                if (value)
                {
                    LogUtil.Log("我冲洗开启了大喇叭");
                    CtrlDispatcher.Instance.Dispatch(CtrlMsg.EnableBroadcast);//激活大喇叭
                }
            }
        }

        public static bool RewardUIIsNeedMask = true;

        public static bool RaffleRedPiont = false;

        /// <summary>
        /// 是否在更新状态
        /// </summary>
        private static bool updataStatus = false;

        public static bool UpdataStatus
        {
            get
            {
                return updataStatus;
            }
            set
            {
                updataStatus = value;

                if (!value)
                {
                    LogUtil.Log("我关闭了更新界面");
                    if (!App.GetIsWeakNetwork())
                    {
                        CtrlDispatcher.Instance.Dispatch(CtrlMsg.EnableReconect);//激活断线
                    }
                }
            }
        }

        /// <summary>
        /// 是否正在播放奖励动画
        /// </summary>
        private static bool rewardAniStatus = false;

        public static bool RewardAniStatus
        {
            get
            {
                return rewardAniStatus;
            }
            set
            {
                rewardAniStatus = value;

                if (!value)
                {
                    LogUtil.Log("我停止了动画界面");
                    CtrlDispatcher.Instance.Dispatch(CtrlMsg.EnableBasicInfo);//激活短线
                }
            }
        }
        #endregion
    }
}
