using System;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MonoHookTest : MonoBehaviour
{
    public Button btn;
    public Text   txtInfo;

    private int _msgId;

    private void Awake()
    {
        btn.onClick.AddListener(OnBtnClick);
    }
    private void Start()
    {
        Debug.Log("普通日志");
        Debug.LogError("普通错误");

        _msgId = PinnedLog.AddMsg("我是不会被清掉的日志");

        // 实例方法替换测试
        InstanceMethodTest InstanceTest = new InstanceMethodTest();
        InstanceTest.Test();

        // 属性替换测试
        PropertyHookTest propTest = new PropertyHookTest();
        propTest.Test();

        // 参数类型是私有类型的方法替换测试
        PrivateTypeArgMethodTest privateTypeArgMethodTest = new PrivateTypeArgMethodTest();
        privateTypeArgMethodTest.Test();

        // 构造函数替换测试
        CtorHookTest ctorHookTest = new CtorHookTest();
        ctorHookTest.Test();
    }

    public void OnBtnClick()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("pointer size:{0}\r\n", System.IntPtr.Size);
        sb.AppendFormat("operation name:{0}\r\n", SystemInfo.operatingSystem);
        sb.AppendFormat("processorType:{0}\r\n", SystemInfo.processorType);
        sb.AppendLine();
        txtInfo.text = sb.ToString();

        // 测试实例方法替换
        InstanceMethodTest InstanceTest = new InstanceMethodTest();
        sb.Length = 0;
        string info = InstanceTest.Test();
        sb.AppendLine(info);
        txtInfo.text += sb.ToString();

        PinnedLog.RemoveMsg(_msgId);
        PinnedLog.ClearAll();
    }

    //[DidReloadScripts] // 最好脚本加载完毕就 hook
    static void InstallHook()
    {
        MethodHooker _hooker = null;
        if (_hooker == null)
        {
            Type type = Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");
            // 找到需要 Hook 的方法
            MethodInfo miTarget = type.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);

            type = typeof(PinnedLog);

            // 找到被替换成的新方法
            MethodInfo miReplacement = type.GetMethod("NewClearLog", BindingFlags.Static | BindingFlags.NonPublic);

            // 这个方法是用来调用原始方法的
            MethodInfo miProxy = type.GetMethod("ProxyClearLog", BindingFlags.Static | BindingFlags.NonPublic);

            // 创建一个 Hooker 并 Install 就OK啦, 之后无论哪个代码再调用原始方法都会重定向到
            //  我们写的方法ヾ(ﾟ∀ﾟゞ)
            _hooker = new MethodHooker(miTarget, miReplacement, miProxy);
            _hooker.Install();
        }
    }
}
