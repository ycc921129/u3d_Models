using UnityEngine;
using UnityEditor;

namespace FutureEditor
{
    public static class Unity3dPrefabTool
    {
        [InitializeOnLoadMethod]
        static void StartInitializeOnLoadMethod()
        {
            //PrefabUtility.prefabInstanceUpdated = delegate (GameObject instance)
            //{
            //    if (instance)
            //    {
            //        ProcessPrefab(instance);
            //    }
            //};
        }

        static void ProcessPrefab(GameObject instance)
        {
            Object parentPrefab = PrefabUtility.GetPrefabParent(instance);
            string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetPrefabParent(instance));

            Debug.Log("保存预设:" + prefabPath);

            //bool needCheck = false;
            //foreach (string path in CheckPrefabPath)
            //{
            //    if (prefabPath.Contains(path))
            //    {
            //        needCheck = true;
            //    }
            //}
            //if (!needCheck) return;
            ////具体对Prefab进行操作的方法
            //bool isChange = DoFixUIPrefab(instance);  
            //if (isChange)
            //{
            //    PrefabUtility.ReplacePrefab(instance, parentPrefab, ReplacePrefabOptions.ConnectToPrefab);
            //}
        }
    }
}