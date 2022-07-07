/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

namespace ProjectApp.Protocol
{
    // 广告事件统计
    public class C2S_ad_event_data
    {
        /// <summary>
        /// 广告SDK接入商
        /// </summary>
        public string sdk;

        /// <summary>
        /// 广告类型
        /// </summary>
        public string type;

        /// <summary>
        /// 广告位ID
        /// </summary>
        public string id;

        /// <summary>
        /// 广告位名称
        /// </summary>
        public string name;

        /// <summary>
        /// 事件
        /// </summary>
        public string @event;
        
        /// <summary>
        /// 当次广告展示收入
        /// </summary>
        public double revenue;
    }
}