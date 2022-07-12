/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp.Protocol;
using System;
using System.IO;

namespace ProjectApp
{
    /// <summary>
    /// 使用Wss游戏配置
    /// (服务器没加密, 只压缩了数据)
    /// </summary>
    public static class LoginWssConfig
    {
        #region Wss游戏配置
        public static string GetServerConfigVersion()
        {
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash))
            {
                string wssServerConfigHash = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash);
                if (!string.IsNullOrEmpty(wssServerConfigHash))
                {
                    string wssServConfigVersion = PathConst.WssGameConfigFilePrefix + wssServerConfigHash;
                }
            }
            return null;
        }

        public static void ReadLocalWssGameConfig()
        {
            bool ls_hasWssServerConfig = false;
            string ls_wssServerConfigHash = null;
            string ls_wssServerConfigVersion = null;

            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash))
            {
                ls_wssServerConfigHash = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash);
                if (!string.IsNullOrEmpty(ls_wssServerConfigHash))
                {
                    ls_wssServerConfigVersion = PathConst.WssGameConfigFilePrefix + ls_wssServerConfigHash;
                    PathConst.WssGameConfigPath = PathConst.WssConfigDir + ls_wssServerConfigVersion + AppConst.ABExtName;
                    if (File.Exists(PathConst.WssGameConfigPath))
                    {
                        ls_hasWssServerConfig = true;
                    }
                }
            }

            if (ls_hasWssServerConfig)
            {
                try
                {
                    object configObject = JsonEncryptUtil.ReadFormLocalFile<object>(PathConst.WssGameConfigPath, ls_wssServerConfigHash);
                    if (configObject == null)
                    {
                        ls_hasWssServerConfig = false;
                    }
                }
                catch (Exception)
                {
                    ls_hasWssServerConfig = false;
                }
            }

            if (ls_hasWssServerConfig)
            {
                AppConst.ConfigServerHash = ls_wssServerConfigHash;
                AppConst.ConfigServerVersion = ls_wssServerConfigVersion;
                LogUtil.Log("[LoginWssConfig]Curr WssServerConfigVersion: " + AppConst.ConfigServerHash);
            }
            else if (AppConst.ConfigInternalVersion != VOModelConst.ErrorConfigVersion)
            {
                AppConst.ConfigServerHash = AppConst.ConfigInternalVersion;
                AppConst.ConfigServerVersion = PathConst.WssGameConfigFilePrefix + AppConst.ConfigServerHash;
                LogUtil.Log("[LoginWssConfig]Curr ConfigInternalVersion: " + AppConst.ConfigServerHash);
            }
            else
            {
                LogUtil.LogError("[LoginWssConfig]Curr ConfigInternalVersion Error: -1" + AppConst.ConfigInternalVersion);
            }
        }

        public static bool IsExistWssConfig()
        {
            if (!Directory.Exists(PathConst.WssConfigDir))
            {
                return false;
            }
            string[] files = Directory.GetFiles(PathConst.WssConfigDir);
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash) && files.Length >= 1)
            {
                return true;
            }
            return false;
        }

        public static bool IsCanReadWssConfig()
        {
            string wssServerConfigHash = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash);
            if (string.IsNullOrEmpty(wssServerConfigHash))
            {
                return false;
            }
            string wssServerConfigVersion = PathConst.WssGameConfigFilePrefix + wssServerConfigHash;
            string path = PathConst.WssConfigDir + wssServerConfigVersion + AppConst.ABExtName;
            if (!File.Exists(path))
            {
                return false;
            }
            object configObject = JsonEncryptUtil.ReadFormLocalFile<object>(path, wssServerConfigHash);
            bool isCan = configObject != null;
            return isCan;
        }        

        /// <summary>
        /// 保存Wss游戏配置    
        /// </summary>
        public static void SaveLocalWssGameConfig(JObject _jsonObj)
        {
            PathUtil.ClearDir(PathConst.WssConfigDir);

            string wssServerConfigHash = _jsonObj["version"].ToString();
            LogUtil.LogFormat("[LoginWssConfig]Server WssGameConfig Hash: " + wssServerConfigHash);

            string wssServerConfigVersion = PathConst.WssGameConfigFilePrefix + wssServerConfigHash;
            PathConst.WssGameConfigPath = PathConst.WssConfigDir + wssServerConfigVersion + AppConst.ABExtName;
            AppConst.ConfigServerHash = wssServerConfigHash;
            AppConst.ConfigServerVersion = wssServerConfigVersion;

            PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_wssServerConfigLocalHash, wssServerConfigHash);
            JsonEncryptUtil.WriteInLocalFile(PathConst.WssGameConfigPath, _jsonObj.ToString(), wssServerConfigHash);
            LogUtil.Log("[LoginWssConfig]Update WssServerConfig Succeed: " + AppConst.ConfigServerVersion);
            LogUtil.Log("[LoginWssConfig]Update WssGameConfigPath: " + PathConst.WssGameConfigPath);
        }
        #endregion
    }
}