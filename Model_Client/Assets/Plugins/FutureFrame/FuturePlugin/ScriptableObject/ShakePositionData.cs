/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    [CreateAssetMenu(menuName = "[FC ScriptableObject]/ShakePositionData")]
    public class ShakePositionData : ScriptableObject
    {
        public AnimationCurve shakeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    }
}