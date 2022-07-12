/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using Newtonsoft.Json.Linq;
using ProjectApp.Data;
using ProjectApp.Protocol;
using System;
using System.IO;
using UnityEngine;

namespace ProjectApp
{
    public static class LoginLocalCache
    {
        #region Install 安装初始化流程
        /// <summary>
        /// 检查APK安装
        /// </summary>
        public static void CheckApkInstall()
        {  
            CheckAppDir();

            string currVerName = Channel.Current.ver;
            int currVerCode = Channel.Current.ver_code;
            if (!PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_beforeVerName))
            {
                PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_beforeVerName, currVerName);
            }
            if (!PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_beforeVerCode))
            {
                PrefsUtil.WriteInt(PrefsKeyConst.LoginCtrl_beforeVerCode, currVerCode);
            }  

            // 覆盖安装
            string beforeVerName = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_beforeVerName);
            int beforeVerCode = PrefsUtil.ReadInt(PrefsKeyConst.LoginCtrl_beforeVerCode);
            if (currVerName != beforeVerName)
            {
                // 写入新版本
                PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_beforeVerCode, currVerName);
            }
            if (currVerCode != beforeVerCode)
            {
                // 写入新版本
                PrefsUtil.WriteInt(PrefsKeyConst.LoginCtrl_beforeVerCode, currVerCode);
            }

            if (currVerName != beforeVerName || currVerCode != beforeVerCode)
            {
                ClearAppDir();
                CheckAppDir();
            }
        }

        private static void ClearAppDir()
        {
            try
            {
                PathUtil.ClearDir(PathConst.OperationConfigDir);
                PathUtil.ClearDir(PathConst.WssConfigDir);
                PathUtil.ClearDir(PathConst.ObsoleteConfigDir);

                PathUtil.ClearDir(PathConst.ConfigDir);
                PathUtil.ClearDir(PathConst.CacheDir);

                PathUtil.ClearDir(PathConst.DataDir);
                PathUtil.ClearDir(PathConst.LogDir);
            }
            catch (Exception e)
            {
                LogUtil.LogError("[LoginLocalCache] ClearAppDir is error.");
                throw;
            }
        }

        private static void CheckAppDir()
        {
            try
            {
                PathUtil.NoExistsCreateDir(PathConst.DataDir);
                PathUtil.NoExistsCreateDir(PathConst.LogDir);

                PathUtil.NoExistsCreateDir(PathConst.ConfigDir);
                PathUtil.NoExistsCreateDir(PathConst.CacheDir);

                PathUtil.NoExistsCreateDir(PathConst.OperationConfigDir);
                PathUtil.NoExistsCreateDir(PathConst.WssConfigDir);
                PathUtil.NoExistsCreateDir(PathConst.ObsoleteConfigDir);
            }
            catch (Exception e)
            {
                LogUtil.LogError("[LoginLocalCache] CheckAppDir is error.");  
                throw;
            }
        }
        #endregion

        #region Uid 用户信息
        /// <summary>
        /// 获取本地Uid
        /// </summary>
        public static long GetLocalUid()
        {
            long uid = -1; 
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_localUid))
            {
                string uidData = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_localUid);
                byte[] uidBytes = Base64EncodeUtil.Base64DecodeBytes(uidData);
                uid = long.Parse(AESEncryptUtil.Decrypt(uidBytes));
            }
            LogUtil.Log("[LoginLocalCache]GetLocalUid");
            return uid; 
        }

        /// <summary>
        /// 设置本地Uid
        /// </summary>
        public static void SetLocalUid(long uid)
        {
            if (!PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_localUid))
            {
                byte[] uidBytes = AESEncryptUtil.Encrypt(uid.ToString());
                string uidText = Base64EncodeUtil.Base64EncodeString(uidBytes);
                PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_localUid, uidText);
                LogUtil.LogFormat("[LoginLocalCache]SetLocalUid:{0}", uid);
            }            
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        public static string GetToken()
        {
            string token = null;
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_token))
            {
                token = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_token);
                byte[] tokenBytes = Base64EncodeUtil.Base64DecodeBytes(token);
                token = AESEncryptUtil.Decrypt(tokenBytes);
            }
            LogUtil.Log("[LoginLocalCache]Get Token");  
            return token;
        }

        /// <summary>
        /// 设置本地Token
        /// </summary>
        public static void SetToken(string token)
        { 
            if (!PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_token))
            {
                byte[] tokenBytes = AESEncryptUtil.Encrypt(token);
                string tokenText = Base64EncodeUtil.Base64EncodeString(tokenBytes);  
                PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_token, tokenText);             
                LogUtil.LogFormat("[LoginLocalCache]Set Token:{0}", tokenText);  
            }
        }
        
        #endregion

        #region Conf 运营配置
        private const string Offline_OperationConfigFile = "Data/Offline/OperationConfig";
        
        public static string GetCacheOperationConfigVersion()
        {
            if (PrefsUtil.HasKey(PrefsKeyConst.LoginCtrl_operationConfigLocalVerision))  
            {
                string operationConfig_version = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_operationConfigLocalVerision);
                if (operationConfig_version != null) return operationConfig_version;  
                else return "0";
            }
            else
                return "0";
        }

        public static bool IsExistOperationConfigLocalCache()
        {
            bool hasOperationConfig = false;
            if (File.Exists(PathConst.CacheOperationConfigPath))
            {
                hasOperationConfig = true;         
            }

            if (App.GetIsOfflineGame())
            {
                if (!hasOperationConfig)
                {
                    TextAsset textAsset = ResMgr.Instance.SyncLoadTextAsset(Offline_OperationConfigFile);
                    if (textAsset != null && textAsset.text != null)
                    {
                        hasOperationConfig = true;
                    }
                }
            }
            return hasOperationConfig;
        }        
        #endregion
                
        #region Preferences 服务器数据
        /// <summary>
        /// 保存Preferences缓存服务器数据
        /// </summary>
        public static void SaveServerPreferencesCache(Preferences preferences)
        {
            JsonEncryptUtil.WriteObjectInLocalFile(PathConst.CacheServerPreferencesPath, preferences, null);
        }

        /// <summary>
        /// 读取Preferences缓存服务器数据
        /// </summary>
        public static Preferences ReadServerPreferencesCache()
        {
            Preferences preferences = null;
            if (File.Exists(PathConst.CacheServerPreferencesPath))
            {
                try
                {
                    preferences = JsonEncryptUtil.ReadFormLocalFile<Preferences>(PathConst.CacheServerPreferencesPath, null);
                }
                catch (Exception)
                {
                    LogUtil.LogError("[LoginLocalCache]ReadServerPreferencesCache Fail!");
                }
            }
            return preferences;
        }

        public static bool IsExistServerPreferencesCache()
        {
            return File.Exists(PathConst.CacheServerPreferencesPath);
        }

        public static bool IsCanReadServerPreferencesCache()
        {
            if (!File.Exists(PathConst.CacheServerPreferencesPath)) return false;

            Preferences preferences = JsonEncryptUtil.ReadFormLocalFile<Preferences>(PathConst.CacheServerPreferencesPath, null);
            return preferences != null;
        }
        #endregion

        #region Preferences 本地数据
        /// <summary>
        /// 保存Preferences缓存本地数据
        /// </summary>
        public static void SaveLocalPreferencesCache(Preferences preferences)
        {
            JsonEncryptUtil.WriteObjectInLocalFile(PathConst.CacheLocalPreferencesPath, preferences, null);
        }

        /// <summary>
        /// 读取Preferences缓存本地数据
        /// </summary>
        public static Preferences ReadLocalPreferencesCache()
        {
            Preferences preferences = null;
            if (File.Exists(PathConst.CacheLocalPreferencesPath))
            {
                try
                {
                    preferences = JsonEncryptUtil.ReadFormLocalFile<Preferences>(PathConst.CacheLocalPreferencesPath, null);
                }
                catch (Exception)
                {
                    LogUtil.LogError("[LoginLocalCache]ReadLocalPreferencesCache Fail!");
                }
            }
            return preferences;
        }

        public static bool IsExistLocalPreferencesCache()
        {
            return File.Exists(PathConst.CacheLocalPreferencesPath);
        }

        public static bool IsCanReadLocalPreferencesCache()
        {
            if (!File.Exists(PathConst.CacheLocalPreferencesPath)) return false;

            Preferences preferences = JsonEncryptUtil.ReadFormLocalFile<Preferences>(PathConst.CacheLocalPreferencesPath, null);
            return preferences != null;
        }
        #endregion
    }
}