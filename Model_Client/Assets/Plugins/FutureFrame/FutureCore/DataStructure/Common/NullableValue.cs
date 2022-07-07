/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public struct NullableValueStruct
    {
        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public NullableValueStruct(float value)
        {
            _value = value;
        }
    }

    public class NullableValue
    {
        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public NullableValue(float value)
        {
            _value = value;
        }
    }
}