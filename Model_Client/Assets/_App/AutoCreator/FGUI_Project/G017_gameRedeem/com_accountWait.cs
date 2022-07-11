/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G017_gameRedeem
{
    public partial class com_accountWait : GComponent
    {
        public GTextField text_title;
        public GTextField text_tips0;
        public GTextField text_tips1;
        public GTextField text_tips2;
        public com_ani com_ani;
        public Transition loading;
        public const string URL = "ui://rl7u9y2lu4s63c";

        public static com_accountWait CreateInstance()
        {
            return (com_accountWait)UIPackage.CreateObject("G017_gameRedeem", "com_accountWait");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_title = (GTextField)GetChildAt(1);
            text_tips0 = (GTextField)GetChildAt(2);
            text_tips1 = (GTextField)GetChildAt(3);
            text_tips2 = (GTextField)GetChildAt(4);
            com_ani = (com_ani)GetChildAt(5);
            loading = GetTransitionAt(0);
        }
    }
}