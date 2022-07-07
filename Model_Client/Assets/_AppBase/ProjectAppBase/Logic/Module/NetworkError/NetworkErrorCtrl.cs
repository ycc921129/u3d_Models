using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class NetworkErrorCtrl : BaseCtrl
    {
        public static NetworkErrorCtrl Instance { get; private set; }

        private NetworkErrorModel model;

        #region 生命周期
        protected override void OnInit()
        {
            Instance = this;
            model = ModuleMgr.Instance.GetModel(ModelConst.NetworkErrorModel) as NetworkErrorModel;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            //ctrlDispatcher.AddListener(CtrlMsg.XXX, OnXXX);
        }

        protected override void RemoveListener()
        {
            //ctrlDispatcher.RemoveListener(CtrlMsg.XXX, OnXXX);
        }

        protected override void AddServerListener()
        {
            //wsNetDispatcher.AddListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }

        protected override void RemoveServerListener()
        {
            //wsNetDispatcher.RemoveListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }
        #endregion

        /// <summary>
        /// 设置为强联网
        /// </summary>
        public void SetStrongNet()
        {
            model.isStrongConnect = true;
            ctrlDispatcher.Dispatch(CtrlMsg.WeakNetworkUI_Click,model.isStrongConnect);
        }
        /// <summary>
        /// 设置为弱联网
        /// </summary>
        public void SetWeakNet()
        {
            model.isStrongConnect = false;
            ctrlDispatcher.Dispatch(CtrlMsg.WeakNetworkUI_Click, model.isStrongConnect);
        }

        private void OnS2CXXXResp(BaseS2CJsonProto protoMsg)
        {
        }
    }
}