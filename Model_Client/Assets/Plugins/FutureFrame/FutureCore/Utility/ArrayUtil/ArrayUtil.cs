/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    public static class ArrayUtil
    {
        private static void Add(int[] array, int obj)
        {
            int num = array.Length;
            int[] newArray = new int[num + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            newArray[newArray.Length - 1] = obj;
            array = newArray;
        }

        public static void Delete(int[] array, int obj)
        {
            int deleteIdx = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == obj)
                {
                    deleteIdx = i;
                    break;
                }
            }
            if (deleteIdx != -1)
            {
                int[] newArray = new int[array.Length - 1];
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != deleteIdx)
                    {
                        newArray[i] = array[i];
                    }
                }
                array = newArray;
            }
        }

        public static void Change(int[] array, int idx, int obj)
        {
            if (idx < array.Length)
            {
                array[idx] = obj;
            }
        }

        public static int SearchIdx(int[] array, int obj)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == obj)
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool Search(int[] array, int idx, int obj)
        {
            if (array[idx] == obj)
            {
                return true;
            }
            return false;
        }

        public static void Random(int[] array)
        {
            System.Random r = new System.Random();
            for (int i = 0; i < array.Length; i++)
            {
                int index = r.Next(array.Length);
                int temp = array[i];
                array[i] = array[index];
                array[index] = temp;
            }
        }
    }
}