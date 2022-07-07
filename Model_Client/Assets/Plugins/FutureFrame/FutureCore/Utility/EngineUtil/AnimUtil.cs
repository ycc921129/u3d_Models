/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.30
*/

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions;
using UnityEngine.Playables;

namespace FutureCore
{
    public static class AnimUtil
    {
        private static int defaultLayer = 0;
        private static int defaultIdx = 0;
        private static float defaultNormalizedTime = 0;

        public static int GetAnimNameHash(string animName)
        {
            return Animator.StringToHash(animName);
        }

        public static void PlayAnim(Animator animator, int stateNameHash)
        {
            animator.Play(stateNameHash);
        }

        public static void PlayAnim(Animator animator, string animName)
        {
            animator.Play(animName, defaultLayer, defaultNormalizedTime);
        }

        public static bool HasAnimState(Animator animator, string animName)
        {
            return animator.HasState(defaultLayer, Animator.StringToHash(animName));
        }

        public static bool HasAnimClip(Animator animator, string animName)
        {
            AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
            for (int i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i].name == animName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 倒放动画
        /// </summary>
        public static void PlayReverseAnim(Animator animator, string animName)
        {
            var clip = animator.runtimeAnimatorController.animationClips[0];
            float speed = -1;
            animator.StartPlayback();
            animator.speed = speed;
            animator.Play(clip.name, 0 , speed < 0 ? 1 : 0);
        }

        /// <summary>
        /// 重置动画帧数
        /// </summary>
        public static void ResetAnimFrame(Animator animator)
        {
            animator.Update(0f);
        }

        public static bool IsPlayingAnim(Animator animator, string animName)
        {
            AnimatorClipInfo clipInfo = animator.GetCurrentAnimatorClipInfo(defaultLayer)[defaultIdx];
            return animName == clipInfo.clip.name;
        }

        public static float GetCurrAnimProgress(Animator animator)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(defaultLayer);
            return stateInfo.normalizedTime;
        }

        public static float GetAnimTimeByAnimFrame(int keyFrame, int animRate)
        {
            float aFrameRate = 1.0f / animRate;
            float animRateTime = keyFrame * aFrameRate;
            float frameNum = animRateTime * AnimConst.AnimFrameRate;
            float time = frameNum * AnimConst.SecondRateUnit;
            return time;
        }

        public static float GetClipLength(Animator animator, string clipName)
        {
            if (null == animator || string.IsNullOrEmpty(clipName) || null == animator.runtimeAnimatorController) return 0;
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            AnimationClip[] tAnimationClips = ac.animationClips;
            if (null == tAnimationClips || tAnimationClips.Length <= 0) return 0;
            for (int tCounter = 0, tLen = tAnimationClips.Length; tCounter < tLen; tCounter++)
            {
                AnimationClip tAnimationClip = ac.animationClips[tCounter];
                if (null != tAnimationClip && tAnimationClip.name == clipName)
                {
                    return tAnimationClip.length;
                }
            }
            return 0;
        }

        public static void PlayableClip(Animator animator, AnimationClip clip)
        {
            PlayableGraph playableGraph = PlayableGraph.Create();
            playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
            AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
            playableOutput.SetSourcePlayable(clipPlayable);
            playableGraph.Play();
        }

        public static void AddEvent(Animator animator, string clipName, float eventTIme, string funcName, float param)
        {
            AnimatorClipInfo[] animInfos = animator.GetCurrentAnimatorClipInfo(0);
            if (animInfos.Length > 0)
            {
                for (int i = 0; i < animInfos.Length; i++)
                {
                    AnimatorClipInfo animInfo = animInfos[i];
                    if (animInfo.clip.name == clipName)
                    {
                        AnimationEvent animEvent = new AnimationEvent();
                        animEvent.functionName = funcName;
                        animEvent.floatParameter = param;
                        animEvent.time = eventTIme;
                        animInfo.clip.AddEvent(animEvent);
                        return;
                    }
                }
            }
        }

        public static void BindPointRoot(Animator animator)
        {
            string[] bindPoints = new string[]
            {
                "root_head",
                "root_arm",
                "root_leg",
            };

            Assert.IsNotNull(animator);
            foreach (string bindPoint in bindPoints)
            {
                GameObject bindPointObj = new GameObject(bindPoint);
                bindPointObj.layer = animator.gameObject.layer;
                bindPointObj.transform.SetParent(animator.transform);
            }
            animator.Rebind();
        }

        public static void ReplaceAnimatorController(Animator templateCom, Animator newCom, AnimationClip[] clips)
        {
            AnimatorOverrideController overrideAnimController = new AnimatorOverrideController();
            overrideAnimController.runtimeAnimatorController = templateCom.runtimeAnimatorController;
            foreach (AnimationClip clip in clips)
            {
                overrideAnimController[clip.name] = clip;
            }
            newCom.runtimeAnimatorController = overrideAnimController;
        }

        public static AnimatorOverrideController CreateAnimatorController(RuntimeAnimatorController templateCtr, AnimationClip[] clips)
        {
            AnimatorOverrideController overrideAnimController = new AnimatorOverrideController();
            overrideAnimController.runtimeAnimatorController = templateCtr;
            foreach (AnimationClip clip in clips)
            {
                overrideAnimController[clip.name] = clip;
            }
            return overrideAnimController;
        }

        public static float AnimationCurveEvaluate(AnimationCurve curve, float time)
        {
            return curve.Evaluate(time);
        }
    }
}