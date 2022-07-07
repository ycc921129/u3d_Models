using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class CharacterAnimationOptimize_AssetImportTool : AssetPostprocessor
    {
        private void OnPreprocessModel()
        {
            if (!assetPath.Contains("_Character")) return;

            ModelImporter importer = assetImporter as ModelImporter;
            importer.optimizeGameObjects = true;
            //若实际骨骼中没有以下节点，可能会报错，但不影响使用
            string[] rootNames = new string[]
            {
            "root_head",
            "root_arm",
            "root_leg",
            };
            importer.extraExposedTransformPaths = rootNames;
        }

        private void OnPostprocessModel(GameObject go)
        {
            if (!assetPath.Contains("_Character")) return;

            List<AnimationClip> animationClipList = new List<AnimationClip>(AnimationUtility.GetAnimationClips(go));
            if (animationClipList.Count == 0)
            {
                AnimationClip[] objectList = Object.FindObjectsOfType(typeof(AnimationClip)) as AnimationClip[];
                animationClipList.AddRange(objectList);
            }

            foreach (AnimationClip theAnimation in animationClipList)
            {
                try
                {
                    //去除scale曲线
                    foreach (EditorCurveBinding theCurveBinding in AnimationUtility.GetCurveBindings(theAnimation))
                    {
                        string name = theCurveBinding.propertyName.ToLower();
                        if (name.Contains("scale"))
                        {
                            AnimationUtility.SetEditorCurve(theAnimation, theCurveBinding, null);
                        }
                    }

                    //浮点数精度压缩到f3
                    AnimationClipCurveData[] curves = null;
                    curves = AnimationUtility.GetAllCurves(theAnimation);
                    Keyframe key;
                    Keyframe[] keyFrames;
                    for (int ii = 0; ii < curves.Length; ++ii)
                    {
                        AnimationClipCurveData curveDate = curves[ii];
                        if (curveDate.curve == null || curveDate.curve.keys == null)
                        {
                            continue;
                        }
                        keyFrames = curveDate.curve.keys;
                        for (int i = 0; i < keyFrames.Length; i++)
                        {
                            key = keyFrames[i];
                            key.value = float.Parse(key.value.ToString("f3"));
                            key.inTangent = float.Parse(key.inTangent.ToString("f3"));
                            key.outTangent = float.Parse(key.outTangent.ToString("f3"));
                            keyFrames[i] = key;
                        }
                        curveDate.curve.keys = keyFrames;
                        theAnimation.SetCurve(curveDate.path, curveDate.type, curveDate.propertyName, curveDate.curve);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(string.Format("CompressAnimationClip Failed !!! animationPath : {0} error: {1}", assetPath, e));
                }
            }
        }
    }
}