/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore.Data;
using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class C2S_subscribeCallback_data
    {
        /// <summary>
        /// 必须收到接口响应code=0时才能停止此条收据的回调，否则需要持续发送当前收据的回调数据
        /// 示例：
        /// {"receipt":"{\"orderId\":\"\",\"packageName\":\"com.tape.luyin\",\"productId\":\"subs_dy1\",
        /// \"purchaseToken\":\"bbpcgfpfodpcghgandgojpja.AO-J1Oxo3WlNv7bupKFqaQ7N2nsz_UJb30Fr7VG1-6aNGUQ93QyHe0n9BWLT86gnLHP79BazDQf_oCpeOdYwsLP3AkECdnRs_Q\"}"}
        /// </summary>
        public C2S_subscribeCallback_info receipt;  
        public C2S_subscribeCallback_data()
        {
            receipt = new C2S_subscribeCallback_info();
        }
    }
}
