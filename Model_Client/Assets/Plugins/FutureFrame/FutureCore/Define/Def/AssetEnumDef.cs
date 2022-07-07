/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.6
*/

namespace FutureCore
{
    public enum UAssetType : int
    {
        Generic = 0,
        Object = 1,
        Prefab = 2,
        GameObject = 3,
        Sprite = 4,
        Texture = 5,
        AudioClip = 6,
        TextAsset = 7,
        Material = 8,
        Shader = 9,
        Font = 10,
        VideoClip = 11,
        RuntimeAnimatorController = 12,
        ScriptableObject = 13,
    }

    /*
    /// <summary>
    /// EnumKey优化写法
    /// Struct继承IEquatable来优化
    /// </summary>
    public class EnumComparer_UAssetType : IEqualityComparer<UAssetType>
    {
        public static EnumComparer_UAssetType Instance = new EnumComparer_UAssetType();

        public bool Equals(UAssetType x, UAssetType y)
        {
            return (int)x == (int)y;
        }

        public int GetHashCode(UAssetType obj)
        {
            return (int)obj;
        }
    }
    */
}