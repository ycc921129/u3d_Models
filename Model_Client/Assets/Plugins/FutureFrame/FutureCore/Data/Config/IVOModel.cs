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
    public interface IVOModel
    {
        #region Process
        void Init();
        void ReleaseAsset();
        void Dispose();
        #endregion

        #region Base API
        VOIdentifyType GetIdentifyType();
        bool IsLocalConfig();
        int GetCount();

        bool HasIndex(int index);
        bool HasId(int id);
        bool HasKey(string key);
        bool HasStaticKey(string staticKey);

        int GetFirstVOIndex();
        int GetLastVOIndex();
        int GetFirstVOId();
        int GetLastVOId();

        object GetStaticValue(int id);
        object GetStaticValue(string staticKey);

        List<string> GetFieldKeyHeadFields();
        object GetObjectByFieldKey(int id, string fieldKey);
        object GetObjectByFieldKey(string key, string fieldKey);
        string GetValueByFieldKey(int id, string fieldKey);
        string GetValueByFieldKey(string key, string fieldKey);

        IEnumerable<BaseVO> Base_GetVOList();

        BaseVO Base_GetFirstVO();
        BaseVO Base_GetLastVO();
        BaseVO Base_GetRandomVO();

        BaseVO Base_GetVOByIndex(int index);
        BaseVO Base_GetVO(int id);
        BaseVO Base_GetVO(string key);
        BaseVO Base_GetVOById(int id);
        BaseVO Base_GetVOByKey(string key);
        BaseVO Base_GetVOByStaticKey(string staticKey);
        #endregion
    }
}