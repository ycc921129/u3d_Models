/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;

namespace FutureCore
{
    public class BasePlatformOS : ISDK
    {
        public virtual PlatformOSType GetPlatformOSType()
        {
            return PlatformOSType.None;
        }
    }
}