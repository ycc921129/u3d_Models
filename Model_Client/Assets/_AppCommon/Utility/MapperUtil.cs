/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public static class MapperUtil
    {
        public static R MappingObject<R, T>(T model)
        {
            string json = SerializeUtil.ToJson(model);

            R result = SerializeUtil.ToObject<R>(json);
           
            return result;
        }
    }
}
