/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public abstract class BaseScene
    {
        public abstract int SceneIdx { get; }
        public abstract int PreLoadId { get; }

        public void Enter()
        {
            App.DisplayLoadingUI();
            OnEnter();
        }
        public void Leave()
        {
            OnLeave();
        }

        public void SwitchSceneComplete(object param)
        {
            AppDispatcher.Instance.Dispatch(AppMsg.Scene_Switch, SceneIdx);
            OnSwitchSceneComplete(param);
        }
        public void HideLoadingUI(bool isDelay = false)
        {
            App.HideLoadingUI(isDelay);
        }

        public abstract AssetLoader GetLoader();

        protected abstract void OnEnter();
        protected abstract void OnLeave();
        protected abstract void OnSwitchSceneComplete(object param);

        public abstract void Dispose();
    }
}