using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public class ProjectCreatetMVCWindow : EditorWindow
    {
        private static string mvcClassName;
        private static string foldePath;

        [MenuItem("[FC Project]/MVC/创建MVC代码模板", false, 0)]
        private static void ShowWindow()
        {
            ProjectCreatetMVCWindow window = GetWindow<ProjectCreatetMVCWindow>(true, "创建MVC代码模板窗口");
            window.minSize = new Vector2(200f, 100f);
            window.maxSize = window.minSize;
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("请输入要生成的模块名:");
            GUILayout.Space(5);
            mvcClassName = EditorGUILayout.TextField(mvcClassName, GUILayout.Height(20));
            GUILayout.Space(10);
            if (GUILayout.Button("创建", GUILayout.Height(25)))
            {
                CreateMVC(mvcClassName);
            }

            EditorGUILayout.EndVertical();
        }

        private static void CreateMVC(string mvcName)
        {
            if (!string.IsNullOrEmpty(mvcName))
            {
                string mvcPath = "/_App/ProjectApp/Logic/ModuleUI/" + mvcName;
                foldePath = Application.dataPath + mvcPath;
                if (Directory.Exists(foldePath))
                {
                    Debug.LogError("[ProjectCreatetMVCWindow]Folde is exist!!!: " + foldePath);
                    Debug.LogError("[ProjectCreatetMVCWindow]Create MVC Fail:" + mvcName);
                    return;
                }
                Directory.CreateDirectory(foldePath);

                CreateModel(mvcName);
                CreateUI(mvcName);
                CreateCtrl(mvcName);
                CreateUICtrl(mvcName);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                SelectObject("Assets/" + mvcPath);
                Debug.Log("[ProjectCreatetMVCWindow]创建MVC代码模板完成");
            }
        }

        private static void SelectObject(string path)
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(path);
            if (obj == null) return;

            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }

        private static void CreateModel(string className)
        {
            string modelClassStr =
@"using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class #ClassName#Model : BaseModel
    {
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
}";

            string replaceClassName = "#ClassName#";
            modelClassStr = modelClassStr.Replace(replaceClassName, className);
            string targetPath = foldePath + "/" + className + "Model.cs";
            File.WriteAllText(targetPath, modelClassStr, new UTF8Encoding(false));
        }

        private static void CreateUI(string className)
        {
            string uiClassStr =
@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FutureCore;
using FairyGUI;
using ProjectApp.Data;
using ProjectApp.Protocol;
using UI;
//using UI.G000_#ClassName#;

namespace ProjectApp
{
    public class #ClassName#UI : BaseUI
    {
        private #ClassName#UICtrl ctrl;
        private #ClassName#Model model;
        //private UI.G000_#ClassName#.UI_#ClassName# ui;

        public #ClassName#UI(#ClassName#UICtrl ctrl) : base(ctrl)
        {
            //uiName = UIConst.#ClassName#UI;
            this.ctrl = ctrl;
        }

        protected override void SetUIInfo(UIInfo uiInfo)
        {
            uiInfo.packageName = ""G000_#ClassName#"";
            uiInfo.assetName = ""UI_#ClassName#"";
            uiInfo.layerType = UILayerType.Normal;
            uiInfo.isNeedOpenAnim = true;
            uiInfo.isNeedCloseAnim = true;
            uiInfo.isNeedUIMask = true;
        }

        #region 生命周期
        protected override void OnInit()
        {
            //model = moduleMgr.GetModel(ModelConst.#ClassName#Model) as #ClassName#Model;
        }

        protected override void OnClose()
        {
        }

        protected override void OnBind()
        {
            //ui = baseUI as UI.G000_#ClassName#.UI_#ClassName#;
        }

        protected override void OnOpenBefore(object args)
        {
        }

        protected override void OnOpen(object args)
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnDisplay(object args)
        {
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
}";

            string replaceClassName = "#ClassName#";
            uiClassStr = uiClassStr.Replace(replaceClassName, className);
            string targetPath = foldePath + "/" + className + "UI.cs";
            File.WriteAllText(targetPath, uiClassStr, new UTF8Encoding(false));
        }

        private static void CreateCtrl(string className)
        {
            string ctrlClassStr =
@"using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class #ClassName#Ctrl : BaseCtrl
    {
        public static #ClassName#Ctrl Instance { get; private set; }

        private #ClassName#Model model;

        #region 生命周期
        protected override void OnInit()
        {
            Instance = this;
            //model = moduleMgr.GetModel(ModelConst.#ClassName#Model) as #ClassName#Model;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }
        #endregion

        #region 消息
        protected override void AddListener()
        {
            //ctrlDispatcher.AddListener(CtrlMsg.XXX, OnXXX);
        }
        protected override void RemoveListener()
        {
            //ctrlDispatcher.RemoveListener(CtrlMsg.XXX, OnXXX);
        }

        protected override void AddServerListener()
        {
            //wsNetDispatcher.AddListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }
        protected override void RemoveServerListener()
        {
            //wsNetDispatcher.RemoveListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }
        #endregion

        private void OnS2CXXXResp(BaseS2CJsonProto protoMsg)
        {
        }
    }
}";

            string replaceClassName = "#ClassName#";
            ctrlClassStr = ctrlClassStr.Replace(replaceClassName, className);
            string targetPath = foldePath + "/" + className + "Ctrl.cs";
            File.WriteAllText(targetPath, ctrlClassStr, new UTF8Encoding(false));
        }

        private static void CreateUICtrl(string className)
        {
            string uiCtrlClassStr =
@"using System.Collections;
using System.Collections.Generic;
using FutureCore;
using ProjectApp.Data;
using ProjectApp.Protocol;

namespace ProjectApp
{
    public class #ClassName#UICtrl : BaseUICtrl
    {
        private #ClassName#UI ui;
        private #ClassName#Model model;

        private uint openUIMsg = 0; //UICtrlMsg.#ClassName#UI_Open;
        private uint closeUIMsg = 0; //UICtrlMsg.#ClassName#UI_Close;

        #region 生命周期
        protected override void OnInit()
        {
            //model = moduleMgr.GetModel(ModelConst.#ClassName#Model) as #ClassName#Model;
        }

        protected override void OnDispose()
        {
        }

        public override void OpenUI(object args = null)
        {
            if (ui == null)
            {
                ui = new #ClassName#UI(this);
                ui.Open(args);
            }
        }

        public override void CloseUI(object args = null)
        {
            if (ui != null && !ui.isClose)
            {
                ui.Close();
            }
            ui = null;
        }
        #endregion

        #region 消息
        public override uint GetOpenUIMsg(string uiName)
        {
            return openUIMsg;
        }
        public override uint GetCloseUIMsg(string uiName)
        {
            return closeUIMsg;
        }

        protected override void AddListener()
        {
            //uiCtrlDispatcher.AddListener(openUIMsg, OpenUI);
            //uiCtrlDispatcher.AddListener(closeUIMsg, CloseUI);
        }
        protected override void RemoveListener()
        {
            //uiCtrlDispatcher.RemoveListener(openUIMsg, OpenUI);
            //uiCtrlDispatcher.RemoveListener(closeUIMsg, CloseUI);
        }

        protected override void AddServerListener()
        {
            //wsNetDispatcher.AddListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }
        protected override void RemoveServerListener()
        {
            //wsNetDispatcher.RemoveListener(WSNetMsg.S2C_XXX, OnS2CXXXResp);
        }
        #endregion

        private void OnS2CXXXResp(BaseS2CJsonProto protoMsg)
        {
        }
    }
}";

            string replaceClassName = "#ClassName#";
            uiCtrlClassStr = uiCtrlClassStr.Replace(replaceClassName, className);
            string targetPath = foldePath + "/" + className + "UICtrl.cs";
            File.WriteAllText(targetPath, uiCtrlClassStr, new UTF8Encoding(false));
        }
    }
}