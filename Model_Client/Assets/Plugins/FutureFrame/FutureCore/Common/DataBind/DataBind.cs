/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public class DataBind<T>
    {
        public delegate void DataChangedHandler(T oldData, T newData);
        public DataChangedHandler onDataChanged;

        private T data;
        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                if (!Equals(data, value))
                {
                    T old = data;
                    data = value;
                    DataChanged(old, data);
                }
            }
        }

        public DataBind()
        {
        }

        public DataBind(T data)
        {
            this.data = data;
        }

        public DataBind(T data, DataChangedHandler onDataChanged)
        {
            this.data = data;
            this.onDataChanged = onDataChanged;
        }

        public void QuietlySetData(T v)
        {
            data = v;
        }

        public void UpdateCurrData()
        {
            DataChanged(Data, Data);
        }

        private void DataChanged(T oldData, T newData)
        {
            if (onDataChanged != null)
            {
                onDataChanged(oldData, newData);
            }
        }

        public override string ToString()
        {
            return (Data != null ? Data.ToString() : "null");
        }
    }
}