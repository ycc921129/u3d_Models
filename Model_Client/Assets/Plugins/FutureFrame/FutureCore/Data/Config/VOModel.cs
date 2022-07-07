/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public enum VOIdentifyType : byte
    {
        None = 0,
        Base = 1,
        All = 2,
        Local = 3,
        Client = 4,
        Server = 5,
    }

    public static class VOModelConst
    {
        public const string BaseVOName = "Base";
        public const string BaseLangueVOName = "BaseLangue";

        // 不使用默认配置的文件名
        public const string ErrorConfigVersion = "-1";
        public const string DefaultConfigVersion = "00000000000000";

        public static List<string> IgnoreFields = new List<string> { "id", "key", "keyDesc", "desc" };
    }

    public abstract class VOModel : IVOModel
    {
        #region Property
        public abstract string DescName { get; }
        public abstract string FileName { get; }
        public virtual string BaseVOName { get { return VOModelConst.BaseVOName; } }
        public abstract string VOName { get; }

        public abstract VOIdentifyType IdentifyType { get; }
        public abstract List<string> HeadFields { get; }

        public abstract bool HasStringKey { get; }
        public abstract bool HasStaticField { get; }
        public virtual bool IsUseFieldKeyObjDict { get { return false; } }
        #endregion

        #region Process
        public abstract void Init();
        public abstract void ReleaseAsset();
        public abstract void Dispose();
        #endregion

        #region Base API
        public abstract VOIdentifyType GetIdentifyType();
        public abstract bool IsLocalConfig();
        public abstract int GetCount();

        public abstract bool HasIndex(int index);
        public abstract bool HasId(int id);
        public abstract bool HasKey(string key);
        public abstract bool HasStaticKey(string staticKey);

        public abstract int GetFirstVOIndex();
        public abstract int GetLastVOIndex();
        public abstract int GetFirstVOId();
        public abstract int GetLastVOId();

        public abstract object GetStaticValue(int id);
        public abstract object GetStaticValue(string staticKey);

        public abstract List<string> GetFieldKeyHeadFields();
        public abstract object GetObjectByFieldKey(int id, string fieldKey);
        public abstract object GetObjectByFieldKey(string key, string fieldKey);
        public abstract string GetValueByFieldKey(int id, string fieldKey);
        public abstract string GetValueByFieldKey(string key, string fieldKey);

        public abstract IEnumerable<BaseVO> Base_GetVOList();

        public abstract BaseVO Base_GetFirstVO();
        public abstract BaseVO Base_GetLastVO();
        public abstract BaseVO Base_GetRandomVO();

        public abstract BaseVO Base_GetVOByIndex(int index);
        public abstract BaseVO Base_GetVO(int id);
        public abstract BaseVO Base_GetVO(string key);
        public abstract BaseVO Base_GetVOById(int id);
        public abstract BaseVO Base_GetVOByKey(string key);
        public abstract BaseVO Base_GetVOByStaticKey(string staticKey);
        #endregion
    }
}