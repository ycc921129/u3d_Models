/****************************************************************************
* ScriptType: 框架 - 基础业务
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

/****************************************************************************
*【收藏夹】
* [Unity]
* Unity问答平台:   https://answers.unity.com/index.html
* Unity文档:      https://docs.unity3d.com/Manual/index.html
* Unity中文文档:   https://connect.unity.com/doc/
* Unity离线文档:   https://docs.unity3d.com/Manual/OfflineDocumentation.html
* 
* [资料]
* PBR的原理和实现:  https://www.cnblogs.com/timlly/p/10631718.html
* 李永乐老师科普:   https://weibo.com/ttarticle/p/show?id=2309404390802737463547
* 
* [程序]
* 变量名定义搜索:   https://unbug.github.io/codelf/
* Dota2维基zh:    https://dota2-zh.gamepedia.com/Dota_2_Wiki
* Dota2维基en:    https://dota2.gamepedia.com/Dota_2_Wiki
* Dota2资源:      http://www.dota2.com/workshop/requirements
* 魔兽世界API对照:  http://api.battlenet.top/api/
* StackOverflow:  https://stackoverflow.com
* IL在线编译:      https://sharplab.io
* .Net源码:       https://referencesource.microsoft.com/
* Json格式:       https://www.json.cn
* 绘制注释:        http://asciiflow.com/
* 正则快查:        https://github.com/ziishaned/learn-regex/blob/master/translations/README-cn.md
* 正则测试:        http://tool.oschina.net/regex/
* 正则可视化:       https://blog.robertelder.org/regular-expression-visualizer/
* 随机密码生成:     https://suijimimashengcheng.51240.com
* 在线公式工具:     https://www.desmos.com/
* 图形计算器:      https://www.geogebra.org/
* 编辑器扩展手册:   http://49.233.81.186/guicreation.html
* 
* [效果]
* CSS缓动函数速查:  http://www.xuanfengge.com/easeing/easeing/
* 球体模拟缓动函数:  http://robertpenner.com/easing/easing_demo.html
* 函数可视化绘制:   https://www.desmos.com
* 
* [美术]
* 3D动画库:        https://www.mixamo.com
* 压缩图片:        https://tinypng.com
* 生成法线贴图:     http://cpetry.github.io/NormalMap-Online/
* 
* [产品]
* 谷歌商店:        https://play.google.com/store/apps/details?id=com.games.game
* 语言编码表：      http://www.lingoes.cn/zh/translator/langcode.htm
****************************************************************************/

/****************************************************************************
*【Unity特性】
* [FormerlySerializedAs]: 序列化值另存为关联
* [SelectionBase]: 点击选择重定向
* [ContextMenu]: 从面板中调用方法
* [Header]: 字段头注释
* [Tooltip]: 字段描述
* [Space]: 字段间隔
* [Range]: 字段范围
* [ProgressBar(0, 20)]: 表示取值范围
* [DisallowMultipleComponent]: 防止一个对象上多次挂载同一组件
* [ExecuteAlways]: 类在运行模式和编辑模式下都会运行
* [SerializeField]: 序列化字段
* [NonSerialized]: 不序列化字段
* [HideInInspector]: 隐藏字段显示
* [EnumToggleButtons]: 标记下面的字段值以分页按钮显示
* [BoxGroup("Member referencing")]: 分组显示
* [FoldoutGroup("设置窗口")]: 折页
* [OnValueChanged("ValueChange")]: 当属性值发生变化时回调函数，如ValueChange即为回调函数
* [EnumPaging]: 枚举向左向右箭头
* [TabGroup("UI1")]: 在默认分组上以一个tab标示一个属性
* [TableList]: 对List和Array使用TableList属性标签，可以在面板中以表的形式进行绘制
* [ValueDropdown("test")]: 创建一个属性的下拉菜单
****************************************************************************/

using System;
using System.Reflection;
using FutureCore;
using FuturePlugin;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectApp.Main
{
    public static class MainLauncher
    {
        public static bool IsAutoLauncher = true;
        private const string MainScene = "0_Main";
        private static bool IsInMain;

        [Beebyte.Obfuscator.Skip]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SceneMain()
        {
            if (!IsAutoLauncher) return;
            if (SceneManager.GetActiveScene().name != MainScene) return;

            LogUtil.Log("<color=green>[MainLauncher]SceneMain</color>");
            Main();
        }

        [Beebyte.Obfuscator.Skip]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void SceneLoadAfter()
        {
            if (!IsAutoLauncher) return;
            if (SceneManager.GetActiveScene().name != MainScene) return;

            LogUtil.Log("<color=green>[MainLauncher]SceneLoadAfter</color>");
        }

        public static void Main()
        {
            if (IsInMain) return;

            LogUtil.Log("<color=green>[MainLauncher]Main</color>");

            // 版本检测
            if (!Application.unityVersion.StartsWith("2019.4"))  
            {
                LogUtil.LogWarning("[MainLauncher]UnityVersion mismatching");
            }

            // 进入框架程序
            IsInMain = true;
            // 应用启动时间
            AppConst.LaunchDateTime = DateTime.Now;

            // 外观入口
            AppFacade.MainFunc();
            // 初始化平台
            SDKGlobal.InitPlatform();
            // 启动引擎层
            AppLauncher();
        }

        private static void AppLauncher()
        {
            AppObjConst.FutureFrameGo = new GameObject(AppObjConst.FutureFrameGoName + "_" + FrameConst.Version + "_" + FrameConst.Timestamp);
            AppObjConst.FutureFrameGo.AddComponent<FutureFrame>();
            Unity3dUtil.SetDontDestroyOnLoad(AppObjConst.FutureFrameGo);

            AppObjConst.LauncherGo = new GameObject(AppObjConst.LauncherGoName);
            AppObjConst.LauncherGo.SetParent(AppObjConst.FutureFrameGo);
            AppObjConst.LauncherGo.AddComponent<EngineLauncher>().Init(AppMain);
        }

        private static void AppMain()
        {
            Assembly appAssembly = Assembly.GetExecutingAssembly();
            Type appMainClass = appAssembly.GetType("ProjectApp.Main.AppMain");
            MethodInfo mainFunc = appMainClass.GetMethod("Main", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);
            mainFunc.Invoke(null, null);
        }

        private static void ScreenOrientationSetting()
        {
            // 有一定几率下 会在一些机型下引起崩溃 屏幕旋转设置由平台层实现
            // 设置屏幕旋转属性
            //Screen.orientation = ScreenOrientation.Portrait;
            //Screen.autorotateToPortrait = false;
            //Screen.autorotateToPortraitUpsideDown = false;
            //Screen.autorotateToLandscapeLeft = false;
            //Screen.autorotateToLandscapeRight = false;
        }
    }
}