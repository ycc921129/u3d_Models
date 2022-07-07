using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  ProjectApp
{
    public class TipsUIRealize : TipsUI
    {
        public TipsUIRealize(TipsUICtrl baseUICtrl) : base(baseUICtrl)
        {
        }

        protected override void OnBind()
        {
            base.OnBind();
            this.text_content = this.ui.com_tipContent.text_content;
            this.tra_rise = this.ui.com_tipContent.tra_rise;
        }
    }
}
