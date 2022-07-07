using FairyGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class FGUIAutoBindEventCodeCreatorTool
    {
        [MenuItem("[FC Project]/FGUI/BindEventCode/复制生成事件绑定代码", false, 0)]
        public static void Creator()
        {
            string className = null;
            string forldName = null;
            bool noSelectionObj = true;
            if (Selection.objects.Length != 1)
            {
                noSelectionObj = false;
            }
            else
            {
                string scriptPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                if (!scriptPath.EndsWith(".cs"))
                {
                    noSelectionObj = false;
                }

                className = Path.GetFileName(scriptPath);
                className = className.Substring(0, className.IndexOf(Path.GetExtension(scriptPath)));

                forldName = Path.GetDirectoryName(scriptPath);
                forldName = forldName.Substring(forldName.LastIndexOfAny(new char[] { '\\', '/' }) + 1);
            }
            if (!noSelectionObj)
            {
                Debug.Log("[FGUIEventBindCodeCreatorTool]请选择代码");
                return;
            }

            string dllpath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOfAny(new char[] { '\\', '/' }));

            dllpath += @"\Library\ScriptAssemblies\Assembly-CSharp.dll";

            Assembly assembly = Assembly.LoadFile(dllpath);

            if (assembly == null)
            {
                Debug.Log("[FGUIEventBindCodeCreatorTool]程序集加载失败!");
                return;
            }

            className = "UI." + forldName + "." + className;
            Type type = assembly.GetType(className);
            if (type == null)
            {
                Debug.Log("[FGUIEventBindCodeCreatorTool]类加载失败!" + className);
                return;
            }

            string fieldstrs = string.Empty;

            Dictionary<Type, bool> checkTypes = new Dictionary<Type, bool>();
            List<string> events = new List<string>();
            string code = string.Empty;
            //得到字段
            FindHasBindEvent(type, "ui", "ui", ref code, events, checkTypes);

            string bindEventStr = @"private void AutoCreator_BindEvent()" +
                                "\r\n{" + "\r\n";
            foreach (var item in events)
            {
                bindEventStr += "    " + item + "\r\n";
            }
            bindEventStr += "}" + "\r\n";

            code = "#region 自动绑定事件\r\n" + bindEventStr + code + "#endregion";

            UnityEngine.GUIUtility.systemCopyBuffer = code;

            Debug.Log("[FGUIEventBindCodeCreatorTool]复制绑定事件代码");
        }

        private static void FindHasBindEvent(Type type, string fieldFullName, string fieldName, ref string code, List<string> events, Dictionary<Type, bool> checkType)
        {
            //正在检查中，不处理
            if (checkType.ContainsKey(type) || type.BaseType == null)
            {
                return;
            }

            if (type.Equals(typeof(GButton)) || type.BaseType.Equals(typeof(GButton)))
            {
                string methodName = "OnClick_" + fieldName;
                events.Add(fieldFullName + ".onClick.Set(" + methodName + ");");
                code += "\r\n";
                code += @"private void " + methodName + "(EventContext context)\r\n" +
                    "{\r\n\r\n" +
                    "}\r\n";
                //Debug.Log("找到事件!" + code);
                return;
            }

            if (type.Equals(typeof(GList)) || type.BaseType.Equals(typeof(GList)))
            {
                string methodName = fieldName + "Renderer";
                events.Add(fieldFullName + ".itemRenderer = " + methodName + ";");
                code += "\r\n";
                code += @"private void " + methodName + "(int index, GObject item)\r\n" +
                    "   {\r\n\r\n" +
                    "   }\r\n";
                //Debug.Log("找到事件!" + code);
                return;
            }

            //添加到正在检查类型列表
            checkType.Add(type, false);

            //遍历处理子类型
            FieldInfo[] subFields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo item in subFields)
            {
                FindHasBindEvent(item.FieldType, fieldFullName + "." + item.Name, item.Name, ref code, events, checkType);
            }
            checkType.Remove(type);
            return;
        }
    }
}