/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

namespace FutureCore
{
    public delegate void VoidBaseDataDelegate(BaseEventData data);

    public delegate void VoidDataDelegate(PointerEventData data);

    public delegate void VoidGameObjectDelegate(GameObject gameObject);
}