/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using FutureCore.Data;
using ProjectApp.Data;

namespace ProjectApp.Protocol
{
    // 注册登录合一返回
    [Serializable]
    public partial class S2C_reg_login_data
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

    /// <summary>
    /// 用户信息
    /// </summary>
    public class Info
    {
        /// <summary>
        /// 渠道
        /// </summary>
        public string channel;
        /// <summary>
        /// 邀请码
        /// </summary>
        public string invite_code;
        /// <summary>
        /// 邀请链接
        /// </summary>
        public string invite_url;
        /// <summary>
        /// 邀请来源，邀请上级的用户ID
        /// </summary>
        public string invite_parent;
        /// <summary>
        /// 归因来源，归因上级的用户ID
        /// </summary>
        public string attribute_parent;
        /// <summary>
        /// 登录时间
        /// </summary>
        public long login_time;
        /// <summary>
        /// 注册时间
        /// </summary>
        public long reg_time;
        /// <summary>
        /// 注册国家
        /// </summary>
        public string reg_country;
        /// <summary>
        /// 是否开启兑换
        /// </summary>  
        public bool is_open_exchange;
        /// <summary>
        /// 是否是有效用户
        /// </summary>
        public bool is_effective;
        /// <summary>
        /// 是否开启敏感功能
        /// </summary>
        public List<string> is_open_sensfunc;
        /// <summary>
        /// 是否灰度用户
        /// </summary>  
        public bool is_grayscale;
        /// <summary>
        /// 上次登出时间(服务器所有时间为毫秒)  
        /// </summary>
        public long logout_time;
    }

    public class Statis
    {
        public int online_day;
        public long online_time;
        public int online_count;
        //邀请数量，即为填写了我的邀请码的用户数
        public int invite_count;
        //归因数量，通过我归因的用户数
        public long attribute_count;
    }

    /// <summary>
    /// 运营配置
    /// </summary>
    public class Pg_setting
    {
        public object ads_video;
        public object ads_icon;
        public object adjust_event;
        public object ads_interstitial;
        public object cp_client;
        public object net_opt;
        public object net_opt_v1;
        public string version;
    }

    public class Update
    {
        /// <summary>
        /// 方式：1不提示，2提示，3强制
        /// </summary>
        public int mode;
        /// <summary>
        /// 范围：1灰度用户，2全量
        /// </summary>  
        public int scope;
        /// <summary>
        /// 可以为空
        /// </summary>  
        public string url;
        /// <summary>
        /// 包名或链接(链接是以http开头的)
        /// </summary>
        public string pgname;
    }

    public class AcctGame
    {
        public int coin;
    }
}