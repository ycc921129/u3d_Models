/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Data;

namespace ProjectApp
{
    public static class AppRedirection
    {
        public static void RedirectionStaticField()
        {
            AppConst_Redirection();
        }

        private static void AppConst_Redirection()
        {
            AppConst.ConfigInternalHash = ConfigVOVersion.InternalVersion;
            AppConst.ConfigInternalVersion = ConfigVOVersion.InternalVersion;
        }
    }
}