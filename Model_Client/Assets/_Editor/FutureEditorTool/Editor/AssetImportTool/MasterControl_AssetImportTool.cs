using UnityEditor;

namespace FutureEditor
{
    public class MasterControl_AssetImportTool : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string movedAsset in movedAssets)
            {
                // 资源移动目录, 重新走导入逻辑
                AssetDatabase.ImportAsset(movedAsset);
            }
        }
    }
}