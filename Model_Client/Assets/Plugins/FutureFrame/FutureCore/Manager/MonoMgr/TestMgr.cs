/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.12.6
*/

using UnityEngine;
#if UNITY_EDITOR
using FutureEditor;
#endif

namespace FutureCore
{
    public sealed partial class TestMgr : BaseMonoMgr<TestMgr>
    {
#if UNITY_EDITOR

        #region 测试按钮
        [SerializeField]
        [Space(3f)]
        [CustomLabel_]
        private string Info_gm = "[GM]";

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("1) 打开GM界面")]
        private bool OpenGMGUI_TestBtn;
        private void OpenGMGUI()
        {
            GMMgr.Instance.OpenGUI();
            LogUtil.Log("[TestMgr]打开GM界面");
        }

        [SerializeField]
        [Space(3f)]
        [RenameField_("输入: GM命令")]
        private string Input_DoGMCmd = "Du_Test2";
        [SerializeField]
        [InspectorButton_("2) 执行GM命令")]
        private bool DoGMCmd_TestBtn;
        private void DoGMCmd()
        {
            GMMgr.Instance.CallCommond(Input_DoGMCmd);
        }

        [SerializeField]
        [Space(3f)]
        [CustomLabel_]
        private string Info_kz = "[快照]";

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("1) AB内存快照")]
        private bool ABMemorySnapshoot_TestBtn;
        private void ABMemorySnapshoot()
        {
            LogUtil.LogFormat("[TestMgr]AB内存快照:", AssetBundleMgr.Instance.GetLoadedABsInfo());
        }

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("2) 资源缓存内存快照")]
        private bool AssetsCacheSnapshoot_TestBtn;
        private void AssetsCacheSnapshoot()
        {
            LogUtil.LogObject("[TestMgr]资源缓存内存快照:", ResMgr.Instance.GetCacheAssetsInfo());
        }

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("3) 动态资源缓存内存快照")]
        private bool DynamicAssetsCacheSnapshoot_TestBtn;
        private void DynamicAssetsCacheSnapshoot()
        {
            LogUtil.LogObject("[TestMgr]动态资源缓存内存快照:", ResMgr.Instance.GetDynamicAssetsCacheInfo());
        }

        [SerializeField]
        [Space(3f)]
        [CustomLabel_]
        private string Info_zy = "[资源]";

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("1) 卸载当前场景AB")]
        private bool UnloadCurrSceneAB_TestBtn;
        private void UnloadCurrSceneAB()
        {
            int currPreLoadId = LoadPipelineMgr.Instance.GetCurrPreLoadId();
            LoadPipelineMgr.Instance.UnloadPreLoad(currPreLoadId);
        }

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("2) 卸载所有空引用资源")]
        private bool UnloadNullReferenceAssets_TestBtn;
        private void UnloadNullReferenceAssets()
        {
            ResMgr.Instance.UnloadNullReferenceAssets();
        }

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("3) 卸载所有已经加载的AB")]
        private bool UnloadAllLoadedAB_TestBtn;
        private void UnloadAllLoadedAB()
        {
            AssetBundleMgr.Instance.UnloadAllLoadedAB();
        }

        [SerializeField]
        [Space(3f)]
        [CustomLabel_]
        private string Info_wl = "[网络]";

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("1) 派发网络状态变化: 已连接")]

        private bool NetworkStatusChangedTrue_TestBtn;
        private void NetworkStatusChangedTrue()
        {
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.NetworkStatusChanged_True, true);
        }

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("2) 派发网络状态变化: 已断开")]
  
        private bool NetworkStatusChangedFalse_TestBtn;
        private void NetworkStatusChangedFalse()
        {
            ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.NetworkStatusChanged_False, false);
        }

        [SerializeField]
        [Space(3f)]
        [CustomLabel_]
        private string Info_gn = "[功能]";

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("1) 震动屏幕")]
        private bool ShakeMainCamera_TestBtn;
        private void ShakeMainCamera()
        {
            CameraMgr.Instance.ShakeMainCamera();
        }

        [SerializeField]
        [Space(3f)]
        [InspectorButton_("2) 查看FGUI字体列表")]
        private bool CheckFGUIFonts_TestBtn;
        private void CheckFGUIFonts()
        {
            LogUtil.LogObject("[TestMgr]查看FGUI字体列表:", FairyGUI.FontManager.GetFontsInfo());
        }

        [SerializeField]
        [Space(3f)]
        [RenameField_("输入: 切换的语言")]
        private string Input_SwitchFGUILangString = "zh";
        [SerializeField]
        [InspectorButton_("3) 切换FGUI的语言")]
        private bool SwitchFGUILang_TestBtn;
        private void SwitchFGUILang()
        {
            string switchLang = Input_SwitchFGUILangString;
            if (string.IsNullOrWhiteSpace(switchLang)) return;

            UIMgr.Instance.SetSwitchLanguage(switchLang);
        }
        #endregion

     
#endif
    }
}