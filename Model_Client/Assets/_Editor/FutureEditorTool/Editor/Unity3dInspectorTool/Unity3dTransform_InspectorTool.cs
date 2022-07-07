//#region 有Unity异常 暂不使用
//using System.Reflection;
//using System.Text;
//using UnityEditor;
//using UnityEngine;

//namespace FutureEditor
//{
//    /// <summary>
//    /// 扩展Transform，添加复制坐标和旋转的按钮
//    /// </summary>
//    [CanEditMultipleObjects]
//    [CustomEditor(typeof(Transform), true)]
//    public class Unity3dTransform_InspectorTool : Editor
//    {
//        private Editor m_transformInspector;
//        private Transform transform;

//        private void OnEnable()
//        {
//            //m_TransformInspector = CreateEditor(target, Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.TransformInspector", true));
//            Assembly assembly = typeof(Editor).Assembly;
//            System.Type inspector = assembly.GetType("UnityEditor.TransformInspector");
//            if (inspector != null)
//            {
//                m_transformInspector = CreateEditor(target, inspector);
//            }
//        }

//        private void OnDestroy()
//        {
//            if (m_transformInspector)
//            {
//                DestroyImmediate(m_transformInspector);
//                m_transformInspector = null;
//            }
//            m_transformInspector = null;
//        }

//        public override void OnInspectorGUI()
//        {
//            if (m_transformInspector)
//            {
//                m_transformInspector.OnInspectorGUI();
//            }
//            else
//            {
//                return;
//            }

//            transform = target as Transform;
//            //EditorGUILayout.Space();
//            EditorGUILayout.BeginHorizontal();
//            if (GUILayout.Button("复制坐标"))
//            {
//                TextEditor textEd = new TextEditor();
//                StringBuilder str = new StringBuilder();
//                str.Append(transform.position.x + ",");
//                str.Append(transform.position.y + ",");
//                str.Append(transform.position.z);
//                textEd.text = str.ToString();
//                textEd.OnFocus();
//                textEd.Copy();
//            }
//            if (GUILayout.Button("复制旋转"))
//            {
//                TextEditor textEd = new TextEditor();
//                StringBuilder str = new StringBuilder();
//                str.Append(transform.rotation.eulerAngles.x + ",");
//                str.Append(transform.rotation.eulerAngles.y + ",");
//                str.Append(transform.rotation.eulerAngles.z);
//                textEd.text = str.ToString();
//                textEd.OnFocus();
//                textEd.Copy();
//            }
//            if (GUILayout.Button("复制缩放"))
//            {
//                TextEditor textEd = new TextEditor();
//                StringBuilder str = new StringBuilder();
//                str.Append(transform.localScale.x + ",");
//                str.Append(transform.localScale.y + ",");
//                str.Append(transform.localScale.z);
//                textEd.text = str.ToString();
//                textEd.OnFocus();
//                textEd.Copy();
//            }

//            EditorGUILayout.EndHorizontal();
//            EditorGUILayout.BeginHorizontal();
//            if (GUILayout.Button("黏贴坐标"))
//            {
//                Vector3 vec = ChangeVector3Str(GUIUtility.systemCopyBuffer);
//                transform.position = vec;
//            }
//            if (GUILayout.Button("黏贴旋转"))
//            {
//                Vector3 qua = ChangeVector3Str(GUIUtility.systemCopyBuffer);
//                transform.rotation = Quaternion.Euler(qua);
//            }
//            if (GUILayout.Button("黏贴缩放"))
//            {
//                Vector3 vec = ChangeVector3Str(GUIUtility.systemCopyBuffer);
//                transform.localScale = vec;
//            }
//            EditorGUILayout.EndHorizontal();
//        }

//        private Vector3 ChangeVector3Str(string str)
//        {
//            str = str.Replace("(", "");
//            str = str.Replace(")", "");
//            str = str.Replace(" ", "");
//            string[] strs = str.Split(',');
//            if (strs.Length != 3)
//            {
//                return Vector3.zero;
//            }
//            else
//            {
//                float x = float.Parse(strs[0].ToString());
//                float y = float.Parse(strs[1].ToString());
//                float z = float.Parse(strs[2].ToString());

//                return new Vector3(x, y, z);
//            }
//        }
//    }
//}
//#endregion