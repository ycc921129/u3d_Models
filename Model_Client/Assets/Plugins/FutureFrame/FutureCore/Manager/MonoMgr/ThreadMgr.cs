/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/****************************************************************************
*    使用范例：
*    //Scale a mesh on a second thread
*    private void ScaleMesh(Mesh mesh, float scale)
*    {
*        //Get the vertices of a mesh
*        var vertices = mesh.vertices;
*        //Run the action on a new thread
*        ThreadMgr.Instance.RunAsyncThread(() => {
*            //Loop through the vertices
*            for (var i = 0; i < vertices.Length; i++)
*            {
*                //Scale the vertex
*                vertices[i] = vertices[i] * scale;
*            }
*            //Run some code on the main thread
*            //to update the mesh
*            ThreadMgr.Instance.RunMainThread(() => {
*                //Set the vertices
*                mesh.vertices = vertices;
*                //Recalculate the bounds
*                mesh.RecalculateBounds();
*            });
*        });
*    }
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace FutureCore
{
    public sealed class ThreadMgr : BaseMonoMgr<ThreadMgr>
    {
        public struct DelayedQueueItem
        {
            public float time;
            public Action mainTreadAction;
        }

        private const int maxThreads = 8;
        private int useThreads = 0;

        private List<Action> _actions = new List<Action>();
        private List<Action> _currentActions = new List<Action>();
        private List<DelayedQueueItem> _delayeds = new List<DelayedQueueItem>();
        private List<DelayedQueueItem> _currentDelayeds = new List<DelayedQueueItem>();

        public void RunAsyncThread(Action asyncAction)
        {
            while (useThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref useThreads);
            ThreadPool.QueueUserWorkItem(RunAction, asyncAction);
        }

        private void RunAction(object asyncAction)
        {
            try
            {
                ((Action)asyncAction)();
            }
            catch
            {
                throw;
            }
            finally
            {
                Interlocked.Decrement(ref useThreads);
            }
        }

        public void RunMainThread(Action mainTreadAction)
        {
            RunMainThread(0f, mainTreadAction);
        }

        public void RunMainThread(float waitTime, Action mainTreadAction)
        {
            if (waitTime != 0)
            {
                lock (_delayeds)
                {
                    _delayeds.Add(new DelayedQueueItem
                    {
                        time = Time.time + waitTime,
                        mainTreadAction = mainTreadAction
                    });
                }
            }
            else
            {
                lock (_actions)
                {
                    _actions.Add(mainTreadAction);
                }
            }
        }

        private void Update()
        {
            if (_actions.Count > 0)
            {
                lock (_actions)
                {
                    _currentActions.Clear();
                    _currentActions.AddRange(_actions);
                    _actions.Clear();
                }
                foreach (Action mainTreadAction in _currentActions)
                {
                    mainTreadAction();
                }
            }

            if (_delayeds.Count > 0)
            {
                lock (_delayeds)
                {
                    _currentDelayeds.Clear();
                    _currentDelayeds.AddRange(_delayeds.Where(item =>
                    {
                        return item.time <= Time.time;
                    }));
                    foreach (DelayedQueueItem delayedItem in _currentDelayeds)
                    {
                        _delayeds.Remove(delayedItem);
                    }
                }
                foreach (DelayedQueueItem delayedItem in _currentDelayeds)
                {
                    delayedItem.mainTreadAction();
                }
            }
        }
    }
}