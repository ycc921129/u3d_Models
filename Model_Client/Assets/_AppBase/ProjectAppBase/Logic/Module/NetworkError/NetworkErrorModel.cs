using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class NetworkErrorModel : BaseModel
    {
        /// <summary>
        /// 是否不再提示
        /// </summary>
        public bool isNoLongerPrompt = false;

        /// <summary>
        /// 是否强联网
        /// </summary>
        public bool isStrongConnect = true;

        #region 生命周期
        protected override void OnInit()
        {
        }

        protected override void OnDispose()
        {
        }

        protected override void OnReset()
        {
        }
        #endregion

        #region 读取数据
        protected override void OnReadData()
        {
        }
        #endregion

        #region 本地存储
        /*
        private LSData lsData;

        private void UpdateLocalStorage(Action updateFunc)
        {
            if (lsData == null)
            {
                lsData = ReadLocalStorage<LSData>() as LSData;
                if (lsData == null)
                {
                    lsData = new LSData();
                }
            }
            if (updateFunc != null)
            {
                updateFunc();
            }
            WriteLocalStorage();
        }
        */

        protected override void WriteLocalStorage()
        {
            //WriteLocalStorage(lsData);
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            //modelDispatcher.AddListener(ModelMsg.XXX, OnXXX);
        }

        protected override void RemoveListener()
        {
            //modelDispatcher.RemoveListener(ModelMsg.XXX, OnXXX);
        }
        #endregion
    }
}