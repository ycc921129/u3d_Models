/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace FutureCore
{
    public static class TimeUtil
    {
        public static float NormalTimeScale = 1;

        public static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            AppDispatcher.Instance.Dispatch(AppMsg.TimeScale_Change, timeScale);
        }

        public static float GetTimeScale()
        {
            return Time.timeScale;
        }

        public static float GetDeltaTime(bool isUnscale)
        {
            if (isUnscale)
            {
                return Time.unscaledDeltaTime;
            }
            else
            {
                return Time.deltaTime;
            }
        }

        public static Tweener SetUnscaledTime(Tweener tweener)
        {
            tweener.SetUpdate(true);
            return tweener;
        }

        public static Animator SetUnscaledTime(Animator animator)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            return animator;
        }

        public static void SetParticaleUnscaledTime(GameObject gameObject)
        {
            gameObject.AddComponent<ParticaleUnscaledTime>();
        }

        public static IEnumerator PlayAnimation(Animation animation, string clipName, Action onComplete, bool isUnscaleTime)
        {
            if (!isUnscaleTime)
            {
                AnimationState currState = animation[clipName];
                bool isPlaying = true;
                float progressTime = 0f;
                float timeAtLastFrame = 0f;
                float timeAtCurrentFrame = 0f;
                float deltaTime = 0f;
                animation.Play(clipName);
                timeAtLastFrame = Time.realtimeSinceStartup;

                while (isPlaying)
                {
                    timeAtCurrentFrame = Time.realtimeSinceStartup;
                    deltaTime = timeAtCurrentFrame - timeAtLastFrame;
                    timeAtLastFrame = timeAtCurrentFrame;

                    progressTime += deltaTime;
                    currState.normalizedTime = progressTime / currState.length;
                    animation.Sample();
                    if (progressTime >= currState.length)
                    {
                        if (currState.wrapMode != WrapMode.Loop)
                        {
                            isPlaying = false;
                        }
                        else
                        {
                            progressTime = 0.0f;
                        }
                    }
                    yield return new WaitForEndOfFrame();
                }

                yield return null;
                if (onComplete != null)
                {
                    onComplete();
                }
            }
            else
            {
                animation.Play(clipName);
            }
        }
    }
}