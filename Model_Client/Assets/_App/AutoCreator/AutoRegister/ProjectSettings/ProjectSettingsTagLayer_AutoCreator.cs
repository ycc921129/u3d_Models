/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectApp
{
    public enum ProjectTag : int
    {
        Untagged = 0,
        Respawn = 1,
        Finish = 2,
        EditorOnly = 3,
        MainCamera = 4,
        Player = 5,
        GameController = 6,
        Entity = 7,
        bg = 8,
    }

    public enum ProjectSortLayer : int
    {
        Default = 0,
        Down = 1,
        Top = 2,
    }

    public enum ProjectLayer : byte
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        Entity = 8,
    }
}