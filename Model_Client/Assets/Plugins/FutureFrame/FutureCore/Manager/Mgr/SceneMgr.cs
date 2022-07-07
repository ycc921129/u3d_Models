/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public sealed class SceneMgr : BaseMgr<SceneMgr>
    {
        public const int DefaultMainSceneIdx = 0;

        private Dictionary<int, BaseScene> sceneDict = new Dictionary<int, BaseScene>();
        private BaseScene m_currScene;

        public void AddScene(BaseScene scene)
        {
            if (!sceneDict.ContainsKey(scene.SceneIdx))
            {
                sceneDict[scene.SceneIdx] = scene;
            }
        }

        public void InitialMain(object param = null)
        {
            if (sceneDict.Count == 0) return;

            LogUtil.LogFormat("[SceneMgr]Start To Init Main Scene {0} Idx", DefaultMainSceneIdx);
            BaseScene scene = GetScene(DefaultMainSceneIdx);
            if (SetScene(scene))
            {
                SceneSwitchMgr.Instance.SwitchInitialScene(DefaultMainSceneIdx, scene.SwitchSceneComplete, param);
            }
        }

        public void SwitchScene(int sceneIdx, object param = null)
        {
            LogUtil.LogFormat("[SceneMgr]Switch Scene To {0} Idx", sceneIdx);
            BaseScene scene = GetScene(sceneIdx);
            if (SetScene(scene))
            {
                UIMgr.Instance.SwitchSceneCloseAllUI();
                SceneSwitchMgr.Instance.SwitchScene(sceneIdx, scene.SwitchSceneComplete, param);
            }
        }

        public BaseScene GetCurrScene()
        {
            return m_currScene;
        }

        public int GetCurrSceneIdx()
        {
            return m_currScene.SceneIdx;
        }

        public bool IsInThisScene(int sceneIdx)
        {
            return m_currScene.SceneIdx == sceneIdx;
        }

        public AssetLoader GetCurrLoader()
        {
            return m_currScene.GetLoader();
        }

        private bool SetScene(BaseScene scene)
        {
            if (scene == null)
            {
                LogUtil.LogError("[SceneMgr]Set Scene Failed: Scene Is Null");
                return false;
            }
            else if (scene == m_currScene)
            {
                LogUtil.LogError("[SceneMgr]Set Scene Failed: Switch Repetitive Scene");
                return false;
            }

            if (m_currScene != null)
            {
                m_currScene.Leave();
            }
            m_currScene = scene;
            m_currScene.Enter();
            return true;
        }

        private BaseScene GetScene(int sceneIdx)
        {
            BaseScene scene = null;
            if (!sceneDict.TryGetValue(sceneIdx, out scene))
            {
                LogUtil.LogError("[SceneMgr]No Have This Scene: " + sceneIdx);
            }
            return scene;
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (BaseScene scene in sceneDict.Values)
            {
                scene.Dispose();
            }
            sceneDict.Clear();
            sceneDict = null;
        }
    }
}