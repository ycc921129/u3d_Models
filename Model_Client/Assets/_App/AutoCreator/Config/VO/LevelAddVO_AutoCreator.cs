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
    /// LevelAdd VO
    /// [等级配置表] [A] [_Excel/游戏配置表/L_等级配置表_A.xlsx]
    /// </summary>
    public partial class LevelAddVO : BaseVO
    {
        /// <summary>
        /// 等级
        /// 标识类型: A
        /// </summary>
        public int levelUp;

        /// <summary>
        /// 可升级的分数值
        /// 标识类型: A
        /// </summary>
        public int score;

        /// <summary>
        /// 自定义构造
        /// </summary>
        public LevelAddVO() : base() { }

        /// <summary>
        /// 标准构造
        /// </summary>
        public LevelAddVO(VOModel voModel) : base(voModel) { }
    }
}