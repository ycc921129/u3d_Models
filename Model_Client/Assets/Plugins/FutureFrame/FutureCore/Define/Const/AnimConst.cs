/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using UnityEngine;

namespace FutureCore
{
    public static class AnimConst
    {
        public const float AnimFrameRate = 30;
        public const float SecondRateUnit = 1 / AnimFrameRate;
        public const float RiseAnimTime = 80 / AnimFrameRate;

        public const int IdleFrameRate = 6;
        public const int MoveFrameRate = 8;
        public const int AtkFrameRate = 10;
        public const int SkillFrameRate = 10;
        public const int DizzyFrameRate = 6;
        public const int RiseFrameRate = 6;

        public const string IdleName = "Idle";
        public const string MoveName = "Move";
        public const string AtkName = "Atk";
        public const string SkillName = "Skill";
        public const string DizzyName = "Dizzy";
        public const string RiseName = "Rise";

        public static int IdleHash = Animator.StringToHash(IdleName);
        public static int MoveHash = Animator.StringToHash(MoveName);
        public static int AtkHash = Animator.StringToHash(AtkName);
        public static int SkillHash = Animator.StringToHash(SkillName);
        public static int DizzyHash = Animator.StringToHash(DizzyName);
        public static int RiseHash = Animator.StringToHash(RiseName);
    }
}