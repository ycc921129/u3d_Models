/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace FuturePlugin
{
    public class FixABShaderMB : MonoBehaviour
    {
#if UNITY_EDITOR
        void Start()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
            Graphic[] graphics = GetComponentsInChildren<Graphic>(true);

            foreach (Renderer renderer in renderers)
            {
                Material[] mats = renderer.sharedMaterials;
                foreach (Material mat in mats)
                {
                    if (mat != null)
                    {
                        Shader fixShader = Shader.Find(mat.shader.name);
                        mat.shader = fixShader;
                    }
                }
            }
            foreach (Graphic graphic in graphics)
            {
                Material mat = graphic.material;
                if (mat != null)
                {
                    Shader fixShader = Shader.Find(mat.shader.name);
                    mat.shader = fixShader;
                }
            }
        }
#endif
    }
}