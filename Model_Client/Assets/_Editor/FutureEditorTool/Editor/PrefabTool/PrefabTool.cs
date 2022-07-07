using System;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FutureEditor
{
    public static class PrefabTool
    {
        public static bool IsInEditPrefab()
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            return prefabStage != null;
        }

        public static void AddPrefabStageOpened(Action<PrefabStage> func)
        {
            PrefabStage.prefabStageOpened += func;
        }

        public static void SavePrefabScene(GameObject gameObject)
        {
            string prefabPath = PrefabStageUtility.GetCurrentPrefabStage().prefabAssetPath;
            PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
            StageUtility.GoBackToPreviousStage();
        }

        public static void SavePrefab(GameObject gameObject)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(gameObject);
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }

        public static void SetStaticEditorFlags(GameObject go, StaticEditorFlags flags)
        {
            GameObjectUtility.SetStaticEditorFlags(go, flags);
        }
    }
}