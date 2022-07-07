/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.12
*/

using System;
using FuturePlugin;

namespace FutureCore
{
    public static class NetConst
    {
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        ///***********************************************
        /// LocalHost = "http://127.0.0.1:80/";
        /// SimulatorLocalHost = "http://10.0.2.2:80/";
        ///***********************************************
        public static string AssetsWebUrl;
        public static string LoginWebUrl;

        ///***********************************************
        /// Host = "127.0.0.1";
        /// Port = 11001;
        ///***********************************************
        public static string AppTcpHost;
        public static int AppTcpPort;
        public static string GamePlayTcpHost;
        public static int GamePlayTcpPort;

        public static void AfterInit()
        {
            RedirectionAssetsWebUrl();
            RedirectionAppLoginWebUrl();
        }

        public static bool IsNetAvailable
        {
            get { return Channel.Current.isNetworkAvailable; }
        }

        private static void RedirectionAssetsWebUrl()
        {
            switch (Channel.CurrType)
            {
                case ChannelType.LocalDebug:
                    AssetsWebUrl = null;
                    break;
                case ChannelType.NetRelease:
                    AssetsWebUrl = null;
                    break;
                case ChannelType.NetCheck:
                    AssetsWebUrl = null;
                    break;
            }
        }

        private static void RedirectionAppLoginWebUrl()
        {
            switch (Channel.CurrType)
            {
                case ChannelType.LocalDebug:
                    LoginWebUrl = null;
                    break;
                case ChannelType.NetRelease:
                    LoginWebUrl = null;
                    break;
                case ChannelType.NetCheck:
                    LoginWebUrl = null;
                    break;
            }
        }
    }
}