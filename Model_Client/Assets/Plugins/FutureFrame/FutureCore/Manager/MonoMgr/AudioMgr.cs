/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace FutureCore
{
    public sealed class AudioMgr : BaseMonoMgr<AudioMgr>
    {
        #region 变量定义

        /// <summary>
        /// 声音资源目录
        /// </summary>
        public const string ResourceDir = "Audio/";

        /// <summary>
        /// 是否不受TimeScale影响
        /// </summary>
        public const bool IsUnscaleTime = true;

        /// <summary>
        /// 音频初始常驻缓存池
        /// </summary>
        private Dictionary<string, AudioClip> audioClipCacheDict = new Dictionary<string, AudioClip>();

        /// <summary>
        /// 新建音效 播放组件节点
        /// </summary>
        private GameObject newEffectSourcesRoot;

        /// <summary>
        /// 动态音效 播放组件节点
        /// </summary>
        private GameObject dynamicEffectSourcesRoot;

        /// <summary>
        /// 音效监听器
        /// </summary>
        [HideInInspector]
        public AudioListener audioListener;

        /// <summary>
        /// 背景声音 播放组件
        /// </summary>
        [HideInInspector]
        public AudioSource bgmSource;

        /// <summary>
        /// 音效声音 播放组件
        /// </summary>
        [HideInInspector]
        public AudioSource effectSource;

        /// <summary>
        /// 新建音效声音 播放组件
        /// </summary>
        [HideInInspector]
        public List<AudioSource> newEffectSources = new List<AudioSource>();

        /// <summary>
        /// 动态音效 播放组件
        /// </summary>
        [HideInInspector]
        public List<AudioSource> dynamicEffectSources = new List<AudioSource>();

        /// <summary>
        /// 是否不受TimeScale影响的播放组件
        /// </summary>
        [HideInInspector]
        public List<AudioSource> unscaleTimeSounds = new List<AudioSource>();

        /// <summary>
        /// 当前UI音效音量
        /// </summary>
        [HideInInspector]
        public float currUISoundVolume = 1;

        #endregion 变量定义

        #region 音乐开关

        private bool isOpenBGM;
        public bool IsOpenBGM
        {
            get
            {
                return isOpenBGM;
            }
            set
            {
                isOpenBGM = value;
                PrefsUtil.WriteBool(PrefsKeyConst.AudioMgr_isOpenBGM, isOpenBGM);
                if (!bgmSource) return;

                bgmSource.enabled = isOpenBGM;
                if (isOpenBGM)
                {
                    bgmSource.Play();
                }
                else
                {
                    bgmSource.Pause();
                }
            }
        }

        private bool isOpenEffect;
        public bool IsOpenEffect
        {
            get
            {
                return isOpenEffect;
            }
            set
            {
                isOpenEffect = value;
                PrefsUtil.WriteBool(PrefsKeyConst.AudioMgr_isOpenEffect, isOpenEffect);

                // FGUI声音控制
                GRoot.inst.soundVolume = isOpenEffect ? currUISoundVolume : 0f;
                if (isOpenEffect)
                {
                    GRoot.inst.EnableSound();
                }
                else
                {
                    GRoot.inst.DisableSound();
                }
            }
        }

        private void InitAudioMode()
        {
            isOpenBGM = PrefsUtil.ReadBool(PrefsKeyConst.AudioMgr_isOpenBGM, true);
            isOpenEffect = PrefsUtil.ReadBool(PrefsKeyConst.AudioMgr_isOpenEffect, true);

            // 初始化UI全局音效
            GRoot.inst.soundVolume = 1;
            currUISoundVolume = GRoot.inst.soundVolume;
            GRoot.inst.soundVolume = isOpenEffect ? currUISoundVolume : 0f;
        }

        public void PauseAllSource()
        {
            bgmSource.Pause();

            IsOpenEffect = false;
            effectSource.Pause();
            for (int i = 0; i < newEffectSources.Count; i++)
            {
                newEffectSources[i].Pause();
            }
            for (int i = 0; i < dynamicEffectSources.Count; i++)
            {
                dynamicEffectSources[i].Pause();
            }

            // UI声音
            GRoot.inst.DisableSound();
        }

        public void UnPauseAllSource()
        {
            bgmSource.UnPause();

            IsOpenEffect = true;
            effectSource.UnPause();
            for (int i = 0; i < newEffectSources.Count; i++)
            {
                newEffectSources[i].UnPause();
            }
            for (int i = 0; i < dynamicEffectSources.Count; i++)
            {
                dynamicEffectSources[i].UnPause();
            }

            // UI声音
            GRoot.inst.EnableSound();
        }

        #endregion 音乐开关

        #region 音量控制

        /// <summary>
        /// 背景音量
        /// </summary>
        public float BgmVolume
        {
            get
            {
                return bgmSource.volume;
            }
            set
            {
                bgmSource.volume = value;
            }
        }

        /// <summary>
        /// 音效音量
        /// </summary>
        public float EffectVolume
        {
            get
            {
                return effectSource.volume;
            }
            set
            {
                effectSource.volume = value;
            }
        }

        /// <summary>
        /// 新建音效音量
        /// </summary>
        public float newEffectVolume
        {
            get
            {
                for (int i = 0; i < newEffectSources.Count; i++)
                {
                    return newEffectSources[i].volume;
                }
                return 0;
            }
            set
            {
                for (int i = 0; i < newEffectSources.Count; i++)
                {
                    newEffectSources[i].volume = value;
                }
            }
        }

        /// <summary>
        /// 动态音效音量
        /// </summary>
        public float dynamicEffectVolume
        {
            get
            {
                for (int i = 0; i < dynamicEffectSources.Count; i++)
                {
                    return dynamicEffectSources[i].volume;
                }
                return 0;
            }
            set
            {
                for (int i = 0; i < dynamicEffectSources.Count; i++)
                {
                    dynamicEffectSources[i].volume = value;
                }
            }
        }

        #endregion

        #region 初始化

        public override void Init()
        {
            base.Init();
            AppDispatcher.Instance.AddListener(AppMsg.TimeScale_Change, OnTimeScaleChange);

            audioListener = gameObject.AddComponent<AudioListener>();

            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.playOnAwake = false;
            bgmSource.loop = true;

            effectSource = gameObject.AddComponent<AudioSource>();
            effectSource.playOnAwake = false;
            effectSource.loop = false;

            newEffectSourcesRoot = new GameObject("NewEffectSources");
            newEffectSourcesRoot.transform.SetParent(gameObject.transform, false);
            dynamicEffectSourcesRoot = new GameObject("DynamicEffectSources");
            dynamicEffectSourcesRoot.transform.SetParent(gameObject.transform, false);

            InitAudioMode();
        }

        public override void Dispose()
        {
            base.Dispose();
            AppDispatcher.Instance.RemoveListener(AppMsg.TimeScale_Change, OnTimeScaleChange);
        }

        public void InitPermanentAudioClip()
        {
            Dictionary<string, UAssetType> permanentAssets = ResMgr.Instance.GetPermanentAssets();
            if (permanentAssets != null && permanentAssets.Count > 0)
            {
                foreach (string assetPath in permanentAssets.Keys)
                {
                    UAssetType type = permanentAssets[assetPath];
                    if (type == UAssetType.AudioClip)
                    {
                        audioClipCacheDict[assetPath] = ResMgr.Loader.GetInCache<AudioClip>(assetPath);
                    }
                }
            }
        }

        public void InitDefaultButtonClickSound(string btnSound)
        {
            string defaultSound = AudioConst.UIButtonDefault;
            if (!string.IsNullOrEmpty(btnSound))
            {
                defaultSound = btnSound;
            }
            AudioClip audioClip = ResMgr.Loader.SyncLoadAudioClip(ResourceDir + defaultSound);
            if (audioClip != null)
            {
                // 设置FGUI配置
                UIConfig.buttonSound = new NAudioClip(audioClip);
                UIConfig.buttonSoundVolumeScale = 1;
            }
        }

        private void OnTimeScaleChange(object value)
        {
            if (unscaleTimeSounds.Count != 0)
            {
                for (int i = 0; i < unscaleTimeSounds.Count; i++)
                {
                    AudioSource audioSource = unscaleTimeSounds[i];
                    if (audioSource)
                    {
                        audioSource.pitch = TimeUtil.NormalTimeScale;
                    }
                }
            }
        }

        #endregion 初始化

        #region 背景音乐: 只有一个播放器

        /// <summary>
        /// 播放背景音乐
        /// 注:相同音乐不切歌
        /// </summary>
        /// <param name="audioName">音乐名字</param>
        public void PlayBGM(string audioName, bool isUnscaleTime = IsUnscaleTime)
        {
            if (string.IsNullOrEmpty(audioName)) return;

            //当前音乐
            string curName;
            if (bgmSource.clip == null)
            {
                curName = null;
            }
            else
            {
                curName = bgmSource.clip.name;
            }

            if (curName != audioName)
            {
                AudioClip currClip = null;
                string path = ResourceDir + audioName;
                if (audioClipCacheDict.ContainsKey(path))
                {
                    currClip = audioClipCacheDict[path];
                }
                else
                {
                    AssetLoader loader = SceneMgr.Instance.GetCurrLoader();
                    currClip = loader.GetCache<AudioClip>(path, UAssetType.AudioClip);
                }

                //播放
                if (currClip != null)
                {
                    bgmSource.clip = currClip;
                    if (!isOpenBGM)
                    {
                        return;
                    }

                    if (isUnscaleTime)
                    {
                        if (!unscaleTimeSounds.Contains(bgmSource))
                        {
                            unscaleTimeSounds.Add(bgmSource);
                        }
                    }
                    else
                    {
                        if (unscaleTimeSounds.Contains(bgmSource))
                        {
                            unscaleTimeSounds.Remove(bgmSource);
                        }
                    }
                    bgmSource.pitch = isUnscaleTime ? TimeUtil.NormalTimeScale : TimeUtil.GetTimeScale();
                    bgmSource.Play();
                }
            }
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBGM()
        {
            bgmSource.Stop();
            bgmSource.clip = null;
        }

        #endregion 背景音乐

        #region 音效: 只有一个播放器

        /// <summary>
        /// 播放一次音效
        /// </summary>
        /// <param name="audioName">音效名</param>
        public void PlayOnceEffect(string audioName, bool isLoop = false, bool isUnscaleTime = IsUnscaleTime)
        {
            if (!isOpenEffect || string.IsNullOrEmpty(audioName)) return;

            //当前音乐
            string currName;
            AudioClip currClip = null;
            if (effectSource.clip == null)
            {
                currName = null;
            }
            else
            {
                currClip = effectSource.clip;
                currName = currClip.name;
            }

            if (currName != audioName)
            {
                string path = ResourceDir + audioName;
                if (audioClipCacheDict.ContainsKey(path))
                {
                    currClip = audioClipCacheDict[path];
                }
                else
                {
                    AssetLoader loader = SceneMgr.Instance.GetCurrLoader();
                    currClip = loader.GetCache<AudioClip>(path, UAssetType.AudioClip);
                }
            }

            //播放
            if (currClip)
            {
                if (effectSource.loop != isLoop)
                {
                    effectSource.loop = isLoop;
                }

                effectSource.clip = currClip;

                if (isUnscaleTime)
                {
                    if (!unscaleTimeSounds.Contains(effectSource))
                    {
                        unscaleTimeSounds.Add(effectSource);
                    }
                }
                else
                {
                    if (unscaleTimeSounds.Contains(effectSource))
                    {
                        unscaleTimeSounds.Remove(effectSource);
                    }
                }
                effectSource.pitch = isUnscaleTime ? TimeUtil.NormalTimeScale : TimeUtil.GetTimeScale();
                effectSource.Play();
            }
        }

        /// <summary>
        /// 播放完整音效
        /// </summary>
        /// <param name="audioName">音效名</param>
        public void PlayFullOnceEffect(string audioName)
        {
            if (!effectSource.isPlaying)
            {
                PlayOnceEffect(audioName);
            }
        }

        /// <summary>
        /// 停止一次音效
        /// </summary>
        public void StopOnceEffect()
        {
            effectSource.Stop();
            effectSource.clip = null;
        }

        /// <summary>
        /// 暂停一次音效
        /// </summary>
        public void PauseOnceEffect()
        {
            effectSource.Pause();
        }

        /// <summary>
        /// 继续播放一次音效
        /// </summary>
        public void ContinuousOnceEffect()
        {
            effectSource.Play();
        }

        #endregion 音效

        #region 新建音效

        /// <summary>
        /// 新建音效
        /// </summary>
        public AudioSource NewAudioSource()
        {
            AudioSource newEffectSource = newEffectSourcesRoot.AddComponent<AudioSource>();
            newEffectSource.playOnAwake = false;
            newEffectSource.loop = false;
            newEffectSources.Add(newEffectSource);
            return newEffectSource;
        }

        /// <summary>
        /// 播放一次音效
        /// </summary>
        /// <param name="audioName">音效名</param>
        public void PlayOnceEffect(AudioSource source, string audioName, bool isLoop = false, bool isUnscaleTime = IsUnscaleTime)
        {
            if (!isOpenEffect || string.IsNullOrEmpty(audioName)) return;

            //当前音乐
            string currName;
            AudioClip currClip = null;
            if (source.clip == null)
            {
                currName = null;
            }
            else
            {
                currClip = source.clip;
                currName = currClip.name;
            }

            if (currName != audioName)
            {
                string path = ResourceDir + audioName;
                if (audioClipCacheDict.ContainsKey(path))
                {
                    currClip = audioClipCacheDict[path];
                }
                else
                {
                    AssetLoader loader = SceneMgr.Instance.GetCurrLoader();
                    currClip = loader.GetCache<AudioClip>(path, UAssetType.AudioClip);
                }
            }

            //播放
            if (currClip)
            {
                if (source.loop != isLoop)
                {
                    source.loop = isLoop;
                }

                source.clip = currClip;

                if (isUnscaleTime)
                {
                    if (!unscaleTimeSounds.Contains(source))
                    {
                        unscaleTimeSounds.Add(source);
                    }
                }
                else
                {
                    if (unscaleTimeSounds.Contains(source))
                    {
                        unscaleTimeSounds.Remove(source);
                    }
                }
                source.pitch = isUnscaleTime ? TimeUtil.NormalTimeScale : TimeUtil.GetTimeScale();
                source.Play();
            }
        }

        /// <summary>
        /// 播放完整音效
        /// </summary>
        /// <param name="audioName">音效名</param>
        public void PlayFullOnceEffect(AudioSource source, string audioName)
        {
            if (!source.isPlaying)
            {
                PlayOnceEffect(source, audioName);
            }
        }

        /// <summary>
        /// 停止一次音效
        /// </summary>
        public void StopOnceEffect(AudioSource source)
        {
            source.Stop();
            source.clip = null;
        }

        #endregion

        #region 动态音效: 多个播放器

        /// <summary>
        /// 播放动态音效
        /// </summary>
        public AudioSource PlayDynamicEffect(string audioName, float delay = 0, bool isLoop = false, bool isUnscaleTime = IsUnscaleTime)
        {
            if (!isOpenEffect || string.IsNullOrEmpty(audioName)) return null;

            AudioSource effectSourceCom = null;
            for (int i = 0; i < dynamicEffectSources.Count; i++)
            {
                AudioSource sourceItem = dynamicEffectSources[i];
                if (!sourceItem.isPlaying)
                {
                    effectSourceCom = sourceItem;
                    break;
                }
            }
            if (effectSourceCom == null)
            {
                effectSourceCom = dynamicEffectSourcesRoot.AddComponent<AudioSource>();
                effectSourceCom.playOnAwake = false;
                dynamicEffectSources.Add(effectSourceCom);
            }

            if (effectSourceCom.loop != isLoop)
            {
                effectSourceCom.loop = isLoop;
            }

            //当前音乐
            string curName;
            AudioClip currClip = null;
            if (effectSourceCom.clip == null)
            {
                curName = null;
            }
            else
            {
                currClip = effectSourceCom.clip;
                curName = currClip.name;
            }

            if (curName != audioName)
            {
                string path = ResourceDir + audioName;
                if (audioClipCacheDict.ContainsKey(path))
                {
                    currClip = audioClipCacheDict[path];
                }
                else
                {
                    AssetLoader loader = SceneMgr.Instance.GetCurrLoader();
                    currClip = loader.GetCache<AudioClip>(path, UAssetType.AudioClip);
                }
            }

            //播放
            if (currClip != null)
            {
                effectSourceCom.clip = currClip;

                if (isUnscaleTime)
                {
                    if (!unscaleTimeSounds.Contains(effectSourceCom))
                    {
                        unscaleTimeSounds.Add(effectSourceCom);
                    }
                }
                else
                {
                    if (unscaleTimeSounds.Contains(effectSourceCom))
                    {
                        unscaleTimeSounds.Remove(effectSourceCom);
                    }
                }
                effectSourceCom.pitch = isUnscaleTime ? TimeUtil.NormalTimeScale : TimeUtil.GetTimeScale();
                if (delay == 0)
                {
                    effectSourceCom.Play();
                }
                else
                {
                    effectSourceCom.PlayDelayed(delay);
                }
                return effectSourceCom;
            }
            return null;
        }

        #endregion 动态音效

        #region UI声音: 只有一个播放器

        public void PlayUIButtonAudio()
        {
            PlayOnceEffect(AudioConst.UIButtonDefault);
        }

        #endregion UI声音

        #region 玩法声音: 多个播放器

        public void PlayGameAudio(string audioName, float delay = 0, bool isLoop = false, bool isUnscaleTime = IsUnscaleTime)
        {
            PlayDynamicEffect(audioName, delay, isLoop, isUnscaleTime);
        }

        #endregion 玩法声音
    }
}