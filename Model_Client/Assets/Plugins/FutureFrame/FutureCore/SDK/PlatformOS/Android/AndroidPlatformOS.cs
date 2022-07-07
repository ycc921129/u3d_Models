/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FuturePlugin;

namespace FutureCore
{
    public class AndroidPlatformOS : BasePlatformOS
    {
        public override PlatformOSType GetPlatformOSType()
        {
            return PlatformOSType.Android;
        }
    }
}