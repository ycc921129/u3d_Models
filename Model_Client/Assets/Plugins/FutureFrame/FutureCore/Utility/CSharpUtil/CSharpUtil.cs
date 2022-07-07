using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace FutureCore
{
    public static class CSharpUtil
    {
        /// <summary>
        /// ��������
        /// </summary>
        public static T DeepCopy<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                T inst = (T)formatter.Deserialize(ms);
                return inst;
            }
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        public static WeakReference GetWeakReference(object obj)
        {
            WeakReference wr = new WeakReference(obj);
            return wr;
        }

        /// <summary>
        /// ��ȡԪ�����
        /// </summary>
        public static Tuple<int, Vector3> GetValueTuple(int param1, Vector3 param2)
        {
            return new Tuple<int, Vector3>(param1, param2);
        }

        /// <summary>
        /// ��ȡֵԪ�����
        /// </summary>
        public static ValueTuple<int, int> GetValueTuple(int param1, int param2)
        {
            return (param1, param2);
        }
    }
}