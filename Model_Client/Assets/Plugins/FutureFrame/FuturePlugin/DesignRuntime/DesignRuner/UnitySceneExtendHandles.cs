/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace FuturePlugin
{
    public class UnitySceneExtendHandles : MonoBehaviour
    {
        public bool showLabel = true;
        public bool showLine = true;
        public bool showSlider = true;
        public bool showRadius = true;
        public bool showCircleHandleCap = true;
        public bool showSphereHandleCap = true;
        public bool showGUI = true;

        public Vector3 sliderPos = Vector3.forward;
        public float areaRadius = 1;
        public float circleSize = 1;
    }

    [CustomEditor(typeof(UnitySceneExtendHandles))]
    public class UnitySceneExtendHandles_Editor : Editor
    {
        //获取SceneExt脚本对象
        private UnitySceneExtendHandles _target { get { return target as UnitySceneExtendHandles; } }

        private void OnSceneGUI()
        {
            if (_target.showLabel)
            {
                //操作句柄,显示文本
                Handles.Label(_target.transform.position + Vector3.up * 0.5f, _target.transform.name + " : " + _target.transform.position);
            }

            if (_target.showLine)
            {
                //修改句柄的颜色
                Handles.color = Color.yellow;
                //绘制一条线
                Handles.DrawLine(_target.transform.position, Vector3.up * 5);
            }

            if (_target.showSlider)
            {
                Handles.color = Color.red;
                //绘制一个可以沿着某个轴向的3D滑动条
                _target.sliderPos = Handles.Slider(_target.sliderPos, _target.transform.forward);
            }

            if (_target.showRadius)
            {
                Handles.color = Color.blue;
                //绘制一个半径控制手柄
                _target.areaRadius = Handles.RadiusHandle(Quaternion.identity, _target.transform.position, _target.areaRadius);
            }

            if (_target.showCircleHandleCap)
            {
                //获取Y轴的颜色
                Handles.color = Handles.yAxisColor;
                //绘制一个圆环
                Handles.CircleHandleCap(0, _target.transform.position + Vector3.up * 2, Quaternion.Euler(90, 0, 0), _target.circleSize, EventType.Repaint);
            }

            if (_target.showSphereHandleCap)
            {
                Handles.color = Color.green;
                //绘制一个球形
                Handles.SphereHandleCap(1, _target.transform.position, Quaternion.identity, HandleUtility.GetHandleSize(_target.transform.position), EventType.Repaint);
            }

            if (_target.showGUI)
            {
                //绘制GUI的内容必须要在BeginGUI、EndGUI的方法对中
                Handles.BeginGUI();
                //设置GUI绘制的区域
                GUILayout.BeginArea(new Rect(50, 50, 200, 200));
                GUILayout.Label("UnitySceneExtendHandles");
                GUILayout.EndArea();
                Handles.EndGUI();
            }
        }
    }
}

#endif