/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class LoginModel : BaseModel
    {
        public int launchAppTime;
        public int loginDays;
        public int loginCount;
        public bool isNewUser;

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnDispose()
        {
        }

        protected override void OnReset()
        {
        }
        #endregion

        #region 读取数据
        protected override void OnReadData()
        {
        }
        #endregion  

        #region 本地存储
        protected override void WriteLocalStorage()
        {
            //WriteLocalStorage(lsData);
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {

        }

        protected override void RemoveListener()
        {

        }
        #endregion  
    }
}