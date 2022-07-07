/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public class ParticaleUnscaledTime : MonoBehaviour
    {
        private float lastTime;
        private ParticleSystem particle;

        private void Start()
        {
            particle = GetComponent<ParticleSystem>();
            lastTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            float realtimeSinceStartup = Time.realtimeSinceStartup;
            float deltaTime = realtimeSinceStartup - lastTime;

            particle.Simulate(deltaTime, true, false);
            lastTime = realtimeSinceStartup;
        }
    }
}