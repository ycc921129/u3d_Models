/*
    1) % - CTRL on Windows / CMD on OSX
    2) # - Shift
    3) & - Alt
    4) LEFT/RIGHT/UP/DOWN - Arrow keys
    5) F1 … F2 - F keys
    6) HOME,END,PGUP,PGDN
    7) 字母键 - _ + 字母（如:_g代表按键）
 */

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace FutureEditor
{
    public static class SpritePivotTool
    {
        private static int cahceAlignment;
        private static Vector2 cacheReadPivot;

        [MenuItem("Assets/[FC Shortcut]/SpritePivot/读取精灵枢轴 #_%_&_1", false, 0)]
        private static void ReadSpritePivotMenu()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            TextureImporterSettings tis = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(tis);
            cahceAlignment = tis.spriteAlignment;
            cacheReadPivot = tis.spritePivot;
            Debug.Log("读取精灵枢轴完成 : " + cahceAlignment + " | " + cacheReadPivot);
        }

        [MenuItem("Assets/[FC Shortcut]/SpritePivot/设置该精灵枢轴为读取枢轴 #_%_&_2", false, 1)]
        private static void SetCacheReadSpritePivotToSpriteMenu()
        {
            UObject selection = Selection.activeObject;
            string selectPath = AssetDatabase.GetAssetPath(selection);
            SetCacheReadSpritePivot(selectPath);
            Debug.Log("设置该精灵枢轴为读取枢轴完成 : " + cahceAlignment + " | " + cacheReadPivot);
        }

        [MenuItem("Assets/[FC Shortcut]/SpritePivot/设置目录的精灵枢轴为读取枢轴 #_%_&_3", false, 2)]
        private static void SetCacheReadSpritePivotToDirMenu()
        {
            SettingSprite(SetCacheReadSpritePivot);
            Debug.Log("设置目录的精灵枢轴为读取枢轴完成 : " + cahceAlignment + " | " + cacheReadPivot);
        }

        [MenuItem("Assets/[FC Shortcut]/SpritePivot/设置目录的精灵枢轴为居中 #_%_&_4", false, 3)]
        private static void SetSpritePivotCenterToDirMenu()
        {
            SettingSprite(SetCSpritePivotCenter);
            Debug.Log("设置目录的精灵枢轴为居中完成");
        }

        private static void SetCacheReadSpritePivot(string assetPath)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            TextureImporterSettings tis = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(tis);
            tis.spriteAlignment = cahceAlignment;
            tis.spritePivot = cacheReadPivot;
            textureImporter.SetTextureSettings(tis);
            AssetDatabase.ImportAsset(assetPath);
        }

        private static void SetCSpritePivotCenter(string assetPath)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            TextureImporterSettings tis = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(tis);
            tis.spriteAlignment = 0;
            textureImporter.SetTextureSettings(tis);
            AssetDatabase.ImportAsset(assetPath);
        }

        private static void SettingSprite(Action<string> func)
        {
            UObject[] selections = Selection.GetFiltered(typeof(UObject), SelectionMode.Assets);
            if (selections != null && selections.Length > 0)
            {
                foreach (UObject obj in selections)
                {
                    string selectPath = AssetDatabase.GetAssetPath(obj);
                    if (Directory.Exists(selectPath))
                    {
                        FindSprite(selectPath, func);
                    }
                }
            }
        }

        private static void FindSprite(string dir, Action<string> func)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] images = dirInfo.GetFiles("*.png");
            for (int i = 0; i < images.Length; i++)
            {
                string assetPath = PathTool.FilePathToAssetPath(images[i].FullName);
                func(assetPath);
            }
            string[] juniorDirs = Directory.GetDirectories(dirInfo.FullName);
            foreach (string juniorDir in juniorDirs)
            {
                FindSprite(juniorDir, func);
            }
        }
    }
}