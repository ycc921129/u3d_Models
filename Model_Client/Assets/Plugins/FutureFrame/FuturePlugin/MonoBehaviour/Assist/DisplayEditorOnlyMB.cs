/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FuturePlugin
{
    public class DisplayEditorOnlyMB : MonoBehaviour
    {
        [HideInInspector]
        public string tag = "Untagged";

        private void OnDrawGizmos()
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
            {
                Handles.Label(go.transform.position, tag);
            }
        }
    }

    [CustomEditor(typeof(DisplayEditorOnlyMB))]
    public class DisplayEditorOnlyMB_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DisplayEditorOnlyMB gizmos = target as DisplayEditorOnlyMB;
            EditorGUI.BeginChangeCheck();
            gizmos.tag = EditorGUILayout.TagField("Tag for Objects:", gizmos.tag);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(gizmos);
            }
        }
    }
}

#endif