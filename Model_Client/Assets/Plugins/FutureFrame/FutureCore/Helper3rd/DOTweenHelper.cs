/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace FutureCore
{
    public static class DOTweenHelper
    {
        public static void Init()
        {
            //IDOTweenInit doTweenInit = DOTween.Init(recycleAllByDefault: true, useSafeMode: true, logBehaviour: LogBehaviour.Default);
            IDOTweenInit doTweenInit = DOTween.Init();
            if (doTweenInit != null)
            {
                doTweenInit.SetCapacity(1024, 1024);
            }
            DOTween.SetTweensCapacity(1024, 1024);
            // 缓动曲线类型
            DOTween.defaultEaseType = Ease.Linear;
            // 过冲或振幅
            DOTween.defaultEaseOvershootOrAmplitude = 1.70158f;
            // 周期
            DOTween.defaultEasePeriod = 0f;
        }

        public static void SetRecyclable()
        {
            DOTween.defaultRecyclable = true;
        }

        public static void SetRecyclable(Tween tween)
        {
            tween.SetRecyclable(true);
        }

        public static Vector3 Move(float time, float startTime, float duration, Vector3 formPos, Vector3 toPos)
        {
            float passedTime = time - startTime;
            float v = EaseManager.Evaluate(Ease.Linear, null, passedTime, duration, 0, 0);
            Vector3 tweenPos = Vector3.Lerp(formPos, toPos, v);
            return tweenPos;
        }

        public static Sequence CreateSequence()
        {
            Sequence sequence = DOTween.Sequence();
            return sequence;
        }

        public static Sequence Append(Sequence sequence, Tween tween)
        {
            return sequence.Append(tween);
        }

        public static Sequence Play(Sequence sequence)
        {
            return sequence.Play();
        }
    }
}