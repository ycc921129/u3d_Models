/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using Beebyte.Obfuscator;

namespace FutureCore
{
    // Base
    [Skip]
    public class BaseJsonProto 
    {
        public string route;
        public string type;

        public static implicit operator bool(BaseJsonProto jsonProto)
        {
            return null != jsonProto;
        }
    }

    // Request
    [Skip]
    public class BaseC2SJsonProto : BaseJsonProto
    {
        public string bind_s2c_type;

        public virtual string send_json { get { return ""; } }  
    }  

    // Response
    [Skip]
    public class BaseS2CJsonProto : BaseJsonProto
    { 
        public string rawJson;
        public string err;

        public void SetRawJson(string rawJson)
        {
            this.rawJson = rawJson;
        }
        public string GetRawJson()
        {
            return rawJson;
        }
    }

    // Request
    [Skip]  
    public class C2SJsonProto<T> : BaseC2SJsonProto where T : class, new()
    {
        public T data;
        public override string send_json { get { return SerializeUtil.ToJson(data); } }

        /// <summary>
        /// 发送协议
        /// </summary>
        /// <param name="notify">不需要协议返回</param>
        /// <returns></returns>
        public bool Send(bool notify = false) 
        {
            return false; 
        }
    }

    // Response
    [Skip]
    public class S2CJsonProto : BaseS2CJsonProto 
    {

    }
}