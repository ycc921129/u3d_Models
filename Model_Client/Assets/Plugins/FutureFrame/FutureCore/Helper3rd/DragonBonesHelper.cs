/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using DragonBones;
using UnityEngine;

namespace FutureCore
{
    public static class DragonBonesHelper
    {
        private const string DefaultAnimName = "Idle";
        private const float FadeInTime = 0.1f;
        private const int PlayOnceTimes = 1;
        private const int LoopTimes = 0;

        public static void PlayDefaultAnim(GameObject gameObject)
        {
            UnityArmatureComponent armatureCom = gameObject.GetComponent<UnityArmatureComponent>();
            if (armatureCom != null)
            {
                PlayDefaultAnim(armatureCom);
            }
        }

        public static void SetSortingOrder(GameObject gameObject, int sortingOrder)
        {
            UnityArmatureComponent armatureCom = gameObject.GetComponent<UnityArmatureComponent>();
            if (armatureCom != null)
            {
                SetSortingOrder(armatureCom, sortingOrder);
            }
        }

        public static void DeleteAnim(GameObject gameObject)
        {
            UnityArmatureComponent armatureCom = gameObject.GetComponent<UnityArmatureComponent>();
            if (armatureCom != null)
            {
                DeleteAnim(armatureCom);
            }
        }

        public static UnityArmatureComponent GetAnimComByParent(GameObject gameObject)
        {
            return gameObject.transform.GetChild(0).GetComponent<UnityArmatureComponent>();
        }

        public static UnityArmatureComponent GetAnimComByParent(UnityEngine.Transform transform)
        {
            return transform.GetChild(0).GetComponent<UnityArmatureComponent>();
        }

        public static UnityArmatureComponent GetAnimCom(GameObject gameObject)
        {
            return gameObject.GetComponent<UnityArmatureComponent>();
        }

        public static UnityArmatureComponent GetAnimCom(UnityEngine.Transform transform)
        {
            return transform.gameObject.GetComponent<UnityArmatureComponent>();
        }

        public static void PlayDefaultAnim(UnityArmatureComponent armatureCom)
        {
            LoopPlayAnim(armatureCom, DefaultAnimName);
        }

        public static void PlayAnim(UnityArmatureComponent armatureCom, string animName)
        {
            PlayAnim(armatureCom, animName, PlayOnceTimes);
        }

        public static void LoopPlayAnim(UnityArmatureComponent armatureCom, string animName)
        {
            PlayAnim(armatureCom, animName, LoopTimes);
        }

        public static void PlayAnim(UnityArmatureComponent armatureCom, string animName, int times)
        {
            armatureCom.animation.Play(animName, times);
        }

        public static void FadeInPlayAnim(UnityArmatureComponent armatureCom, string animName)
        {
            FadeInPlayAnim(armatureCom, animName, PlayOnceTimes);
        }

        public static void LoopFadeInPlayAnim(UnityArmatureComponent armatureCom, string animName)
        {
            FadeInPlayAnim(armatureCom, animName, LoopTimes);
        }

        public static void FadeInPlayAnim(UnityArmatureComponent armatureCom, string animName, int times)
        {
            armatureCom.animation.FadeIn(animName, FadeInTime, times);
        }

        public static void StopAnim(UnityArmatureComponent armatureCom)
        {
            armatureCom.animation.Stop();
        }

        public static void ContinuePlayAnim(UnityArmatureComponent armatureCom, int times)
        {
            armatureCom.animation.Play(null, times);
        }

        public static DragonBones.AnimationState GetAnimState(UnityArmatureComponent armatureCom, string animName)
        {
            return armatureCom.animation.GetState(animName);
        }

        public static bool IsPlayingAnim(UnityArmatureComponent armatureCom, string animName)
        {
            return GetAnimState(armatureCom, animName).isPlaying;
        }

        public static bool IsCompletedPlayingAnim(UnityArmatureComponent armatureCom, string animName)
        {
            return GetAnimState(armatureCom, animName).isCompleted;
        }

        public static float GetAnimTimeScale(UnityArmatureComponent armatureCom)
        {
            return armatureCom.animation.timeScale;
        }

        public static void SetAnimTimeScale(UnityArmatureComponent armatureCom, float timeScale)
        {
            armatureCom.animation.timeScale = timeScale;
        }

        public static bool GetFlipX(UnityArmatureComponent armatureCom)
        {
            return armatureCom.armature.flipX;
        }

        public static void SetFlipX(UnityArmatureComponent armatureCom, bool isFlipX)
        {
            armatureCom.armature.flipX = isFlipX;
        }

        public static int GetSortingOrder(UnityArmatureComponent armatureCom)
        {
            return armatureCom.sortingOrder;
        }

        public static void SetSortingOrder(UnityArmatureComponent armatureCom, int sortingOrder)
        {
            armatureCom.sortingOrder = sortingOrder;
        }

        public static void DeleteAnim(UnityArmatureComponent armatureCom)
        {
            armatureCom.Dispose();
        }

        public static float GetTimeByFrame(int keyFrameNum)
        {
            float time = keyFrameNum * AnimConst.SecondRateUnit;
            return time;
        }

        public static void AddStartEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.AddEventListener(EventObject.START, listener);
        }

        public static void AddLoopCompleteEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.AddEventListener(EventObject.LOOP_COMPLETE, listener);
        }

        public static void AddCompleteEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.AddEventListener(EventObject.COMPLETE, listener);
        }

        public static void AddFrameEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.AddEventListener(EventObject.FRAME_EVENT, listener);
        }

        public static void AddSoundEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.AddEventListener(EventObject.SOUND_EVENT, listener);
        }

        public static void RemoveStartEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.RemoveEventListener(EventObject.START, listener);
        }

        public static void RemoveLoopCompleteEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.RemoveEventListener(EventObject.LOOP_COMPLETE, listener);
        }

        public static void RemoveCompleteEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.RemoveEventListener(EventObject.COMPLETE, listener);
        }

        public static void RemoveFrameEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.RemoveEventListener(EventObject.FRAME_EVENT, listener);
        }

        public static void RemoveSoundEvent(UnityArmatureComponent armatureCom, ListenerDelegate<EventObject> listener)
        {
            armatureCom.RemoveEventListener(EventObject.SOUND_EVENT, listener);
        }

        #region 换装
        public static Slot GetSlot(UnityArmatureComponent armatureCom, string slotName)
        {
            return armatureCom.armature.GetSlot(slotName);
        }

        public static GameObject GetSlotGameObject(UnityArmatureComponent armatureCom, string slotName)
        {
            Slot slot = GetSlot(armatureCom, slotName);
            if (slot != null)
            {
                return slot.display as GameObject;
            }
            return null;
        }

        public static Bone GetBone(UnityArmatureComponent armatureCom, string boneName)
        {
            return armatureCom.armature.GetBone(boneName);
        }

        public static void ReplaceSelfSlotDisplay(UnityArmatureComponent armatureCom, string slotName, string displayName, int displayIndex = -1)
        {
            Slot slot = GetSlot(armatureCom, slotName);
            if (slot != null)
            {
                UnityFactory.factory.ReplaceSlotDisplay(null, armatureCom.armature.name, slotName, displayName, slot, displayIndex);
            }
        }
        #endregion
    }
}