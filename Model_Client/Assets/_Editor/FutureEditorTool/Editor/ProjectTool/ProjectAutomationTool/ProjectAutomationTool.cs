namespace FutureEditor
{
    public static class ProjectAutomationTool
    {
        public static string GetAutomationInfo()
        {
            string automationInfo = "自动化\r\n" +
                                    "1) AssetPath: 生成资源路径脚本\r\n" +
                                    "2) SpineAnimName: 生成Spine动画常量脚本\r\n" +
                                    "3) FGUI: 生成项目界面控制器常量";
            return automationInfo;
        }

        public static void DoAutomation()
        {
            /// 自动化
            // 1) AssetPath: 生成资源路径脚本
            AudioAssetPathCreateTool.Create();
            // 2）SpineAnimName: 生成Spine动画常量脚本
            SpineAnimNameCreateTool.Create();
            // 3) FGUI: 生成项目界面控制器常量
            FGUIContollerCreateTool_v2.CreateController_ScriptsProject();
        }
    }
}