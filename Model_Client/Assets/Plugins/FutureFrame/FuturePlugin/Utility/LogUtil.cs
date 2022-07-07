/****************************************************************************
* Author: yang
****************************************************************************/

using System;
using UnityEngine;

public static class LogUtil
{
    public const string TAG = "[LogUtil] ";

    public static void Log(string msg)
    {
        Debug.Log(msg);
    }

    public static void LogWarning(string msg)
    {
        Debug.LogWarning(msg);
    }

    public static void LogError(string msg)
    {
        Debug.LogError(msg);
    }

    public static void LogError(Exception e)
    {
        Debug.LogError(e.Message);
    }

    public static void LogFormat(string format, params object[] args)
    {
        Debug.LogFormat(format, args);
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        Debug.LogErrorFormat(format, args);        
    }

    public static void LogObject(string desc, object obj)
    {
        try
        {
            string msg = JsonUtility.ToJson(obj);
            Debug.Log($"{desc}{msg}");  
        }
        catch (System.Exception)
        {
            Debug.LogError($"{TAG} LogObject is error.");
        }
    }
    public static void LogObject(object obj)
    {
        Debug.Log(obj.ToString());
    }
}
