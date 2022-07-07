/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.9
*/

//#define C_DEBUG_AB
//#undef C_DEBUG_AB

using FuturePlugin;
using System;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace FutureCore
{
    public sealed class AssetBundleMgr : BaseMgr<AssetBundleMgr>
    {
        #region Field
        /// <summary>
        /// 加载根目录
        /// </summary>
        private string m_abLoadPath;
        /// <summary>
        /// AB主依赖名单
        /// </summary>
        private AssetBundleManifest m_abManifest;

        /// <summary>
        /// AB包名列表
        /// </summary>
        private List<string> m_abPakList = new List<string>();
        /// <summary>
        /// AB依赖缓存字典
        /// </summary>
        public Dictionary<string, string[]> dependencieDic = new Dictionary<string, string[]>();
        /// <summary>
        /// AB项缓存字典
        /// </summary>
        public Dictionary<string, AssetBundleItem> loadedABItemDic = new Dictionary<string, AssetBundleItem>();
        /// <summary>
        /// 资源加载请求字典
        /// </summary>
        public Dictionary<string, List<ABLoadRequest>> loadReqDic = new Dictionary<string, List<ABLoadRequest>>();
        /// <summary>
        /// 依赖AB加载请求字典
        /// </summary>
        public List<string> loadDepABReqList = new List<string>();

        /// <summary>
        /// 静态AB列表
        /// </summary>
        private List<string> staticAssetBundleList = new List<string>();
        /// <summary>
        /// 卸载AB列表
        /// </summary>
        private List<AssetBundleItem> unloadABItems = new List<AssetBundleItem>();
        #endregion

        #region Public
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitAssetBundle(Action onComplete)
        {
            LogUtil.Log("[AssetBundleMgr]Init AssetBundle");

            m_abLoadPath = PathUtil.PlatformDataPath;
            InitAssetBundleManifest();
            onComplete();
        }

        /// <summary>
        /// 添加静态AB
        /// </summary>
        public void AddStaticAssetBundle(string assetBundlePak)
        {
            staticAssetBundleList.Add(assetBundlePak);
        }

        /// <summary>
        /// 初始化AB主依赖名单
        /// </summary>
        private void InitAssetBundleManifest()
        {
            string abManifestABPakName = PlatformUtil.CurrPlatformName;
            string assetBundleManifest = "AssetBundleManifest";
            AssetBundle abManifestAB = AssetBundleLoader.Instance.SyncLoadSingleAB(abManifestABPakName);
            m_abManifest = AssetBundleLoader.Instance.SyncLoadAssetFormAB<AssetBundleManifest>(abManifestAB, assetBundleManifest);
            m_abPakList.AddRange(m_abManifest.GetAllAssetBundles());

            abManifestAB.Unload(false);
        }

        /// <summary>
        /// 是否是静态AB
        /// </summary>
        public bool IsStaticAssetBundle(string abName)
        {
            return staticAssetBundleList.Contains(abName);
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="abName">AB包名</param>
        /// <param name="isThorough">是否彻底删除</param>
        public void UnloadAsset(string abName, bool isThorough = true)
        {
            abName = GetCheckABName(abName);
            if (abName == null)
                return;

            InternalUnloadAB(abName, isThorough);
        }

        /// <summary>
        /// 卸载空引用AB
        /// </summary>
        public void UnloadNullReferenceAB()
        {
            foreach (string abName in loadedABItemDic.Keys)
            {
                AssetBundleItem abItem = loadedABItemDic[abName];
                if (abItem.CanUnload())
                {
                    if (loadReqDic.ContainsKey(abName))
                        return;

                    unloadABItems.Add(abItem);
                }
            }
            foreach (AssetBundleItem abItem in unloadABItems)
            {
                abItem.Unload(true);
                loadedABItemDic.Remove(abItem.abName);
                UnloadABAllDependencies(abItem.abName, true);

                #region C_DEBUG_AB
#if C_DEBUG_AB
                LogUtil.LogFormat("[AssetBundleMgr]UnloadNullReferenceAB:{0} | LoadedABCount:{1}", abItem.abName, loadedABItemDic.Count);
#endif
                #endregion C_DEBUG_AB
            }
            unloadABItems.Clear();
        }
        #endregion

        #region Public Loader
        /// <summary>
        /// 获取检查后的AB包名
        /// </summary>
        /// <param name="abName">AB包名</param>
        /// <returns>资源路径</returns>
        public string GetCheckABName(string abName)
        {
            abName = abName.ToLower();
            if (m_abPakList.Contains(abName))
            {
                return abName;
            }
            LogUtil.LogError("[AssetBundleMgr]GetCheckABPakName Error:" + abName);
            return null;
        }

        /// <summary>
        /// 获取已经加载完毕的AB项
        /// 指的是该AB的所有依赖项也都已加载的AB
        /// </summary>
        /// <param name="abName">AB名</param>
        /// <returns>AB项</returns>
        public AssetBundleItem GetLoadedAssetBundle(string abName)
        {
            AssetBundleItem bundleItem = null;
            loadedABItemDic.TryGetValue(abName, out bundleItem);
            if (bundleItem == null)
                return null;

            string[] dependencies = null;
            if (!dependencieDic.TryGetValue(abName, out dependencies))
                return bundleItem;

            foreach (string dependency in dependencies)
            {
                AssetBundleItem dependentBundleItem;
                loadedABItemDic.TryGetValue(dependency, out dependentBundleItem);
                if (dependentBundleItem == null)
                    return null;
            }
            return bundleItem;
        }

        /// <summary>
        /// 获取完整加载路径
        /// </summary>
        public string GetFullLoadPath(string abName)
        {
            return m_abLoadPath + abName;
        }

        /// <summary>
        /// 获取AB所有依赖
        /// </summary>
        public string[] GetABAllDependencies(string abName)
        {
            return m_abManifest.GetAllDependencies(abName);
        }
        #endregion

        #region Private
        /// <summary>
        /// 内部卸载AB
        /// </summary>
        private void InternalUnloadAB(string abName, bool isThorough, bool isDependencie = false)
        {
            AssetBundleItem abItem = GetLoadedAssetBundle(abName);
            if (abItem == null)
                return;

            abItem.RemoveReferenced();
            #region C_DEBUG_AB_PROFILER
#if C_DEBUG_AB_PROFILER
            if (!isDependencie)
                LogUtil.LogFormat("<color=green>[AssetBundleMgr]Unload After Referenced:{0} | {1}</color>", abName, abItem.Referenced);
            else
                LogUtil.LogFormat("<color=yellow>[AssetBundleMgr]Unload After Referenced:{0} | {1}</color>", abName, abItem.Referenced);
#endif
            #endregion C_DEBUG_AB_PROFILER

            if (abItem.CanUnload())
            {
                if (loadReqDic.ContainsKey(abName))
                    return;

                UnloadAB(abItem, isThorough);
            }
        }

        /// <summary>
        /// 卸载AB
        /// </summary>
        private void UnloadAB(AssetBundleItem abItem, bool isThorough)
        {
            abItem.Unload(isThorough);
            loadedABItemDic.Remove(abItem.abName);
            #region C_DEBUG_AB
#if C_DEBUG_AB
            LogUtil.LogFormat("[AssetBundleMgr]UnloadAB:{0} | LoadedABCount:{1}", abItem.abName, loadedABItemDic.Count);
#endif
            #endregion C_DEBUG_AB

            UnloadABAllDependencies(abItem.abName, isThorough);
        }

        /// <summary>
        /// 卸载AB所有依赖
        /// </summary>
        private void UnloadABAllDependencies(string abName, bool isThorough)
        {
            string[] dependencies = null;
            if (!dependencieDic.TryGetValue(abName, out dependencies))
                return;

            foreach (string dependency in dependencies)
            {
                InternalUnloadAB(dependency, isThorough, true);
            }
            dependencieDic.Remove(abName);
        }

        /// <summary>
        /// 卸载所有已经加载的AB
        /// </summary>
        public void UnloadAllLoadedAB()
        {
            foreach (AssetBundleItem abItem in loadedABItemDic.Values)
            {
                unloadABItems.Add(abItem);
            }
            foreach (AssetBundleItem abItem in unloadABItems)
            {
                if (loadedABItemDic.ContainsValue(abItem))
                {
                    UnloadAB(abItem, true);
                }
            }
            unloadABItems.Clear();
            LogUtil.Log("[AssetBundleMgr]UnloadAllLoadedAB");
        }

        public override void Dispose()
        {
            base.Dispose();
            UnloadAllLoadedAB();

            m_abPakList.Clear();
            dependencieDic.Clear();
            loadedABItemDic.Clear();
            loadReqDic.Clear();
            loadDepABReqList.Clear();
            staticAssetBundleList.Clear();
            unloadABItems.Clear();
            m_abPakList = null;
            dependencieDic = null;
            loadedABItemDic = null;
            loadReqDic = null;
            loadDepABReqList = null;
            unloadABItems = null;
            staticAssetBundleList = null;

            m_abManifest = null;
        }
        #endregion

        #region Info
        public List<AssetBundleItem> GetLoadedABsInfo()
        {
            List<AssetBundleItem> infos = new List<AssetBundleItem>();
            infos.AddRange(loadedABItemDic.Values);
            return infos;
        }
        #endregion
    }
}