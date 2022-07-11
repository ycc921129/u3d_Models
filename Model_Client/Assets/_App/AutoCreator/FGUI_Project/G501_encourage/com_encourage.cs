/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G501_encourage
{
    public partial class com_encourage : GComponent
    {
        public GLoader gld_img;
        public Transition tra_encourage;
        public const string URL = "ui://q9blglmwoyu95";

        public static com_encourage CreateInstance()
        {
            return (com_encourage)UIPackage.CreateObject("G501_encourage", "com_encourage");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            gld_img = (GLoader)GetChildAt(0);
            tra_encourage = GetTransitionAt(0);
        }
    }
}