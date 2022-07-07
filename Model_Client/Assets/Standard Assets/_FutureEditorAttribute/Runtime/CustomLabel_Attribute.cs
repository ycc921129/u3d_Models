#if UNITY_EDITOR

using UnityEngine;

namespace FutureEditor
{
    public class CustomLabel_Attribute : PropertyAttribute
    {
        public Color textColor;

        public CustomLabel_Attribute()
        {
            textColor = Color.white;
        }

        public CustomLabel_Attribute(float r, float g, float b)
        {
            textColor = new Color(r, g, b);
        }

        public CustomLabel_Attribute(float r, float g, float b, float a)
        {
            textColor = new Color(r, g, b, a);
        }
    }
}

#endif