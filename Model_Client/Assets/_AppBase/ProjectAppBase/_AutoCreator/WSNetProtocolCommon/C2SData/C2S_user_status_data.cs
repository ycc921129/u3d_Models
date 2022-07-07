/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace ProjectApp.Protocol
{
    public class C2S_user_status_data
    {
        public List<UserStatus> status;
    }

    /// <summary>
    /// 玩家状态
    /// </summary>
    public class UserStatus
    {
        // 状态名
        public string name;
        // 状态值
        public string value;
    }
}
