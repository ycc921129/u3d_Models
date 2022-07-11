/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.MM201_consumeCoinAni
{
    public partial class text_consumeCoin : GComponent
    {
        public GTextField text_coin;
        public const string URL = "ui://va0xqtuwbu211";

        public static text_consumeCoin CreateInstance()
        {
            return (text_consumeCoin)UIPackage.CreateObject("MM201_consumeCoinAni", "text_consumeCoin");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            text_coin = (GTextField)GetChildAt(0);
        }
    }
}