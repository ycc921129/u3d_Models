using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    /// <summary>
    /// BMFont导入自动生成静态字体工具
    /// </summary>
    public class StaticBMFont_AssetImportTool : AssetPostprocessor
    {
        private struct ChrRect
        {
            public int id;
            public int x;
            public int y;
            public int width;
            public int height;
            public int xoffset;
            public int yoffset;

            public int index;
            public float xadvance;
            public float uvX;
            public float uvY;
            public float uvW;
            public float uvH;
            public float vertX;
            public float vertY;
            public float vertW;
            public float vertH;

            public float imgW;
            public float imgH;
        }

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (importedAssets.Length == 0) return;

            foreach (string importedAsset in importedAssets)
            {
                if (!importedAsset.Contains("_Res/Resources/StaticFont")) return;

                string extension = Path.GetExtension(importedAsset).ToLower();
                switch (extension)
                {
                    case ".fnt":
                        {
                            ImportFnt(importedAsset);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 导入字体
        /// </summary>
        private static void ImportFnt(string path)
        {
            bool isNewFont = false;
            string fileName = Path.GetFileName(path);
            string fontName = Path.GetFileNameWithoutExtension(fileName);
            string texPath = path.Replace(".fnt", "_0.png");
            string fontPath = path.Replace(".fnt", "_fnt.fontsettings");
            string matPath = path.Replace(".fnt", "_mat.mat");

            TextAsset posTbl = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(texPath);
            Font font = AssetDatabase.LoadAssetAtPath<Font>(fontPath);
            Material mat = null;

            if (font == null)
            {
                font = new Font();
                isNewFont = true;

                mat = new Material(Shader.Find("UI/Default Font"));
                // 考虑到改色需求不使用这个着色器
                //mat = new Material(Shader.Find("Unlit/Transparent"));
                mat.SetTexture("_MainTex", tex);
                AssetDatabase.CreateAsset(mat, matPath);
            }
            else
            {
                mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                mat.SetTexture("_MainTex", tex);
            }

            float imgw = tex.width;
            float imgh = tex.height;
            string txt = posTbl.text;

            List<ChrRect> tblList = new List<ChrRect>();
            foreach (string line in txt.Split('\n'))
            {
                if (line.IndexOf("char id=") == 0)
                {
                    ChrRect d = GetChrRect(line, imgw, imgh);
                    tblList.Add(d);
                }
            }
            if (tblList.Count == 0)
            {
                Debug.LogError("[StaticBMFont_AssetImportTool]导入失败");
                return;
            }

            ChrRect[] tbls = tblList.ToArray();
            font.name = fontName;
            SetCharacterInfo(tbls, font);
            font.material = mat;

            Object.DestroyImmediate(posTbl, true);
            AssetDatabase.DeleteAsset(path);

            if (isNewFont)
            {
                AssetDatabase.CreateAsset(font, fontPath);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private static ChrRect GetChrRect(string line, float imgW, float imgH)
        {
            ChrRect d = new ChrRect();

            foreach (string s in line.Split(' '))
            {
                if (s.IndexOf("id=") >= 0) d.id = GetParamInt(s, "id=");
                else if (s.IndexOf("xadvance=") >= 0) d.xadvance = GetParamInt(s, "xadvance=");
                else if (s.IndexOf("x=") >= 0) d.x = GetParamInt(s, "x=");
                else if (s.IndexOf("y=") >= 0) d.y = GetParamInt(s, "y=");
                else if (s.IndexOf("width=") >= 0) d.width = GetParamInt(s, "width=");
                else if (s.IndexOf("height=") >= 0) d.height = GetParamInt(s, "height=");
                else if (s.IndexOf("xoffset=") >= 0) d.xoffset = GetParamInt(s, "xoffset=");
                else if (s.IndexOf("yoffset=") >= 0) d.yoffset = GetParamInt(s, "yoffset=");
            }

            d.index = d.id;
            d.uvX = d.x / imgW;
            d.uvY = (imgH - d.y - d.height) / imgH;
            d.uvW = d.width / imgW;
            d.uvH = d.height / imgH;

            d.vertX = d.xoffset;
            d.vertY = -d.yoffset;
            d.vertW = d.width;
            d.vertH = -d.height;

            d.imgW = imgW;
            d.imgH = imgH;

            return d;
        }

        private static int GetParamInt(string s, string wd)
        {
            if (s.IndexOf(wd) >= 0)
            {
                int v;
                if (int.TryParse(s.Substring(wd.Length), out v))
                {
                    return v;
                }
            }
            return int.MaxValue;
        }

        private static void SetCharacterInfo(ChrRect[] tbls, Font fontObj)
        {
            CharacterInfo[] ncis = new CharacterInfo[tbls.Length];

            for (int i = 0; i < tbls.Length; i++)
            {
                ChrRect tbl = tbls[i];
                CharacterInfo nci = new CharacterInfo();

                nci.index = tbl.index;
                nci.advance = (int)tbl.xadvance;

                nci.glyphWidth = (int)tbl.imgW;
                nci.glyphHeight = (int)tbl.imgH;

                //这里注意下UV坐标系和从BMFont里得到的信息的坐标系是不一样的哦，前者左下角为（0,0），右上角为（1,1）
                //而后者则是左上角为（0,0），右下角为（图宽，图高）
                nci.uvTopLeft = new Vector2(tbl.x / tbl.imgW, 1 - tbl.y / tbl.imgH);
                nci.uvTopRight = new Vector2((tbl.x + tbl.width) / tbl.imgW, 1 - tbl.y / tbl.imgH);
                nci.uvBottomLeft = new Vector2(tbl.x / tbl.imgW, 1 - (tbl.y + tbl.height) / tbl.imgH);
                nci.uvBottomRight = new Vector2((tbl.x + tbl.width) / tbl.imgW, 1 - (tbl.y + tbl.height) / tbl.imgH);

                nci.minX = 0;
                nci.maxX = tbl.width;
                nci.minY = -tbl.height;
                nci.maxY = 0;

                /// 旧版接口实现
                //nci.uv.x = tbl.uvX;
                //nci.uv.y = tbl.uvY;
                //nci.uv.width = tbl.uvW;
                //nci.uv.height = tbl.uvH;

                //nci.vert.x = tbl.vertX;
                //nci.vert.y = tbl.vertY;
                //nci.vert.width = tbl.vertW;
                //nci.vert.height = tbl.vertH;

                ncis[i] = nci;
            }

            fontObj.characterInfo = ncis;
        }
    }
}