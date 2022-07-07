#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FutureRuntimeEditor
{
    public class Unity3dParticleSystem_BatchOptimizeTool : MonoBehaviour
    {
        // 使用方法
        // 1：把此脚本拖拽进场景任意物体中。
        // 2：选好要调整得到项后，在监视面板（Inspector）的右上角点击小锁 锁定监视面板。
        // 3：在Project面板多选你要改变的预制体。
        // 4：在监视面板右键脚本 选择【添加选中的物体】。
        // 5：在监视面板右键脚本 选择【开始优化】。
        // 6：优化完毕，Remove挂载的脚本，解除监视面板小锁。
        public string helpInfo = "请Inspector面板右键脚本查看如何操作";

        [Header("是否限制粒子最大数量↓")]
        public bool isParSize = false;

        [Header("暴力设置：循环特效最大粒子数&非循环特效最大粒子数")]
        public int loopParricleMaxSize = 500;

        public int onceParricleMaxSize = 100;

        [Header("智能计算粒子峰值数(考虑原本的最大粒子，结果小于原本 则设置为峰值，大于则不)")]
        public bool isAuto = false;

        [Header("是否把粒子的ScalingMode改为Hierarchy以适应父级缩放")]
        public bool setScalingMode = false;

        [Header("勾选√ 管理粒子发射完毕后状态：无动作、隐藏物体、销毁物体、回调")]
        public bool isDes = false;

        public ParticleSystemStopAction des;

        [Header("对所有粒子，NoiSe模块，统一设置其精度为2DNoise")]
        public bool isNoise = false;

        [Header("检查并关闭灯光功能")]
        public bool isLight = false;

        [Header("未使用拖尾功能的，清除trail材质引用")]
        public bool isTrailmat = false;

        [Header("是否统一设置粒子在屏幕上最大大小限制")]
        public bool isMaxParticleSize = false;

        public float maxParsize = 5;

        [Header("是否统一把所有Order in Layer还原到以下值")]
        public bool isOIL = false;

        public int oil = 0;

        [Header("是否检查并关闭所有粒子的投影和光照接收")]
        public bool isShadow = false;

        [Header("下列清单仅展示已添加的物体和获取到的粒子，无需操作")]
        private GameObject[] addgo = new GameObject[0];

        private List<ParticleSystemRenderer> parRenders = new List<ParticleSystemRenderer>();
        private List<ParticleSystem> pars = new List<ParticleSystem>();

        [ContextMenu("》先添加选中的物体《")]
        private void Add_TXGO()
        {
            addgo = Selection.gameObjects;
            for (int i = 0; i < addgo.Length; i++)
            {
                ParticleSystem[] linshipar = new ParticleSystem[0];
                ParticleSystemRenderer[] linshiparren = new ParticleSystemRenderer[0];
                linshipar = addgo[i].GetComponentsInChildren<ParticleSystem>(true);
                linshiparren = addgo[i].GetComponentsInChildren<ParticleSystemRenderer>(true);
                for (int a = 0; a < linshipar.Length; a++)
                {
                    pars.Add(linshipar[a]);
                    parRenders.Add(linshiparren[a]);
                }
            }
            Debug.Log("本次选择了" + addgo.Length + "个物体" + "，已加入清单的粒子系统的有" + parRenders.Count + "个，请【开始优化】");
        }

        [ContextMenu("__》开始优化《__")]
        private void YouhuaTX()
        {
            for (int i = 0; i < pars.Count; i++)
            {
                ParticleSystem.MainModule mainmodule = pars[i].main;
                ParticleSystem.EmissionModule emimodule = pars[i].emission;
                ParticleSystem.NoiseModule noiseModule = pars[i].noise;
                ParticleSystem.LightsModule lightsModule = pars[i].lights;
                ParticleSystem.TrailModule trailModule = pars[i].trails;
                ParticleSystem.CustomDataModule customDataModule = pars[i].customData;
                if (isParSize == true)
                {
                    if (isAuto == true)
                    {
                        OnAuto(mainmodule, emimodule);
                    }
                    else
                    {
                        if (pars[i].main.loop == true)
                        {
                            mainmodule.maxParticles = loopParricleMaxSize;
                        }
                        else
                        {
                            mainmodule.maxParticles = onceParricleMaxSize;
                        }
                        if (mainmodule.maxParticles <= 1)
                        {
                            mainmodule.maxParticles = 1;
                        }
                    }
                }
                if (setScalingMode == true)
                {
                    mainmodule.scalingMode = ParticleSystemScalingMode.Hierarchy;
                }
                if (isDes == true)
                {
                    mainmodule.stopAction = des;
                }
                if (isNoise == true)
                {
                    noiseModule.quality = ParticleSystemNoiseQuality.Medium;
                }
                if (isLight == true)
                {
                    lightsModule.enabled = false;
                }
                if (isTrailmat == true)
                {
                    if (trailModule.enabled == false)
                    {
                        parRenders[i].trailMaterial = null;
                    }
                }
                if (isMaxParticleSize)
                {
                    parRenders[i].maxParticleSize = maxParsize;
                }
                if (isOIL == true)
                {
                    parRenders[i].sortingOrder = oil;
                }
                if (isShadow == true)
                {
                    parRenders[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    parRenders[i].receiveShadows = false;
                }
            }
            Debug.Log("所选物体全部优化完毕，共修改了" + parRenders.Count + "个，请 CTRL+S 保存修改。");
        }

        [ContextMenu("控制台显示文字教程")]
        private void LogJiaoCheng()
        {
            Debug.Log("1：把此脚本拖拽进场景任意物体中。2：选好要调整得到项后，在监视面板（Inspector）的右上角点击小锁 锁定监视面板。3：在Project面板多选你要改变的预制体。4：在监视面板右键脚本 选择【添加选中的物体】。5：在监视面板右键脚本 选择【开始优化】。6：优化完毕，Remove挂载的脚本，解除监视面板小锁。");
        }

        public void OnAuto(ParticleSystem.MainModule mainModule, ParticleSystem.EmissionModule emissionModule)
        {
            float b = mainModule.maxParticles;
            float particleCount = 0;
            if (emissionModule.rateOverTime.constant > 0)
            {
                float size = Mathf.Max(emissionModule.rateOverTime.constantMax, emissionModule.rateOverTime.constantMin);
                particleCount += size;
            }
            if (emissionModule.rateOverTime.curveMultiplier != 0)
            {
                float[] dd = new float[emissionModule.rateOverTime.curve.keys.Length];
                for (int z = 0; z < dd.Length; z++)
                {
                    dd[z] = emissionModule.rateOverTime.curve.keys[z].value;
                }
                float maxValue = Mathf.Max(dd) * emissionModule.rateOverTime.curveMultiplier;
                particleCount += maxValue;
            }
            if (emissionModule.burstCount >= 1)
            {
                for (int i = 0; i < emissionModule.burstCount; i++)
                {
                    if (emissionModule.GetBurst(i).count.constant != 0)
                    {
                        particleCount += Mathf.Max(emissionModule.GetBurst(i).count.constantMax, emissionModule.GetBurst(i).count.constantMin);
                        if (emissionModule.GetBurst(i).repeatInterval > 0.01f)
                        {
                            float size = Mathf.Max(emissionModule.GetBurst(i).count.constantMax, emissionModule.GetBurst(i).count.constantMin);
                            size = size / emissionModule.GetBurst(i).repeatInterval;
                            particleCount += size;
                        }
                    }
                    else
                    {
                        if (emissionModule.GetBurst(i).count.curve == null)
                        {
                            Debug.Log("没有发射任何粒子");
                        }
                        else
                        {
                            float[] dd = new float[(int)emissionModule.GetBurst(i).count.curve.length];
                            for (int z = 0; z < dd.Length; z++)
                            {
                                dd[z] = emissionModule.GetBurst(i).count.curve.keys[z].value;
                            }
                            float maxValue = Mathf.Max(dd) * emissionModule.GetBurst(i).count.curveMultiplier;
                            particleCount += maxValue;
                            if (emissionModule.GetBurst(i).repeatInterval > 0.01f)
                            {
                                float size = Mathf.Max(emissionModule.GetBurst(i).count.constantMax, emissionModule.GetBurst(i).count.constantMin);
                                size = size / emissionModule.GetBurst(i).repeatInterval;
                                particleCount += size;
                            }
                        }
                    }
                }
            }
            float sizeclamp = 0;
            sizeclamp = mainModule.startLifetime.constantMax;
            sizeclamp = Mathf.Clamp(sizeclamp, 1, 99999);
            particleCount = particleCount * sizeclamp;
            Debug.Log(sizeclamp);
            if (particleCount <= b && particleCount != 0)
            {
                float ad = Mathf.Max(emissionModule.rateOverDistance.constantMax, emissionModule.rateOverDistance.constantMin);
                if (ad == 0 && emissionModule.rateOverDistance.curve == null)
                {
                    if (particleCount <= 2)
                    {
                        mainModule.maxParticles = 5;
                    }
                    else
                    {
                        mainModule.maxParticles = (int)particleCount;
                    }
                }
                else
                {
                    Debug.Log("当前粒子拥有距离发射量，安全起见不设置其max粒子限制，当前值：" + mainModule.maxParticles);
                }
            }
        }
    }
}

#endif