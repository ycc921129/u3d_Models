using System.IO;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class CheckUnity3dFont : MonoBehaviour
    {
        [MenuItem("[FC Toolkit]/Check/检查引用Unity3d内置字体的预设", false, 0)]
        private static void Check()
        {
            string[] tmpFilePathArray = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);

            EditorUtility.DisplayProgressBar("检查引用Unity3d内置字体的预设", "检查引用Unity3d内置字体的预设", 0f);

            for (int i = 0; i < tmpFilePathArray.Length; i++)
            {
                EditorUtility.DisplayProgressBar("检查引用Unity3d内置字体的预设", "检查引用Unity3d内置字体的预设", (i * 1.0f) / tmpFilePathArray.Length);
                string tmpFilePath = tmpFilePathArray[i];
                if (tmpFilePath.EndsWith(".prefab"))
                {
                    StreamReader tmpStreamReader = new StreamReader(tmpFilePath);
                    string tmpContent = tmpStreamReader.ReadToEnd();
                    if (tmpContent.Contains("m_Font: {fileID: 10102, guid: 0000000000000000e000000000000000, type: 0}"))
                    {
                        Debug.LogErrorFormat("错误预设: {0}", tmpFilePath);
                    }
                }
            }

            EditorUtility.ClearProgressBar();
        }
    }
}