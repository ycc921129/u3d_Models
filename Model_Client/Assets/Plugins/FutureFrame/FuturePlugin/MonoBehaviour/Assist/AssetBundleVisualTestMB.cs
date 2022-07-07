/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FuturePlugin
{
    public class AssetBundleVisualTestMB : MonoBehaviour
    {
        public Object abFolder;
        public Object abObject;
        public Object[] abLoadObjects;

        private void Awake()
        {
            string folderAssetPath = AssetDatabase.GetAssetPath(abFolder);

            string abAssetPath = AssetDatabase.GetAssetPath(abObject);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(abAssetPath);
            abLoadObjects = assetBundle.LoadAllAssets();
        }
    }
}

#endif