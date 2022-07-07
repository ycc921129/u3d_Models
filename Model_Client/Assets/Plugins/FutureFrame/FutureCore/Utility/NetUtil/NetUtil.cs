/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace FutureCore
{
    public static class NetUtil
    {
        public static uint GetVerificationCode()
        {
            return 0;
        }

        public static NetworkReachability GetInternetReachability()
        {
            return Application.internetReachability;
        }

        public static string GetToServerdMac()
        {
            List<string> macs = GetMacList();
            if (macs.Count > 0)
            {
                return macs[0];
            }
            else
            {
                return DeviceConst.DeviceIdentifier;
            }
        }

        public static List<string> GetMacList()
        {
            List<string> macs = new List<string>();
            NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nis)
            {
                string mac = ni.GetPhysicalAddress().ToString();
                if (!string.IsNullOrEmpty(mac))
                {
                    macs.Add(mac);
                }
            }
            return macs;
        }
    }
}