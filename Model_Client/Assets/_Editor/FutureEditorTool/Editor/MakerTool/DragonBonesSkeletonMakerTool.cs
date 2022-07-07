using System.IO;
using UnityEditor;
using UnityEngine;
using DragonBones;

namespace FutureEditor
{
    public static class DragonBonesSkeletonMakerTool
    {
        private static string SkeletonDir = "Assets/_Res/Art/Skeleton/";
        private static string DefaultAnimName = "Idle";

        [MenuItem("[FC Project]/Res/Maker/Skeleton (DragonBones)/生成所有骨骼动画预设 (DragonBones)", false, 10)]
        private static void BuildAllSkeletonMenu()
        {
            DirectoryInfo raw = new DirectoryInfo(SkeletonDir);
            foreach (DirectoryInfo dictorys in raw.GetDirectories())
            {
                string path = SkeletonDir + dictorys.Name;
                BuildSkeleton(path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetsSyncTool.SyncSkeleton();
            Debug.Log("[DragonBonesSkeletonMakerTool]同步生成所有骨骼动画预设完成");
        }

        [MenuItem("[FC Project]/Res/Maker/Skeleton (DragonBones)/生成选中骨骼动画预设 (DragonBones)", false, 11)]
        private static void BuildSelectSkeletonMenu()
        {
            Object[] pathsArr = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
            foreach (Object obj in pathsArr)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path))
                {
                    Debug.LogError("路径错误");
                    return;
                }
                BuildSkeleton(path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[DragonBonesSkeletonMakerTool]生成选中骨骼动画预设完成");
        }

        private static void BuildSkeleton(string path)
        {
            DirectoryInfo dictory = new DirectoryInfo(path);
            string dicName = path.Substring(path.LastIndexOf("/") + 1);

            FileInfo[] jsonData = dictory.GetFiles("*_ske.json");
            if (jsonData.Length > 0)
            {
                string assetPath = path + "/" + jsonData[0].Name;

                GameObject gameObject = new GameObject("New Armature Object", typeof(UnityArmatureComponent));
                UnityArmatureComponent armatureComponent = gameObject.GetComponent<UnityArmatureComponent>();
                TextAsset dragonBonesJSON = AssetDatabase.LoadMainAssetAtPath(assetPath) as TextAsset;

                bool isChange = DragonBones.UnityEditor.ChangeDragonBonesData(armatureComponent, dragonBonesJSON);
                if (isChange)
                {
                    string prefabPath = path + "/" + dicName + ".prefab";
                    PrefabUtility.SaveAsPrefabAsset(armatureComponent.gameObject, prefabPath);
                    Object.DestroyImmediate(armatureComponent.gameObject);
                }
            }
        }
    }
}