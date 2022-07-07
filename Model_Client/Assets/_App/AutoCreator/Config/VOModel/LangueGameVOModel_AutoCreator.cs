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
    /// LangueGame(BaseLangue) VOModel
    /// [多语言玩法表] [L] [_Excel/本地配置表/D_多语言玩法表_L (动态多语言).xlsx]
    /// </summary>
    public partial class LangueGameVOModel : BaseVOModel<LangueGameVOModel, LangueGameVO>
    {
        public override string DescName { get { return "_Excel/本地配置表/D_多语言玩法表_L (动态多语言).xlsx"; } }
        public override string FileName { get { return "Local"; } }
        public override string BaseVOName { get { return "BaseLangue"; } }
        public override string VOName { get { return "LangueGame"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.Local; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "key", "en", "zh", "zh_CN", "es", "fr", "ja", "de", "ru", "pt", "@in", "hi_IN", "vi", "tr", "ar", "pl", "th", "ko", "uk", "ro", }; } }

        public override bool HasStringKey { get { return true; } }
        public override bool HasStaticField { get { return false; } }
        public override bool IsUseFieldKeyObjDict { get { return true; } }

        protected override LangueGameVO MakeVO()
        {
            LangueGameVO vo = new LangueGameVO(this);
            return vo;
        }
    }
}