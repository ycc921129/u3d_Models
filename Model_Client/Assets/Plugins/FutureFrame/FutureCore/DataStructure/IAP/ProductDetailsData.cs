/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore.Data
{
    /// <summary>
    /// 查询商品属性列表
    /// </summary>
    public class ProductDetailsData
    {
        /// <summary>
        /// 内购商品id列表
        /// </summary>
        public List<string> iap_list = new List<string>();
        /// <summary>
        /// 订阅商品id列表
        /// </summary>
        public List<string> subs_list = new List<string>();
    }
}