using System;

namespace Pomelo.Client
{
    public enum PackageType
    {
        PKG_HANDSHAKE = 1,
        PKG_HANDSHAKE_ACK = 2,//资料有无正确传输到服务器端
        PKG_HEARTBEAT = 3,
        PKG_DATA = 4,
        PKG_KICK = 5
    }
}

