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

        /// <summary>
        /// 缓存的运营配置
        /// </summary>
        public static Pg_setting CacheOperationConfig = null;

        /// <summary>
        /// 读取本地运营配置
        /// </summary>
        public static void ReadOperationConfigLocalCache(C2S_reg_login_data reqData)
        {
            bool hasOperationConfig = IsExistOperationConfigLocalCache();
            string operationConfigHash = null;
            if (hasOperationConfig)
            {
                try
                {
                    if (File.Exists(PathConst.CacheOperationConfigPath))
                    {
                        //operationConfigHash = PrefsUtil.ReadString(PrefsKeyConst.LoginCtrl_operationConfigLocalHash);
                        CacheOperationConfig = JsonEncryptUtil.ReadFormLocalFile<Pg_setting>(PathConst.CacheOperationConfigPath, operationConfigHash);
                        if (CacheOperationConfig == null)
                        {
                            hasOperationConfig = false;
                        }
                    }
                    if (CacheOperationConfig == null)
                    {
                        hasOperationConfig = false;
                    }

                    if (App.GetIsOfflineGame())
                    {
                        if (!hasOperationConfig)
                        {
                            TextAsset textAsset = ResMgr.Instance.SyncLoadTextAsset(Offline_OperationConfigFile);
                            if (textAsset != null && textAsset.text != null)
                            {
                                string text = JsonEncryptUtil.DecryptString(Offline_OperationConfigFile, textAsset.text, null);
                                CacheOperationConfig = SerializeUtil.ToObject<Pg_setting>(text);
                                if (CacheOperationConfig != null)
                                {
                                    hasOperationConfig = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    hasOperationConfig = false;                    
                    LogUtil.LogError("[LoginLocalCache]Not Find OperationConfig Exception: " + e.ToString());
                }
            }
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

        public static bool IsCanReadOperationConfigLocalCache()
        {
            Pg_setting operationConfig = null;
            bool res = false;
            if (File.Exists(PathConst.CacheOperationConfigPath))
            {
                operationConfig = JsonEncryptUtil.ReadFormLocalFile<Pg_setting>(PathConst.CacheOperationConfigPath, AppFacade.AESKey);
                res = operationConfig != null; 
            }

            if (App.GetIsOfflineGame())
            {
                if (!res)
                {
                    TextAsset textAsset = ResMgr.Instance.SyncLoadTextAsset(Offline_OperationConfigFile);
                    if (textAsset != null && textAsset.text != null)
                    {
                        string text = JsonEncryptUtil.DecryptString(Offline_OperationConfigFile, textAsset.text, null);
                        operationConfig = SerializeUtil.ToObject<Pg_setting>(text); 
                        res = operationConfig != null;
                    }
                }
            }
            return res;
        }
         
        /// <summary>
        /// 保存本地运营配置
        /// </summary>
        public static void SaveOperationConfigLocalCache(S2C_reg_login resp)
        {
            try
            {
                JsonEncryptUtil.WriteObjectInLocalFile(PathConst.CacheOperationConfigPath, resp.data.pg_setting, AppFacade.AESKey);
                JObject jobj = SerializeUtil.ToObject<JObject>(resp.data.pg_setting.ToString());
                PrefsUtil.WriteString(PrefsKeyConst.LoginCtrl_operationConfigLocalVerision, jobj["version"].ToString());
                LogUtil.Log("[LoginLocalCache]Update OperationConfig Succeed");
            }
            catch (Exception e)
            {
                LogUtil.LogError(e.ToString());
            }
        }
          
        /// <summary>  
        /// 设置本地运营配置
        /// </summary>
        public static void SetOperationConfigLocalCache(S2C_reg_login resp)
        {
            if (resp.data.pg_setting == null)
            {
                resp.data.pg_setting = CacheOperationConfig;
            }
            else
            {
                SaveOperationConfigLocalCache(resp);
            }
        }
        #endregion

        #region S2CLoginMsg 登录消息
        private const string Offline_CacheLoginMsgFile = "Data/Offline/CacheLoginMsg";

        /// <summary> 
        /// 获取本地缓存登录消息对象
        /// </summary>
        private static Cache_S2C_reg_login GetS2CLoginMsgLocalCache(S2C_reg_login resp)
        {
            Cache_S2C_reg_login cacheMsg = new Cache_S2C_reg_login();
            cacheMsg.rawJson = resp.GetRawJson();
            cacheMsg.type = resp.type;
            cacheMsg.err = resp.err;            
            cacheMsg.data = new Cache_S2C_reg_login_data();
            cacheMsg.data.uid = resp.data.uid;
            cacheMsg.data.token = resp.data.token;
            cacheMsg.data.info = resp.data.info;
            cacheMsg.data.pg_setting = resp.data.pg_setting;
            cacheMsg.data.pg_settingex = resp.data.pg_settingex;
            cacheMsg.data.ltv_24h = resp.data.ltv_24h;
            cacheMsg.data.upgrade = resp.data.upgrade;
            cacheMsg.data.pref = resp.data.pref;  
            return cacheMsg;
        }

        /// <summary>
        /// 保存本地缓存登录消息
        /// </summary>
        public static void SaveS2CLoginMsgLocalCache(S2C_reg_login resp)
        {
            Cache_S2C_reg_login cacheMsg = GetS2CLoginMsgLocalCache(resp);
            JsonEncryptUtil.WriteObjectInLocalFile(PathConst.CacheS2CLoginMsgPath, cacheMsg, null);
            LogUtil.Log("[LoginLocalCache]Update SaveS2CLoginMsgLocalCache");
        }

        /// <summary>
        /// 读取本地缓存登录消息
        /// </summary>
        public static S2C_reg_login ReadS2CLoginMsgLocalCache()
        {
            S2C_reg_login resp = null;
            try
            {
                if (File.Exists(PathConst.CacheS2CLoginMsgPath))
                {
                    resp = JsonEncryptUtil.ReadFormLocalFile<S2C_reg_login>(PathConst.CacheS2CLoginMsgPath, null);
                }

                if (App.GetIsOfflineGame())
                {
                    if (resp == null)
                    {
                        TextAsset textAsset = ResMgr.Instance.SyncLoadTextAsset(Offline_CacheLoginMsgFile);
                        if (textAsset != null && textAsset.text != null)
                        {
                            string text = JsonEncryptUtil.DecryptString(Offline_CacheLoginMsgFile, textAsset.text, null);
                            resp = SerializeUtil.ToObject<S2C_reg_login>(text);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("[LoginLocalCache]ReadS2CLoginMsgLocalCache Fail: " + e.ToString());
            }
            return resp;
        }

        public static bool IsExistS2CLoginMsgLocalCache()
        {
            bool res = File.Exists(PathConst.CacheS2CLoginMsgPath);

            if (App.GetIsOfflineGame())
            {
                if (!res)
                {
                    TextAsset textAsset = ResMgr.Instance.SyncLoadTextAsset(Offline_CacheLoginMsgFile);
                    if (textAsset != null && textAsset.text != null)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        public static bool IsCanReadS2CLoginMsgLocalCache()
        {
            S2C_reg_login resp = null;
            bool res = false;
            if (File.Exists(PathConst.CacheS2CLoginMsgPath))
            {
                resp = JsonEncryptUtil.ReadFormLocalFile<S2C_reg_login>(PathConst.CacheS2CLoginMsgPath, null);
                res = resp != null;
            }

            if (App.GetIsOfflineGame())
            {
                if (!res)
                {
                    TextAsset textAsset = ResMgr.Instance.SyncLoadTextAsset(Offline_CacheLoginMsgFile);
                    if (textAsset != null && textAsset.text != null)
                    {
                        string text = JsonEncryptUtil.DecryptString(Offline_CacheLoginMsgFile, textAsset.text, null);
                        resp = SerializeUtil.ToObject<S2C_reg_login>(text);
                        res = resp != null;
                    }
                }
            }
            return res;
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