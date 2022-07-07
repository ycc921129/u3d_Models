/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using System.Reflection;
using UnityEngine;

namespace FuturePlugin
{
    /// <summary>
    /// 粒子系统调试器
    /// </summary>
    public class ParticleSystemDebuger : MonoBehaviour
    {
        private ParticleSystem[] m_ParticleSystems;
        private MethodInfo m_CalculateEffectUIDataMethod;
        private int m_ParticleCount = 0;

        private void Start()
        {
            m_ParticleSystems = FindObjectsOfType<ParticleSystem>();
            m_CalculateEffectUIDataMethod = typeof(ParticleSystem).GetMethod("CalculateEffectUIData", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private void Update()
        {
            m_ParticleCount = 0;
            foreach (var ps in m_ParticleSystems)
            {
                int count = 0;
                object[] invokeArgs = new object[] { count, 0.0f, Mathf.Infinity };
                m_CalculateEffectUIDataMethod.Invoke(ps, invokeArgs);
                count = (int)invokeArgs[0];
                m_ParticleCount += count;
            }
        }

        private void OnGUI()
        {
            GUILayout.Label(string.Format("<size=50>粒子数量 : {0}</size>", m_ParticleCount));
        }
    }
}

#endif