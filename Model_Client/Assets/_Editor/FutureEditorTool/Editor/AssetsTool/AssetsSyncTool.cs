/*
 Author:du
 Time:2017.11.8
*/

using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class AssetsSyncTool
    {
        [MenuItem("[FC Project]/Res/AssetsSync/同步所有资源", false, 10)]
        public static void SyncAll()
        {
            SyncEffect();
            SyncAnimFrames();
            SyncSkeleton();
        }

        [MenuItem("[FC Project]/Res/AssetsSync/同步特效", false, 11)]
        public static void SyncEffect()
        {
            string formDir = Application.dataPath + EditorPathConst.ShortResArtPath + "Effect";
            string toDir = "Assets" + EditorPathConst.ShortResResourcesPath + "Effect";
            FileTool.CopyPrefabsToDir(formDir, toDir);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[AssetsSyncTool]同步特效完成");
        }

        [MenuItem("[FC Project]/Res/AssetsSync/同步序列帧", false, 12)]
        public static void SyncAnimFrames()
        {
            string formDir = Application.dataPath + EditorPathConst.ShortResArtPath + "Frames";
            string toDir = "Assets" + EditorPathConst.ShortResResourcesPath + "Frames";
            FileTool.CopyPrefabsToDir(formDir, toDir);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[AssetsSyncTool]同步帧动画完成");
        }

        [MenuItem("[FC Project]/Res/AssetsSync/同步2D骨骼动画", false, 13)]
        public static void SyncSkeleton()
        {
            string formDir = Application.dataPath + EditorPathConst.ShortResArtPath + "Skeleton";
            string toDir = "Assets" + EditorPathConst.ShortResResourcesPath + "Skeleton";
            FileTool.CopyPrefabsToDir(formDir, toDir);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[AssetsSyncTool]同步骨骼动画完成");
        }
    }
}