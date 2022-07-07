/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using UnityEngine;

namespace FutureCore
{
    public static class ShaderUtil
    {
        public static bool HasShader(string shaderName)
        {
            return Shader.Find(shaderName) != null;
        }

        public static void WarmupAllShaders()
        {
            Shader.WarmupAllShaders();
        }

        public static void WarmupIncludedShader(string shaderName)
        {
            Shader.Find(shaderName);
        }

        public static void WarmupShaderVariantCollection(ShaderVariantCollection shaderVariantCollection)
        {
            shaderVariantCollection.WarmUp();
        }

        public static bool IsSupported(string shaderName)
        {
            Shader shader = Shader.Find(shaderName);
            if (shader != null && shader.isSupported)
            {
                return true;
            }
            return false;
        }

        public static bool IsSupported(Shader shader)
        {
            if (shader != null && shader.isSupported)
            {
                return true;
            }
            return false;
        }

        public static MaterialPropertyBlock SetMaterialPropertyBlock(Renderer renderer, MaterialPropertyBlock materialPropertyBlock = null, Action<Renderer, MaterialPropertyBlock> setFunc = null)
        {
            if (materialPropertyBlock == null)
            {
                materialPropertyBlock = new MaterialPropertyBlock();
            }
            if (setFunc != null)
            {
                setFunc(renderer, materialPropertyBlock);
            }
            renderer.SetPropertyBlock(materialPropertyBlock);
            return materialPropertyBlock;
        }

        public static void ClearMaterialPropertyBlock(MaterialPropertyBlock materialPropertyBlock)
        {
            materialPropertyBlock.Clear();
            materialPropertyBlock = null;
        }
    }
}