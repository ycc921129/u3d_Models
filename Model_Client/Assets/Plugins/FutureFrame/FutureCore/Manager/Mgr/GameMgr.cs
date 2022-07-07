/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.13
*/

using UnityEngine;

namespace FutureCore
{
    public sealed class GameMgr : BaseMgr<GameMgr>
    {
        public bool IsPause { get; private set; }
        private float pauseCacheTimeScale = 1;

        #region Game
        public void Restart()
        {
            LogUtil.Log("[GameMgr]Restart");
            App.Restart();
        }

        public void Quit()
        {
            LogUtil.Log("[GameMgr]Quit");
            App.Quit();
        }

        public void Pause()
        {
            LogUtil.Log("[GameMgr]Pause");
            IsPause = true;
            AppGlobal.IsGamePause = true;
            AppDispatcher.Instance.Dispatch(AppMsg.App_GamePause);
            AudioMgr.Instance.PauseAllSource();
            pauseCacheTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            LogUtil.Log("[GameMgr]Resume");
            IsPause = false;
            AppGlobal.IsGamePause = IsPause;
            AppDispatcher.Instance.Dispatch(AppMsg.App_GameResume);
            AudioMgr.Instance.UnPauseAllSource();
            Time.timeScale = pauseCacheTimeScale;
        }
        #endregion

        #region Scene

        public void InitialMain()
        {
            LogUtil.Log("[GameMgr]InitialMain");
            SceneMgr.Instance.InitialMain();
        }

        public void EnterMain()
        {
            LogUtil.Log("[GameMgr]EnterMain");
            SceneMgr.Instance.SwitchScene(SceneMgr.DefaultMainSceneIdx);
        }

        #endregion Scene

        #region Msg

        private void AddListener()
        {
        }

        private void RemoveListener()
        {
        }

        #endregion Msg

        #region Private

        public override void Init()
        {
            base.Init();
            AddListener();
        }

        public override void Dispose()
        {
            base.Dispose();
            RemoveListener();
        }

        #endregion Private
    }
}