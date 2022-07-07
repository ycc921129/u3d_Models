/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace FutureCore
{
    public sealed class GraphicsMgr : BaseMgr<GraphicsMgr>
    {
        private Dictionary<string, Shader> shaderCacheDict = new Dictionary<string, Shader>();
        private Dictionary<string, Material> materialCacheDict = new Dictionary<string, Material>();
        private Dictionary<string, Font> fontCacheDict = new Dictionary<string, Font>();

        public void InitPermanentGraphics()
        {
            Dictionary<string, UAssetType> permanentAssets = ResMgr.Instance.GetPermanentAssets();
            if (permanentAssets != null && permanentAssets.Count > 0)
            {
                foreach (string assetPath in permanentAssets.Keys)
                {
                    UAssetType type = permanentAssets[assetPath];
                    if (type == UAssetType.Shader)
                    {
                        shaderCacheDict[assetPath] = ResMgr.Loader.GetInCache<Shader>(assetPath);
                    }
                    else if (type == UAssetType.Material)
                    {
                        materialCacheDict[assetPath] = ResMgr.Loader.GetInCache<Material>(assetPath);
                    }
                    else if (type == UAssetType.Font)
                    {
                        fontCacheDict[assetPath] = ResMgr.Loader.GetInCache<Font>(assetPath);
                    }
                }
            }

            Shader.WarmupAllShaders();
        }

        public Shader GetShader(string assetPath)
        {
            return shaderCacheDict[assetPath];
        }

        public Material GetMaterial(string assetPath)
        {
            return materialCacheDict[assetPath];
        }

        public Font GetFont(string assetPath)
        {
            return fontCacheDict[assetPath];
        }

        public override void Dispose()
        {
            base.Dispose();
            shaderCacheDict.Clear();
            materialCacheDict.Clear();
            fontCacheDict.Clear();

            shaderCacheDict = null;
            materialCacheDict = null;
            fontCacheDict = null;
        }
    }
}