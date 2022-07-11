using FairyGUI;
using UnityEngine;

namespace ProjectApp
{
    public static class LoadingTrasitionConst
    {
        public const float loadingTraTime = 0.3f;
    }


    public class LoadingUIRealize : LoadingUI
    {
        public LoadingUIRealize(LoadingUICtrl ctrl) : base(ctrl)
        {
        }

        protected override void OnBind()
        {
            base.OnBind();
            //text_severStatus = this.ui.text_severStatus;
            pb_loading = this.ui.pb_loading;
        }

        protected override void OnOpen(object args)
        {
            base.OnOpen(args);
            SetSeverStatus();
        }

        /// <summary>
        /// 设置进度条动画
        /// </summary>
        /// <param name="value"></param>
        public void SetLoading(int value)
        {
            SetLoadingValue(value, LoadingTrasitionConst.loadingTraTime);
        }
    }
}
