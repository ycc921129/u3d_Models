/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System;
using FutureCore;
using FutureCore.Data;

namespace ProjectApp.Data
{
    /// <summary>
    /// Langue(BaseLangue)-动态多语言 VOModel
    /// [多语言通用表] [L] [_Excel/本地配置表/D_多语言通用表_L (动态多语言).xlsx]
    /// </summary>
    public partial class LangueVOModel : BaseVOModel<LangueVOModel, LangueVO>
    {
        public override string DescName { get { return "_Excel/本地配置表/D_多语言通用表_L (动态多语言).xlsx"; } }
        public override string FileName { get { return "Local"; } }
        public override string BaseVOName { get { return "BaseLangue"; } }
        public override string VOName { get { return "Langue"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.Local; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "key", "en", "zh", "zh_CN", "es", "fr", "ja", "de", "ru", "pt", "@in", "hi_IN", "vi", "tr", "ar", "pl", "th", "ko", "uk", "ro", "ms", }; } }

        public override bool HasStringKey { get { return true; } }
        public override bool HasStaticField { get { return false; } }
        public override bool IsUseFieldKeyObjDict { get { return true; } }

        protected override LangueVO MakeVO()
        {
            LangueVO vo = new LangueVO(this);
            return vo;
        }
    }
}