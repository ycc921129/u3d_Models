/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public sealed class WorldSpaceMgr : BaseMonoMgr<WorldSpaceMgr>
    {
        private GameObject worldMask;

        public override void Init()
        {
            base.Init();
            AppObjConst.WorldSpaceGo = new GameObject(AppObjConst.WorldSpaceGoName);
            AppObjConst.WorldSpaceGo.SetParent(AppObjConst.FutureFrameGo);
            InitWorldMask();
        }

        private void InitWorldMask()
        {
            return;

            worldMask = ResourcesLoader.Instance.SyncLoadGameObject("Preset/WorldMgr/WorldMask");
            if (worldMask)
            {
                worldMask.name = "WorldMask";
                worldMask.SetActive(false);
                In(worldMask);
            }
        }

        public void SetWorldMaskActive(bool value)
        {
            worldMask.SetActive(value);
        }

        public void In(GameObject go)
        {
            go.SetParent(AppObjConst.WorldSpaceGo);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EngineUtil.Destroy(AppObjConst.WorldSpaceGo);
        }
    }
}