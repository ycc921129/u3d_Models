/****************************************************************************
* ScriptType: Ö÷¿ò¼Ü
* ÇëÎðÐÞ¸Ä!!!
****************************************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FutureCore
{
    public sealed class SceneSwitchMgr : BaseMonoMgr<SceneSwitchMgr>
    {
        private const bool IsUseUnityScene = false;

        public delegate void LoadCallBack(object param);

        public void SwitchInitialScene(int idx, LoadCallBack loadHandler, object param)
        {
            StartCoroutine(OnLoadInitialScene(idx, loadHandler, param));
        }

        public void SwitchScene(int idx, LoadCallBack loadHandler, object param)
        {
            StartCoroutine(OnLoadScene(idx, loadHandler, param));
        }

        private IEnumerator OnLoadInitialScene(int idx, LoadCallBack loadHandle, object param)
        {
            yield return YieldConst.Time10ms;

            if (loadHandle != null)
            {
                loadHandle(param);
            }
        }

        private IEnumerator OnLoadScene(int idx, LoadCallBack loadHandle, object param)
        {
            yield return YieldConst.WaitFor100ms;

            ResMgr.Instance.UnloadNullReferenceAssets();
            ResMgr.Instance.ClearDynamicCache();

            AsyncOperation asyncGC = ResMgr.Instance.GCAssets(true);
            yield return asyncGC;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            #region C_DEBUG_AB_PROFILER
#if C_DEBUG_AB_PROFILER
            LogUtil.Log("ABÄÚ´æ¿ìÕÕ:", AssetBundleMgr.Instance.GetLoadedABsInfo());
#endif
            #endregion C_DEBUG_AB_PROFILER

            if (IsUseUnityScene)
            {
                AsyncOperation asyncUnityScene = SceneManager.LoadSceneAsync(idx, LoadSceneMode.Single);
                yield return asyncUnityScene;
            }

            if (loadHandle != null)
            {
                loadHandle(param);
            }
        }
    }
}