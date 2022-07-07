/****************************************************************************
* ScriptType: Ö÷¿ò¼Ü
* ÇëÎðÐÞ¸Ä!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace FutureCore
{
    [DisallowMultipleComponent]
    public abstract class BaseComponentState : MonoBehaviour, IState
    {
        [SerializeField]
        private StateUnityEvent _OnEnter;

        [SerializeField]
        private StateUnityEvent _OnLeave;

        public event UnityAction<IState, object, object> onEnter
        {
            add
            {
                if (_OnEnter == null) _OnEnter = new StateUnityEvent();
                _OnEnter.AddListener(value);
            }
            remove
            {
                if (_OnEnter != null) _OnEnter.RemoveListener(value);
            }
        }

        public event UnityAction<IState, object, object> onLeave
        {
            add
            {
                if (_OnLeave == null) _OnLeave = new StateUnityEvent();
                _OnLeave.AddListener(value);
            }
            remove
            {
                if (_OnLeave != null) _OnLeave.RemoveListener(value);
            }
        }

        public void OnEnter(IState prevState, object param1 = null, object param2 = null)
        {
            if (_OnEnter != null) _OnEnter.Invoke(prevState, param1, param2);
        }

        public void OnLeave(IState nextState, object param1 = null, object param2 = null)
        {
            if (_OnLeave != null) _OnLeave.Invoke(nextState, param1, param2);
        }

        public abstract uint StateId { get; }

        public abstract void OnRegister(object fsm);

        public abstract void OnRemove(object fsm);

        public abstract void OnUpdate();

        public abstract void OnFixedUpdate();

        public abstract void OnLateUpdate();
    }
}