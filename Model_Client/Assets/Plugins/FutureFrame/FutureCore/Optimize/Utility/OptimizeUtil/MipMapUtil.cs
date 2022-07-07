/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class MipMapUtil
    {
        /// <summary>
        /// 加载指定等级MipMap
        /// </summary>
        public static void SetRequestedMipmapLevel(Texture2D texture, int value)
        {
            texture.requestedMipmapLevel = value;
        }

        /// <summary>
        /// 清空MipMap等级
        /// </summary>
        public static void ClearRequestedMipmapLevel(Texture2D texture)
        {
            texture.ClearRequestedMipmapLevel();
        }

        /// <summary>
        /// 设置加载MipMap等级的偏移量
        /// </summary>
        public static void SetMipMapBias(Texture2D texture, int value)
        {
            texture.mipMapBias += value;
        }

        /// <summary>
        /// 添加MipMap流控制
        /// </summary>
        public static StreamingController AddStreamingController(GameObject gameObject)
        {
            return gameObject.AddComponent<StreamingController>();
        }
    }
}