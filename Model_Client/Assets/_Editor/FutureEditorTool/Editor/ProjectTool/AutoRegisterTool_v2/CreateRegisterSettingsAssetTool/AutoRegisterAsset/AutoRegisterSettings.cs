using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class AutoRegisterSettings : ScriptableObject
    {
        public List<DefaultAsset> autoRegisterPath = new List<DefaultAsset>();
    }
}