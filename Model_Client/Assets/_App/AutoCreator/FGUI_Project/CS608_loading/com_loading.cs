/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.CS608_loading
{
    public partial class com_loading : GComponent
    {
        public pb_bar pb_loading;
        public const string URL = "ui://9euvtldhd4qv0";

        public static com_loading CreateInstance()
        {
            return (com_loading)UIPackage.CreateObject("CS608_loading", "com_loading");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            pb_loading = (pb_bar)GetChildAt(2);
        }
    }
}