/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System;
using System.Net;

namespace FutureCore
{
    public static class NetEndianUtil
    {
        /// <summary>
        /// 小端转换大端
        /// 主机字节序转换网络字节序
        /// </summary>
        public static void HostToNetworkOrder(byte[] bytes)
        {
            if (NetConst.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
        }
        public static short HostToNetworkOrder(short host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }
        public static int HostToNetworkOrder(int host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }
        public static long HostToNetworkOrder(long host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        /// <summary>
        /// 大端转换小端
        /// 网络字节序转换主机字节序
        /// </summary>
        public static void NetworkToHostOrder(byte[] bytes)
        {
            if (NetConst.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
        }
        public static short NetworkToHostOrder(short network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }
        public static int NetworkToHostOrder(int network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }
        public static long NetworkToHostOrder(long network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }
        public static UInt32 NetworkToHostOrder(UInt32 network)
        {
            byte[] valueBytes = BitConverter.GetBytes(network);
            NetworkToHostOrder(valueBytes);
            UInt32 value = BitConverter.ToUInt32(valueBytes, 0);
            return value;
        }
    }
}