/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.13
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FuturePlugin;
using UnityEngine;
using UnityEngine.Networking;

namespace FutureCore
{
    public sealed class VersionUpdateMgr : BaseMonoMgr<VersionUpdateMgr>
    {
        private class FileUpdateData
        {
            public string abName;
            public string md5;
            public int length;
            public string downServUrl;
            public string saveLoaclPath;
        }

        #region Field
        private string wwwZipName = "www.zip";
        private string abFilesTxtName = "abfiles.txt";
        private string versionTxtName = "version.txt";

        private string streamingPath;
        private string dataPath;
        private string webAssetsUrl;
        private string webPlatformAssetsUrl;
        private string platformDataPath;

        private string inReadZipUrl;
        private string outReadZipUrl;
        private string inVersionFileUrl;
        private string outVersionFileUrl;
        private string servVersionFileUrl;
        private string localABFilesTxtUrl;
        private string servABFilesTxtUrl;

        private List<FileUpdateData> updateDatas = new List<FileUpdateData>();
        private Action onCompleteFunc;

        // HotUpdate Complete Flag
        private byte[] servVerData;
        private byte[] servCompleteABFilesTxtBytes;

        private string firstBundleVersion = "1.0.1";
        private int firstBundleVersionCode = 1;
        #endregion

        public void StartUpProcess(Action onCompleteFunc)
        {
            Init(onCompleteFunc);
            HandleInstallApkProcess();
            StartCoroutine(HandleAssetProcess());
        }
        
        /// <summary>
        /// 处理安装流程
        /// </summary>
        private void HandleInstallApkProcess()
        {
            if (!AppConst.IsReleaseApp) return;

            long beforeInstallCode = PrefsUtil.ReadString(PrefsKeyConst.VersionUpdateMgr_beforeInstallCode).ToLong();
            long currInstallCode = VersionMgr.Instance.BundleVersionCode;
            if (beforeInstallCode == firstBundleVersionCode)
            {
                PrefsUtil.WriteString(PrefsKeyConst.VersionUpdateMgr_beforeInstallCode, currInstallCode.ToString());
                LogUtil.Log("[VersionUpdateMgr]首次安装包");
            }
            else if (beforeInstallCode != currInstallCode)
            {
                ClearEnviroment();
                PrefsUtil.WriteString(PrefsKeyConst.VersionUpdateMgr_beforeInstallCode, currInstallCode.ToString());
                LogUtil.Log("[VersionUpdateMgr]覆盖安装不同包");
            }
            LogUtil.LogFormat("[VersionUpdateMgr]InstallApkInfo:BeforeCode:{0} CurrCode:{1}", beforeInstallCode, currInstallCode);
        }

        private void ClearEnviroment()
        {
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            // 资源
            if (Directory.Exists(platformDataPath))
            {
                IOUtil.DeleteTargetFolderTheAllFileAndFolder(platformDataPath);
            }
            // 资源版本
            if (File.Exists(outVersionFileUrl))
            {
                File.Delete(outVersionFileUrl);
            }
        }

        private void Init(Action onCompleteFunc)
        {
            streamingPath = PathUtil.StreamingAssetsPath;
            dataPath = PathUtil.DataPath;
            webAssetsUrl = NetConst.AssetsWebUrl;
            webPlatformAssetsUrl = NetConst.AssetsWebUrl + PathUtil.PlatformSubDataPath;
            platformDataPath = PathUtil.PlatformDataPath;

            inReadZipUrl = streamingPath + wwwZipName;
            outReadZipUrl = dataPath + wwwZipName;
            inVersionFileUrl = streamingPath + versionTxtName;
            outVersionFileUrl = dataPath + versionTxtName;
            servVersionFileUrl = webAssetsUrl + versionTxtName;
            localABFilesTxtUrl = platformDataPath + abFilesTxtName;
            servABFilesTxtUrl = webPlatformAssetsUrl + abFilesTxtName;

            this.onCompleteFunc = onCompleteFunc;
        }

        /// <summary>
        /// 处理资源流程
        /// </summary>
        private IEnumerator HandleAssetProcess()
        {
            bool isExtracted = Directory.Exists(dataPath)
                && File.Exists(outVersionFileUrl)
                && Directory.Exists(platformDataPath)
                && File.Exists(localABFilesTxtUrl);

            if (!isExtracted)
            {
                // 复制资源包到程序沙盒环境
                ClearEnviroment();
                yield return StartCoroutine(CopyAssetPackage());
            }

            bool hasAssetPackage = AppConst.HasAssetPackage;
            if (!isExtracted && hasAssetPackage)
            {
                // 提取资源包解压资源和资源版本文件
                StartCoroutine(ExtractAll());
            }
            else
            {
                // 热更新资源
                StartCoroutine(HotUpdateAsset());
            }
        }

        private IEnumerator CopyAssetPackage()
        {
            LogUtil.Log("[VersionUpdateMgr]Start Copy Asset Package");
            App.SetLoadingUI("载入资源包...", 5);

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(inReadZipUrl);
                yield return www;
                if (www.isDone && !string.IsNullOrEmpty(www.error))
                {
                    LogUtil.Log("[VersionUpdateMgr]Android Check Asset Package Error: " + www.error);
                    SetNoHasAssetPackage();
                    yield break;
                }
                File.WriteAllBytes(outReadZipUrl, www.bytes);
                www.Dispose();
            }
            else
            {
                if (!File.Exists(inReadZipUrl))
                {
                    SetNoHasAssetPackage();
                }
            }
        }

        private void SetNoHasAssetPackage()
        {
            AppConst.HasAssetPackage = false;
            LogUtil.Log("[VersionUpdateMgr]App Has Asset Package : " + AppConst.HasAssetPackage);
        }

        private IEnumerator ExtractAll()
        {
            LogUtil.Log("[VersionUpdateMgr]Start Extract Asset");
            App.SetLoadingUI("解压资源包...", 10);

            // Assets
            bool isZipResult = false;
            string readZipPath = inReadZipUrl;
            if (Application.platform == RuntimePlatform.Android)
            {
                readZipPath = outReadZipUrl;
            }
              
            //HACK 压缩插件删除，暂时用不到
            //yield return new YieldWaitForThreaded(() => isZipResult = GZipHelper.UnZip(readZipPath, dataPath));
            //LogUtil.Log("[VersionUpdateMgr]Extract " + wwwZipName + " Inside Asset Complete Result : " + isZipResult);

            yield return YieldConst.WaitForEndOfFrame;
            if (!isZipResult)
            {
                yield break;
            }
            else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    File.Delete(readZipPath);
                    LogUtil.Log("[VersionUpdateMgr]Clear Android External AssetZip");
                    yield return YieldConst.WaitForEndOfFrame;
                }
            }

            // AssetsVersion
            if (File.Exists(outVersionFileUrl))
            {
                File.Delete(outVersionFileUrl);
            }

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(inVersionFileUrl);
                yield return www;
                if (www.isDone && !string.IsNullOrEmpty(www.error))
                {
                    OnHotUpdateFailed(www);
                    yield break;
                }
                File.WriteAllBytes(outVersionFileUrl, www.bytes);
                www.Dispose();
            }
            else
            {
                File.Copy(inVersionFileUrl, outVersionFileUrl, true);
            }
            LogUtil.Log("[VersionUpdateMgr]Extract Local " + versionTxtName + " Complete");
            yield return YieldConst.WaitForEndOfFrame;

            LogUtil.Log("[VersionUpdateMgr]All Extract Complete");

            // HotUpdateAsset
            StartCoroutine(HotUpdateAsset());
        }

        private IEnumerator HotUpdateAsset()
        {
            if (!AppConst.IsHotUpdateMode)
            {
                OnHotUpdateComplete();
                yield break;
            }

            App.SetLoadingUIInfo("检测资源...");
            LogUtil.Log("[VersionUpdateMgr]HotUpdateAsset");

            if (!Directory.Exists(platformDataPath))
            {
                Directory.CreateDirectory(platformDataPath);
            }
            StartCoroutine(CheckAssetVersionUpdate());
        }

        private IEnumerator CheckAssetVersionUpdate()
        {
            LogUtil.Log("[VersionUpdateMgr]Check AssetVersion");
            LogUtil.Log("[VersionUpdateMgr]Downing " + servVersionFileUrl);

            UnityWebRequest webReq = UnityWebRequest.Get(servVersionFileUrl);
            yield return webReq.SendWebRequest();
            if (webReq.isNetworkError || webReq.isHttpError || webReq.responseCode != WebRequestConst.Succeed)
            {
                OnHotUpdateFailed(webReq);
                yield break;
            }
            servVerData = webReq.downloadHandler.data;
            string servVerText = webReq.downloadHandler.text;
            string[] servVersions = GetVersionData(servVerText);
            AppConst.ServerAssetVersions = servVersions;
            int servAssetsVersion = VersionMgr.Instance.GetSmallVersion(AppConst.ServerAssetVersions);
            webReq.Dispose();

            bool isNeedAssetUpdate = true;
            if (File.Exists(outVersionFileUrl))
            {
                string localVerText = File.ReadAllText(outVersionFileUrl);
                string[] localVersions = GetVersionData(localVerText);
                AppConst.LocalAssetVersions = localVersions;
                int localAssetsVersion = VersionMgr.Instance.GetSmallVersion(AppConst.LocalAssetVersions);
                if (localAssetsVersion >= servAssetsVersion)
                {
                    isNeedAssetUpdate = false;
                }
                LogUtil.Log("[VersionUpdateMgr]AssetVersion Info:LocalVersion: " + localVerText + " | ServerVersion: " + servVerText);
            }
            else
            {
                isNeedAssetUpdate = true;
                LogUtil.Log("[VersionUpdateMgr]Curr AssetVersion:No Have LocalVersion File");
            }
            yield return YieldConst.WaitForEndOfFrame;

            if (isNeedAssetUpdate)
            {
                yield return StartCoroutine(GetUpdateFile());
                OpenHotUpdateView();
            }
            else
            {
                OnHotUpdateComplete();
            }
        }

        private string[] GetVersionData(string versionText)
        {
            string[] servTexts = versionText.Trim().Split(SplitConst.DefaultText);
            string[] servVersions = servTexts[1].Split('.');
            return servVersions;
        }

        private IEnumerator GetUpdateFile()
        {
            LogUtil.Log("[VersionUpdateMgr]Downing:" + servABFilesTxtUrl);

            UnityWebRequest webReq = UnityWebRequest.Get(servABFilesTxtUrl);
            yield return webReq.SendWebRequest();
            if (webReq.isNetworkError || webReq.isHttpError || webReq.responseCode != WebRequestConst.Succeed)
            {
                OnHotUpdateFailed(webReq);
                yield break;
            }
            servCompleteABFilesTxtBytes = webReq.downloadHandler.data;
            string servText = webReq.downloadHandler.text;
            string[] servFiles = servText.Trim().Split('\n');
            webReq.Dispose();

            string[] localFiles = null;
            bool hasLocalABFile = File.Exists(localABFilesTxtUrl);
            if (hasLocalABFile)
            {
                localFiles = File.ReadAllLines(localABFilesTxtUrl);
            }
            List<FileUpdateData> currNeedupdateDatas = GetUpdateFileDatas(webPlatformAssetsUrl, platformDataPath, servFiles, localFiles);
            updateDatas.AddRange(currNeedupdateDatas);

            yield return YieldConst.WaitForEndOfFrame;
        }

        private void OpenHotUpdateView()
        {
            int totalLength = 0;
            for (int i = 0; i < updateDatas.Count; i++)
            {
                totalLength += updateDatas[i].length;
            }
            if (totalLength > 0)
            {
                string lengthInfo = GetFileSizeInfo(totalLength);
                string contentInfo = string.Format("检测到有{0}资源更新，是否更新？", lengthInfo);
                string affirmInfo = "更新";
                string cancelInfo = "退出";
                Action affirmFunc = () => { StartCoroutine(OnHotUpdateDown()); };
                Action cancelFunc = GameMgr.Instance.Quit;
                App.ShowAffirmUI(contentInfo, affirmInfo, cancelInfo, affirmFunc, cancelFunc);
            }
            else
            {
                OnHotUpdateComplete();
            }
        }

        private List<FileUpdateData> GetUpdateFileDatas(string servUrl, string localPath, string[] servFiles, string[] localFiles)
        {
            Dictionary<string, string> localFilesDic = new Dictionary<string, string>();
            if (localFiles != null)
            {
                for (int i = 0; i < localFiles.Length; i++)
                {
                    string localFile = localFiles[i];
                    string[] localFileArray = localFile.Split(SplitConst.DefaultText);
                    string abName = localFileArray[0];
                    string abMD5 = localFileArray[1];
                    localFilesDic.Add(abName, abMD5);
                }
            }

            List<FileUpdateData> updateFiles = new List<FileUpdateData>();
            for (int i = 0; i < servFiles.Length; i++)
            {
                string servfile = servFiles[i];
                string[] fileArray = servfile.Split(SplitConst.DefaultText);
                string abName = fileArray[0];
                string abMD5 = fileArray[1];
                string length = fileArray[2];

                bool canUpdate = false;
                if (localFilesDic.ContainsKey(abName))
                {
                    string valuelocalFile;
                    localFilesDic.TryGetValue(abName, out valuelocalFile);
                    if (!abMD5.Equals(valuelocalFile))
                        canUpdate = true;
                }
                else
                {
                    canUpdate = true;
                }
                if (canUpdate)
                {
                    FileUpdateData data = new FileUpdateData
                    {
                        abName = abName,
                        md5 = abMD5,
                        length = length.ToInt(),
                        downServUrl = servUrl,
                        saveLoaclPath = localPath
                    };
                    updateFiles.Add(data);
                }
            }
            return updateFiles;
        }

        private IEnumerator OnHotUpdateDown()
        {
            LogUtil.Log("[VersionUpdateMgr]Start Hot Update Down");

            App.SetLoadingUIInfo("资源更新中...");

            int totalUpdateCount = updateDatas.Count;
            for (int i = 0; i < totalUpdateCount; i++)
            {
                FileUpdateData data = updateDatas[i];

                string dataLenInfo = GetFileSizeInfo(data.length);
                string loadingUIInfo = string.Format("更新资源{0} {1}", data.abName, dataLenInfo);
                App.SetLoadingUIInfo(loadingUIInfo);

                string localABFile = data.saveLoaclPath + data.abName;
                string localABFilePath = Path.GetDirectoryName(localABFile);
                if (!Directory.Exists(localABFilePath))
                    Directory.CreateDirectory(localABFilePath);
                if (File.Exists(localABFile))
                    File.Delete(localABFile);

                string servABUrl = data.downServUrl + data.abName;
                LogUtil.Log("[VersionUpdateMgr]Hot Downing " + servABUrl);

                UnityWebRequest webReq = UnityWebRequest.Get(servABUrl);
                yield return webReq.SendWebRequest();
                if (webReq.isNetworkError || webReq.isHttpError || webReq.responseCode != WebRequestConst.Succeed)
                {
                    OnHotUpdateFailed(webReq);
                    yield break;
                }
                File.WriteAllBytes(localABFile, webReq.downloadHandler.data);
                webReq.Dispose();

                float downPercent = (float)(i + 1) / totalUpdateCount;
                int loadingPercent = 10 + (int)(downPercent * (89 - 10));
                App.SetLoadingUIProgress(loadingPercent);
            }

            // HotUpdate Complete Flag
            File.WriteAllBytes(outVersionFileUrl, servVerData);
            File.WriteAllBytes(localABFilesTxtUrl, servCompleteABFilesTxtBytes);
            servVerData = null;
            servCompleteABFilesTxtBytes = null;

            yield return YieldConst.WaitForEndOfFrame;
            OnHotUpdateComplete();
        }

        private string GetFileSizeInfo(int size)
        {
            string sizeInfo = null;
            if (size < MathConst.OneKBSize)
            {
                sizeInfo = size + "B";
            }
            else if (size < MathConst.OneMBSize)
            {
                sizeInfo = Mathf.Ceil(size / MathConst.OneKBSize) + "K";
            }
            else
            {
                sizeInfo = Mathf.Floor(size / MathConst.OneMBSize) + "M";
            }
            return sizeInfo;
        }

        private void OnHotUpdateFailed(WWW www)
        {
            App.SetLoadingUIInfo("下载失败，请检查网络状态。");
            string msg = string.Format(www.url + " Error: " + www.error + " Text: " + www.text);
            LogUtil.LogError("[VersionUpdateMgr]WWW Failed: " + msg);
        }

        private void OnHotUpdateFailed(UnityWebRequest webReq)
        {
            App.SetLoadingUIInfo("下载失败，请检查网络状态。");
            string msg = string.Format(webReq.url + " Error: " + webReq.error + " Code: " + webReq.responseCode + " Text: " + webReq.downloadHandler.text);
            LogUtil.LogError("[VersionUpdateMgr]WebRequest Failed: " + msg);
        }

        private void OnHotUpdateComplete()
        {
            LogUtil.Log("[VersionUpdateMgr]Hot Update Complete");
            AppConst.LocalAssetVersions = AppConst.ServerAssetVersions;
            onCompleteFunc();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            updateDatas.Clear();
            updateDatas = null;
        }
    }
}