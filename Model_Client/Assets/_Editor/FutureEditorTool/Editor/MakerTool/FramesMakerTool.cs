/*
 资源目录结构
 [10000]
   -[Atk]
    -[0]
     -[0.png]
     -[1.png]
    -[1]
     -[1.png]
     -[2.png]
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace FutureEditor
{
    public static class FramesMakerTool
    {
        // 帧图片路径
        private static string frameImagePath = EditorPathConst.ResArtPath + "Frames";
        // 默认图片类型
        private static int imagesTypeInt = 0;
        private static string[] imagesTypeString = new string[] { "png", "jpg" };

        // 帧率
        private static float frameRate = EP_AnimRateConst.AnimFrameRate;
        // 要循环的动作名字
        private static string[] loopAnimNames = new string[] { EP_EntityAnimType.Loop.ToString(), EP_EntityAnimType.Idle.ToString(), EP_EntityAnimType.Move.ToString(), EP_EntityAnimType.Dizzy.ToString() };
        private static string defaultCurrAnimName = "Idle_0";

        /// <summary>
        /// 批量生成序列帧预设
        /// </summary>
        [MenuItem("[FC Project]/Res/Maker/Frames/生成所有序列帧预设", false, 0)]
        private static void BatchBuildFramesAnimMenu()
        {
            BuildAniamObjs();
            AssetsSyncTool.SyncAnimFrames();

            AssetDatabase.Refresh();
            Resources.UnloadUnusedAssets();
            GC.Collect();
            Debug.Log("[FramesMakerTool]批量生成序列帧预设完成");
        }

        private static void BuildAniamObjs()
        {
            UObject[] pathsArr = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
            // 生成默认路径下的所有对象
            if (pathsArr.Length == 0)
            {
                DirectoryInfo raw = new DirectoryInfo(frameImagePath);
                foreach (DirectoryInfo dictory in raw.GetDirectories())
                {
                    BuildCompleteAnimObj(dictory);
                }
            }
            else
            {
                foreach (DefaultAsset path in pathsArr)
                {
                    if (!Directory.Exists(frameImagePath + "/" + path.name))
                    {
                        Debug.LogError("选中的文件夹" + path.name + " , 不在" + frameImagePath + "帧动画文件夹中");
                        continue;
                    }
                    DirectoryInfo dictory = new DirectoryInfo(frameImagePath + "/" + path.name);
                    BuildCompleteAnimObj(dictory);
                }
            }
        }

        private static void BuildCompleteAnimObj(DirectoryInfo dictory)
        {
            string objImagePath = frameImagePath + "/" + dictory.Name;
            string objName = dictory.Name;

            string animationPath = GetAnimationPath(frameImagePath, objName);
            if (Directory.Exists(animationPath))
            {
                Directory.Delete(animationPath, true);
            }

            FileInfo defaultShowImage = null;
            DirectoryInfo fristIamgeDir = null;
            // AnimationClip
            List<AnimationClip> clips = new List<AnimationClip>();
            DirectoryInfo objImageDir = new DirectoryInfo(objImagePath);
            foreach (DirectoryInfo animDic in objImageDir.GetDirectories())
            {
                if (animDic.Name == "Animation")
                {
                    continue;
                }
                foreach (DirectoryInfo animIndexDic in animDic.GetDirectories())
                {
                    if (fristIamgeDir == null)
                    {
                        fristIamgeDir = animIndexDic;
                    }
                    clips.Add(BuildAnimationClip(animIndexDic, objName, ref defaultShowImage));
                }
            }
            // AnimationController
            AnimatorController controller = BuildanimatorController(clips, objName);
            // Prefab
            BuildPrefab(objName, controller, defaultShowImage, fristIamgeDir);
        }

        private static AnimationClip BuildAnimationClip(DirectoryInfo animIndexDic, string objName, ref FileInfo defaultShowImage)
        {
            string animName = Directory.GetParent(animIndexDic.FullName).Name;
            string animationClipName = string.Format("{0}_{1}", animName, animIndexDic.Name);

            // 序列帧图片排序
            FileInfo[] images = animIndexDic.GetFiles("*." + imagesTypeString[imagesTypeInt]);
            for (int i = 0; i < images.Length - 1; i++)
            {
                for (int j = 0; j < images.Length - 1 - i; j++)
                {
                    if (images[j].Name.Split('.')[0].ToInt() > images[j + 1].Name.Split('.')[0].ToInt())
                    {
                        FileInfo temp = images[j];
                        images[j] = images[j + 1];
                        images[j + 1] = temp;
                    }
                }
            }
            AnimationClip clip = CreateAnimationClip(images, animationClipName);
            // Loop
            if (IsLoopAnim(animationClipName))
            {
                SerializedObject serializedClip = new SerializedObject(clip);
                Editor_AnimationClipSettings clipSettings = new Editor_AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
                clipSettings.loopTime = true;
                serializedClip.ApplyModifiedProperties();
            }
            // Create
            if (animationClipName == defaultCurrAnimName)
            {
                defaultShowImage = images[0];
            }
            string animationPath = GetAnimationPath(frameImagePath, objName);
            if (!Directory.Exists(animationPath))
            {
                Directory.CreateDirectory(animationPath);
            }
            // 不直接创建动画
            //AssetDatabase.CreateAsset(clip, animationPath + "/" + animationClipName + ".anim");
            return clip;
        }

        private static AnimationClip CreateAnimationClip(FileInfo[] images, string animationClipName)
        {
            // Clip
            AnimationClip clip = new AnimationClip();
            clip.name = animationClipName;
            ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Length + 1];
            clip.frameRate = frameRate;
            double frameTime = GetAnimTypeFrameTime(animationClipName);
            for (int i = 0; i < images.Length; i++)
            {
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(PathTool.FilePathToAssetPath(images[i].FullName));
                keyFrames[i] = new ObjectReferenceKeyframe
                {
                    value = sprite,
                    time = (float)(frameTime * i),
                };
            }
            Sprite finallySprite = AssetDatabase.LoadAssetAtPath<Sprite>(PathTool.FilePathToAssetPath(images[images.Length - 1].FullName));
            keyFrames[keyFrames.Length - 1] = new ObjectReferenceKeyframe
            {
                value = finallySprite,
                time = (float)(frameTime * images.Length),
            };
            // Curve
            EditorCurveBinding curveBinding = new EditorCurveBinding();
            curveBinding.type = typeof(SpriteRenderer);
            curveBinding.propertyName = "m_Sprite";
            curveBinding.path = "";
            AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
            return clip;
        }

        private static double GetAnimTypeFrameTime(string animationClipName)
        {
            int frame = 10;
            if (animationClipName.Contains(EP_EntityAnimType.Idle.ToString()))
                frame = EP_AnimRateConst.Idle;
            else if (animationClipName.Contains(EP_EntityAnimType.Move.ToString()))
                frame = EP_AnimRateConst.Move;
            else if (animationClipName.Contains(EP_EntityAnimType.Atk.ToString()))
                frame = EP_AnimRateConst.Atk;
            else if (animationClipName.Contains(EP_EntityAnimType.Skill.ToString()))
                frame = EP_AnimRateConst.Skill;
            else if (animationClipName.Contains(EP_EntityAnimType.Dizzy.ToString()))
                frame = EP_AnimRateConst.Dizzy;
            frame -= 1;
            if (frame <= 0)
            {
                return 0;
            }
            double frameTime = 1d / frame;
            return frameTime;
        }

        private static AnimatorController BuildanimatorController(List<AnimationClip> clips, string objName)
        {
            string animationPath = GetAnimationPath(frameImagePath, objName);
            if (Directory.Exists(animationPath))
            {
                Directory.Delete(animationPath, true);
            }
            Directory.CreateDirectory(animationPath);

            string animatorControllerPath = animationPath + "/" + objName + ".controller";
            animatorControllerPath = PathTool.FilePathToAssetPath(animatorControllerPath);

            UObject[] oldAssets = AssetDatabase.LoadAllAssetsAtPath(animatorControllerPath);
            if (oldAssets != null)
            {
                for (int i = 0; i < oldAssets.Length; i++)
                {
                    UObject oldAsset = oldAssets[i];
                    if (oldAsset != null)
                    {
                        UObject.DestroyImmediate(oldAssets[i], true);
                    }
                }
            }

            AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(animatorControllerPath);
            AnimatorControllerLayer layer = animatorController.layers[0];
            AnimatorStateMachine sm = layer.stateMachine;

            Dictionary<string, Vector2> animPrefixNames = new Dictionary<string, Vector2>();
            Vector2 dv = new Vector2(-430f, 200f);
            for (int i = 0; i < clips.Count; i++)
            {
                AnimationClip clipItem = clips[i];
                string stateName = clipItem.name;
                string[] data = stateName.Split('_');
                string animPrefix = data[0];
                int index = data[1].ToInt();
                if (!animPrefixNames.ContainsKey(animPrefix))
                {
                    dv = new Vector2(dv.x + 230f, dv.y);
                    animPrefixNames[animPrefix] = dv;
                }
                Vector2 prePos = animPrefixNames[animPrefix];
                Vector2 pos = new Vector2(prePos.x, prePos.y + (index * 60));

                AnimatorState state = sm.AddState(stateName, pos);
                state.motion = clipItem;
                if (stateName.Equals(defaultCurrAnimName))
                {
                    sm.defaultState = state;
                }
            }
            for (int i = 0; i < clips.Count; i++)
            {
                AnimationClip clipItem = clips[i];
                AssetDatabase.AddObjectToAsset(clipItem, animatorController);
            }
            AssetDatabase.ImportAsset(animatorControllerPath);
            return animatorController;
        }

        private static void BuildPrefab(string objName, AnimatorController animatorController, FileInfo defaultShowImage, DirectoryInfo fristIamgeDir)
        {
            string prefabPath = frameImagePath + "/" + objName;
            GameObject go = new GameObject(objName);
            SpriteRenderer spriteRender = go.AddComponent<SpriteRenderer>();
            string defaultImagePath = null;
            if (defaultShowImage != null)
            {
                defaultImagePath = defaultShowImage.FullName;
            }
            else
            {
                FileInfo[] images = fristIamgeDir.GetFiles("*." + imagesTypeString[imagesTypeInt]);
                defaultImagePath = images[0].FullName;
            }
            spriteRender.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(PathTool.FilePathToAssetPath(defaultImagePath));
            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;

            string assetPath = PathTool.FilePathToAssetPath(prefabPath + "/" + go.name + ".prefab");
            PrefabUtility.SaveAsPrefabAsset(go, assetPath);
            UObject.DestroyImmediate(go);
        }

        private static bool IsLoopAnim(string name)
        {
            foreach (string s in loopAnimNames)
            {
                if (name.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }

        private static string GetAnimationPath(string frameImagePath, string objName)
        {
            string animationPath = frameImagePath + "/" + objName + "/" + "Animation";
            return animationPath;
        }
    }
}