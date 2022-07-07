using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class TextureCombineTool
    {
        //[MenuItem("[FC Toolkit]/Bake/TextureCombine", false, 0)]
        private static void Combine()
        {
            Texture2D tex512 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/512.jpg");
            Texture2D tex128 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/128.jpg");
            Texture2D @out = Combine(
                new[] { tex512, tex128 }, //贴图
                new[] { (0, 0), (600, 600) } //偏移
                , 1024);//最终贴图大小

            File.WriteAllBytes("Assets/out.jpg", @out.EncodeToJPG());
            AssetDatabase.Refresh();
        }

        private static Texture2D Combine(Texture2D[] texs, ValueTuple<int, int>[] offests, int size)
        {
            Texture2D @out = new Texture2D(size, size, TextureFormat.RGBA32, true);
            for (int i = 0; i < texs.Length; i++)
            {
                var tex = texs[i];
                var offest = offests[i];
                var width = tex.width;
                var height = tex.height;
                RenderTexture tmp = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
                Graphics.Blit(tex, tmp);
                RenderTexture previous = RenderTexture.active;
                RenderTexture.active = tmp;
                Texture2D @new = new Texture2D(width, height);
                @new.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                @new.Apply();
                @out.SetPixels(offest.Item1, offest.Item2, width, height, @new.GetPixels());
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(tmp);
            }
            return @out;
        }
    }
}