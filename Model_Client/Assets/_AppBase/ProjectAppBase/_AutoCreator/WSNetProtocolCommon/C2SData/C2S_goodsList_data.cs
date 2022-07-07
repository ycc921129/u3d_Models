/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
    //列表数据 
    public class C2S_goodsList_data
    {
        /// <summary>
        /// 示例['exchange', 'iap']  iap/exchange/subscribe
        /// </summary>
        public string[] types;

        public C2S_goodsList_data()
        {
            types = new string[] { "exchange", "iap", "subscribe" };
        }
    }
}