/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using System.Collections.Generic;

namespace ProjectApp
{
    public class GameScene : BaseScene
    {
        public override int SceneIdx { get { return SceneConst.GameIdx; } }
        public override int PreLoadId { get { return PreLoadIdConst.GamePlayScene; } }

        private static AssetLoader m_loader;
        public static AssetLoader Loader
        {
            get
            {
                if (m_loader == null)
                {
                    m_loader = new AssetLoader();
                }
                return m_loader;
            }
        }

        private Dictionary<string, UAssetType> preLoadAssetDict = new Dictionary<string, UAssetType>();

        public GameScene()
        {
        }

        public override AssetLoader GetLoader()
        {
            return Loader;
        }

        protected override void OnEnter()
        {
            AppDispatcher.Instance.AddListener(AppMsg.UI_LoadingUIProgressComplete, OnLoadingComplete);

            App.SetLoadingUI("加载玩法", 0, AppConst.IsLoadingDelay);
        }

        protected override void OnLeave()
        {
            AppDispatcher.Instance.RemoveListener(AppMsg.UI_LoadingUIProgressComplete, OnLoadingComplete);

            LoadPipelineMgr.Instance.UnloadPreLoad(PreLoadId);
            preLoadAssetDict.Clear();
        }

        protected override void OnSwitchSceneComplete(object param)
        {
            LoadPipelineMgr.Instance.PreLoad(PreLoadId, Loader, preLoadAssetDict, OnPreLoadCB, OnPreLoadComplete);
        }

        private void OnPreLoadCB(int curCount, int totalCount)
        {
            float loadPercent = (float)curCount / totalCount;
            int percent = 0 + (int)(loadPercent * (100 - 0));
            App.SetLoadingUIProgress(percent, AppConst.IsLoadingDelay);
        }

        private void OnPreLoadComplete()
        {
            App.SetLoadingUIProgressComplete(AppConst.IsLoadingDelay);
        }

        private void OnLoadingComplete(object param)
        {
            ShowScene();
            HideLoadingUI();
        }

        private void ShowScene()
        {
            LogUtil.Log("[GameScene]ShowScene");
        }

        public override void Dispose()
        {
            if (null != m_loader)
            {
                m_loader.Dispose();
                m_loader = null;
            }
        }
    }
}