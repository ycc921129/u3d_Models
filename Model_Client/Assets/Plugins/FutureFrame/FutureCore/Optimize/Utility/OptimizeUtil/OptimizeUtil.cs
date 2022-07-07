/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.Rendering;

namespace FutureCore
{
    public static class OptimizeUtil
    {
        public static void SetEnabled(Behaviour behaviour, bool value)
        {
            if (behaviour.enabled != value)
            {
                behaviour.enabled = value;
            }
        }

        public static void SetActive(GameObject gameObject, bool value)
        {
            if (gameObject.activeSelf != value)
            {
                gameObject.SetActive(value);
            }
        }

        public static Transform InstantiateSetPositionAndRotation(Transform transform, Vector3 position, Quaternion rotation)
        {
            return Object.Instantiate(transform, position, rotation);
        }

        public static void SetPositionAndRotation(Transform transform, Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }

        public static bool CompareTag(GameObject gameObject, string tag)
        {
            return gameObject.CompareTag(tag);
        }

        public static int GetObjectID(Object obj)
        {
            return obj.GetInstanceID();
        }

        public static int GetObjectHash(Object obj)
        {
            return obj.GetHashCode();
        }

        public static bool IsObjectEquslas(Object obj1, Object obj2)
        {
            return ReferenceEquals(obj1, obj2);
        }

        public static int GetShaderPropertyId(string name)
        {
            return Shader.PropertyToID(name);
        }

        public static int GetAnimNameHash(string animName)
        {
            return Animator.StringToHash(animName);
        }

        public static void PlayAnim(Animator animator, int stateNameHash)
        {
            animator.Play(stateNameHash);
        }

        /// <summary>
        /// 网格优化：
        /// Mesh Compression选项：设置为High
        /// OptimizeMesh选项：对模型开启后，Unity会对其进行网格优化
        /// 没有动画的模型动画导入设置要设置为None
        /// 有动画的要勾选Optimize Game Objects
        /// </summary>
        public static void OptimizeMesh(MeshRenderer meshRenderer)
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;

            meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

            meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            meshRenderer.motionVectors = false;

            meshRenderer.allowOcclusionWhenDynamic = true;
        }

        public static void StaticBatchingCombine(GameObject[] gos, GameObject staticBatchRoot)
        {
            StaticBatchingUtility.Combine(gos, staticBatchRoot);
        }

        public static SortingGroup AddSortingGroup(GameObject go)
        {
            return go.AddComponent<SortingGroup>();
        }
    }
}