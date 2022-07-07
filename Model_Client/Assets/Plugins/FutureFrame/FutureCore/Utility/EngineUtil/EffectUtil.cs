/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class EffectUtil
    {
        public static void Play(GameObject effect)
        {
            ParticleSystem[] particleSystems = effect.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.time = 0;
                particleSystem.Play(false); // 参数false不递归子物体
            }
        }

        public static void Stop(GameObject effect)
        {
            ParticleSystem[] particleSystems = effect.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleSystem.Clear(false);
                particleSystem.Stop(false);
            }
        }
    }
}
