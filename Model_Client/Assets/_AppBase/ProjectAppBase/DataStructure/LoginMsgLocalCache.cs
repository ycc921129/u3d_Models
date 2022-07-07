/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using ProjectApp.Protocol;

namespace ProjectApp.Data
{
    public class Cache_S2C_reg_login
    {
        public string rawJson;
        public string type;
        public string err;
        public Cache_S2C_reg_login_data data;
    }

    public class Cache_S2C_reg_login_data
    {
        /// <summary>
        ///  用户唯一id
        /// <summary>
        public long uid;
        /// <summary>
        /// token  
        /// </summary>
        public string token;
        /// <summary>
        /// 货币 (payday游戏，不同的游戏变量不一样。 coin:金币 cup:奖杯)  
        /// </summary>  
        public AcctGame acct;
        /// <summary>
        /// info
        /// </summary>
        public Info info;
        /// <summary>
        /// preference  
        /// </summary>  
        public Preferences pref;
        /// <summary>
        /// 登录天数和在线时长
        /// </summary>
        public Statis statis;
        /// <summary>  
        /// 运营配置
        /// </summary>  
        public object pg_setting;
        /// <summary>  
        /// 游戏打表数据 (里面包含版本号:version)
        /// </summary>
        public object pg_settingex;
        /// <summary>
        /// ltv24h配置
        /// </summary>
        public object ltv_24h;
        /// <summary>
        /// Update
        /// </summary>
        public Update upgrade;
    }
}