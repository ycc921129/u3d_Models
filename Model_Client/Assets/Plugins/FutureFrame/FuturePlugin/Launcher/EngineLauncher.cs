/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

//                                  
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                                                        
//        观自在菩萨，行深般若波罗蜜多时，照见五蕴皆空，渡一切苦厄。
//                                                        

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace FuturePlugin
{
    [DefaultExecutionOrder(-10000)]
    public class EngineLauncher : MonoBehaviour
    {
        public static EngineLauncher Instance { get; private set; }

        public Reporter reporter;
        private Action appMainFunc;

        public void Init(Action appMainFunc)
        {
            InitAssistSetting();
            LogUtil.LogFormat("[EngineLauncher]Awake Time:{0}", Time.unscaledTime);

            Instance = this;

            this.appMainFunc = appMainFunc;
            Launcher();
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void InitAssistSetting()
        {
            LogUtil.EnableLog(LauncherConst.IsEnabledDebugLog, JsonConvert.SerializeObject);

            if (LauncherConst.IsShowUnityLogsViewerReporter)
            {
                string path = "Preset/UnityLogsViewer/Reporter";
                GameObject reporterGo = Instantiate(Resources.Load<GameObject>(path));
                reporterGo.name = "Reporter";
                reporterGo.transform.SetParent(gameObject.transform, false);
                reporterGo.SetActive(LauncherConst.IsEnabledDebugLog);
                reporter = reporterGo.GetComponent<Reporter>();
                reporter.numOfCircleToShow = LauncherConst.ShowUnityLogsViewerReporterCount;
                LogUtil.Log("[EngineLauncher]Init UnityLogsViewer");
            }
        }

        private void Launcher()
        {
            GameObject engineEventSystemGo = new GameObject("[EngineEventSystem]");
            engineEventSystemGo.AddComponent<EngineEventSystem>();
            engineEventSystemGo.transform.SetParent(FutureFrame.Instance.transform, false);
            LogUtil.Log("[EngineLauncher]Launcher EngineEventSystem");

            StartLauncher();
        }

        private void StartLauncher()
        {
            ParseAppInfo();
            EnterAppMain();
        }

        private void ParseAppInfo()
        {
            AppInfoParser.Init();
            LogUtil.LogFormat("[EngineLauncher]ParseAppInfo Time:{0}", Time.unscaledTime);
        }

        private void EnterAppMain()
        {
            appMainFunc();
            appMainFunc = null;
        }
    }
}