using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace FutureEditor
{
    public class FramesMakerWindow : EditorWindow
    {
        private int frameRate = 30;
        private int frame = 10;
        private int orderInLayer = 0;
        private bool isLoop = false;

        private FileInfo[] images;

        [MenuItem("[FC Project]/Res/Maker/Frames/序列帧预设生成窗口", false, 1)]
        private static void ShowWindow()
        {
            GetWindow<FramesMakerWindow>("序列帧预设生成窗口", true);
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            frame = EditorGUILayout.IntField("动画帧数 (帧数高动画速度快)", frame);
            isLoop = EditorGUILayout.Toggle("动画是否循环", isLoop);
            orderInLayer = EditorGUILayout.IntField("渲染层级", orderInLayer);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(" 注:\n" +
                " [帧数不得高于 12]\n" +
                " [地面特效 20 ~ 49]\n" +
                " [底层特效 50 ~ 99]\n" +
                " [角色上层特效 100 ~ 5999]\n" +
                " [顶层特效 7000 ~ 7999]"
                , GUILayout.Height(85));

            if (GUILayout.Button("选中文件夹 -> 生成序列帧预设"))
            {
                BuildAnimPrefab();
            }
            GUILayout.Space(5);
            if (GUILayout.Button("选中文件夹 -> 生成精灵预设"))
            {
                BuildSpritePrefab();
            }
        }

        private void BuildAnimPrefab()
        {
            UObject[] selections = Selection.GetFiltered(typeof(UObject), SelectionMode.Assets);
            string selectPath = AssetDatabase.GetAssetPath(selections[0]);
            DirectoryInfo dictory = new DirectoryInfo(selectPath);

            BuildAnimObj(dictory);
            images = null;
            AssetDatabase.Refresh();
            Debug.Log("生成序列帧动画完成");
        }

        private void BuildAnimObj(DirectoryInfo dictory)
        {
            string name = dictory.Name;
            images = dictory.GetFiles("*.png");
            AnimationClip clip = CreateAnimationClip(images, name);

            if (isLoop)
            {
                SerializedObject serializedClip = new SerializedObject(clip);
                Editor_AnimationClipSettings clipSettings = new Editor_AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
                clipSettings.loopTime = true;
                serializedClip.ApplyModifiedProperties();
            }

            string animatorCountorllerPath = string.Format("Assets/{0}.controller", name);
            AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(animatorCountorllerPath);
            AnimatorControllerLayer layer = animatorController.layers[0];
            AnimatorStateMachine sm = layer.stateMachine;
            AnimatorState state = sm.AddState("Default", Vector3.zero);
            state.motion = clip;
            sm.defaultState = state;
            AssetDatabase.AddObjectToAsset(clip, animatorController);
            AssetDatabase.ImportAsset(animatorCountorllerPath);

            string prefabPath = string.Format("Assets/{0}.prefab", name);
            GameObject go = new GameObject(name);
            SpriteRenderer spriteRender = go.AddComponent<SpriteRenderer>();
            string defaultImagePath = images[0].FullName;
            spriteRender.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(PathTool.FilePathToAssetPath(defaultImagePath));
            spriteRender.sortingOrder = orderInLayer;
            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
            DestroyImmediate(go);
        }

        private AnimationClip CreateAnimationClip(FileInfo[] images, string animationClipName)
        {
            // Clip
            AnimationClip clip = new AnimationClip();
            clip.name = animationClipName;
            ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Length];
            clip.frameRate = frameRate;
            double frameTime = GetAnimTypeFrameTime(frame - 1);
            for (int i = 0; i < images.Length; i++)
            {
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(PathTool.FilePathToAssetPath(images[i].FullName));
                keyFrames[i] = new ObjectReferenceKeyframe
                {
                    value = sprite,
                    time = (float)(frameTime * i),
                };
            }
            // Curve
            EditorCurveBinding curveBinding = new EditorCurveBinding();
            curveBinding.type = typeof(SpriteRenderer);
            curveBinding.propertyName = "m_Sprite";
            curveBinding.path = "";
            AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
            return clip;
        }

        private double GetAnimTypeFrameTime(int frame)
        {
            if (frame <= 0)
            {
                return 0;
            }
            double frameTime = 1d / frame;
            return frameTime;
        }

        private void BuildSpritePrefab()
        {
            UObject[] selections = Selection.GetFiltered(typeof(UObject), SelectionMode.Assets);
            string selectPath = AssetDatabase.GetAssetPath(selections[0]);
            DirectoryInfo dictory = new DirectoryInfo(selectPath);

            BuildSpriteObj(dictory);
            images = null;
            AssetDatabase.Refresh();
            Debug.Log("生成精灵预设完成");
        }

        private void BuildSpriteObj(DirectoryInfo dictory)
        {
            string name = dictory.Name;
            images = dictory.GetFiles("*.png");

            string prefabPath = string.Format("Assets/{0}.prefab", name);
            GameObject go = new GameObject(name);
            SpriteRenderer spriteRender = go.AddComponent<SpriteRenderer>();
            string defaultImagePath = images[0].FullName;
            spriteRender.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(PathTool.FilePathToAssetPath(defaultImagePath));
            spriteRender.sortingOrder = orderInLayer;
            PrefabUtility.CreatePrefab(prefabPath, go);
            DestroyImmediate(go);
        }
    }
}