/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.G001_tutorial
{
    public partial class com_moveAni : GComponent
    {
        public btn_block n2_2_1;
        public btn_block n0_1_3;
        public btn_block slider_img;
        public btn_block n2_1_5;
        public btn_block btn_merge1;
        public btn_block n0_0_1;
        public btn_block n2_0_2;
        public btn_block n0_2_2;
        public btn_block btn_2_merge;
        public btn_block n1_1_3;
        public btn_block n1_2_4;
        public com_fingerAni tra_finger;
        public Transition tra_merge1;
        public const string URL = "ui://5tzgfbiwm9c20";

        public static com_moveAni CreateInstance()
        {
            return (com_moveAni)UIPackage.CreateObject("G001_tutorial", "com_moveAni");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2_2_1 = (btn_block)GetChildAt(0);
            n0_1_3 = (btn_block)GetChildAt(1);
            slider_img = (btn_block)GetChildAt(2);
            n2_1_5 = (btn_block)GetChildAt(3);
            btn_merge1 = (btn_block)GetChildAt(4);
            n0_0_1 = (btn_block)GetChildAt(5);
            n2_0_2 = (btn_block)GetChildAt(6);
            n0_2_2 = (btn_block)GetChildAt(7);
            btn_2_merge = (btn_block)GetChildAt(8);
            n1_1_3 = (btn_block)GetChildAt(9);
            n1_2_4 = (btn_block)GetChildAt(10);
            tra_finger = (com_fingerAni)GetChildAt(11);
            tra_merge1 = GetTransitionAt(0);
        }
    }
}