using UnityEditor;

namespace FutureEditor
{
    /// <summary>
    /// 编辑器状态
    /// </summary>
    public class EditorState : ScriptableSingleton<EditorState>
    {
        /// <summary>
        /// 是否启动设置平台文件夹
        /// </summary>
        public bool IsLaunchPlatformFolder { get; set; }

        /// <summary>
        /// 是否进行过引擎版本检查
        /// </summary>
        public bool IsDoCheckAllUnity3dVersion { get; set; }
    }
}