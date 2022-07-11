/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public static class AppHelper
    {
        public static void ShowNeedUpdateUI(Update update, Action cancelFunc)
        {
            UpdateUIData updateUIData = new UpdateUIData();
            updateUIData.update = update;
            updateUIData.cancelFunc = cancelFunc;
            UICtrlDispatcher.Instance.Dispatch(UICtrlMsg.UpdateUI_ShowNeed, updateUIData);
        }

        public static void ShowMustUpdateUI(Update update)
        {
            UICtrlDispatcher.Instance.Dispatch(UICtrlMsg.UpdateUI_ShowMust, update);
        }

        public static void ShowReconnectUI()
        {
            AppGlobal.IsShowDisconnectionTips = true;
        }
    }
}