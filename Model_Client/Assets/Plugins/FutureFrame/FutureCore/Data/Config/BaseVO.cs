/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.3
*/

using Beebyte.Obfuscator;

namespace FutureCore
{
    [Skip]
    public partial class BaseVO
    {
        /// <summary>
        /// 数据VO模型层
        /// </summary>
        private VOModel voModel;

        /// <summary>
        /// 下标索引
        /// </summary>
        public int index = 0;

        /// <summary>
        /// id索引
        /// </summary>
        public int id = 0;

        /// <summary>
        /// 字符串Key索引
        /// </summary>
        public string key = null;

        /// <summary>
        /// 静态字段Key索引
        /// </summary>
        public string staticKey = null;

        /// <summary>
        /// 静态字段Value
        /// </summary>
        public object staticValue = null;

        public BaseVO() { }

        public BaseVO(VOModel voModel)
        {
            this.voModel = voModel;
        }

        public void SeVOModel(VOModel voModel)
        {
            if (voModel == null) return;
            this.voModel = voModel;
        }

        public VOModel GetVOModel()
        {
            return voModel;
        }
    }
}