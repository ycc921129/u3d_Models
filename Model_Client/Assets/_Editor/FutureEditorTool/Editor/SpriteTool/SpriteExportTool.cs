using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FutureEditor
{
    public static class SpriteExportTool
    {
        [MenuItem("[FC Toolkit]/SpriteExport/选中图集导出精灵", false, 0)]
        public static void ExportSpriteAtlas()
        {
            Object[] selObjs = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
            if (selObjs == null || selObjs.Length == 0)
            {
                EditorUtility.DisplayDialog("错误", "请至少选中一个切好的图集!", "我知道了");
                return;
            }

            foreach (Object obj in selObjs)
            {
                string resPath = AssetDatabase.GetAssetPath(obj);
                Object[] spriteObjs = AssetDatabase.LoadAllAssetsAtPath(resPath);
                List<Sprite> spriteList = new List<Sprite>();
                foreach (Object spriteObj in spriteObjs)
                {
                    if (spriteObj is Sprite)
                    {
                        spriteList.Add(spriteObj as Sprite);
                    }
                }

                if (spriteList.Count == 0)
                {
                    EditorUtility.DisplayDialog("错误", "你选中的不是图集！请选择切好的图集！", "我知道了");
                    return;
                }

                string outPath = Application.dataPath + "/$TempSpriteExport/" + obj.name;
                Directory.CreateDirectory(outPath);
                foreach (Sprite sprite in spriteList)
                {
                    try
                    {
                        Texture2D t = sprite.texture;
                        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.ARGB32, false);
                        texture.SetPixels(t.GetPixels((int)sprite.rect.xMin, (int)sprite.rect.yMin, (int)sprite.rect.width, (int)sprite.rect.height));
                        texture.Apply();
                        File.WriteAllBytes(outPath + "/" + sprite.name + ".png", texture.EncodeToPNG());
                    }
                    catch(Exception e)
                    {
                        EditorUtility.DisplayDialog("错误", "图集不可读，请在图集的Inspector面板的Advanced下勾选Read/Write Enabled并应用。", "我知道了");
                        throw;
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[SpriteExportTool]选中图集导出精灵");
        }

        [MenuItem("[FC Toolkit]/SpriteExport/选中精灵列表导出精灵", false, 0)]
        public static void ExportSprite()
        {
            Object[] selects = Selection.objects;
            string savePath = Application.dataPath + "/$TempSpriteExport/";
            Directory.CreateDirectory(savePath);

            foreach (Object item in selects)
            {
                Sprite sprite = item as Sprite;
                if (sprite == null) continue;

                Texture2D t = sprite.texture;
                Texture2D newTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.ARGB32, false);
                newTex.SetPixels(t.GetPixels((int)sprite.rect.xMin, (int)sprite.rect.yMin, (int)sprite.rect.width, (int)sprite.rect.height));
                newTex.Apply();

                byte[] buffer = newTex.EncodeToPNG();
                File.WriteAllBytes(savePath + sprite.name + ".png", buffer);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[SpriteExportTool]选中精灵列表导出精灵");
        }
    }
}