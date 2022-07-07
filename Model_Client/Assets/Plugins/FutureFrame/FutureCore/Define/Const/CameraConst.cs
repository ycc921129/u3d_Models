/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 Author:du
 Time:2017.10.12
*/

using UnityEngine;

namespace FutureCore
{
    public static class CameraConst
    {
        public const int MainDepth = 0;
        public const int UICameraDepth = 10;

        public const int MainCameraPosValue = 100;
        public const int MainCameraZPos = 0;
        public const int UICameraPosValue = 10000;

        public static Vector3 MainCameraPos = new Vector3(MainCameraPosValue, MainCameraPosValue, MainCameraZPos);
        public static Vector3 UICameraPos = new Vector3(UICameraPosValue, UICameraPosValue, MainCameraZPos);
    }
}