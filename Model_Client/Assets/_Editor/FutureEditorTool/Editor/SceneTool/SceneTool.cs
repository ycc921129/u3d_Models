using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace FutureEditor
{
    public static class SceneTool
    {
        public static Scene GetCurrentScene()
        {
            return SceneManager.GetActiveScene();
        }

        public static void SaveScene(Scene scene)
        {
            EditorSceneManager.SaveScene(scene);
        }

        public static void OpenScene(string scenePath)
        {
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
}