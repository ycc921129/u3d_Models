/****************************************************************************
* ScriptType: Ö÷¿ò¼Ü
* ÇëÎðÐÞ¸Ä!!!
****************************************************************************/

using System;
using UnityEngine.Events;

namespace FutureCore
{
    [Serializable]
    public class StateUnityEvent : UnityEvent<IState, object, object> { }

    [Serializable]
    public class StackStateUnityEvent : UnityEvent<IState, ChangeStateType, object> { }
}