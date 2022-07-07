/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FuturePlugin
{
    public class MPBFillColor : MonoBehaviour
    {
        private static readonly int FillColor = Shader.PropertyToID("_FillColor");
        private static readonly int FillPhase = Shader.PropertyToID("_FillPhase");

        public Color color;
        public float phase;

        private MeshRenderer mr;
        private MaterialPropertyBlock mpb;

        void Awake()
        {
            mr = GetComponent<MeshRenderer>();
            mpb = new MaterialPropertyBlock();
        }

        [ContextMenu("应用材质块")]
        public void ApplyComponentPropertyBlock()
        {
            SetProperty(color);
            SetProperty(phase);
            ApplyPropertyBlock();
        }

        public void SetProperty(Color _color, float _phase)
        {
            SetProperty(_color);
            SetProperty(_phase);
        }

        public void SetProperty(Color _color)
        {
            mpb.SetColor(FillColor, _color);
        }

        public void SetProperty(float _phase)
        {
            mpb.SetFloat(FillPhase, _phase);
        }

        public void ApplyPropertyBlock(Color _color, float _phase)
        {
            SetProperty(_color);
            SetProperty(_phase);
            ApplyPropertyBlock();
        }

        public void ApplyPropertyBlock(Color _color)
        {
            SetProperty(_color);
            ApplyPropertyBlock();
        }

        public void ApplyPropertyBlock(float _phase)
        {
            SetProperty(_phase);
            ApplyPropertyBlock();
        }

        public void ApplyPropertyBlock()
        {
            mr.SetPropertyBlock(mpb);
        }

        void OnDestroy()
        {
            mpb.Clear();
            mpb = null;
        }
    }
}