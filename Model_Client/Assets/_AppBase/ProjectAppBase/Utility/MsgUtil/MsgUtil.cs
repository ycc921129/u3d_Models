/****************************************************************************
* ScriptType: 框架 - 基础业务
* 请勿修改!!!
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using FutureCore;

namespace ProjectApp
{
    public static class MsgUtil
    {
        private static Type UintType = typeof(uint);
        private static Dictionary<Type, FieldInfo[]> MsgDict = new Dictionary<Type, FieldInfo[]>();

        private static Type AppMsgType = typeof(AppMsg);
        private static Type MainThreadMsgType = typeof(MainThreadMsg);
        private static Type ChannelRawMsgType = typeof(ChannelRawMsg);
        private static Type ChannelMsgType = typeof(ChannelMsg);
        private static Type CtrlMsgType = typeof(CtrlMsg);
        private static Type UICtrlMsgType = typeof(UICtrlMsg);
        private static Type ModelMsgType = typeof(ModelMsg);
        private static Type GamePlayMsgType = typeof(GameMsg);
        private static Type RedDotMsgType = typeof(RedDotMsg);

        public static uint FullName2Id(string fullMsgName)
        {
            string[] temp = fullMsgName.Split('.');
            string className = temp[0];
            string msgName = temp[1];
            return Name2Id(className, msgName);
        }

        public static uint Name2Id(string msgClassName, string msgName)
        {
            Type msgType = GetTypeByClassName(msgClassName);
            if (msgType == null)
            {
                return 0;
            }
            return Name2Id(msgType, msgName);
        }

        public static uint Name2Id(Type msgType, string msgName)
        {
            if (!MsgDict.ContainsKey(msgType))
            {
                MsgDict.Add(msgType, msgType.GetFields());
            }
            FieldInfo[] fields = MsgDict[msgType];

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == UintType)
                {
                    if (field.Name == msgName)
                    {
                        return (uint)field.GetValue(0);
                    }
                }
            }
            return 0;
        }

        public static string Id2Name(string msgClassName, uint id)
        {
            Type msgType = GetTypeByClassName(msgClassName);
            if (msgType == null)
            {
                return null;
            }
            return Id2Name(msgType, id);
        }

        public static string Id2Name(Type msgType, uint id)
        {
            if (!MsgDict.ContainsKey(msgType))
            {
                MsgDict.Add(msgType, msgType.GetFields());
            }
            FieldInfo[] fields = MsgDict[msgType];

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == UintType)
                {
                    uint fieldId = (uint)field.GetValue(0);
                    if (fieldId == id)
                    {
                        return msgType.Name + "." + field.Name;
                    }
                }
            }
            return null;
        }

        public static void Dispatch(string fullMsgName)
        {
            string[] temp = fullMsgName.Split('.');
            string className = temp[0];
            string msgName = temp[1];
            Dispatch(className, Name2Id(className, msgName));
        }

        public static void Dispatch(Type msgType, uint id)
        {
            Dispatch(GetClassNameByType(msgType), id);
        }

        public static void Dispatch(string className, uint id)
        {
            if (className == AppMsg.NAME)
            {
                AppDispatcher.Instance.Dispatch(id);
            }
            if (className == MainThreadMsg.NAME)
            {
                MainThreadDispatcher.Instance.Dispatch(id);
            }
            if (className == ChannelRawMsg.NAME)
            {
                ChannelDispatcher.Instance.Dispatch(id);
            }
            if (className == ChannelMsg.NAME)
            {
                ChannelDispatcher.Instance.Dispatch(id);
            }
            if (className == CtrlMsg.NAME)
            {
                CtrlDispatcher.Instance.Dispatch(id);
            }
            if (className == UICtrlMsg.NAME)
            {
                UICtrlDispatcher.Instance.Dispatch(id);
            }
            if (className == ModelMsg.NAME)
            {
                ModelDispatcher.Instance.Dispatch(id);
            }
            if (className == GameMsg.NAME)
            {
                GameDispatcher.Instance.Dispatch(id);
            }
            if (className == RedDotMsg.NAME)
            {
                RedDotDispatcher.Instance.Dispatch(id);
            }
        }

        public static string GetClassNameByType(Type type)
        {
            string className = null;
            if (type == AppMsgType)
            {
                className = AppMsg.NAME;
            }
            if (type == MainThreadMsgType)
            {
                className = MainThreadMsg.NAME;
            }
            if (type == ChannelRawMsgType)
            {
                className = ChannelRawMsg.NAME;
            }
            if (type == ChannelMsgType)
            {
                className = ChannelMsg.NAME;
            }
            if (type == CtrlMsgType)
            {
                className = CtrlMsg.NAME;
            }
            if (type == UICtrlMsgType)
            {
                className = UICtrlMsg.NAME;
            }
            if (type == ModelMsgType)
            {
                className = ModelMsg.NAME;
            }
            if (type == GamePlayMsgType)
            {
                className = GameMsg.NAME;
            }
            if (type == RedDotMsgType)
            {
                className = RedDotMsg.NAME;
            }
            return className;
        }

        public static Type GetTypeByClassName(string className)
        {
            Type msgType = null;
            switch (className)
            {
                case AppMsg.NAME:
                    msgType = AppMsgType;
                    break;
                case MainThreadMsg.NAME:
                    msgType = MainThreadMsgType;
                    break;
                case ChannelRawMsg.NAME:
                    msgType = ChannelRawMsgType;
                    break;
                case ChannelMsg.NAME:
                    msgType = ChannelMsgType;
                    break;
                case CtrlMsg.NAME:
                    msgType = CtrlMsgType;
                    break;
                case UICtrlMsg.NAME:
                    msgType = UICtrlMsgType;
                    break;
                case ModelMsg.NAME:
                    msgType = ModelMsgType;
                    break;
                case GameMsg.NAME:
                    msgType = GamePlayMsgType;
                    break;
                case RedDotMsg.NAME:
                    msgType = RedDotMsgType;
                    break;
            }
            return msgType;
        }
    }
}