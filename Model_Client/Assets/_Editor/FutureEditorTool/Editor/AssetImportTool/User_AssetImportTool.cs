using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class User_AssetImportTool
    {
        public static void SetUserData(Object obj, string userData)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            AssetImporter import = AssetImporter.GetAtPath(path);
            import.userData = userData;
            import.SaveAndReimport();
        }

        public static string GetUserData(Object obj)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            AssetImporter import = AssetImporter.GetAtPath(path);
            return import.userData;
        }
    }
}