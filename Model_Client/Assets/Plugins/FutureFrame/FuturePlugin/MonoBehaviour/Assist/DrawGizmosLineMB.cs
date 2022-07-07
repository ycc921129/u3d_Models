/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace FuturePlugin
{
    public class DrawGizmosLineMB : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool isShow = true;
        public Color color = Color.green;
        public List<Vector3> posList;

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetData(List<Vector3> posList)
        {
            this.posList = posList;
        }

        void OnDrawGizmos()
        {
            if (!isShow) return;
            if (posList != null)
            {
                Gizmos.color = color;
                Vector3 frontPos = posList[0];
                for (int i = 1; i < posList.Count; i++)
                {
                    Vector3 pos = posList[i];
                    Gizmos.DrawLine(frontPos, pos);
                    frontPos = pos;
                }
            }
        }
#endif
    }
}