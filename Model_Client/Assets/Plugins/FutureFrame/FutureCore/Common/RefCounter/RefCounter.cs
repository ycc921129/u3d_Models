/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public class RefCounter : IRefCounter
    {
        public RefCounter()
        {
            RefCount = 0;
        }

        public int RefCount { get; private set; }

        public void Retain()
        {
            ++RefCount;
        }

        public void Release()
        {
            --RefCount;
            if (RefCount == 0)
            {
                OnNullRef();
            }
        }

        protected virtual void OnNullRef() { }
    }
}