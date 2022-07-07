/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.6
*/

using System.Collections.Generic;

namespace FutureCore
{
    public sealed class ConfigMgr : BaseMgr<ConfigMgr>
    {
        private Dictionary<string, IVOModel> configVOModelDict = new Dictionary<string, IVOModel>();
        private List<IVOModel> localConfigs = new List<IVOModel>();
        private List<IVOModel> clientConfigs = new List<IVOModel>();
        private int completeLocalConfigCount;
        private int completeClientConfigCount;

        public bool IsParsedLocalConfig { get; private set; }
        public bool IsParsedClientConfig { get; private set; }

        public override void Init()
        {
            base.Init();
            ParseAllLocalConfig();
            if (AppConst.IsConfigPreInit)
            {
                ParseAllClientConfig();
            }
        }

        #region Public All Local Config
        private void ParseAllLocalConfig()
        {
            IsParsedLocalConfig = true;
            foreach (IVOModel model in configVOModelDict.Values)
            {
                if (model.GetIdentifyType() == VOIdentifyType.Local)
                {
                    localConfigs.Add(model);
                }
            }

            LogUtil.LogFormat("[ConfigMgr]Local Config Total Count:{0}", localConfigs.Count);

            if (localConfigs.Count == 0)
            {
                OnAllLocalConfigComplete();
            }
            else
            {
                foreach (IVOModel model in localConfigs)
                {
                    model.Init();
                }
            }
        }

        public void OnOnceLocalConfigComplete()
        {
            completeLocalConfigCount++;
            if (completeLocalConfigCount == localConfigs.Count)
            {
                OnAllLocalConfigComplete();
            }
        }

        private void OnAllLocalConfigComplete()
        {
            foreach (IVOModel model in localConfigs)
            {
                model.ReleaseAsset();
            }
            JsonConfigParser.ReleaseLocalConfigJson();
            LogUtil.LogFormat("[ConfigMgr]Local Config Parse End");

            AppDispatcher.Instance.Dispatch(AppMsg.System_LocalConfigInitComplete);
        }
        #endregion

        #region Public All Client Config
        public void ParseAllClientConfig()
        {
            IsParsedClientConfig = true;
            foreach (IVOModel model in configVOModelDict.Values)
            {
                if (model.GetIdentifyType() != VOIdentifyType.Local)
                {
                    clientConfigs.Add(model);
                }
            }

            string internalVersion = AppConst.ConfigInternalVersion;
            string serverVersion = AppConst.ConfigServerVersion;
            if (AppConst.IsConfigPreInit)
            {
                serverVersion = GetAgoServerConfigVersion();
            }
            LogUtil.LogFormat("[ConfigMgr]ClientConfigInternalVersion: {0} | ClientConfigServerVersion: {1}", internalVersion, serverVersion);
            LogUtil.LogFormat("[ConfigMgr]Client Config Total Count:{0}", clientConfigs.Count);

            if (clientConfigs.Count == 0)
            {
                OnAllClientConfigComplete();
            }
            else
            {
                foreach (IVOModel model in clientConfigs)
                {
                    model.Init();
                }
            }
        }

        public void OnOnceClientConfigComplete()
        {
            completeClientConfigCount++;
            if (completeClientConfigCount == clientConfigs.Count)
            {
                OnAllClientConfigComplete();
            }
        }

        private void OnAllClientConfigComplete()
        {
            foreach (IVOModel model in clientConfigs)
            {
                model.ReleaseAsset();
            }
            JsonConfigParser.ReleaseClientConfigJson();
            LogUtil.LogFormat("[ConfigMgr]Client Config Parse End");

            ModuleMgr.Instance.AllModuleReadData();
            AppDispatcher.Instance.Dispatch(AppMsg.System_ConfigInitComplete);
        }
        #endregion

        #region Private
        private string GetAgoServerConfigVersion()
        {
            string wssServConfigVersion = null;
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash))
            {
                string wssGameConfigHash = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash);
                if (!string.IsNullOrEmpty(wssGameConfigHash))
                {
                    wssServConfigVersion = PathConst.WssGameConfigFilePrefix + wssGameConfigHash;
                }
            }
            return wssServConfigVersion;
        }

        public void AddConfigVOModel(string configName, IVOModel model)
        {
            if (!configVOModelDict.ContainsKey(configName))
            {
                configVOModelDict.Add(configName, model);
            }
        }

        private void DisposeAllConfig()
        {
            foreach (IVOModel model in configVOModelDict.Values)
            {
                model.Dispose();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeAllConfig();

            configVOModelDict.Clear();
            configVOModelDict = null;
        }
        #endregion
    }
}