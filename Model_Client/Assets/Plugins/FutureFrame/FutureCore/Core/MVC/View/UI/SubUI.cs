/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using FairyGUI;

namespace FutureCore
{
    public class SubUI
    {
        public string uiName;
        public string packageName;
        public string assetName;

        public string rawGameObjectName;
        public string gameObjectName;

        public GObject baseGObj;
        public GComponent baseUI;

        public SubUI(string uiName, string packageName, string assetName)
        {
            this.uiName = uiName;
            this.packageName = packageName;
            this.assetName = assetName;
        }
    }
}