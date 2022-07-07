/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FuturePlugin
{
    [ExecuteInEditMode]
    public class DesignCopyPosText : MonoBehaviour
    {
        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectedChanged;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= OnSelectedChanged;
        }

        private void OnSelectedChanged()
        {
            GameObject gobj = Selection.activeGameObject;
            if (gobj != null)
            {
                string ret = gobj.transform.position.x.ToString("0.00") + ","
                    + gobj.transform.position.y.ToString("0.00") + ","
                    + gobj.transform.position.z.ToString("0.00");

                GUIUtility.systemCopyBuffer = ret;
            }
        }
    }
}

#endif