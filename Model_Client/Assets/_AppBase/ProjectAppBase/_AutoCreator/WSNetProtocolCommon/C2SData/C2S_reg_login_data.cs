/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace ProjectApp.Protocol
{
    // 登录
    public class C2S_reg_login_data
    {
        /// <summary>
        /// 用户ID，重复登录时必须使用uid+token登录    
        /// </summary>
        public long uid;
        public string token;

        /// <summary>  
        /// 用户标识集
        /// </summary>
        public object ids;    
        /// <summary>
        /// 包名
        /// </summary>
        public string pgname = AppFacade.PackageName;
        /// <summary>
        /// 类型，不传或传0表示生产包，1debug，2debugformal
        /// </summary>
        public int pgtype = 0;
        /// <summary> 
        /// 客户端版本值, 用于判定升级
        /// </summary>  
        public int pgvercode = 1;
        /// <summary>
        /// 运营配置，不使用传-1，没有传0
        /// </summary>
        public string pgverstring = "1.0.0";
        /// <summary>
        /// 游戏打表配置，不使用传-1，没有传0
        /// </summary>
        public string pg_settingex_version = "0";
        /// <summary>
        /// 是否断线重连
        /// </summary>  
        public int is_reconnect = 0;
        /// <summary>
        /// 包配置(不区分版本)，不使用传-1，没有传0
        /// </summary>
        public string pg_setting_version = "0";
        /// <summary>
        /// 设备信息  
        /// </summary>
        public Device device = new Device();        
    }    
      
    public class Ids
    {
        public string adjust_id = "1";  
        public string device_id = AppFacade.AppName + DateTime.Now;
        public string idfa_id = AppFacade.AppName + DateTime.Now;
        public string umeng_id = "1";    
    }
       
    public class Device
    {
        // 时区
        public int tz = 0;
        // 系统
        public string os = "";
        // 系统版本
        public string ver = "";
        // 厂商
        public string fac = "";
        // 语言
        public string lang = "";
        // 运营商
        public string imsi = "";
        // 型号
        public string model = "";
        // 网络  
        public string network = "";
        // 网络附加值
        public string[] network_plugin = new string[2];      
        // 是否root：0否1是
        public int is_root = 0;
        // 分辨率
        public string resolution = "";
    }    
}