/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore.Data
{
    public class FirebaseUserInfo 
    {
        /// <summary>
        /// 登录是否成功
        /// </summary>
        public bool isSuccess;

        /// <summary>
        /// facebook/google
        /// </summary>
        public string type;

        /// <summary>
        /// id
        /// </summary>
        public string id;

        /// <summary>
        /// 昵称
        /// </summary>
        public string nick;

        /// <summary>
        ///  头像(链接)
        /// </summary>
        public string icon;

        /// <summary>
        /// 性别: '0'.未知 '1'.男 '2'.女
        /// </summary>
        public string sex;

        /// <summary>
        /// 手机
        /// </summary>
        public string mobile;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email;

        /// <summary>
        /// firebase消息推送token
        /// </summary>
        public string firebase_token;
    }
}