/****************************************************************************
* ScriptType: ??????
* ?????޸?!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace FutureCore
{
    [System.Serializable]
    public abstract class BaseStackStateComponent : MonoBehaviour, IStackState
    {
        public uint StateId
        {
            get
            {
                return 0;
            }
        }

        [SerializeField]
        protected StackStateUnityEvent _OnEnter;

        public event UnityAction<IState, ChangeStateType, object> OnEentEvent
        {
            add
            {
                if (_OnEnter == null) _OnEnter = new StackStateUnityEvent();
                _OnEnter.AddListener(value);
            }
            remove
            {
                if (_OnEnter != null) _OnEnter.RemoveListener(value);
            }
        }

        [SerializeField]
        protected StackStateUnityEvent _OnLeave;

        public event UnityAction<IState, ChangeStateType, object> OnLeaveEvent
        {
            add
            {
                if (_OnLeave == null) _OnLeave = new StackStateUnityEvent();
                _OnLeave.AddListener(value);
            }
            remove
            {
                if (_OnLeave != null) _OnLeave.RemoveListener(value);
            }
        }

        public void OnEnter(IState prevState, object param1 = null, object param2 = null)
        {
            var changeType = (ChangeStateType)param1;
            if (_OnEnter != null) _OnEnter.Invoke(prevState as IStackState, changeType, param2);
            OnEnter(prevState, changeType, param2);
        }

        public void OnLeave(IState nextState, object param1 = null, object param2 = null)
        {
            var changeType = (ChangeStateType)param1;
            if (_OnLeave != null) _OnLeave.Invoke(nextState as IStackState, changeType, param2);
            OnLeave(nextState, changeType, param2);
        }

        public abstract void OnRegister(object fsm);

        public abstract void OnRemove(object fsm);

        protected abstract void OnEnter(IState prevState, ChangeStateType type, object param1 = null);

        protected abstract void OnLeave(IState prevState, ChangeStateType type, object param1 = null);

        public virtual void OnUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        private void OnDestroy()
        {
            _OnEnter = null;
            _OnLeave = null;
        }
    }
}