/****************************************************************************
* ScriptType: �����
* �����޸�!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class MipMapUtil
    {
        /// <summary>
        /// ����ָ���ȼ�MipMap
        /// </summary>
        public static void SetRequestedMipmapLevel(Texture2D texture, int value)
        {
            texture.requestedMipmapLevel = value;
        }

        /// <summary>
        /// ���MipMap�ȼ�
        /// </summary>
        public static void ClearRequestedMipmapLevel(Texture2D texture)
        {
            texture.ClearRequestedMipmapLevel();
        }

        /// <summary>
        /// ���ü���MipMap�ȼ���ƫ����
        /// </summary>
        public static void SetMipMapBias(Texture2D texture, int value)
        {
            texture.mipMapBias += value;
        }

        /// <summary>
        /// ���MipMap������
        /// </summary>
        public static StreamingController AddStreamingController(GameObject gameObject)
        {
            return gameObject.AddComponent<StreamingController>();
        }
    }
}