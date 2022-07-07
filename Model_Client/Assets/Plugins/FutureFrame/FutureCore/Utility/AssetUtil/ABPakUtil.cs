/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class ABPakUtil
    {
        #region Parse
        public static string GetABPak(string assetPath)
        {
            string pak = null;
            string assetConstPath = GetAssetConstPath(assetPath);
            ABBuildPakType abBuildPakType = GetABBuildPakType(assetConstPath);
            switch (abBuildPakType)
            {
                case ABBuildPakType.OneShared:
                    pak = GetABOneSharedPak(assetConstPath, assetPath);
                    break;
                case ABBuildPakType.Single_Group:
                    pak = GetABSingleGroupPak(assetConstPath, assetPath);
                    break;
                case ABBuildPakType.Multi_Group:
                    pak = GetABMultiGroupPak(assetConstPath, assetPath);
                    break;
                case ABBuildPakType.Alone_ObjPath:
                    pak = GetABAloneObjPathPak(assetConstPath, assetPath);
                    break;
                case ABBuildPakType.Alone_SecFolderPath:
                    pak = GetABAloneSecFolderPathPak(assetConstPath, assetPath);
                    break;
            }
            return string.Format("{0}{1}", pak, AppConst.ABExtName);
        }

        private static string GetAssetConstPath(string assetPath)
        {
            return assetPath.Substring(0, assetPath.IndexOf("/") + 1);
        }

        private static ABBuildPakType GetABBuildPakType(string assetConstPath)
        {
            return ABPakConst.PakTypeDic[assetConstPath];
        }

        private static string GetABOneSharedPak(string assetConstPath, string assetPath)
        {
            string pak = ABPakConst.OneSharedPakDic[assetConstPath];
            return pak;
        }

        private static string GetABSingleGroupPak(string assetConstPath, string assetPath)
        {
            int index = assetConstPath.IndexOf("/");
            string pak = assetConstPath.Substring(0, index);
            pak = assetConstPath + pak;
            return pak;
        }

        private static string GetABMultiGroupPak(string assetConstPath, string assetPath)
        {
            int index = assetPath.IndexOf("/") + 1;
            index = assetPath.IndexOf("/", index);
            string pak = assetPath.Substring(0, index);
            return pak;
        }

        private static string GetABAloneObjPathPak(string assetConstPath, string assetPath)
        {
            string pak = assetPath;
            return pak;
        }

        private static string GetABAloneSecFolderPathPak(string assetConstPath, string assetPath)
        {
            string pak = assetPath;
            return pak;
        }
        #endregion
    }
}