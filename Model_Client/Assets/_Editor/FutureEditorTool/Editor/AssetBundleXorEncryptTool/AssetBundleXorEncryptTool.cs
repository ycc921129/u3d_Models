/*
 Author:du
 Time:2019.08.14
*/

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class AssetBundleXorEncryptTool
    {
        //[MenuItem("[FC Toolkit]/BuildXorAB")]
        private static void BuildAB()
        {
            FileUtil.DeleteFileOrDirectory(Application.streamingAssetsPath);
            Directory.CreateDirectory(Application.streamingAssetsPath);
            var manifest = BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.iOS);
            foreach (var name in manifest.GetAllAssetBundles())
            {
                var uniqueSalt = Encoding.UTF8.GetBytes(name);
                var data = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, name));
                using (var myStream = new AssetBundleXorStream(Path.Combine(Application.streamingAssetsPath, "encypt_" + name), FileMode.Create))
                {
                    myStream.Write(data, 0, data.Length);
                }
            }
            AssetDatabase.Refresh();
        }

        private static void LoadXorABTest()
        {
            using (var fileStream = new AssetBundleXorStream(Application.streamingAssetsPath + "/encypt_myab.unity3d", FileMode.Open, FileAccess.Read, FileShare.None, 1024 * 4, false))
            {
                var myLoadedAssetBundle = AssetBundle.LoadFromStream(fileStream);
            }
        }
    }

    public class AssetBundleXorStream : FileStream
    {
        private const byte KEY = 64;

        public AssetBundleXorStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) : base(path, mode, access, share, bufferSize, useAsync)
        {
        }
        public AssetBundleXorStream(string path, FileMode mode) : base(path, mode)
        {
        }

        public override int Read(byte[] array, int offset, int count)
        {
            var index = base.Read(array, offset, count);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] ^= KEY;
            }
            return index;
        }

        public override void Write(byte[] array, int offset, int count)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] ^= KEY;
            }
            base.Write(array, offset, count);
        }
    }
}