/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class com_loading : GComponent
    {
        public GTextField text_loading;
        public GProgressBar pb_loading;
        public GTextField text_severStatus;
        public const string URL = "ui://0yiuh9unbdyo0";

        public static com_loading CreateInstance()
        {
            return (com_loading)UIPackage.CreateObject("CS608_loading", "com_loading");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_loading = (GTextField)GetChildAt(1);
            pb_loading = (GProgressBar)GetChildAt(2);
            text_severStatus = (GTextField)GetChildAt(3);
        }
    }
}