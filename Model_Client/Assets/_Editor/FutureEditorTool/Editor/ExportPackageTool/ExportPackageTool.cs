using System;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class ExportPackageTool
    {
        [MenuItem("[FC Toolkit]/ExportPackage/����ѡ��Ŀ¼Unity��", false, 0)]
        private static void ExportPackage()
        {
            UnityEngine.Object[] selections = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
            if (selections.Length > 0)
            {
                string assetSelectPath = AssetDatabase.GetAssetPath(selections[0]);
                string fileName = EditorAppConst.AppName + "_" + assetSelectPath + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh") + ".unitypackage";
                AssetDatabase.ExportPackage(assetSelectPath, fileName, ExportPackageOptions.IncludeDependencies);
                Debug.Log("����ѡ��Ŀ¼Unity��");
            }
        }
    }
}