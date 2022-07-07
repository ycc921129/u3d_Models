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
    /// AppCommon-应用常量 VOModel
    /// [应用通用表] [A] [_Excel/通用配置表/Y_应用通用表_A.xlsx]
    /// </summary>
    public partial class AppCommonVOModel : BaseVOModel<AppCommonVOModel, AppCommonVO>
    {
        public override string DescName { get { return "_Excel/通用配置表/Y_应用通用表_A.xlsx"; } }
        public override string FileName { get { return "Client"; } }
        public override string VOName { get { return "AppCommon"; } }

        public override VOIdentifyType IdentifyType { get { return VOIdentifyType.All; } }
        public override List<string> HeadFields { get { return new List<string> { "id", "staticKey", "staticValue", }; } }

        public override bool HasStringKey { get { return false; } }
        public override bool HasStaticField { get { return true; } }

        protected override AppCommonVO MakeVO()
        {
            AppCommonVO vo = new AppCommonVO(this);
            return vo;
        }

        protected override void InitVOStaticField()
        {
            AppCommonVOStatic.InitStaticField();
        }
    }
}