/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public enum ABBuildPakType
    {
        // One
        OneShared,                      //公共包 (不同类型/目录)
                                        // Group
        Single_Group,                   //资源单组
        Multi_Group,                    //资源多组
                                        // Alone
        Alone_ObjPath,                  //单个资源以资源命名打包
        Alone_SecFolderPath,            //单个资源以二级文件夹命名打包
    }

    public static class ABSharedPakCosnt
    {
        // 静态公共包 (Shader、材质球、静态字体)
        public const string StaticShared = "shared/staticshared";
        // 特效库公共包
        public const string EffectLibShared = "shared/effectlibshared";
    }

    public static class ABPakConst
    {
        // 加载资源
        public const string Anim = "Anim/";
        public const string Atlas = "Atlas/";
        public const string Audio = "Audio/";
        public const string Data = "Data/";
        public const string DynamicFont = "DynamicFont/";
        public const string Effect = "Effect/";
        public const string Frames = "Frames/";
        public const string Module = "Module/";
        public const string Prefab = "Prefab/";
        public const string Skeleton = "Skeleton/";
        public const string Sprite = "Sprite/";
        public const string Texture = "Texture/";
        public const string Video = "Video/";
        // 公共加载资源
        public const string Shader = "Shader/";
        public const string Material = "Material/";
        public const string StaticFont = "StaticFont/";
        public const string EffectLib = "EffectLib/";

        public static Dictionary<string, ABBuildPakType> PakTypeDic;
        public static Dictionary<string, string> OneSharedPakDic;

        #region Init
        public static void Init()
        {
            PakTypeDic = GetABAssetDic();
            OneSharedPakDic = GetOneSharedPakDic();
        }

        private static Dictionary<string, ABBuildPakType> GetABAssetDic()
        {
            Dictionary<string, ABBuildPakType> dir = new Dictionary<string, ABBuildPakType>();
            // 加载资源
            dir.Add(Anim, ABBuildPakType.Multi_Group);
            dir.Add(Atlas, ABBuildPakType.Multi_Group);
            dir.Add(Audio, ABBuildPakType.Alone_ObjPath);
            dir.Add(Data, ABBuildPakType.Multi_Group);
            dir.Add(Effect, ABBuildPakType.Alone_SecFolderPath);
            dir.Add(Frames, ABBuildPakType.Alone_SecFolderPath);
            dir.Add(DynamicFont, ABBuildPakType.Single_Group);
            dir.Add(Module, ABBuildPakType.Multi_Group);
            dir.Add(Prefab, ABBuildPakType.Multi_Group);
            dir.Add(Skeleton, ABBuildPakType.Alone_SecFolderPath);
            dir.Add(Sprite, ABBuildPakType.Alone_ObjPath);
            dir.Add(Texture, ABBuildPakType.Alone_ObjPath);
            dir.Add(Video, ABBuildPakType.Alone_ObjPath);
            // 公共加载资源
            dir.Add(Shader, ABBuildPakType.OneShared);
            dir.Add(Material, ABBuildPakType.OneShared);
            dir.Add(StaticFont, ABBuildPakType.OneShared);
            dir.Add(EffectLib, ABBuildPakType.OneShared);
            return dir;
        }

        private static Dictionary<string, string> GetOneSharedPakDic()
        {
            Dictionary<string, string> dir = new Dictionary<string, string>();
            // 静态公共包 (着色器、材质球、静态字体)
            dir.Add(Shader, ABSharedPakCosnt.StaticShared);
            dir.Add(Material, ABSharedPakCosnt.StaticShared);
            dir.Add(StaticFont, ABSharedPakCosnt.StaticShared);
            // 特效库公共包
            dir.Add(EffectLib, ABSharedPakCosnt.EffectLibShared);
            return dir;
        }
        #endregion
    }
}