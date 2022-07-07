/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.1
*/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FutureCore
{
    public class JsonConfigParser : IDisposable
    {
        private class ConfigData
        {
            public string assetPath;
            public Dictionary<string, object> allConfigDict;
        }

        #region Field
        private const bool IsSyncLoadMode = true;

        private static string TAG = "[JsonConfigParser] ";

        private static readonly string s_Statistic_config_parseerror = "config_parseerror";
        private static readonly string s_Statistic_config_serializeerror = "config_serializeerror";

        private static ConfigData s_LocalConfigData;
        private static ConfigData s_ClientConfigData;

        private string rawEditorAssetPath;
        private string assetPath;
        private string configName;
        private bool isLocal;
        private Action<JsonConfigParser> onConfigParser;
        #endregion

        #region Func
        public JsonConfigParser(string assetPath, string configName, bool isLocal, Action<JsonConfigParser> onConfigParser)
        {
            this.rawEditorAssetPath = assetPath;
            this.assetPath = assetPath;
            this.configName = configName;
            this.isLocal = isLocal;
            this.onConfigParser = onConfigParser;

            Load();
        }

        private void Load()
        {
#if C_NO_IN_UNITY
            assetPath = configName;
            IOLoadConfig();
#elif C_DESIGNER_MODE
            assetPath = string.Concat(PathUtil.ProjectPath, "../Config/", configName, AppConst.ABExtName);
            IOLoadConfig();
#else
            if (isLocal)
            {
                EngineLoadLocalConfig();
            }
            else
            {
                EngineLoadConfig();
            }
#endif
        }

        private void EngineLoadLocalConfig()
        {
            if (GetConfigData() != null)
            {
                onConfigParser(this);
                Dispose();
                return;
            }

            ResMgr.Loader.LoadTextAsset(assetPath, OnLoadedTextAsset, IsSyncLoadMode);
        }

        private void EngineLoadConfig()
        {
            if (GetConfigData() != null)
            {
                onConfigParser(this);
                Dispose();
                return;
            }
              
#if UNITY_EDITOR
            // 编辑器环境直接读取本地配置
            if (Application.isEditor && AppConst.IsConfigEditorLoadInternally)
            {
                ResMgr.Loader.LoadTextAsset(assetPath, OnLoadedTextAsset, IsSyncLoadMode);
                return;
            }
#endif

            try
            {
                // 标准初始化配置
                if (AppConst.ConfigServerHash == AppConst.ConfigInternalHash)
                {
                    assetPath = rawEditorAssetPath;
                    ResMgr.Loader.LoadTextAsset(assetPath, OnLoadedTextAsset, IsSyncLoadMode);
                    return;
                }
                else
                {
                    string configServerVersion = AppConst.ConfigServerVersion;
                    string serverConfigPath = GetServerConfigPath(AppConst.ConfigServerVersion); ;
                    if (configServerVersion != null && serverConfigPath != null && File.Exists(serverConfigPath))
                    {
                        assetPath = serverConfigPath;
                        string text = File.ReadAllText(assetPath);
                        OnLoadedJson(text);
                        return;
                    }
                }

                if (AppConst.ConfigServerHash == null)
                {
                    LogUtil.LogError("[JsonConfigParser]EngineLoadConfig Exception: AppConst.ConfigServerHash is null");
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("[JsonConfigParser]EngineLoadConfig Exception: " + e.ToString());
                ReadRollbackConfig();
                return;
            }

            ReadRollbackConfig();
        }

        private void ReadRollbackConfig()
        {
            if (AppConst.IsConfigRollback)
            {
                LogUtil.Log("[JsonConfigParser]Read Rollback Config");
                assetPath = rawEditorAssetPath;
                ResMgr.Loader.LoadTextAsset(assetPath, OnLoadedTextAsset, IsSyncLoadMode);
                return;
            }
            LogUtil.LogError("[JsonConfigParser]Error: No Read Any Config!");
        }

        private ConfigData GetConfigData()
        {
            if (isLocal)
            {
                return s_LocalConfigData;
            }
            else
            {
                return s_ClientConfigData;
            }
        }

        private string GetAgoServerConfigVersion()
        {
            string wssServerConfigVersion = null;
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash))
            {
                string wssServerConfigHash = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash);
                if (!string.IsNullOrEmpty(wssServerConfigHash))
                {
                    wssServerConfigVersion = PathConst.WssGameConfigFilePrefix + wssServerConfigHash;
                }
            }
            return wssServerConfigVersion;
        }

        private string GetServerConfigPath(string fullConfigServerVersion)
        {
            if (fullConfigServerVersion == null)
            {
                return null;
            }

            string configDir = null;
            if (WSNetMgr.Instance.IsAppWssUrl())
            {
                configDir = PathConst.WssConfigDir;
            }
            else
            {
                configDir = PathConst.ObsoleteConfigDir;
            }
            string serverConfigPath = configDir + fullConfigServerVersion + AppConst.ABExtName;
            return serverConfigPath;
        }

        private void OnLoadedTextAsset(TextAsset textAsset)
        {
            OnLoadedJson(textAsset.text);
        }

        private void OnLoadedJson(string text)
        {
            ParseReadConfig(text);
            onConfigParser(this);
            Dispose();
        }

        private void ParseReadConfig(string text)
        {
            try
            {
                if (isLocal)
                {
                    s_LocalConfigData = new ConfigData();
                }
                else
                {
                    s_ClientConfigData = new ConfigData();
                }

                ConfigData configData = GetConfigData();

                // 本地配置表
                if (isLocal || assetPath == rawEditorAssetPath)
                {
                    string jsonContent = JsonEncryptUtil.DecryptString(assetPath, text, string.Empty);
                    configData.assetPath = assetPath;
                    configData.allConfigDict = SerializeUtil.ToDicLocal(jsonContent);
                }
                // 客户端配置表
                else
                {
                    string jsonContent = JsonEncryptUtil.DecryptString(assetPath, text, AppConst.ConfigServerHash);
                    configData.assetPath = assetPath;
                    configData.allConfigDict = SerializeUtil.ToDicClient(jsonContent);
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(StringUtil.Concat(TAG, "ParseReadConfig is error, assetPath = ", assetPath));
                NotificationParseError(e);
                throw e;
            }
        }

        public List<V> SerializeConfig<V>() where V : class
        {
            string voJson = null;
            try
            {
                ConfigData configData = GetConfigData();
                object configObject = configData.allConfigDict[configName];
                voJson = configObject.ToString();
                List<V> list = SerializeUtil.ToObject<List<V>>(voJson);
                return list;
            }
            catch (Exception e)
            {
                NotificationSerializeError(e);
                throw e;
            }
        }

        private void NotificationParseError(Exception e)
        {
            LogUtil.LogErrorFormat("[JsonConfigParser]ConfigPath: {0} ConfigName: {1} ParseError: {2}", assetPath, configName, e.ToString());
            WSNetMgr.Instance.State = WSNetState.ConfigParseError;
            AppDispatcher.Instance.Dispatch(AppMsg.System_ConfigInitError);
            // 异常统计
            Channel.Current.onEvent(s_Statistic_config_parseerror);
        }

        private void NotificationSerializeError(Exception e)
        {
            LogUtil.LogErrorFormat("[JsonConfigParser]ConfigPath: {0} ConfigName: {1} SerializeError: {2}", assetPath, configName, e.ToString());
            WSNetMgr.Instance.State = WSNetState.ConfigSerializeError;
            AppDispatcher.Instance.Dispatch(AppMsg.System_ConfigInitError);
            // 异常统计
            Channel.Current.onEvent(s_Statistic_config_serializeerror);
        }

        public void Dispose()
        {
            onConfigParser = null;
        }
        #endregion

        #region Static Func
        public static void ReleaseLocalConfigJson()
        {
            string assetPath = string.Empty;
            int count = 0;
            ConfigData configData = s_LocalConfigData;
            if (configData != null && configData.allConfigDict != null)
            {
                assetPath = configData.assetPath;
                count = configData.allConfigDict.Count;
            }

            LogUtil.LogFormat("[JsonConfigParser]LocalConfigJson AssetPath: {0}", assetPath);
            LogUtil.LogFormat("[JsonConfigParser]LocalConfigJson Raw Count: {0}", count);

            configData = null;
            s_LocalConfigData = null;
        }

        public static void ReleaseClientConfigJson()
        {
            string assetPath = string.Empty;
            int count = 0;
            ConfigData configData = s_ClientConfigData;
            if (configData != null && configData.allConfigDict != null)
            {
                assetPath = configData.assetPath;
                count = configData.allConfigDict.Count;
            }

            LogUtil.LogFormat("[JsonConfigParser]ClientConfigJson AssetPath: {0}", assetPath);
            LogUtil.LogFormat("[JsonConfigParser]ClientConfigJson Raw Count: {0}", count);

            configData = null;
            s_ClientConfigData = null;
        }
        #endregion
    }
}