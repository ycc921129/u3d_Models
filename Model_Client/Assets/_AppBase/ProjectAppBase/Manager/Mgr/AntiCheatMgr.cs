/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using FutureCore;
using CodeStage.AntiCheat.Detectors;

namespace ProjectApp
{
    public sealed class AntiCheatMgr : BaseMonoMgr<AntiCheatMgr>
    {
        [HideInInspector]
        public bool isCheaterShpeed = false;

        public override void Init()
        {
            base.Init();
            //GameObject go = Instantiate(Resources.Load<GameObject>("Preset/AntiCheatMgr/Anti-Cheat Toolkit"));
            //go.transform.SetParent(transform, false);
        }

        public override void StartUp()
        {
            base.StartUp();
            //SpeedHackDetector.StartDetection(OnSpeedHackDetected);
        }

        private void OnSpeedHackDetected()
        {
            isCheaterShpeed = true;
            AppDispatcher.Instance.Dispatch(AppMsg.AntiCheat_SpeedHackDetected);
            LogUtil.LogError("[AntiCheatMgr]SpeedHackDetected aid: " + Channel.Current.aid);
        }
    }
}
