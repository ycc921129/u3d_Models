/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;

namespace FutureCore
{
    public sealed class VersionMgr : BaseMgr<VersionMgr>
    {
        public ChannelType ChannelType { get; private set; }
        public string AppName { get; private set; }
        public string ChannelName { get; private set; }
        public bool IsRelease { get; private set; }
        public string AppVersion { get; private set; }
        public long BundleVersionCode { get; private set; }

        public string[] AppVersions { get; private set; }

        private void InitVersion()
        {
            AppName = AppInfoConst.AppName;
            ChannelType = AppInfoConst.ChannelType;
            ChannelName = AppInfoConst.ChannelName;
            IsRelease = AppInfoConst.IsRelease;
            AppVersion = AppInfoConst.AppVersion;
            BundleVersionCode = AppInfoConst.BundleVersionCode;

            AppVersions = AppVersion.Split('.');
        }

        public int GetMainVersion(string[] version)
        {
            return version[0].ToInt();
        }

        public int GetSubVersion(string[] version)
        {
            return version[1].ToInt();
        }

        public int GetSmallVersion(string[] version)
        {
            return version[2].ToInt();
        }

        public string GetVersionJoinText(string[] version)
        {
            return string.Join(".", version);
        }

        #region Mgr
        public override void Init()
        {
            base.Init();
            InitVersion();
        }

        public override void StartUp()
        {
            base.StartUp();
        }
        #endregion Mgr
    }
}