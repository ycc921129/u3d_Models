/****************************************************************************
* ScriptType: Ö÷¿ò¼Ü
* ÇëÎðÐÞ¸Ä!!!
****************************************************************************/

using System.Collections;

namespace FutureCore
{
    /// <summary>
    /// Î»ÑÚÂë
    /// </summary>
    public class BitMask
    {
        public BitArray array;

        public bool IsAllTrue
        {
            get
            {
                foreach (bool value in array)
                {
                    if (!value)
                        return false;
                }

                return true;
            }
        }

        public bool HasTrue
        {
            get
            {
                foreach (bool value in array)
                {
                    if (value)
                        return true;
                }

                return false;
            }
        }

        public BitMask(int _length = 32, bool _defaultValue = false)
        {
            array = new BitArray(_length, _defaultValue);
        }

        public void Set(int _index, bool _value)
        {
            array[_index] = _value;
        }

        public void SetLast(int _length, bool _value)
        {
            int bitArrayLength = array.Length;
            for (int i = 0; i < _length; ++i)
            {
                array[bitArrayLength - i - 1] = _value;
            }
        }
    }
}