/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections;
using UnityEngine;

namespace FuturePlugin
{
    /// <summary>
    /// 随机弧线运动
    /// </summary>
    public class RandomArcsMotionMB : MonoBehaviour
    {
        public Transform from;
        public Transform to;

        private float mOffsetF1;
        private float mOffsetF2;
        private float mTimeFloat = 0;

        private bool mIsOpen = false;

        private void Start()
        {
            mOffsetF1 = Random.Range(-5.0f, 5.0f);
            mOffsetF2 = Random.Range(0.0f, 1.0f);
            mIsOpen = true;
        }

        private void Update()
        {
            if (mIsOpen)
            {
                mTimeFloat += Time.deltaTime;

                // 弧线的中心
                Vector3 center = (from.position + to.position) * 0.5f;
                // 向下移动中心，垂直于弧线
                center -= new Vector3(mOffsetF2, mOffsetF1, 0);

                // 相对于中心在弧线上插值
                Vector3 riseRelCenter = from.position - center;
                Vector3 setRelCenter = to.position - center;

                transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, mTimeFloat);
                transform.position += center;

                if (transform.position == to.position)
                {
                    StartCoroutine(Destroy());
                }
            }
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(this.gameObject);
        }
    }
}