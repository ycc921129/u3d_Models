/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using UnityEngine;


namespace ProjectApp
{
    public class CommonInitCtrl : BaseCtrl
    {
        private float[] singularProbability = new float[2] { 0f, 1f };

        private float[] pluralProbability = new float[2] { 1f, 0f };

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnDispose()
        {
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            ctrlDispatcher.AddListener(CtrlMsg.GameStartProcess, InitGameStartProcess);
            ctrlDispatcher.AddListener(CtrlMsg.NextGameStartProcess, NextGameStartProcess);

            ctrlDispatcher.AddListener(CtrlMsg.Preferences_InitComplete, InitCommonTools);
            ctrlDispatcher.AddListener(CtrlMsg.Module_InviteCountChange, SaveInviteTask);
            ctrlDispatcher.AddListener(CtrlMsg.Game_Start, InitInviteTask);
        }


        protected override void RemoveListener()
        {
            ctrlDispatcher.RemoveListener(CtrlMsg.GameStartProcess, InitGameStartProcess);
            ctrlDispatcher.RemoveListener(CtrlMsg.NextGameStartProcess, NextGameStartProcess);

            ctrlDispatcher.RemoveListener(CtrlMsg.Preferences_InitComplete, InitCommonTools);
            ctrlDispatcher.RemoveListener(CtrlMsg.Module_InviteCountChange, SaveInviteTask);
            ctrlDispatcher.RemoveListener(CtrlMsg.Game_Start, InitInviteTask);
        }

        protected override void AddServerListener()
        {
        }

        protected override void RemoveServerListener()
        {
        }


        public void InitCommonTools(object args = null)
        {
            CommonGlobal.Instance.Init();
        }
        #endregion
        TaskSequence taskSequence;

        TaskProcedure procedure;

        /// <summary>
        /// game login popups process  checkIn  ---  offine  ---  subscribe  ---  invite
        /// </summary>
        /// <param name="param"></param>
        private void InitGameStartProcess(object param)
        {
            PreferencesMgr.Instance.GameStartCount += 1;
            taskSequence = new TaskSequence();
            taskSequence.Add(GameStartConst.openCheckIn, ShowCheckInView);
            taskSequence.Add(GameStartConst.openOffine, ShowOffineView);
            ProbabilityShowSubscribeOrReferralCode();
            taskSequence.Add(true, GameStartProcessEnd);
            taskSequence.Run();
        }

        private void NextGameStartProcess(object param)
        {
            if (procedure != null)
            {
                procedure.InvokeComplete();
            }
        }

        public void ProbabilityShowSubscribeOrReferralCode()
        {
            if ((!GameStartConst.OpenSubscribe && GameStartConst.OpenReferralCode) /*|| (GameStartConst.OpenReferralCode && CommonGlobal.Instance.IsVip())*/)
            {
                taskSequence.Add(GameStartConst.OpenReferralCode, ShowInviteReferralCodeView);
                return;
            }

            //
            //if (!GameStartConst.OpenReferralCode && GameStartConst.OpenSubscribe || (GameStartConst.OpenSubscribe && InvitedWakeUpGlobal.Instance.Invited))
            //{
            //    taskSequence.Add(GameStartConst.OpenSubscribe, ShowSubscribeView);
            //    return;
            //}
            //float[] rate = PreferencesMgr.Instance.GameStartCount % 2 == 0 ? singularProbability : pluralProbability;
            //float probability = Random.Range(0, 1f);
            //if (probability <= rate[0])
            //{
            //    taskSequence.Add(GameStartConst.OpenSubscribe, ShowSubscribeView);
            //}
            //else  
            //{
            //    taskSequence.Add(GameStartConst.OpenReferralCode, ShowInviteReferralCodeView);
            //}

            taskSequence.Add(GameStartConst.OpenReferralCode, ShowInviteReferralCodeView);
        }

        private void ShowCheckInView(TaskProcedure obj)
        {
            procedure = obj;

            //if (!CheckInGlobal.Instance.TodayReceiveReward)
            //{
            //    ctrlDispatcher.Dispatch(CtrlMsg.OpenCheckIn);
            //}
            //else
            //{
            //    NextGameStartProcess(null);
            //}
        }

        private void ShowOffineView(TaskProcedure obj)
        {
            procedure = obj;
            if (CommonGlobal.Instance.CanShowOffline())
            {
                ctrlDispatcher.Dispatch(CtrlMsg.OpenOffline);
            }
            else
            {
                NextGameStartProcess(null);
            }
        }

        private void ShowInviteReferralCodeView(TaskProcedure obj)
        {
            procedure = obj;
            //ODO 邀请系统
            //if (!InvitedWakeUpGlobal.Instance.Invited && !CommonGlobal.Instance.Is_open_exchange)
            //{  
            //    ctrlDispatcher.Dispatch(CtrlMsg.OpenReferralCode);
            //}
            //else
            //{
            //    NextGameStartProcess(null);
            //}         
        }
        
        private void GameStartProcessEnd(TaskProcedure obj)
        {
            procedure = null;
            LogUtil.Log("游戏启动弹窗流程结束");
            //ctrlDispatcher.Dispatch(CtrlMsg.GameStartProcessFinish);
        }

        public void SaveInviteTask(object args = null)
        {
            //ODO 邀请系统
            //int sum = (int)args;
            //TaskAchievementGlobal.Instance.RecordMaxValue(PreferencesTaskConst.Invite_N_Friends, sum);//保存分享任务
        }

        public void InitInviteTask(object args = null)
        {
            //ODO 邀请系统
            //int sum = CommonGlobal.Instance.infoModel.invite.count;
            //TaskAchievementGlobal.Instance.RecordMaxValue(PreferencesTaskConst.Invite_N_Friends, sum);//保存分享任务
        }
    }
}