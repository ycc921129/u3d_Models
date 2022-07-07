/****************************************************************************
 * ScriptType: AutoCreator
 * 请勿修改
 ****************************************************************************/

using FutureCore;

namespace ProjectApp
{
    public static class SceneMgrRegister
    {
        public static void AutoRegisterScene()
        {
            SceneMgr sceneMgr = SceneMgr.Instance;
            sceneMgr.AddScene(new GameScene());
            sceneMgr.AddScene(new LobbyScene());
            sceneMgr.AddScene(new MainScene());
        }
    }
}