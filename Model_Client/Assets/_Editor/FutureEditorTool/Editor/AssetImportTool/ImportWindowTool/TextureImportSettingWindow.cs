using UnityEngine;
using UnityEditor;
using System.IO;

namespace FutureEditor
{
    /// <summary>
    /// 批量图片资源导入设置 编辑器窗口
    /// </summary>
    public class TextureImportSettingWindow : EditorWindow
    {
        private string defaultType = "png";

        // Filter Mode
        private int filterModeInt = 1;
        private string[] filterModeString = new string[] { "Point", "Bilinear", "Trilinear" };
        // Wrap Mode
        private int wrapModeInt = 1;
        private string[] wrapModeString = new string[] { "Repeat", "Clamp" };
        // Texture Type
        private int textureTypeInt = 3;
        private string[] textureTypeString = new string[] { "Texture", "Normal Map", "GUI", "Sprite", "Cursor", "Cubemap", "Cookie", "Lightmap", "Advanced" };
        // Max Size
        private int maxSizeInt = 4;
        private string[] maxSizeString = new string[] { "32", "64", "128", "256", "512", "1024", "2048", "4096" };

        private static bool isHelp = false;

        /// <summary>
        /// 纹理文件夹属性设置窗口
        /// 使用说明：
        /// 1.选择需要设置的贴图文件夹
        /// 2.单击 [FC Toolkit]/ImportSetting/选择纹理进行导入设置
        /// 3.打开窗口后选择对应参数
        /// 4.点击Set Texture ImportSettings
        /// 5.批量设置完成
        /// </summary>
        [MenuItem("[FC Toolkit]/ImportSetting/纹理文件夹属性设置窗口", false, 0)]
        private static void TextureImportSettingMenu()
        {
            ShowWindow();
        }

        public static void ShowWindow()
        {
            TextureImportSettingWindow window = (TextureImportSettingWindow)EditorWindow.GetWindow(typeof(TextureImportSettingWindow), true, "TextureImportSettingWindow");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(10);

            //Filter Mode
            filterModeInt = EditorGUILayout.IntPopup("Filter Mode", filterModeInt, filterModeString, null);
            GUILayout.Space(10);
            //Wrap Mode
            wrapModeInt = EditorGUILayout.IntPopup("Wrap Mode", wrapModeInt, wrapModeString, null);
            GUILayout.Space(10);
            //Texture Type
            textureTypeInt = EditorGUILayout.IntPopup("Texture Type", textureTypeInt, textureTypeString, null);
            GUILayout.Space(10);
            //Max Size
            maxSizeInt = EditorGUILayout.IntPopup("Max Size", maxSizeInt, maxSizeString, null);
            GUILayout.Space(10);

            if (GUILayout.Button("Set Texture ImportSettings"))
            {
                LoopSetTexture();
            }
            GUILayout.Space(20);

            if (GUILayout.Button("Help"))
            {
                if (isHelp)
                {
                    isHelp = false;
                }
                else
                {
                    isHelp = true;
                }
            }
            if (isHelp)
            {
                GUILayout.TextArea("使用说明： " +
                    "\n1.选择需要设置的贴图文件夹" +
                    "\n2.单击 [FC Toolkit]/ImportSetting/选择纹理进行导入设置" +
                    "\n3.打开窗口后选择对应参数" +
                    "\n4.点击Set Texture ImportSettings" +
                    "\n5.批量设置完成");
            }
        }

        private void LoopSetTexture()
        {
            Object[] selections = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
            if (selections != null && selections.Length > 0)
            {
                foreach (Object obj in selections)
                {
                    string selectPath = AssetDatabase.GetAssetPath(obj);
                    if (Directory.Exists(selectPath))
                    {
                        FindTexture(selectPath);
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        Debug.Log("请选择文件夹");
                    }
                }
            }
        }

        private void FindTexture(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            FileInfo[] images = dirInfo.GetFiles("*." + defaultType);
            for (int i = 0; i < images.Length; i++)
            {
                string assetPath = PathTool.FilePathToAssetPath(images[i].FullName);
                SetTextureSettings(assetPath);
                AssetDatabase.ImportAsset(assetPath);
            }
            string[] juniorDirs = Directory.GetDirectories(dirInfo.FullName);
            foreach (string juniorDir in juniorDirs)
            {
                FindTexture(juniorDir);
            }
        }

        private void SetTextureSettings(string path)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            //Filter Mode
            switch (filterModeInt)
            {
                case 0:
                    textureImporter.filterMode = FilterMode.Point;
                    break;
                case 1:
                    textureImporter.filterMode = FilterMode.Bilinear;
                    break;
                case 2:
                    textureImporter.filterMode = FilterMode.Trilinear;
                    break;
            }
            //Wrap Mode
            switch (wrapModeInt)
            {
                case 0:
                    textureImporter.wrapMode = TextureWrapMode.Repeat;
                    break;
                case 1:
                    textureImporter.wrapMode = TextureWrapMode.Clamp;
                    break;
            }
            //Texture Type
            switch (textureTypeInt)
            {
                case 0:
                    textureImporter.textureType = TextureImporterType.Default;
                    break;
                case 1:
                    textureImporter.textureType = TextureImporterType.NormalMap;
                    break;
                case 2:
                    textureImporter.textureType = TextureImporterType.GUI;
                    break;
                case 3:
                    textureImporter.textureType = TextureImporterType.Sprite;
                    break;
                case 4:
                    textureImporter.textureType = TextureImporterType.Cursor;
                    break;
                case 5:
                    //textureImporter.textureType = TextureImporterType.Cubemap;
                    break;
                case 6:
                    textureImporter.textureType = TextureImporterType.Cookie;
                    break;
                case 7:
                    textureImporter.textureType = TextureImporterType.Lightmap;
                    break;
                case 8:
                    textureImporter.textureType = TextureImporterType.Default;
                    break;
            }
            //Max Size
            switch (maxSizeInt)
            {
                case 0:
                    textureImporter.maxTextureSize = 32;
                    break;
                case 1:
                    textureImporter.maxTextureSize = 64;
                    break;
                case 2:
                    textureImporter.maxTextureSize = 128;
                    break;
                case 3:
                    textureImporter.maxTextureSize = 256;
                    break;
                case 4:
                    textureImporter.maxTextureSize = 512;
                    break;
                case 5:
                    textureImporter.maxTextureSize = 1024;
                    break;
                case 6:
                    textureImporter.maxTextureSize = 2048;
                    break;
                case 7:
                    textureImporter.maxTextureSize = 4096;
                    break;
            }
            //Format
            textureImporter.textureCompression = TextureImporterCompression.Compressed;
            //Set
            textureImporter.mipmapEnabled = false;
            textureImporter.isReadable = false;
        }
    }
}