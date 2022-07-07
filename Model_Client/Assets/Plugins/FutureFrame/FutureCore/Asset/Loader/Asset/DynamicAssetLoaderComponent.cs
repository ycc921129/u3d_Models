/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public class DynamicAssetLoaderComponent : MonoBehaviour
    {
        [SerializeField]
        private DynamicAssetLoader loader;

        public void Set(DynamicAssetLoader loader)
        {
            this.loader = loader;
        }

        private void OnDestroy()
        {
            if (null == loader) return;
            loader.Release();
        }

        private void OnApplicationQuit()
        {
            loader = null;
        }
    }
}