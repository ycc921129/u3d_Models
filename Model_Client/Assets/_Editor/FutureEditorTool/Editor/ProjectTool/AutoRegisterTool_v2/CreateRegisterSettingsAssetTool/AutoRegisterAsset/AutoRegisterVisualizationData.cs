using System.Collections.Generic;
using UnityEngine;

namespace FutureEditor
{
    public class AutoRegisterVisualizationData : ScriptableObject
    {
        public List<Object> config = new List<Object>();
        public List<Object> gameData = new List<Object>();
        public List<Object> preferences = new List<Object>();
        public List<Object> scene = new List<Object>();
        public List<Object> module = new List<Object>();
    }
}