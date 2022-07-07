#if UNITY_EDITOR

using UnityEngine;

namespace FutureEditor
{
    public class SetRange_Attribute : PropertyAttribute
    {
        public string headerName;
        public float min;
        public float max;

        public SetRange_Attribute(string headerName, float min, float max)
        {
            this.headerName = headerName;
            this.min = min;
            this.max = max;
        }
    }
}

#endif