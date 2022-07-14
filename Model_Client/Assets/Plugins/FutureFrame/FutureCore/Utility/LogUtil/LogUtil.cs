/****************************************************************************
* Author: yang
****************************************************************************/

using System;
using UnityEngine;

public static class LogUtil 
{
    private static bool IsEnableLog;
    private static Func<object, string> SerializeFunc;

    public static void EnableLog(bool isEnableLog, Func<object, string> serializeFunc)
    {
        IsEnableLog = isEnableLog;
        SerializeFunc = serializeFunc;
    }
    
    public static void Log(string msg)  
    {
        if (!IsEnableLog) return;

        Debug.Log(msg);
    }

    public static void LogWarning(string msg)
    {
        if (!IsEnableLog) return;

        Debug.LogWarning(msg);
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
        if (!IsEnableLog) return;

        Debug.LogWarningFormat(format, args);  
    }

    public static void LogFormat(string format, params object[] args)
    {
        if (!IsEnableLog) return;

        Debug.LogFormat(format, args);
    }

    public static void LogObject(string prefix, object obj)
    {
        if (!IsEnableLog) return;

        string str = SerializeFunc(obj);
        object[] args = new object[] { prefix, str };
        LogFormat("{0}\n{1}", args);            
    }  

    public static void LogError(object msg)
    {
        if (!IsEnableLog) return;
          
        Debug.LogError(msg);  
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        if (!IsEnableLog) return;

        Debug.LogErrorFormat(format, args);  
    }

}
