#if UNITY_EDITOR

using UnityEngine;

namespace FutureEditor
{
    public class EnumRename_Attribute : HeaderAttribute
    {
        public EnumRename_Attribute(string header) : base(header) { }
    }
}

#endif