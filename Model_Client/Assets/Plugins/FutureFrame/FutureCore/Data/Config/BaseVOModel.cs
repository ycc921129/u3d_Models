/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.11.3
*/

using System.Collections.Generic;

namespace FutureCore
{
    public abstract class BaseVOModel<M, V> : VOModel
        where M : VOModel, new() // class, new()
        where V : BaseVO
    {
        #region Field
        private static M m_instance;
        public static M Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new M();
                }
                return m_instance;
            }
        }

        protected string m_assetPath;
        protected List<V> m_voList;
        protected Dictionary<int, V> m_idDict;
        protected Dictionary<string, V> m_stringKeyDict;
        protected Dictionary<string, V> m_staticFieldDict;
        protected Dictionary<int, Dictionary<string, object>> m_intId_FieldKeyObjDict;
        protected Dictionary<string, Dictionary<string, object>> m_stringKey_FieldKeyObjDict;
        protected List<string> m_fieldKeyHeadFields;
        #endregion

        #region Virtual
        protected abstract V MakeVO();
        protected virtual void InitVOStaticField() { }

        protected virtual void CustomFormatData() { }
        protected virtual void CustomDispose() { }
        #endregion

        #region Process
        public override void Init()
        {
            LogUtil.LogFormat("[VOModel]Parse: {0} - {1}", VOName, DescName);

            if (IsLocalConfig())
            {
                m_assetPath = string.Concat("Data/Config/Local_", AppConst.ConfigInternalVersion);
            }
            else
            {
                m_assetPath = string.Concat("Data/Config/Client_", AppConst.ConfigInternalVersion);
            }

            new JsonConfigParser(m_assetPath, VOName, IsLocalConfig(), OnConfigParser);
        }
        public override void ReleaseAsset()
        {
            ResMgr.Instance.Unload(m_assetPath);
        }
        public override void Dispose()
        {
            CustomDispose();
            m_instance = null;

            if (m_voList != null)
            {
                m_voList.Clear();
            }
            if (m_idDict != null)
            {
                m_idDict.Clear();
            }
            if (m_stringKeyDict != null)
            {
                m_stringKeyDict.Clear();
            }
            if (m_staticFieldDict != null)
            {
                m_staticFieldDict.Clear();
            }
            if (m_intId_FieldKeyObjDict != null)
            {
                m_intId_FieldKeyObjDict.Clear();
            }
            if (m_stringKey_FieldKeyObjDict != null)
            {
                m_stringKey_FieldKeyObjDict.Clear();
            }
            if (m_fieldKeyHeadFields != null)
            {
                m_fieldKeyHeadFields.Clear();
            }

            m_voList = null;
            m_idDict = null;
            m_stringKeyDict = null;
            m_staticFieldDict = null;
            m_intId_FieldKeyObjDict = null;
            m_stringKey_FieldKeyObjDict = null;
            m_fieldKeyHeadFields = null;
        }
        #endregion

        #region Parser
        private void OnConfigParser(JsonConfigParser parser)
        {
            InitParserVOTable(parser);
            InitParserVOFieldKeyObjDict(parser);
            InitVOStaticField();

            LogUtil.LogFormat("[VOModel]Parse Complete: {0} - {1}, Data Count: {2}", VOName, DescName, GetCount());
            ParseComplete(parser);
        }
        private void InitParserVOTable(JsonConfigParser parser)
        {
            m_voList = parser.SerializeConfig<V>();
            m_idDict = new Dictionary<int, V>();
            if (HasStringKey)
            {
                m_stringKeyDict = new Dictionary<string, V>();
            }
            if (HasStaticField)
            {
                m_staticFieldDict = new Dictionary<string, V>();
            }
            for (int i = 0; i < m_voList.Count; i++)
            {
                V vo = m_voList[i];
                vo.SeVOModel(this);
                vo.index = i;

                m_idDict[vo.id] = vo;
                if (HasStringKey && vo.key != null)
                {
                    m_stringKeyDict[vo.key] = vo;
                }
                if (HasStaticField && vo.staticKey != null)
                {
                    m_staticFieldDict[vo.staticKey] = vo;
                }
            }
        }
        private void InitParserVOFieldKeyObjDict(JsonConfigParser parser)
        {
            // 字符串Key-字段Field 索引字典
            if (!IsUseFieldKeyObjDict) return;

            List<Dictionary<string, object>> fieldObjectKeyDictList = parser.SerializeConfig<Dictionary<string, object>>();
            m_intId_FieldKeyObjDict = new Dictionary<int, Dictionary<string, object>>();
            m_stringKey_FieldKeyObjDict = new Dictionary<string, Dictionary<string, object>>();
            m_fieldKeyHeadFields = new List<string>();

            Dictionary<string, object> fullFieldKeyDict = null;
            if (fieldObjectKeyDictList != null)
            {
                for (int i = 0; i < fieldObjectKeyDictList.Count; i++)
                {
                    Dictionary<string, object> itemVODict = fieldObjectKeyDictList[i];

                    object currIntId = 0;
                    if (itemVODict.TryGetValue(BaseVOField.id, out currIntId))
                    {
                        m_intId_FieldKeyObjDict.Add((int)((long)currIntId), itemVODict);

                        if (fullFieldKeyDict == null)
                        {
                            fullFieldKeyDict = itemVODict;
                        }
                        else
                        {
                            if (fullFieldKeyDict.Count < itemVODict.Count)
                            {
                                fullFieldKeyDict = itemVODict;
                            }
                        }
                    }

                    object currStringKey = null;
                    if (itemVODict.TryGetValue(BaseVOField.stringKey, out currStringKey))
                    {
                        m_stringKey_FieldKeyObjDict.Add((string)currStringKey, itemVODict);
                    }
                }

                if (fullFieldKeyDict != null)
                {
                    foreach (string fieldKey in fullFieldKeyDict.Keys)
                    {
                        m_fieldKeyHeadFields.Add(fieldKey);
                    }
                }
            }
        }
        private void ParseComplete(JsonConfigParser parser)
        {
            CustomFormatData();
            if (IsLocalConfig())
            {
                ConfigMgr.Instance.OnOnceLocalConfigComplete();
            }
            else
            {
                ConfigMgr.Instance.OnOnceClientConfigComplete();
            }
        }
        #endregion

        #region Base API
        public override VOIdentifyType GetIdentifyType()
        {
            return IdentifyType;
        }
        public override bool IsLocalConfig()
        {
            return IdentifyType == VOIdentifyType.Local;
        }
        public override int GetCount()
        {
            return m_voList.Count;
        }

        public override bool HasIndex(int index)
        {
            if (index >= 0 && index < m_voList.Count)
            {
                return true;
            }
            return false;
        }
        public override bool HasId(int id)
        {
            return m_idDict.ContainsKey(id);
        }
        public override bool HasKey(string key)
        {
            return m_stringKeyDict.ContainsKey(key);
        }
        public override bool HasStaticKey(string staticKey)
        {
            return m_staticFieldDict.ContainsKey(staticKey);
        }

        public override int GetFirstVOIndex()
        {
            return GetFirstVO().index;
        }
        public override int GetLastVOIndex()
        {
            return GetFirstVO().index;
        }
        public override int GetFirstVOId()
        {
            return GetFirstVO().id;
        }
        public override int GetLastVOId()
        {
            return GetLastVO().id;
        }

        public override object GetStaticValue(int id)
        {
            V value = null;
            if (m_idDict.TryGetValue(id, out value))
            {
                return value.staticValue;
            }
            LogUtil.LogErrorFormat("[VOModel]GetStaticValue Error: {0} Without this staticKey: {1}", VOName, id);
            return null;
        }
        public override object GetStaticValue(string staticKey)
        {
            V value = null;
            if (m_staticFieldDict.TryGetValue(staticKey, out value))
            {
                return value.staticValue;
            }
            LogUtil.LogErrorFormat("[VOModel]GetStaticValue Error: {0} Without this staticKey: {1}", VOName, staticKey);
            return null;
        }

        public override List<string> GetFieldKeyHeadFields()
        {
            return m_fieldKeyHeadFields;
        }
        public override object GetObjectByFieldKey(int id, string fieldKey)
        {
            Dictionary<string, object> valueDict = null;
            if (m_intId_FieldKeyObjDict == null || !m_intId_FieldKeyObjDict.TryGetValue(id, out valueDict))
            {
                LogUtil.LogErrorFormat("[VOModel]GetValueByFieldKey Error: {0} Without this key: {1} fieldKey: {2}", VOName, id, fieldKey);
                return null;
            }
            object value = null;
            if (valueDict == null || !valueDict.TryGetValue(fieldKey, out value))
            {
                LogUtil.LogErrorFormat("[VOModel]GetValueByFieldKey Error: {0} Without this key: {1} fieldKey: {2}", VOName, id, fieldKey);
                return null;
            }
            return value;
        }
        public override object GetObjectByFieldKey(string key, string fieldKey)
        {
            Dictionary<string, object> valueDict = null;
            if (m_stringKey_FieldKeyObjDict == null || !m_stringKey_FieldKeyObjDict.TryGetValue(key, out valueDict))
            {
                LogUtil.LogErrorFormat("[VOModel]GetValueByFieldKey Error: {0} Without this key: {1} fieldKey: {2}", VOName, key, fieldKey);
                return null;
            }
            object value = null;
            if (valueDict == null || !valueDict.TryGetValue(fieldKey, out value))
            {
                LogUtil.LogErrorFormat("[VOModel]GetValueByFieldKey Error: {0} Without this key: {1} fieldKey: {2}", VOName, key, fieldKey);
                return null;
            }
            return value;
        }
        public override string GetValueByFieldKey(int id, string fieldKey)
        {
            object value = GetObjectByFieldKey(id, fieldKey);
            if (value != null)
            {
                return (string)value;
            }
            return string.Empty;
        }
        public override string GetValueByFieldKey(string key, string fieldKey)
        {
            object value = GetObjectByFieldKey(key, fieldKey);
            if (value != null)
            {
                return (string)value;
            }
            return string.Empty;
        }

        public override IEnumerable<BaseVO> Base_GetVOList()
        {
            return m_voList;
        }

        public override BaseVO Base_GetFirstVO()
        {
            return m_voList[0];
        }
        public override BaseVO Base_GetLastVO()
        {
            return m_voList[m_voList.Count - 1];
        }
        public override BaseVO Base_GetRandomVO()
        {
            int idx = UnityEngine.Random.Range(0, m_voList.Count);
            return GetVOByIndex(idx);
        }

        public override BaseVO Base_GetVOByIndex(int index)
        {
            if (index >= 0 && index < m_voList.Count)
            {
                return m_voList[index];
            }
            LogUtil.LogErrorFormat("[VOModel]Base_GetVOByIndex Error: {0} Without this index: {1}", VOName, index);
            return null;
        }
        public override BaseVO Base_GetVO(int id)
        {
            V value = null;
            if (m_idDict.TryGetValue(id, out value))
            {
                return value;
            }
            LogUtil.LogErrorFormat("[VOModel]Base_GetVO Error: {0} Without this id: {1}", VOName, id);
            return null;
        }
        public override BaseVO Base_GetVO(string key)
        {
            V value = null;
            if (m_stringKeyDict.TryGetValue(key, out value))
            {
                return value;
            }
            LogUtil.LogErrorFormat("[VOModel]Base_GetVO Error: {0} Without this key: {1}", VOName, key);
            return null;
        }
        public override BaseVO Base_GetVOById(int id)
        {
            return GetVO(id);
        }
        public override BaseVO Base_GetVOByKey(string key)
        {
            return GetVO(key);
        }
        public override BaseVO Base_GetVOByStaticKey(string staticKey)
        {
            V value = null;
            if (m_staticFieldDict.TryGetValue(staticKey, out value))
            {
                return value;
            }
            LogUtil.LogErrorFormat("[VOModel]Base_GetVOByStaticKey Error: {0} Without this staticKey: {1}", VOName, staticKey);
            return null;
        }
        #endregion

        #region Specific API
        public List<V> GetVOList()
        {
            return m_voList;
        }
        public Dictionary<int, V> GetIdDict()
        {
            return m_idDict;
        }
        public Dictionary<string, V> GetStringKeyDict()
        {
            return m_stringKeyDict;
        }
        public Dictionary<string, V> GetStaticFieldDict()
        {
            return m_staticFieldDict;
        }

        public V GetFirstVO()
        {
            return m_voList[0];
        }
        public V GetLastVO()
        {
            return m_voList[m_voList.Count - 1];
        }
        public V GetRandomVO()
        {
            int idx = UnityEngine.Random.Range(0, m_voList.Count);
            return GetVOByIndex(idx);
        }

        public V GetVOByIndex(int index)
        {
            if (index >= 0 && index < m_voList.Count)
            {
                return m_voList[index];
            }
            LogUtil.LogErrorFormat("[VOModel]GetVOByIndex Error: {0} Without this index: {1}", VOName, index);
            return null;
        }
        public V GetVO(int id)
        {
            V value = null;
            if (m_idDict.TryGetValue(id, out value))
            {
                return value;
            }
            LogUtil.LogErrorFormat("[VOModel]GetVO Error: {0} Without this id: {1}", VOName, id);
            return null;
        }
        public V GetVO(string key)
        {
            V value = null;
            if (m_stringKeyDict.TryGetValue(key, out value))
            {
                return value;
            }
            LogUtil.LogErrorFormat("[VOModel]GetVO Error: {0} Without this key: {1}", VOName, key);
            return null;
        }
        public V GetVOById(int id)
        {
            return GetVO(id);
        }
        public V GetVOByKey(string key)
        {
            return GetVO(key);
        }
        public V GetVOByStaticKey(string staticKey)
        {
            V value = null;
            if (m_staticFieldDict.TryGetValue(staticKey, out value))
            {
                return value;
            }
            LogUtil.LogErrorFormat("[VOModel]GetVOByStaticKey Error: {0} Without this staticKey: {1}", VOName, staticKey);
            return null;
        }
        #endregion
    }
}