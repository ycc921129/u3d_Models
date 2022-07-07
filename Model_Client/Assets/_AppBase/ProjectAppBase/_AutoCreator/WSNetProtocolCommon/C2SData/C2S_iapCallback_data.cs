/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore.Data;
using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class C2S_iapCallback_data
    {
        /// <summary>
        /// 必须收到接口响应code=0时才能停止此条收据的回调，否则需要持续发送当前收据的回调数据
        /// 示例：
        /// {"receipt":{"dataSignature":"QwV1yMQjHthYrN/Z5X0gXjYeISl2Z59pzi8k4sbxJQydGas80GzheF70wQ/9GrnEKlUpLVGx5+ho+Mv9fTcsRiDuDRBT2tJZv5220u7iJGGmkOMqDxlslo3u/Nk1soZo5qi3YmcwqA14k4/XdAcPGwkav7rdSvGPv2mySp/qbJwOKqrl7z2heT4EojgoMo/kbiHga3ygzRbyigMtRCdPqlUwkHrlZQKUw+r5Iut8TJBjxPwj/drmKam+nphLX9F7KpgE+iNtgWIjOLu8X7sqvq7a6tPlJ8V0SKuy+S23byEkYt8IzG8zQaC1KmCD+l6AApxpqXkJVS7jSjp1/ZCQ5A\u003d\u003d",
        /// "packageName":"com.tape.luyin","purchaseData":"{\"orderId\":\"GPA.3396-0674-9780-02917\",
        /// \"packageName\":\"com.tape.luyin\",\"productId\":\"iap_1\",\"purchaseTime\":1644982474595,\"purchaseState\":0,
        /// \"purchaseToken\":\"icaakckfnobaaodfbhkonmhi.AO-J1Oz9qE1u8XJsRL9FBJ_-gpy_e7zHWfaRjY0Ha_m_FwGZz81UU7DkYHxWKVE1I_g0L-LuMj_dUhUkx0x_Uk7SS4UJVX4YNQ\",\"acknowledged\":false}"}
        /// </summary>
        public IapPurchaseData receipt;
        public C2S_iapCallback_data()
        {
            receipt = new IapPurchaseData();
        }  
    }
}
