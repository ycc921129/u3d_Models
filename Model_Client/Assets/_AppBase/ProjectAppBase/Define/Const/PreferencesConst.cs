/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    /// <summary>
    /// 在PreferencesMgrAutoRegisterTool_v2注册字段
    /// </summary>
    public static class PreferencesConst
    {
        /// <summary>
        /// 自动快速保存时间
        /// </summary>  
        public static float AutoSaveTimeInterval = 0.5f + AppConst.FrameRateTimestep;   
    }
}