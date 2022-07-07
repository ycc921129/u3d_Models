using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FutureEditor
{
    public static class BakeTool
    {
        [MenuItem("[FC Toolkit]/Bake/同步烘焙选中场景", false, 0)]
        public static void SyncBakeSelectedScenes()
        {
            Object[] selectedAsset = Selection.GetFiltered(typeof(SceneAsset), SelectionMode.DeepAssets);
            foreach (Object obj in selectedAsset)
            {
                string scenePath = AssetDatabase.GetAssetPath(obj);
                Debug.Log("开始烘焙场景:" + scenePath);

                EditorSceneManager.OpenScene(scenePath);
                Lightmapping.Bake();
                EditorSceneManager.SaveOpenScenes();

                //这里写更新Prefab的需求
                EditorSceneManager.SaveOpenScenes();
                Debug.Log("场景烘焙完成:" + scenePath);
            }
        }
    }
}