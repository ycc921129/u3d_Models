/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using Spine;
using Spine.Unity;
using UnityEngine;

namespace FutureCore
{
    public static class SpineHelper
    {
        private const string DefaultAnimName = "Idle";
        private const int CurrTrack = 0;

        public static void PlayDefaultAnim(GameObject gameObject)
        {
            SkeletonAnimation animCom = gameObject.GetComponent<SkeletonAnimation>();
            if (animCom != null)
            {
                PlayDefaultAnim(animCom);
            }
        }

        public static void SetAllSortingOrder(GameObject gameObject, int sortingOrder)
        {
            Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders)
            {
                if (render != null)
                {
                    render.sortingOrder = sortingOrder;
                }
            }
        }

        public static void SetSortingOrder(GameObject gameObject, int sortingOrder)
        {
            Renderer render = gameObject.GetComponent<Renderer>();
            if (render != null)
            {
                render.sortingOrder = sortingOrder;
            }
        }

        public static SkeletonAnimation GetAnimComByParent(GameObject gameObject)
        {
            return gameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        }

        public static SkeletonAnimation GetAnimComByParent(Transform transform)
        {
            return transform.GetChild(0).GetComponent<SkeletonAnimation>();
        }

        public static SkeletonAnimation GetAnimCom(GameObject gameObject)
        {
            return gameObject.GetComponent<SkeletonAnimation>();
        }

        public static SkeletonAnimation GetAnimCom(Transform transform)
        {
            return transform.gameObject.GetComponent<SkeletonAnimation>();
        }

        public static SkeletonGraphic GetAnimUICom(GameObject gameObject)
        {
            return gameObject.GetComponent<SkeletonGraphic>();
        }

        public static Spine.Animation FindAnim(SkeletonAnimation animCom, string animName)
        {
            return animCom.skeleton.Data.FindAnimation(animName);
        }

        public static TrackEntry PlayDefaultAnim(SkeletonAnimation animCom)
        {
            return LoopPlayAnim(animCom, DefaultAnimName);
        }

        public static TrackEntry PlayAnim(SkeletonAnimation animCom, string animName, bool isLoop)
        {
            return animCom.AnimationState.SetAnimation(CurrTrack, animName, isLoop);
        }

        public static TrackEntry PlayAnim(SkeletonAnimation animCom, string animName)
        {
            return animCom.AnimationState.SetAnimation(CurrTrack, animName, false);
        }

        public static TrackEntry PlayAnim(SkeletonGraphic animCom, string animName)
        {
            return animCom.AnimationState.SetAnimation(CurrTrack, animName, false);
        }

        public static TrackEntry LoopPlayAnim(SkeletonAnimation animCom, string animName)
        {
            return animCom.AnimationState.SetAnimation(CurrTrack, animName, true);
        }

        public static TrackEntry AddPlayAnim(SkeletonAnimation animCom, string animName, bool isLoop, float delay = 0f)
        {
            return animCom.AnimationState.AddAnimation(CurrTrack, animName, isLoop, delay);
        }

        public static TrackEntry AddPlayAnim(SkeletonAnimation animCom, string animName)
        {
            return animCom.AnimationState.AddAnimation(CurrTrack, animName, false, 0f);
        }

        public static TrackEntry AddLoopPlayAnim(SkeletonAnimation animCom, string animName)
        {
            return animCom.AnimationState.AddAnimation(CurrTrack, animName, true, 0f);
        }

        public static void PauseAnim(SkeletonAnimation animCom)
        {
            SetTimeScale(animCom, 0);
        }

        public static void ResumePlayAnim(SkeletonAnimation animCom, int times)
        {
            SetTimeScale(animCom, 1);
        }

        public static void ClearAnimPose(SkeletonAnimation animCom)
        {
            SetToSetupPose(animCom);
            ClearTracks(animCom);
        }

        public static void SetToSetupPose(SkeletonAnimation animCom)
        {
            animCom.Skeleton.SetToSetupPose();
        }

        public static void ClearTracks(SkeletonAnimation animCom)
        {
            animCom.AnimationState.ClearTracks();
        }

        public static void ClearTrack(SkeletonAnimation animCom)
        {
            animCom.AnimationState.ClearTrack(0);
        }

        public static void ClearPlayAnim(SkeletonAnimation animCom, string animName)
        {
            ClearTrack(animCom);
            TrackEntry entry = animCom.AnimationState.SetAnimation(CurrTrack, animName, false);
            entry.TrackTime = 0f;
        }

        // 3.8版本删除了此接口
        //public static void PoseWithAnimation(SkeletonAnimation animCom, string animName, float time, bool loop = false)
        //{
        //    animCom.Skeleton.PoseWithAnimation(animName, time, loop);
        //}

        public static float GetAnimTimeScale(SkeletonAnimation animCom)
        {
            return animCom.AnimationState.TimeScale;
        }

        public static void SetTimeScale(SkeletonAnimation animCom, float timeScale)
        {
            animCom.AnimationState.TimeScale = timeScale;
        }

        public static TrackEntry GetCurrAnimTrack(SkeletonAnimation animCom)
        {
            return animCom.AnimationState.GetCurrent(0);
        }

        public static string GetCurrAnimName(SkeletonAnimation animCom)
        {
            return animCom.AnimationName;
        }

        public static bool IsPlayingAnim(SkeletonAnimation animCom, string animName)
        {
            return GetCurrAnimName(animCom) == animName;
        }

        public static bool GetFlipX(SkeletonAnimation animCom)
        {
            return animCom.Skeleton.ScaleX == -1f;
        }

        public static void SetFlipX(SkeletonAnimation animCom, bool isFlipX)
        {
            animCom.Skeleton.ScaleX = isFlipX ? -1f : 1f;
        }

        public static Renderer GetRenderer(SkeletonAnimation animCom)
        {
            return animCom.GetComponent<Renderer>();
        }

        public static void EnabledRenderer(SkeletonAnimation animCom, bool enabled)
        {
            GetRenderer(animCom).enabled = enabled;
        }

        public static int GetSortingOrder(SkeletonAnimation animCom)
        {
            return GetRenderer(animCom).sortingOrder;
        }

        public static void SetSortingOrder(SkeletonAnimation animCom, int sortingOrder)
        {
            GetRenderer(animCom).sortingOrder = sortingOrder;
        }

        public static void StopCurrAnimJumpToFrame(SkeletonAnimation animCom, float time)
        {
            JumpToTime(animCom, 0, time, true);
        }

        public static void StopCurrAnimJumpToFrame(SkeletonAnimation animCom, int frame)
        {
            JumpToTime(animCom, 0, GetTimeByFrame(frame), true);
        }

        public static TrackEntry JumpToTime(SkeletonAnimation animCom, int trackNumber, float time, bool stop)
        {
            return JumpToTime(animCom.state.GetCurrent(trackNumber), time, stop);
        }

        public static TrackEntry JumpToTime(TrackEntry trackEntry, float time, bool stop)
        {
            if (trackEntry != null)
            {
                trackEntry.TrackTime = time;
                if (stop) trackEntry.TimeScale = 0;
            }
            return trackEntry;
        }

        public static void AddStartEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryDelegate func)
        {
            animCom.AnimationState.Start += func;
        }

        public static void AddEndEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryDelegate func)
        {
            animCom.AnimationState.End += func;
        }

        public static void AddCompleteEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryDelegate func)
        {
            animCom.AnimationState.Complete += func;
        }

        public static void AddCustomEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryEventDelegate func)
        {
            animCom.AnimationState.Event += func;
        }

        public static void RemoveStartEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryDelegate func)
        {
            animCom.AnimationState.Start -= func;
        }

        public static void RemoveEndEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryDelegate func)
        {
            animCom.AnimationState.End -= func;
        }

        public static void RemoveCompleteEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryDelegate func)
        {
            animCom.AnimationState.Complete -= func;
        }

        public static void RemoveCustomEvent(SkeletonAnimation animCom, Spine.AnimationState.TrackEntryEventDelegate func)
        {
            animCom.AnimationState.Event -= func;
        }

        public static float GetTimeByFrame(int keyFrameNum)
        {
            float time = keyFrameNum * AnimConst.SecondRateUnit;
            return time;
        }

        #region 颜色
        public static void SetColorAlpha(SkeletonAnimation animCom, float alpha)
        {
            SetColor(animCom, new Color(1, 1, 1, alpha));
        }

        public static void SetRawColorAlpha(SkeletonAnimation animCom, float alpha)
        {
            SetColor(animCom, new Color(animCom.Skeleton.R, animCom.Skeleton.G, animCom.Skeleton.B, alpha));
        }

        public static void SetColor(SkeletonAnimation animCom, Color color)
        {
            animCom.Skeleton.SetColor(color);
        }
        #endregion

        #region 挂载点
        public static BoneData FindBone(SkeletonAnimation animCom, string boneName)
        {
            return animCom.skeleton.Data.FindBone(boneName);
        }

        public static Transform SkeletonSpawnHierarchy(SkeletonAnimation animCom)
        {
            SkeletonUtility skeletonUtility = AddSkeletonUtility(animCom);
            return skeletonUtility.SpawnHierarchy(SkeletonUtilityBone.Mode.Follow, true, true, true).transform;
        }

        public static SkeletonUtility AddSkeletonUtility(SkeletonAnimation animCom)
        {
            return animCom.gameObject.AddComponent<SkeletonUtility>();
        }

        public static SkeletonUtilityBone AddSkeletonUtilityBone(SkeletonAnimation animCom)
        {
            return animCom.gameObject.AddComponent<SkeletonUtilityBone>();
        }

        public static SkeletonUtilityConstraint AddSkeletonUtilityConstraint(SkeletonAnimation animCom)
        {
            return animCom.gameObject.AddComponent<SkeletonUtilityConstraint>();
        }
        #endregion

        #region 换装
        public static Skin FindSkin(SkeletonAnimation animCom, string skinName)
        {
            return animCom.skeleton.Data.FindSkin(skinName);
        }

        public static void SetSkin(SkeletonAnimation animCom, string skinName)
        {
            animCom.Skeleton.SetSkin(skinName);
        }

        public static void SetAttachment(SkeletonAnimation animCom, string slotName, string attachmentName)
        {
            animCom.Skeleton.SetAttachment(slotName, attachmentName);
        }

        public static void SwitchSkin(SkeletonAnimation animCom, string skinName)
        {
            animCom.Skeleton.SetSkin(skinName);
            animCom.Skeleton.SetSlotsToSetupPose();
        }
        #endregion

        #region 骨骼碰撞盒
        /// <summary>
        /// 骨骼碰撞盒跟随
        /// </summary>
        public static void BoneColliderFollower(SkeletonAnimation animCom, string boneName, string slotName)
        {
            GameObject boxBone = new GameObject("boxBone");
            boxBone.transform.SetParent(animCom.transform, false);
            boxBone.AddComponent<Rigidbody2D>();
            boxBone.AddComponent<PolygonCollider2D>();
            BoneFollower boneFollower = boxBone.AddComponent<BoneFollower>();
            BoundingBoxFollower boundingBoxFollower = boxBone.AddComponent<BoundingBoxFollower>();
            boneFollower.boneName = boneName;
            boneFollower.followScaleMode = BoneFollower.ScaleMode.WorldUniform;
            boundingBoxFollower.slotName = slotName;
        }
        #endregion
    }
}