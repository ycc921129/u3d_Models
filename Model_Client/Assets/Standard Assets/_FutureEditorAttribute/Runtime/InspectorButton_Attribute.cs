#if UNITY_EDITOR

using UnityEngine;

namespace FutureEditor
{
    public class InspectorButton_Attribute : PropertyAttribute
    {
        public string headerName;

        public InspectorButton_Attribute(string headerName)
        {
            this.headerName = headerName;
        }
    }
}


#endif