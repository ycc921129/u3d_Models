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
    /// AppLocal VOModel
    /// [应用本地表] [L] [_Excel/本地配置表/Y_应用本地表_L.xlsx]
    /// </summary>
    public partial class AppLocalVOModel : BaseVOModel<AppLocalVOModel, AppLocalVO>
    {
        public override string DescName { get { return "_Excel/本地配置表/Y_应用本地表_L.xlsx"; } }
        public override string FileName { get { return "Local"; } }
        public override string VOName { get { return "AppLocal"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.Local; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "value", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return false; } }

        protected override AppLocalVO MakeVO()
        {
            AppLocalVO vo = new AppLocalVO(this);
            return vo;
        }
    }
}