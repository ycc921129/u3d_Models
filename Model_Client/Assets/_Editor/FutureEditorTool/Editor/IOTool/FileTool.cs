using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class FileTool
    {
        public static void CopyFileOrDirectoryFollowSymlinks(string formPath, string toPath)
        {
            FileUtil.CopyFileOrDirectoryFollowSymlinks(formPath, toPath);
        }

        /// <summary>
        /// 文件夹拷贝
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="dest">目标目录</param>
        /// <param name="filter">过滤条件 false 表示文件将不会拷贝</param>
        public static void ConditionCopyDirectoryFile(string source, string dest, Func<string, bool> filter = null)
        {
            DirectoryInfo sourceDirInfo = new DirectoryInfo(source);

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            FileInfo[] files = sourceDirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                if (filter != null && !filter(file.FullName)) continue;
                file.CopyTo(dest + "\\" + file.Name, true);
            }

            DirectoryInfo[] dirs = sourceDirInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                ConditionCopyDirectoryFile(dir.FullName, dest + "\\" + dir.Name, filter);
            }
        }

        public static string GetFilePathFromAssetPath(string assetPath)
        {
            return Application.dataPath + assetPath.Replace("Assets", string.Empty);
        }

        public static string GetLastDirectory(string path)
        {
            string directory = Path.GetDirectoryName(path);
            int lastIndex = directory.LastIndexOf("/");
            return directory.Substring(lastIndex + 1);
        }

        public static string GetLastDirectoryAndName(string path)
        {
            int lastIndex = path.LastIndexOf("/");
            int lastSecondIndex = path.Substring(0, lastIndex - 1).LastIndexOf("/");
            return path.Substring(lastSecondIndex + 1);
        }

        public static string GetResourcePath(string path, string subPath, bool IsExcludeExt = true)
        {
            if (IsExcludeExt)
            {
                path = ExcludeExtention(path);
            }
            string assetDir = "Resources/" + subPath;
            path = path.Substring(path.LastIndexOf(assetDir) + assetDir.Length);
            return path;
        }

        public static string ExcludeExtention(string origin)
        {
            if (origin.Contains("."))
            {
                origin = origin.Remove(origin.LastIndexOf('.'));
            }
            return origin;
        }

        public static FileInfo[] GetFileNameAndPathUnderDirectory(string directoryPath, string type = "*.*")
        {
            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo direction = new DirectoryInfo(directoryPath);
                FileInfo[] files = direction.GetFiles(type, SearchOption.AllDirectories);
                return files;
            }
            return null;
        }

        public static string GetFileNameWithOutExtension(string filepath)
        {
            string fileName = Path.GetFileName(filepath);
            string extent = Path.GetExtension(filepath);
            return fileName.Replace(extent, string.Empty);
        }

        public static void CreateTxt(string path, string Txt, bool IsCover = false, bool IsAssetPath = false)
        {
            if (IsAssetPath)
            {
                path = Application.dataPath.Replace("Assets", string.Empty) + path;
            }
            if (!Directory.Exists(GetFullDiretoryPath(path)))
            {
                Directory.CreateDirectory(GetFullDiretoryPath(path));
            }
            if (File.Exists(path))
            {
                if (!IsCover)
                {
                    Debug.LogError("[FileTool]文件已经存在,如果要覆盖,请打开覆盖选项");
                    return;
                }
                else
                {
                    File.Delete(path);
                }
            }
            File.WriteAllText(path, Txt, new UTF8Encoding(false));
        }

        public static string GetFullDiretoryPath(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            return filePath.Replace(fileName, string.Empty);
        }

        public static void CreateFile(string filePath, string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            CreateFile(filePath, textBytes);
        }

        public static void CreateEncryptFile(string filePath, string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            textBytes = XorEncryptUtil.Encrypt(textBytes);
            CreateFile(filePath, textBytes);
        }

        public static void CreateFile(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        public static void CopyPrefabsToDir(string formDir, string toDir)
        {
            DirectoryTool.ClearDirectory(toDir);
            DirectoryInfo formfolder = new DirectoryInfo(formDir);
            FileSystemInfo[] folders = formfolder.GetFileSystemInfos();
            foreach (FileSystemInfo info in folders)
            {
                if (info is DirectoryInfo)
                {
                    FileSystemInfo[] assetFiles = (info as DirectoryInfo).GetFileSystemInfos();
                    foreach (FileSystemInfo assetfile in assetFiles)
                    {
                        CopyPrefabToDir(assetfile, toDir);
                    }
                }
                else
                {
                    CopyPrefabToDir(info, toDir);
                }
            }
            AssetDatabase.Refresh();
        }

        public static void CopyPrefabToDir(FileSystemInfo info, string toDir)
        {
            if (!(info is DirectoryInfo) && info.Name.EndsWith(".prefab"))
            {
                File.Copy(info.FullName, toDir + "/" + info.Name, true);
            }
        }

        public static void CopyToFolder(string SourceFolder, string TargetFolder)
        {
            if (Directory.Exists(SourceFolder))
            {
                if (Directory.Exists(TargetFolder))
                {
                    Directory.Delete(TargetFolder, true);
                }
                Directory.CreateDirectory(TargetFolder);
                List<string> files = new List<string>(Directory.GetFiles(SourceFolder));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(TargetFolder, Path.GetFileName(c));
                    File.Copy(c, destFile, true);
                });
                List<string> folders = new List<string>(Directory.GetDirectories(SourceFolder));
                folders.ForEach(c =>
                {
                    string destFile = Path.Combine(TargetFolder, Path.GetFileName(c));
                    CopyToFolder(c, destFile);
                });
                Debug.LogFormat("[FileTool]从{0}复制到{1}成功", SourceFolder, TargetFolder);
            }
        }
    }
}