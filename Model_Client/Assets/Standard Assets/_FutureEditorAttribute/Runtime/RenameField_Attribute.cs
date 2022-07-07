#if UNITY_EDITOR

using UnityEngine;

namespace FutureEditor
{
    public class RenameField_Attribute : HeaderAttribute
    {
        public bool isDisplay;

        public RenameField_Attribute(string header, bool isDisplay = true) : base(header)
        {
            this.isDisplay = isDisplay;
        }
    }
}

#endif