/****************************************************************************
* ScriptType: 框架 - 通用业务
* 请勿修改!!!
****************************************************************************/

using FutureCore;
using System.Text.RegularExpressions;

namespace ProjectApp
{
    public class CheckMgr:BaseMgr<CheckMgr>
    {
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="phone">mobile</param>
        /// <returns></returns>
        public bool CheckPhone(string phone)
        {
            bool isTruePhone = true;

            if (phone == null || phone.Contains("@") || phone.Trim().Length == 0)
            {
                isTruePhone = false;
            }
            return isTruePhone;
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="str">email</param>
        /// <returns></returns>
        public bool IsEmail(string email)
        {
            string strRegex = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (regex.Match(email).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
