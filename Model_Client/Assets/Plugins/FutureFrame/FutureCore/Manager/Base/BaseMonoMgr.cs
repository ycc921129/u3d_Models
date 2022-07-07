/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.13
*/

namespace FutureCore
{
    // 管理器单例可替换成Me
    public abstract class BaseMonoMgr<T> : SingletonMono<T>, IMgr where T : BaseMonoMgr<T>
    {
        public bool IsInit { get; private set; }
        public bool IsStartUp { get; private set; }
        public bool IsDispose { get; private set; }

        protected override string ParentRootName
        {
            get
            {
                return AppObjConst.MonoManagerGoName;
            }
        }

        protected override void New()
        {
            base.New();
            IsDispose = false;
        }

        public virtual void Init()
        {
            IsInit = true;
        }

        public virtual void StartUp()
        {
            IsStartUp = true;
        }

        public virtual void DisposeBefore()
        {
            IsDispose = true;
            IsInit = false;
            IsStartUp = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}