/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FuturePlugin
{
    [Serializable]
    public enum ReferenceCollectorType : int
    {
        GameObject = 0,
        Transform = 1,

        Rigidbody = 2,
        BoxCollider = 3,
        Rigidbody2D = 4,
        BoxCollider2D = 5,

        MeshFilter = 6,
        MeshRenderer = 7,
        SpriteRenderer = 8,

        Animator = 9,
        SkeletonAnimation = 10,
    }

    [Serializable]
    public class ReferenceCollectorData
    {
        public int index;
        public string key;
        public Object obj;
        public ReferenceCollectorType type;

        public GameObject GetGameObject()
        {
            switch (type)
            {
                case ReferenceCollectorType.GameObject:
                    if (obj is GameObject gameObject && gameObject)
                        return gameObject;
                    break;
                case ReferenceCollectorType.Transform:
                    if (obj is Transform transform && transform)
                        return transform.gameObject;
                    break;
                default:
                    if (obj is Component component && component)
                        return component.gameObject;
                    break;
            }
            return null;
        }
    }

    public class ReferenceCollectorData_NameComparer : IComparer<ReferenceCollectorData>
    {
        public int Compare(ReferenceCollectorData x, ReferenceCollectorData y)
        {
            return string.Compare(x.key, y.key, StringComparison.Ordinal);
        }
    }

    public class ReferenceCollectorData_IndexComparer : IComparer<ReferenceCollectorData>
    {
        public int Compare(ReferenceCollectorData x, ReferenceCollectorData y)
        {
            return x.index.CompareTo(y.index);
        }
    }

    [AddComponentMenu("[FC 组件]/对象引用绑定组件")]
    public class ReferenceCollectorMB : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Tooltip("是否自动销毁")]
        [HideInInspector]
        public bool isAutoDestroy = false;

        [HideInInspector]
        public List<ReferenceCollectorData> data = new List<ReferenceCollectorData>();

        private readonly Dictionary<string, Object> dict = new Dictionary<string, Object>();

        public T Get<T>(string key) where T : Object
        {
            Object dictGo = null;
            if (!dict.TryGetValue(key, out dictGo))
            {
                return null;
            }
            return dictGo as T;
        }

        public Object Get(string key)
        {
            Object dictGo = null;
            if (!dict.TryGetValue(key, out dictGo))
            {
                return null;
            }
            return dictGo;
        }

        private void Awake()
        {
            if (isAutoDestroy)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (this != null)
            {
                data.Clear();
                dict.Clear();
                Destroy(this);
            }
        }

        #region Unity3d反序列化回调
        public void OnBeforeSerialize()
        {
        }

        //在反序列化后运行
        public void OnAfterDeserialize()
        {
#if UNITY_EDITOR
            dict.Clear();
            foreach (ReferenceCollectorData referenceCollectorData in data)
            {
                if (!dict.ContainsKey(referenceCollectorData.key))
                {
                    dict.Add(referenceCollectorData.key, referenceCollectorData.obj);
                }
            }
#endif
        }
        #endregion

#if UNITY_EDITOR
        #region 生成绑定代码
        [ContextMenu("1) 生成引用对象的赋值", false, -1000)]
        private void ContextMenu_CreateReferenceObjectGet()
        {
            CreateReferenceCollectorDataGetText(data);
        }

        [ContextMenu("2) 生成引用对象的查找路径", false, -1000)]
        private void ContextMenu_CreateReferenceObjectFindPath()
        {
            CreateReferenceCollectorDataFindPathText(data);
        }
        #endregion

        public void Add(int index, string key, Object obj)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            //根据PropertyPath读取数据
            //如果不知道具体的格式，可以右键用文本编辑器打开一个prefab文件（如Bundles/UI目录中的几个）
            //因为这几个prefab挂载了ReferenceCollector，所以搜索data就能找到存储的数据
            SerializedProperty dataProperty = serializedObject.FindProperty("data");
            int i;
            //遍历data，看添加的数据是否存在相同key
            for (i = 0; i < data.Count; i++)
            {
                if (data[i].key == key)
                {
                    break;
                }
            }
            //不等于data.Count意为已经存在于data List中，直接赋值即可
            if (i != data.Count)
            {
                //根据i的值获取dataProperty，也就是data中的对应ReferenceCollectorData，不过在这里，是对Property进行的读取，有点类似json或者xml的节点
                SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
                //对对应节点进行赋值，值为gameobject相对应的fileID
                //fileID独一无二，单对单关系，其他挂载在这个gameobject上的script或组件会保存相对应的fileID
                element.FindPropertyRelative("obj").objectReferenceValue = obj;
            }
            else
            {
                //等于则说明key在data中无对应元素，所以得向其插入新的元素
                dataProperty.InsertArrayElementAtIndex(i);
                SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
                element.FindPropertyRelative("index").intValue = index;
                element.FindPropertyRelative("key").stringValue = key;
                element.FindPropertyRelative("obj").objectReferenceValue = obj;
            }
            //应用与更新
            EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        public void Remove(string key)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty dataProperty = serializedObject.FindProperty("data");
            int i;
            for (i = 0; i < data.Count; i++)
            {
                if (data[i].key == key)
                {
                    break;
                }
            }
            if (i != data.Count)
            {
                dataProperty.DeleteArrayElementAtIndex(i);
            }
            EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        public void Clear()
        {
            SerializedObject serializedObject = new SerializedObject(this);
            //根据PropertyPath读取prefab文件中的数据
            //如果不知道具体的格式，可以直接右键用文本编辑器打开，搜索data就能找到
            SerializedProperty dataProperty = serializedObject.FindProperty("data");
            dataProperty.ClearArray();
            EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        public void RenameTargetReferenceCollectorData()
        {
        }

        public void SortIndex()
        {
            SerializedObject serializedObject = new SerializedObject(this);
            data.Sort(new ReferenceCollectorData_IndexComparer());
            EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        public void SortName()
        {
            SerializedObject serializedObject = new SerializedObject(this);
            data.Sort(new ReferenceCollectorData_NameComparer());
            EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        private string CreateReferenceCollectorDataGetText(List<ReferenceCollectorData> data)
        {
            StringBuilder sb = new StringBuilder();
            //模板
            sb.AppendLine("private void GetAllRefObj(ReferenceCollector refCtr)\r\n{");
            string temp = "    {0} {1} = refCtr.Get<{2}>(\"{3}\");";
            foreach (ReferenceCollectorData collectorData in data)
            {
                if (collectorData.obj == null) continue;
                sb.AppendLine(string.Format(temp, collectorData.type.ToString(), collectorData.key, collectorData.type.ToString(), collectorData.key));
            }
            sb.Append("}");
            //放在复制缓冲区里
            GUIUtility.systemCopyBuffer = sb.ToString();
            return sb.ToString();
        }

        private string CreateReferenceCollectorDataFindPathText(List<ReferenceCollectorData> data)
        {
            StringBuilder sb = new StringBuilder();
            //第一行
            sb.AppendLine("private void GetAllRefObj(Transform root)\r\n{");
            //其他模板
            Dictionary<ReferenceCollectorType, string> tempDic = new Dictionary<ReferenceCollectorType, string>()
            {
                {ReferenceCollectorType.GameObject,"    GameObject {0} = root.Find(\"{1}\").gameObject;" },
                {ReferenceCollectorType.Transform,"    Transform {0} = root.Find(\"{1}\");" },
            };
            //组件模板
            string temp = "    {0} {1} = root.Find(\"{2}\").GetComponent<{3}>();";
            foreach (ReferenceCollectorData collectorData in data)
            {
                if (collectorData.obj == null) continue;
                //获得相对路径(文本为空 代表两物体不存在父子关系)
                string path = GetGameObjectRelativePath(gameObject, collectorData.GetGameObject());
                if (!string.IsNullOrEmpty(path))
                {
                    switch (collectorData.type)
                    {
                        case ReferenceCollectorType.GameObject:
                        case ReferenceCollectorType.Transform:
                            sb.AppendLine(string.Format(tempDic[collectorData.type], collectorData.key, path));
                            break;
                        default:
                            sb.AppendLine(string.Format(temp, collectorData.type.ToString(), collectorData.key, path, collectorData.type.ToString()));
                            break;
                    }
                }
            }
            sb.Append("}");
            //放在复制缓冲区里
            GUIUtility.systemCopyBuffer = sb.ToString();
            return sb.ToString();
        }

        /// <summary>
        /// 获得子物体相对于该物体在Hierarchy面板上的路径
        /// </summary>
        private string GetGameObjectRelativePath(GameObject gameObject, GameObject child)
        {
            string parentPath = GetGameObjectPath(gameObject);
            string childPath = GetGameObjectPath(child);
            if (childPath.StartsWith(parentPath))
            {
                return childPath.Remove(0, parentPath.Length + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得物体在Hierarchy面板上的路径
        /// </summary>
        private string GetGameObjectPath(GameObject gameObject)
        {
            Transform transform = gameObject.transform;
            StringBuilder sb = new StringBuilder();
            sb.Append(transform.name);
            while (transform.parent != null)
            {
                transform = transform.parent;
                sb.Insert(0, transform.name + "/");
            }
            return sb.ToString();
        }
#endif
    }

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ReferenceCollectorMB))]
    public class ReferenceCollectorMBEditor : Editor
    {
        private ReferenceCollectorMB referenceCollector;

        private bool isAutoDestroy;

        private Object heroPrefab;

        private string _searchKey = string.Empty;
        //输入在textfield中的字符串
        private string searchKey
        {
            get
            {
                return _searchKey;
            }
            set
            {
                if (_searchKey != value)
                {
                    _searchKey = value;
                    heroPrefab = referenceCollector.Get<Object>(searchKey);
                }
            }
        }

        private void DelNullReference()
        {
            SerializedProperty dataProperty = serializedObject.FindProperty("data");
            for (int i = dataProperty.arraySize - 1; i >= 0; i--)
            {
                SerializedProperty gameObjectProperty = dataProperty.GetArrayElementAtIndex(i).FindPropertyRelative("obj");
                if (gameObjectProperty.objectReferenceValue == null)
                {
                    dataProperty.DeleteArrayElementAtIndex(i);
                }
            }
        }

        private void OnEnable()
        {
            referenceCollector = (ReferenceCollectorMB)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //EditorGUILayout.Space();

            //使ReferenceCollector支持撤销操作，还有Redo，不过没有在这里使用
            Undo.RecordObject(referenceCollector, "Changed Settings");
            SerializedProperty dataProperty = serializedObject.FindProperty("data");

            EditorGUILayout.LabelField("脚本索引: [FC 组件]/对象引用绑定组件");

            isAutoDestroy = EditorGUILayout.Toggle("是否自动销毁:", referenceCollector.isAutoDestroy);
            referenceCollector.isAutoDestroy = isAutoDestroy;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("添加引用"))
            {
                //添加新的元素，具体的函数注释
                //Guid.NewGuid().GetHashCode().ToString() 就是新建后默认的key
                AddReference(dataProperty, Guid.NewGuid().GetHashCode().ToString(), null);
            }
            if (GUILayout.Button("删除全部"))
            {
                dataProperty.ClearArray();
                serializedObject.ApplyModifiedProperties();
                serializedObject.UpdateIfRequiredOrScript();
            }
            if (GUILayout.Button("删除空引用"))
            {
                DelNullReference();
                serializedObject.ApplyModifiedProperties();
                serializedObject.UpdateIfRequiredOrScript();
            }
            if (GUILayout.Button("下标排序"))
            {
                referenceCollector.SortIndex();
            }
            if (GUILayout.Button("名字排序"))
            {
                referenceCollector.SortName();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            //可以在编辑器中对searchKey进行赋值，只要输入对应的Key值，就可以点后面的删除按钮删除相对应的元素
            EditorGUILayout.LabelField("搜索:", GUILayout.Width(28));
            searchKey = EditorGUILayout.TextField(searchKey);
            //添加的可以用于选中Object的框，这里的object也是(UnityEngine.Object
            //第三个参数为是否只能引用scene中的Object
            EditorGUILayout.ObjectField(heroPrefab, typeof(Object), false);
            if (GUILayout.Button("删除", GUILayout.Width(45), GUILayout.Height(15)))
            {
                referenceCollector.Remove(searchKey);
                heroPrefab = null;
            }

            GUILayout.EndHorizontal();
            EditorGUILayout.Space();

            List<int> delList = new List<int>();
            SerializedProperty property;
            //遍历ReferenceCollector中data list的所有元素，显示在编辑器中
            for (int i = referenceCollector.data.Count - 1; i >= 0; i--)
            {
                SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
                if (dataProperty.arraySize > i && element == null)
                {
                    continue;
                }

                GUILayout.BeginHorizontal();

                property = element.FindPropertyRelative("index");
                string indexStr = EditorGUILayout.TextField(property.intValue.ToString(), GUILayout.ExpandWidth(true));
                property.intValue = int.Parse(indexStr);
                property = element.FindPropertyRelative("key");
                property.stringValue = EditorGUILayout.TextField(property.stringValue, GUILayout.ExpandWidth(true));
                property = element.FindPropertyRelative("obj");
                EditorGUILayout.ObjectField(property.objectReferenceValue, typeof(Object), true);
                property = element.FindPropertyRelative("type");
                ReferenceCollectorType propertyType = (ReferenceCollectorType)property.enumValueIndex;
                //显示下拉框
                if (EditorGUILayout.DropdownButton(new GUIContent(propertyType.ToString()), FocusType.Passive, GUILayout.Width(122)))
                {
                    //点击后显示下拉框的内容
                    GenericMenu menu = new GenericMenu();
                    int enumLen = Enum.GetValues(typeof(ReferenceCollectorType)).Length;
                    for (int j = 0; j < enumLen; j++)
                    {
                        ReferenceCollectorType type = (ReferenceCollectorType)j;
                        //添加下拉框的item并注册点击事件
                        menu.AddItem(new GUIContent(type.ToString()), type.Equals(propertyType), () =>
                        {
                            ChangeReference(element, type);
                        });
                    }
                    //显示下拉框内容
                    menu.ShowAsContext();
                }

                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(15)))
                {
                    //将元素添加进删除list
                    delList.Add(i);
                }

                GUILayout.EndHorizontal();
            }

            EventType eventType = Event.current.type;
            //在Inspector 窗口上创建区域，向区域拖拽资源对象，获取到拖拽到区域的对象
            if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform)
            {
                // Show a copy icon on the drag
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (eventType == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (var o in DragAndDrop.objectReferences)
                    {
                        AddReference(dataProperty, o.name, o);
                    }
                }

                Event.current.Use();
            }

            //遍历删除list，将其删除掉
            foreach (var i in delList)
            {
                dataProperty.DeleteArrayElementAtIndex(i);
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        private void AddReference(SerializedProperty dataProperty, string key, Object obj, ReferenceCollectorType type = ReferenceCollectorType.GameObject)
        {
            int dataPropertyIndex = dataProperty.arraySize;
            dataProperty.InsertArrayElementAtIndex(dataPropertyIndex);
            SerializedProperty element = dataProperty.GetArrayElementAtIndex(dataPropertyIndex);

            element.FindPropertyRelative("index").intValue = dataPropertyIndex;
            element.FindPropertyRelative("key").stringValue = key;
            element.FindPropertyRelative("obj").objectReferenceValue = GetReferenceObject(element, obj as GameObject, type);
            element.FindPropertyRelative("type").enumValueIndex = (int)type;
        }

        private void ChangeReference(SerializedProperty element, ReferenceCollectorType type)
        {
            GameObject gameObject = GetReferenceGameObject(element);
            if (!gameObject) return;
            SerializedProperty objProperty = element.FindPropertyRelative("obj");
            objProperty.objectReferenceValue = null;
            SerializedProperty typeProperty = element.FindPropertyRelative("type");
            typeProperty.enumValueIndex = (int)type;
            switch (type)
            {
                case ReferenceCollectorType.GameObject:
                    objProperty.objectReferenceValue = gameObject;
                    break;
                case ReferenceCollectorType.Transform:
                    objProperty.objectReferenceValue = gameObject.transform;
                    break;
                default:
                    Component component = gameObject.GetComponent(type.ToString());
                    if (component != null)
                    {
                        objProperty.objectReferenceValue = component;
                    }
                    else
                    {
                        objProperty.objectReferenceValue = gameObject;
                    }
                    break;
            }
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }

        private GameObject GetReferenceGameObject(SerializedProperty element)
        {
            //ReferenceCollectorType type = (ReferenceCollectorType)element.FindPropertyRelative("type").enumValueIndex;

            Object obj = element.FindPropertyRelative("obj").objectReferenceValue;
            if (obj == null)
                return null;
            if (obj is GameObject gameObject && gameObject)
                return gameObject;
            if (obj is Transform transform && transform)
                return transform.gameObject;
            if (obj is Component component && component)
                return component.gameObject;
            return null;
        }

        private Object GetReferenceObject(SerializedProperty element, GameObject gameObject, ReferenceCollectorType type)
        {
            if (!gameObject) return null;
            switch (type)
            {
                case ReferenceCollectorType.GameObject:
                    return gameObject;
                case ReferenceCollectorType.Transform:
                    return gameObject.transform;
                default:
                    Component component = gameObject.GetComponent(type.ToString());
                    if (component != null)
                    {
                        return component;
                    }
                    return gameObject;
            }
        }
    }
#endif
}