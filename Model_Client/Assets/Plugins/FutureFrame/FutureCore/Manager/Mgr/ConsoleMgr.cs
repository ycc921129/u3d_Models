/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace FutureCore
{
    public sealed class ConsoleMgr : BaseMgr<ConsoleMgr>
    {
        private const string INFO = "INFO";
        private const string WARN = "WARN";
        private const string ERROR = "ERROR";
        private const string DEBUG = "DEBUG";

        private const string Log = "Log";
        private const string Warning = "Warning";
        private const string Error = "Error";
        private const string Assert = "Assert";
        private const string Exception = "Exception";

        private StringBuilder logStringBuilder;
        private StringBuilder unhandledLogBuilder;
        private StreamWriter logFileWriter;
        private StreamWriter unhandledLogFileWriter;

        public override void Init()
        {
            base.Init();

            if (IsWriteLog())
            {
                PathUtil.ClearDir(PathConst.LogDir);
                string logFileFormat = DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + PathConst.LogFilesFormat;
                string logFilePath = PathConst.LogDir + PathConst.ReceivedLogPrefix + logFileFormat;
                string exceptionLogFilePath = PathConst.LogDir + PathConst.UnhandledLogPrefix + logFileFormat;

                logStringBuilder = new StringBuilder(4096);
                unhandledLogBuilder = new StringBuilder(4096);

                FileInfo logFileInfo = new FileInfo(logFilePath);
                FileStream logFileStream = logFileInfo.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                FileInfo exceptionLogFileInfo = new FileInfo(exceptionLogFilePath);
                FileStream exceptionLogFileStream = exceptionLogFileInfo.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                logFileWriter = new StreamWriter(logFileStream);
                unhandledLogFileWriter = new StreamWriter(exceptionLogFileStream);
            }

            Application.logMessageReceivedThreaded += OnLogReceivedCallback;
            AppDomain.CurrentDomain.UnhandledException += OnUnresolvedExceptionCallback;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (IsWriteLog())
            {
                logStringBuilder.Remove(0, logStringBuilder.Length);
                logStringBuilder.Clear();
                unhandledLogBuilder.Remove(0, unhandledLogBuilder.Length);
                unhandledLogBuilder.Clear();

                logFileWriter.Close();
                logFileWriter.Dispose();
                logFileWriter = null;

                unhandledLogFileWriter.Close();
                unhandledLogFileWriter.Dispose();
                unhandledLogFileWriter = null;
            }

            Application.logMessageReceivedThreaded -= OnLogReceivedCallback;
            AppDomain.CurrentDomain.UnhandledException -= OnUnresolvedExceptionCallback;
        }

        private void OnLogReceivedCallback(string condition, string stackTrace, LogType type)
        {
            if (IsDispose) return;

            // 上报
            string currLog = condition;
            if (type == LogType.Exception)
            {
                currLog += ("\n" + stackTrace + "|->EndReportError");
                ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.ReportError, "EXCEPTION: " + currLog);
            }
            else if (type == LogType.Error)
            {
                currLog += ("\n" + stackTrace + "|->EndReportError");
                ChannelDispatcher.Instance.Dispatch(ChannelRawMsg.ReportError, "ERROR: " + currLog);
            }

            // 日志
            if (!IsWriteLog()) return;

            try
            {
                if (logFileWriter != null)
                {
                    if (type == LogType.Assert || type == LogType.Warning)
                    {
                        currLog += ("\n" + stackTrace);
                    }
                    logStringBuilder.Remove(0, logStringBuilder.Length);
                    string currTime = DateTime.Now.ToLongTimeString();
                    string logInfo = logStringBuilder.AppendFormat(
                        "[{0}] [{1}] ({2}): {3}"
                        , currTime, GetHighLightTag(type), GetLogTypeTag(type), currLog).ToString();

                    logFileWriter.WriteLine(logInfo);
                    logFileWriter.Flush();
                }
            }
            catch (Exception e)
            {
                LogUtil.LogErrorFormat("[ConsoleMgr]OnLogReceivedCallback Exception:{0}\nLogType: {1} Log: {2}", e.ToString(), type, currLog);
            }
        }

        private void OnUnresolvedExceptionCallback(object sender, UnhandledExceptionEventArgs args)
        {
            if (IsDispose) return;

            // 日志
            if (!IsWriteLog()) return;

            try
            {
                if (unhandledLogFileWriter != null)
                {
                    Exception e = (Exception)args.ExceptionObject;
                    if (e != null)
                    {
                        unhandledLogBuilder.Remove(0, unhandledLogBuilder.Length);
                        string currTime = DateTime.Now.ToLongTimeString();
                        string logInfo = unhandledLogBuilder.AppendFormat(
                            "[{0}] [ERROR] (UnresolvedException): \nExceptionMessage:{1} IsTerminating:{2}\nExceptionSource:{3} ExceptionStackTrace:{4}}\nException:{5}"
                            , currTime, e.Message, args.IsTerminating, e.Source, e.StackTrace, e.ToString()).ToString();

                        unhandledLogFileWriter.WriteLine(logInfo);
                        unhandledLogFileWriter.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogErrorFormat("[ConsoleMgr]OnUnresolvedExceptionCallback Exception:{0}", e.ToString());
            }
        }

        private bool IsWriteLog()
        {
            return AppConst.IsDebugVersion;
        }

        private string GetHighLightTag(LogType type)
        {
            switch (type)
            {
                case LogType.Log:
                    return INFO;
                case LogType.Warning:
                    return WARN;
                case LogType.Error:
                    return ERROR;

                case LogType.Assert:
                    return DEBUG;
                case LogType.Exception:
                    return ERROR;

                default:
                    return string.Empty;
            }
        }

        private string GetLogTypeTag(LogType type)
        {
            switch (type)
            {
                case LogType.Log:
                    return Log;
                case LogType.Warning:
                    return Warning;
                case LogType.Error:
                    return Error;

                case LogType.Assert:
                    return Assert;
                case LogType.Exception:
                    return Exception;

                default:
                    return string.Empty;
            }
        }
    }
}