using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectApp
{
    public class LoginUIRealize : LoginUI
    {
        public LoginUIRealize(LoginUICtrl ctrl) : base(ctrl)
        {
        }

        protected override void OnBind()
        {
            base.OnBind();
            this.btn_facebookLogin = this.ui.btn_facebookLogin;
            this.btn_googleLogin = this.ui.btn_googleLogin;
        }
    }
}
