/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.12.26
*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace FutureCore
{
    public class WeakEvent<TEventArgs> where TEventArgs : EventArgs
    {
        public delegate void WeakEventFunc(TEventArgs e);

        private class Unit
        {
            private WeakReference reference;
            private MethodInfo method;
            private bool isStatic;

            public bool IsDead
            {
                get
                {
                    return !isStatic && !reference.IsAlive;
                }
            }

            public Unit(WeakEventFunc callback)
            {
                isStatic = callback.Target == null;
                reference = new WeakReference(callback.Target);
                method = callback.Method;
            }

            public bool Equals(WeakEventFunc callback)
            {
                return reference.Target == callback.Target && method == callback.Method;
            }

            public void Invoke(object[] args)
            {
                method.Invoke(reference.Target, args);
            }
        }

        private static object[] ARGS = new object[1];

        private List<Unit> list = new List<Unit>();

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void Add(WeakEventFunc callback)
        {
            list.Add(new Unit(callback));
        }

        public void Remove(WeakEventFunc callback)
        {
            for (int i = list.Count - 1; i > -1; i--)
            {
                if (list[i].Equals(callback))
                {
                    list.RemoveAt(i);
                }
            }
        }

        public void Invoke(TEventArgs args = null)
        {
            ARGS[0] = args;
            for (int i = list.Count - 1; i > -1; i--)
            {
                if (list[i].IsDead)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    list[i].Invoke(ARGS);
                }
            }
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}