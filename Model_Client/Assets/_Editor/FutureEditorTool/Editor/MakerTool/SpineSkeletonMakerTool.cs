using System.IO;
using Spine;
using Spine.Unity;
using Spine.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class SpineSkeletonMakerTool
    {
        private static string SkeletonPath = "Assets/_Res/Art/Skeleton/";
        private static string DefaultAnimName = "Idle";

        [MenuItem("[FC Project]/Res/Maker/Skeleton (Spine)/生成所有骨骼动画预设 (Spine)", false, 5)]
        private static void BuildAllSkeletonMenu()
        {
            DirectoryInfo raw = new DirectoryInfo(SkeletonPath);
            foreach (DirectoryInfo dictorys in raw.GetDirectories())
            {
                string path = SkeletonPath + dictorys.Name;
                BuildSkeleton(path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetsSyncTool.SyncSkeleton();
            Debug.Log("[SpineSkeletonMakerTool]同步生成所有骨骼动画预设完成");
        }

        [MenuItem("[FC Project]/Res/Maker/Skeleton (Spine)/生成选中骨骼动画预设 (Spine)", false, 6)]
        private static void BuildSelectSkeletonMenu()
        {
            Object[] pathsArr = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
            foreach (Object obj in pathsArr)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path))
                {
                    Debug.LogError("[SpineSkeletonMakerTool]路径错误");
                    return;
                }
                BuildSkeleton(path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[SpineSkeletonMakerTool]生成选中骨骼动画预设完成");
        }

        private static void BuildSkeleton(string path)
        {
            DirectoryInfo dictory = new DirectoryInfo(path);
            string dicName = path.Substring(path.LastIndexOf("/") + 1);

            FileInfo[] skeletonData = dictory.GetFiles("*SkeletonData.asset");
            if (skeletonData.Length == 0)
            {
                Debug.LogError(dicName + "," + dictory.Name + "找不到SkeletonData");
                return;
            }

            GameObject skeletonGo = new GameObject(dicName);
            string assetPath = dictory + "/" + skeletonData[0].Name;
            SkeletonDataAsset sda = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object)) as SkeletonDataAsset;
            SkeletonAnimation skeletonAnimation = EditorInstantiation.InstantiateSkeletonAnimation(sda); //旧API: SpineEditorUtilities.EditorInstantiation.InstantiateSkeletonAnimation(sda);
            Reload(skeletonAnimation);
            SetSkeletonMaterial(skeletonAnimation);
            skeletonAnimation.AnimationName = DefaultAnimName;
            skeletonAnimation.loop = true;
            skeletonAnimation.gameObject.name = dictory.Name + "Anim";
            skeletonAnimation.gameObject.transform.SetParent(skeletonGo.transform);

            //CheckSlot(skeletonAnimation);
            //BindSlot(skeletonAnimation);
            //SetShader(skeletonAnimation);
            string prefabPath = path + "/" + dicName + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(skeletonGo, prefabPath);
            Object.DestroyImmediate(skeletonGo);
        }

        private static void SetSkeletonMaterial(SkeletonAnimation skeletonAnimation)
        {
            Material[] mats = skeletonAnimation.gameObject.GetComponent<MeshRenderer>().sharedMaterials;
            foreach (Material matItem in mats)
            {
                //Material mat = (Material)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(matItem), typeof(Material));
                matItem.SetInt("_StraightAlphaInput", 1);
                EditorUtility.SetDirty(matItem);
            }
        }

        private static void Reload(SkeletonAnimation skeletonAnimation)
        {
            SkeletonRenderer component = skeletonAnimation as SkeletonRenderer;
            if (component.skeletonDataAsset != null)
            {
                foreach (AtlasAssetBase aa in component.skeletonDataAsset.atlasAssets)
                {
                    if (aa != null) aa.Clear();
                }
                component.skeletonDataAsset.Clear();
            }
            component.Initialize(true);
        }

        private static void CheckSlot(SkeletonAnimation skeletonAnimation)
        {
            Slot hitPointSlot = skeletonAnimation.Skeleton.FindSlot(EP_AnimSlotConst.HitPoint);
            Slot atkPointSlot = skeletonAnimation.Skeleton.FindSlot(EP_AnimSlotConst.AtkPoint);
            Debug.AssertFormat(hitPointSlot != null, "{0}:没有找到{1}插槽", skeletonAnimation.gameObject.name, EP_AnimSlotConst.HitPoint);
            Debug.AssertFormat(atkPointSlot != null, "{0}:没有找到{1}插槽", skeletonAnimation.gameObject.name, EP_AnimSlotConst.AtkPoint);

            Spine.Animation idleAnim = skeletonAnimation.skeleton.Data.FindAnimation(EP_EntityAnimType.Idle.ToString());
            Spine.Animation moveAnim = skeletonAnimation.skeleton.Data.FindAnimation(EP_EntityAnimType.Move.ToString());
            Spine.Animation atkAnim = skeletonAnimation.skeleton.Data.FindAnimation(EP_EntityAnimType.Atk.ToString());
            Spine.Animation skillAnim = skeletonAnimation.skeleton.Data.FindAnimation(EP_EntityAnimType.Skill.ToString());
            Spine.Animation dizzyAnim = skeletonAnimation.skeleton.Data.FindAnimation(EP_EntityAnimType.Dizzy.ToString());
            Spine.Animation riseAnim = skeletonAnimation.skeleton.Data.FindAnimation(EP_EntityAnimType.Rise.ToString());

            Debug.AssertFormat(idleAnim != null, "{0}:缺失{1}动画", skeletonAnimation.gameObject.name, EP_EntityAnimType.Idle);
            Debug.AssertFormat(moveAnim != null, "{0}:缺失{1}动画", skeletonAnimation.gameObject.name, EP_EntityAnimType.Move);
            Debug.AssertFormat(atkAnim != null, "{0}:缺失{1}动画", skeletonAnimation.gameObject.name, EP_EntityAnimType.Atk);
            Debug.AssertFormat(skillAnim != null, "{0}:缺失{1}动画", skeletonAnimation.gameObject.name, EP_EntityAnimType.Skill);
            Debug.AssertFormat(dizzyAnim != null, "{0}:缺失{1}动画", skeletonAnimation.gameObject.name, EP_EntityAnimType.Dizzy);
            Debug.AssertFormat(riseAnim != null, "{0}:缺失{1}动画", skeletonAnimation.gameObject.name, EP_EntityAnimType.Rise);
        }

        private static void BindSlot(SkeletonAnimation skeletonAnimation)
        {
            GameObject bones = new GameObject("bones");
            bones.transform.SetParent(skeletonAnimation.transform, false);
            SetSlot(skeletonAnimation, EP_AnimSlotConst.HitPoint, bones);
            SetSlot(skeletonAnimation, EP_AnimSlotConst.AtkPoint, bones);
        }

        private static void SetSlot(SkeletonAnimation skeletonAnimation, string slotName, GameObject root)
        {
            GameObject slot = new GameObject(slotName);
            PointFollower follower = slot.AddComponent<PointFollower>();
            follower.skeletonRenderer = skeletonAnimation;
            follower.slotName = slotName;
            follower.pointAttachmentName = slotName;
            follower.followRotation = false;
            follower.followSkeletonFlip = true;
            follower.followSkeletonZPosition = false;
            slot.transform.SetParent(root.transform, false);
        }

        private static void SetShader(SkeletonAnimation skeletonAnimation)
        {
            Renderer render = skeletonAnimation.GetComponent<Renderer>();
            foreach (Material m in render.sharedMaterials)
            {
                m.shader = Shader.Find("Spine/Skeleton Fill");
            }
        }

        private static void SetLayer(GameObject go)
        {
            Transform[] ts = go.GetComponentsInChildren<Transform>(false);
            foreach (Transform item in ts)
            {
                item.gameObject.layer = LayerMask.NameToLayer("Skeleton");
            }
        }
    }
}