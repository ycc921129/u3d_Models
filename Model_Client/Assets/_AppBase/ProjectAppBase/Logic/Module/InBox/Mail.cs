using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectApp.Data
{
    public class Mail
    {
        /// <summary>
        /// 道具id
        /// </summary>
        public int id;

        /// <summary>
        /// 标题id
        /// </summary>
        public int title_id;
        /// <summary>
        /// 描述id
        /// </summary>
        public int desc_id;
        /// <summary>
        /// 道具数值
        /// </summary>
        public float amount;

        /// <summary>
        /// 道具类型
        /// </summary>
        public string type;

        /// <summary>
        /// 道具状态（used，unused）
        /// </summary>
        public string state;
    }
}