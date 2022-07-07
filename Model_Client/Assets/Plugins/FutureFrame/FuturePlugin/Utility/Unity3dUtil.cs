/****************************************************************************
* ScriptType: �����
* �����޸�!!!
****************************************************************************/

using UnityEngine;
using UObject = UnityEngine.Object;

namespace FuturePlugin
{
    public static class Unity3dUtil
    {
        // Ĭ�ϲ�����ģʽ �Ƿ��г���������ģʽ
        public const bool IsDontDestroyOnLoad = true;

        public static void SetDontDestroyOnLoad(GameObject go)
        {
            if (IsDontDestroyOnLoad)
            {
                UObject.DontDestroyOnLoad(go);
            }
        }

        public static void SetDontDestroyOnLoad(Component com)
        {
            if (IsDontDestroyOnLoad)
            {
                UObject.DontDestroyOnLoad(com);
            }
        }
    }
}