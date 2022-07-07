namespace FutureEditor
{
    public static class EditorAppConst
    {
        public const string AppName = AppFacade_Editor.AppName;
        public const string AppDesc = AppFacade_Editor.AppDesc;

        public const string MenuItemAppName = "【" + AppName + ":" + AppDesc + "】";
        public const string MenuSubItemAppName = MenuItemAppName + "/代号:【" + AppName + "】";
        public const string MenuSubItemAppDesc = MenuItemAppName + "/描述:【" + AppDesc + "】";
        public const string MenuSubItemOpenAppCodeVSIDE = MenuItemAppName + "/" + "启动项目IDE";

        public const string AtlasTag_FullRect = "[FullRect]";
        public const string AtlasTag_Tight = "[Tight]";
    }
}