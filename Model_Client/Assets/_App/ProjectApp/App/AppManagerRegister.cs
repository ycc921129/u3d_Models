using System.Collections.Generic;
using FutureCore;

namespace ProjectApp
{
    public static class AppManagerRegister
    {
        public static void Register()
        {
            GlobalMgr globalMgr = GlobalMgr.Instance;
            // Mgr
            globalMgr.AddMgr(CameraMgr.Instance);
            // MonoMgr
            globalMgr.AddMgr(AudioMgr.Instance);
            globalMgr.AddMgr(UIMgr.Instance);
        }

        public static void RegisterData()
        {
            // ResMgr
            RegisterPermanentAssets();
            // AssetBundleMgr
            RegisterStaticAssetBundle();
            // UIMgr
            RegisterFont();
            RegisterCustomCommonPackage();
        }

        public static void StartUpAfterRegister()
        {
        }

        private static void RegisterPermanentAssets()
        {
            Dictionary<string, UAssetType> permanentAssets = new Dictionary<string, UAssetType>();
            // FairyGUI
            permanentAssets.Add("Shader/FairyGUI/FairyGUI-BMFont", UAssetType.Shader);
            permanentAssets.Add("Shader/FairyGUI/FairyGUI-Image", UAssetType.Shader);
            permanentAssets.Add("Shader/FairyGUI/FairyGUI-Text", UAssetType.Shader);
            permanentAssets.Add("Shader/FairyGUI/AddOn/FairyGUI-BlurFilter", UAssetType.Shader);

            ResMgr resMgr = ResMgr.Instance;
            resMgr.SetPermanentAssets(permanentAssets);
        }

        private static void RegisterStaticAssetBundle()
        {
            AssetBundleMgr assetBundleMgr = AssetBundleMgr.Instance;
            assetBundleMgr.AddStaticAssetBundle("atlas/common.bytes");
            assetBundleMgr.AddStaticAssetBundle("shared/staticshared.bytes");
            assetBundleMgr.AddStaticAssetBundle("shared/spinelibshared.bytes");
        }

        private static void RegisterFont()
        {  
            UIMgr uiMgr = UIMgr.Instance; 
            uiMgr.RegisterDefaultFont("Techna Sans");
            uiMgr.RegisterFont("Blogger Sans");
            uiMgr.RegisterFont("GrilledCheese BTN Toasted");   
        }

        private static void RegisterCustomCommonPackage()
        {
            UIMgr uiMgr = UIMgr.Instance;
            //uiMgr.RegisterCommonPackage("自定义公共UI资源包名");
        }
    }
}