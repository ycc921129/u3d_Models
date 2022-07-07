/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using System.Collections;
using FuturePlugin;

namespace FutureCore
{
    public class ShakePosition : MonoBehaviour
    {
        public ShakePositionData shakeData;
        [Header("方向")]
        public Vector3 direction = Vector3.right;
        [Header("持续时间")]
        public float duration = 0.5f;
        [Header("量级")]
        public float magnitude = 0.5f;

        private bool isInShake;

        public void Shake()
        {
            if (!isInShake && shakeData != null)
            {
                StartCoroutine(OnShake());
            }
        }

        private IEnumerator OnShake()
        {
            isInShake = true;
            Vector3 shakeInitPos = transform.localPosition;
            Vector3 shakeDir = transform.TransformDirection(direction);

            float passTime = 0;
            while (passTime <= duration)
            {
                passTime += Time.deltaTime;
                float rate = passTime / duration;
                float value = shakeData.shakeCurve.Evaluate(rate);
                float valueMagnitude = value * magnitude;
                transform.localPosition = shakeInitPos + shakeDir * valueMagnitude;
                yield return YieldConst.WaitForEndOfFrame;
            }
            isInShake = false;
            transform.localPosition = shakeInitPos;
        }

#if UNITY_EDITOR
        #region Editor
        [FutureEditor.InspectorButton_("震屏")]
        public bool Shake_TestBtn;
        #endregion
#endif
    }
}