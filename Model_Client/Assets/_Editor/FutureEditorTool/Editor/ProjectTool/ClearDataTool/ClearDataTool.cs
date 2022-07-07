using System.IO;
using UnityEngine;

namespace FutureEditor
{
    public static class ClearDataTool
    {
        public static void ClearPlayerData()
        {
            if (!Application.isPlaying)
            {
                PlayerPrefs.DeleteAll();
                if (Directory.Exists(Application.persistentDataPath))
                {
                    Directory.Delete(Application.persistentDataPath, true);
                }
                Debug.Log("[ClearDataTool]ClearPlayerData");
            }
        }
    }
}