/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;

namespace ProjectApp.Protocol
{
    public class C2S_adwords : C2SJsonProto<C2S_adwords_data>
    {
        public C2S_adwords()
        {
            type = WSNetMsg.C2S_adwords;
        }
    }
}