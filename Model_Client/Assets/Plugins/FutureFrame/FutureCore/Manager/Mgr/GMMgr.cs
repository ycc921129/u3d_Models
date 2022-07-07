/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2018.1.10
*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace FutureCore
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GMCommond_Attribute : Attribute
    {
        public string gmDesc;
        public string paramDesc;
        public string cmdKey;
        public int paramNum;
        public string classTypeName;

        public GMCommond_Attribute(string gmDesc, string paramDesc, string cmdKey, int paramNum)
        {
            this.gmDesc = gmDesc;
            this.paramDesc = paramDesc;
            this.cmdKey = cmdKey;
            this.paramNum = paramNum;
        }
    }

    public sealed class GMMgr : BaseMgr<GMMgr>
    {
        #region Private

        public override void Init()
        {
            base.Init();
            if (!Channel.IsRelease)
            {
                //InitAllCommond(); 
            }
        }

        public override void StartUp()
        {
            base.StartUp();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion Private

        private Dictionary<string, GMCommond_Attribute> m_commonds;
        private Dictionary<string, MethodInfo> m_methods;
        private List<string> m_tempParams;

        private void InitAllCommond()
        {
            m_commonds = new Dictionary<string, GMCommond_Attribute>();
            m_methods = new Dictionary<string, MethodInfo>();
            m_tempParams = new List<string>();

            Type gmAttribute = typeof(GMCommond_Attribute);
            m_methods.Clear();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assembly assembly = null;
            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assemblie = assemblies[i];
                if (assemblie.GetName(false).Name == "Assembly-CSharp")
                {
                    assembly = assemblie;
                    break;
                }
            }

            if (assembly == null) return;

            AddCommond(assembly, "ProjectApp.GM.GM_Frame", gmAttribute);
            AddCommond(assembly, "ProjectApp.GM.GM_Logic", gmAttribute);
            AddCommond(assembly, "ProjectApp.GM.GM_Game", gmAttribute);
        }

        private void AddCommond(Assembly assembly, string classTypeName, Type gmAttribute)
        {
            Type gmClassType = assembly.GetType(classTypeName, false, false);
            if (gmClassType != null)
            {
                AddCommond(classTypeName, gmClassType, gmAttribute);
            }
            else
            {
                LogUtil.LogFormat ("[GMMgr]找不到此命令类型: {0}", classTypeName);
            }
        }

        private void AddCommond(string classTypeName, Type type, Type gmAttribute)
        {
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                object[] attributes = method.GetCustomAttributes(gmAttribute, false);
                if (attributes != null && attributes.Length > 0)
                {
                    GMCommond_Attribute gmc = attributes[0] as GMCommond_Attribute;
                    gmc.classTypeName = classTypeName;
                    m_commonds.Add(gmc.cmdKey, gmc);
                    m_methods.Add(gmc.cmdKey, method);
                }
            }
        }

        public void OpenGUI()
        {
            if (!Channel.IsRelease)
            {
                AssistDebugMgr.Instance.EnabledDebuggerConsole(true);
            }
        }

        public void CloseGUI()
        {
            if (!Channel.IsRelease)
            {
                AssistDebugMgr.Instance.EnabledDebuggerConsole(false);
            }
        }

        public Dictionary<string, GMCommond_Attribute> GetAllCommond()
        {
            return m_commonds;
        }

        public List<GMCommond_Attribute> GetAllCommondByClass(string classTypeName)
        {
            List<GMCommond_Attribute> list = new List<GMCommond_Attribute>();
            foreach (GMCommond_Attribute item in m_commonds.Values)
            {
                if (item.classTypeName == classTypeName)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public object CallCommond(string cmdKey, string[] allParam)
        {
            if (m_commonds.ContainsKey(cmdKey))
            {
                GMCommond_Attribute gmc = m_commonds[cmdKey];
                MethodInfo method = m_methods[cmdKey];

                if (gmc.paramNum == 0)
                {
                    string[] invokeParam = new string[] { string.Join(" ", allParam) };
                    return method.Invoke(this, new object[] { invokeParam });
                }
                else
                {
                    return method.Invoke(this, new object[] { allParam });
                }
            }
            else
            {
                LogUtil.LogErrorFormat("[GMMgr]找不到该命令: {0}", cmdKey);
                return null;
            }
        }

        public object CallCommond(string cmdKey, string input)
        {
            string[] tmpStrs = input.Split(' ');
            if (m_commonds.ContainsKey(cmdKey))
            {
                GMCommond_Attribute gmc = m_commonds[cmdKey];
                MethodInfo method = m_methods[cmdKey];

                m_tempParams.Clear();
                List<string> param = m_tempParams;
                for (int i = 0; i < tmpStrs.Length; i++)
                {
                    string tmpStr = tmpStrs[i];
                    if (!string.IsNullOrWhiteSpace(tmpStr))
                    {
                        param.Add(tmpStr);
                    }
                }

                if (gmc.paramNum == 0)
                {
                    string[] invokeParam = new string[] { string.Join(" ", param) };
                    return method.Invoke(this, new object[] { invokeParam });
                }
                else
                {
                    return method.Invoke(this, new object[] { param.ToArray() });
                }
            }
            else
            {
                LogUtil.LogErrorFormat("[GMMgr]找不到该命令: {0}", cmdKey);
                return null;
            }
        }

        public object CallCommond(string userInput)
        {
            string[] tmpStrs = userInput.Split(' ');
            string cmdKey = tmpStrs[0];
            if (m_commonds.ContainsKey(cmdKey))
            {
                GMCommond_Attribute gmc = m_commonds[cmdKey];
                MethodInfo method = m_methods[cmdKey];

                m_tempParams.Clear();
                List<string> param = m_tempParams;
                for (int i = 0; i < tmpStrs.Length; i++)
                {
                    string tmpStr = tmpStrs[i];
                    if (!string.IsNullOrWhiteSpace(tmpStr))
                    {
                        param.Add(tmpStr);
                    }
                }

                if (gmc.paramNum == 0)
                {
                    string[] invokeParam = new string[] { string.Join(" ", param) };
                    return method.Invoke(this, new object[] { invokeParam });
                }
                else
                {
                    return method.Invoke(this, new object[] { param.ToArray() });
                }
            }
            else
            {
                LogUtil.LogErrorFormat("[GMMgr]找不到该命令: {0}", cmdKey);
                return null;
            }
        }
    }
}