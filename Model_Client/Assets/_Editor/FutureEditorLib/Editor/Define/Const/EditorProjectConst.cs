using UnityEngine;

namespace FutureEditor
{
    #region App
    public static class EP_AppConst
    {
        public const string ABExtName = ".bytes";
        public const float PixelsPerUnit = 100;
        public static Vector2 ScreenResolution = new Vector2(1080, 1920);
    }

    public static class EP_SplitConst
    {
        public const char DefaultTxtSplit = '=';
        public const char VerticalBar = '|';
        public const char Semicolon = ';';
    }
    #endregion

    #region Anim
    public static class EP_AnimRateConst
    {
        public const float AnimFrameRate = 30;
        public const float SecondRateUnit = 1 / AnimFrameRate;

        public const int Idle = 5;
        public const int Move = 5;
        public const int Atk = 5;
        public const int Skill = 5;
        public const int Dizzy = 5;
        public const int Rise = 5;
        public const int Die = 5;
    }

    public enum EP_EntityAnimType : int
    {
        Idle = 0,
        Move = 1,
        Atk = 2,
        Skill = 3,
        Dizzy = 4,
        Rise = 5,
        Die = 6,
        Loop = 7,
    }
    #endregion

    #region Skeleton
    public static class EP_AnimSlotConst
    {
        public const string AtkPoint = "AtkPoint";
        public const string HitPoint = "HitPoint";
    }
    #endregion
}