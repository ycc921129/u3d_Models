using UnityEditor;

namespace FutureEditor
{
    public class ModelOptimize_AssetImportTool : AssetPostprocessor
    {
        public void OnPreprocessModel()
        {
            if (assetPath.IndexOf("_Res/Art/Model") != -1)
            {
                ModelImporter modelImporter = (ModelImporter)assetImporter;
                modelImporter.importCameras = false;
                modelImporter.importLights = false;
                modelImporter.isReadable = false;
                //modelImporter.meshCompression = ModelImporterMeshCompression.Low;
                //modelImporter.SaveAndReimport();
            }
        }
    }
}