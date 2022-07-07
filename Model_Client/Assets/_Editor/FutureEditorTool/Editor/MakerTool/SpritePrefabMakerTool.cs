/*
 Author:du
 Time:2017.11.23
*/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FutureEditor
{
    public static class SpritePrefabMakerTool
    {
        private const string AtlasPath = "Assets/_Res/Art/Atlas/";
        private const string SpritePath = "Assets/_Res/Art/Sprite/";
        private const string OutAtlasPath = "Assets/_Res/Resources/Atlas/";
        private const string OutSpritePath = "Assets/_Res/Resources/Sprite/";
        private const string AtlasName = "Atlas_";

        [MenuItem("[FC Project]/Res/Maker/Sprite/生成所有精灵预设", false, 0)]
        private static void MakerAll()
        {
            AtlasUIMakerBySelectAll();
            SpriteUIMakerBySelectAll();

            AssetDatabase.Refresh();
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        [MenuItem("[FC Project]/Res/Maker/Sprite/生成所有图集精灵预设", false, 1)]
        private static void AtlasUIMakerBySelectAll()
        {
            if (Directory.Exists(OutAtlasPath))
            {
                Directory.Delete(OutAtlasPath, true);
            }
            Directory.CreateDirectory(OutAtlasPath);

            DirectoryInfo raw = new DirectoryInfo(AtlasPath);
            foreach (DirectoryInfo dictorys in raw.GetDirectories())
            {
                string path = AtlasPath + dictorys.Name;
                if (!path.Contains("Atlas"))
                {
                    Debug.LogError("精灵路径错误!");
                    continue;
                }

                BuildTargetDirPrefab(path);
                Debug.Log("生成所有图集精灵预设完成:" + path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("[FC Project]/Res/Maker/Sprite/生成所有单图精灵预设", false, 2)]
        private static void SpriteUIMakerBySelectAll()
        {
            if (Directory.Exists(OutSpritePath))
            {
                Directory.Delete(OutSpritePath, true);
            }
            Directory.CreateDirectory(OutSpritePath);

            DirectoryInfo raw = new DirectoryInfo(SpritePath);
            foreach (DirectoryInfo dictorys in raw.GetDirectories())
            {
                string path = SpritePath + dictorys.Name;
                if (!path.Contains("Sprite"))
                {
                    Debug.LogError("精灵路径错误!");
                    continue;
                }

                BuildTargetDirPrefab(path);
                Debug.Log("生成所有精灵预设完成:" + path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("[FC Project]/Res/Maker/Sprite/生成目录精灵预设", false, 3)]
        private static void AtlasUIMakerBySelectsDir()
        {
            Object[] objectArr = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
            foreach (Object obj in objectArr)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (!path.Contains("Atlas") && !path.Contains("Sprite"))
                {
                    Debug.LogError("精灵路径错误!");
                    continue;
                }

                BuildTargetDirPrefab(path);
                Debug.Log("生成目录精灵预设完成:" + path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("[FC Project]/Res/Maker/Sprite/生成单个精灵预设", false, 4)]
        private static void AtlasUIMakerBySelectsOne()
        {
            Object[] ObjectArr = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);

            foreach (Object item in ObjectArr)
            {
                string path = AssetDatabase.GetAssetPath(item);
                FileInfo file = new FileInfo(path);
                string toTargetPath = path.Replace("Art", "Resources");
                toTargetPath = toTargetPath.Replace(".png", ".prefab");
                toTargetPath = toTargetPath.Replace(".jpg", ".prefab");
                string allPath = file.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                SetSpriteAtlasTag(assetPath);

                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                string prefabPath = toTargetPath.Substring(toTargetPath.IndexOf("Assets"));
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                Object.DestroyImmediate(go);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void BuildTargetDirPrefab(string path)
        {
            DirectoryInfo rawDir = new DirectoryInfo(path);
            string toTargetPath = path.Replace("Art", "Resources");

            if (Directory.Exists(toTargetPath))
            {
                Directory.Delete(toTargetPath, true);
            }
            Directory.CreateDirectory(toTargetPath);

            List<FileInfo> files = new List<FileInfo>();
            foreach (FileInfo f in rawDir.GetFiles("*.png", SearchOption.AllDirectories))
            {
                files.Add(f);
            }
            foreach (FileInfo f in rawDir.GetFiles("*.jpg", SearchOption.AllDirectories))
            {
                files.Add(f);
            }
            foreach (FileInfo file in files)
            {
                string allPath = file.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                SetSpriteAtlasTag(assetPath);

                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                string prefabPath = toTargetPath.Substring(toTargetPath.IndexOf("Assets")) + "/" + go.name + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                Object.DestroyImmediate(go);
            }
        }

        private static void SetSpriteAtlasTag(string assetPath)
        {
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            TextureImporter textureImporter = importer as TextureImporter;
            if (assetPath.Contains("Atlas"))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(new FileInfo(assetPath).DirectoryName);
                string dirName = directoryInfo.Name;
                textureImporter.spritePackingTag = EditorAppConst.AtlasTag_FullRect + AtlasName + dirName;
            }

            TextureImporterSettings setting = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(setting);
            setting.spriteMeshType = SpriteMeshType.FullRect;
            setting.spriteGenerateFallbackPhysicsShape = false;
            textureImporter.SetTextureSettings(setting);
            textureImporter.SaveAndReimport();
        }
    }
}