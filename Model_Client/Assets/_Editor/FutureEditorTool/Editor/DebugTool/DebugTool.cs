using UnityEditor;

namespace FutureEditor
{
    public static class DebugTool
    {
        public static void PlayEditor()
        {
            EditorApplication.isPlaying = true;
        }

        public static void StopEditor()
        {
            EditorApplication.isPlaying = false;
        }

        public static void PauseEditor()
        {
            EditorApplication.isPaused = true;
        }
    }
}