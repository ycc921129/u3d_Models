using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class ProjectSettings_GraphicsTool
    {
        [MenuItem("[FC Project]/ProjectSettings/添加常驻着色器", false, 0)]
        public static void AddAlwaysIncludedShader()
        {
            string[] shaders = new string[]
            {
                ///引擎
                //Unity
                "Legacy Shaders/Diffuse",
                "Hidden/CubeBlur",
                "Hidden/CubeCopy",
                "Hidden/CubeBlend",
                "Hidden/VideoDecode",
                "Hidden/VideoDecodeAndroid",
                "Sprites/Default",
                "UI/Default",
                "UI/DefaultETC1",

                ///框架
                //UI
                "[FC UI]/AlphaGradual",
                "[FC UI]/AtlasGrayScale",
                "[FC UI]/CircleGuide",
                "[FC UI]/MoveLightImage",
                "[FC UI]/RectGuide",
                "[FC UI]/SimpleGrabPassBlur",
                "[FC UI]/SingleGrayScale",

                ///插件
            };

            SerializedObject graphicsSettings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/GraphicsSettings.asset")[0]);
            SerializedProperty it = graphicsSettings.GetIterator();
            SerializedProperty dataPoint;

            while (it.NextVisible(true))
            {
                if (it.name == "m_AlwaysIncludedShaders")
                {
                    it.ClearArray();

                    for (int i = 0; i < shaders.Length; i++)
                    {
                        it.InsertArrayElementAtIndex(i);
                        dataPoint = it.GetArrayElementAtIndex(i);
                        dataPoint.objectReferenceValue = Shader.Find(shaders[i]);
                    }
                    graphicsSettings.ApplyModifiedProperties();
                }
            }
        }
    }
}