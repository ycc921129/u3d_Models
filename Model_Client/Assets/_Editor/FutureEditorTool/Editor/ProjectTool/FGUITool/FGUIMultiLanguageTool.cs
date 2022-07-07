//国际化翻译服务器: https://script.google.com/macros/d/1_rax7mZ4qpxTWe3PuYWF31A_bUHMUl60Wj0fbaz1n27aRrUrhlzYxeGk/edit

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace FutureEditor
{
    public static class FGUIMultiLanguageTool
    {
        private static string i18nDir = @"\_I18N\";
        private static string i19nEnXmlFileName = "en.xml";
        private static string i18nExcelFileName = "W_文本多语言_L(" + EditorAppConst.AppName + @").xlsx";
        private static string fguiLanguageXMLPath = @"\_I18N\en.xml"; //@"\$SVN\翻译\en.xml";
        private static string languageExcelXlsxPath = @"\_I18N\" + i18nExcelFileName; //@"\$SVN\翻译\" + i18nExcelFileName;
        private static List<Language> languageList = new List<Language>();

        [MenuItem("[FC Project]/FGUI/MultiLanguage/1) 打开SVN多语言翻译目录", false, 0)]
        private static void OpenSVNDir()
        {
            string path = AppEditorInfo.MultiLanguageSVNPath;
            if (Directory.Exists(path))
            {
                DirectoryTool.OpenDirectory(path);
            }
            else
            {
                DirectoryTool.OpenDirectory(Application.dataPath + @"\..\..\");
                Debug.Log("[FGUIMultiLanguageTool]没有找到SVN多语言翻译目录: " + path);
            }
        }

        [MenuItem("[FC Project]/FGUI/MultiLanguage/2) 打开I18N目录", false, 0)]
        private static void OpenI18NDir()
        {
            string path = Path.GetFullPath(Application.dataPath + @"\..\..\" + i18nDir);
            if (Directory.Exists(path))
            {
                DirectoryTool.OpenDirectory(path);
            }
            else
            {
                DirectoryTool.OpenDirectory(Application.dataPath + @"\..\..\");
                Debug.Log("[FGUIMultiLanguageTool]没有找到I18N目录: " + path);
            }
        }

        [MenuItem("[FC Project]/FGUI/MultiLanguage/3) 同步SVN更新Excel翻译文件", false, 0)]
        private static void UpdateSVNSyncFile()
        {
            string svnDir = AppEditorInfo.MultiLanguageSVNPath;
            if (!Directory.Exists(svnDir))
            {
                UnityEngine.Debug.Log("[FGUIMultiLanguageTool]更新Excel翻译文件失败, 没有找到SVN多语言翻译目录");
                return;
            }

            System.Diagnostics.Process process = System.Diagnostics.Process.Start("TortoiseProc.exe", "/command:update /path:" + svnDir + " /closeonend:0");
            process.WaitForExit();
            process.Close();
            process.Dispose();

            string i18nDir = Path.GetFullPath(Application.dataPath + @"\..\..\" + FGUIMultiLanguageTool.i18nDir);
            if (!Directory.Exists(i18nDir))
            {
                Directory.CreateDirectory(i18nDir);
            }

            string svn_enXml = svnDir + i19nEnXmlFileName;
            if (File.Exists(svn_enXml))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
                string i18n_enXml = directoryInfo.Parent.Parent.FullName + fguiLanguageXMLPath;
                File.Copy(svn_enXml, i18n_enXml, true);
            }
            string svn_langXls = svnDir + i18nExcelFileName;
            if (File.Exists(svn_langXls))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
                string i18n_langXls = directoryInfo.Parent.Parent.FullName + languageExcelXlsxPath;
                File.Copy(svn_langXls, i18n_langXls, true);
            }

            UnityEngine.Debug.Log("[FGUIMultiLanguageTool]同步SVN更新Excel翻译文件");
        }

        [MenuItem("[FC Project]/FGUI/MultiLanguage/4) 创建FGUI多语言Excel (翻译)", false, 1)]
        private static void CreateLanguageExcel()
        {
            languageList.Clear();
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
            string EnXmlath = directoryInfo.Parent.Parent.FullName + fguiLanguageXMLPath;
            string Langxls = directoryInfo.Parent.Parent.FullName + languageExcelXlsxPath;
            if (File.Exists(EnXmlath))
            {
                string xmlString = File.ReadAllText(EnXmlath);

                #region 读取en.xml，生成language

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlString);
                XmlNode xn = xml.SelectSingleNode("resources");
                XmlNodeList xnl = xn.ChildNodes;
                int id = 1;
                foreach (XmlNode item in xnl)
                {
                    if (item.Name == "string")
                    {
                        XmlElement xe = (XmlElement)item;
                        string name = xe.GetAttribute("name");
                        string value = item.InnerText;

                        Language lg = new Language();
                        lg.name = name;
                        lg.keyvalue.Add("key", lg.name);

                        lg.mz = xe.GetAttribute("mz");
                        lg.keyvalue.Add("mz", lg.mz);

                        lg.keyvalue.Add("en", value);

                        lg.keyvalue.Add("id", id.ToString());
                        lg.id = id;
                        id++;
                        languageList.Add(lg);
                    }
                }

                #endregion 读取en.xml，生成language

                FileStream myAddress;
                IWorkbook myWorkbook;
                ISheet sheetLanguage;
                IRow row;
                bool isExists = File.Exists(Langxls);

                myAddress = new FileStream(Langxls, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                if (!isExists)
                {
                    //生成xls
                    myWorkbook = NewIWorkBook();
                    Debug.Log("[FGUIMultiLanguageTool]不存在文件，第一次生成：" + Langxls);

                    sheetLanguage = myWorkbook.CreateSheet("TextLanguage");
                    List<string> stringKeyList = new List<string> { "id", "key", "mz", "en", "zh", "zh_CN", "es", "fr", "ja", "de", "ru", "pt", "@in", "hi_IN", "vi", "tr", "ar", "pl", "th", "ko", "uk", "ro", };
                    List<string> descList = new List<string> { "id", "组件索引", "控件名字", "英文", "繁体中文(默认)", "简体中文", "西班牙", "法国", "日本", "德国", "俄罗斯", "葡萄牙", "印度尼西亚（in是关键字，需要加@）", "印地语", "越南", "土耳其", "阿拉伯", "波兰", "泰国", "韩国", "乌克兰", "罗马尼亚" };
                    keyList = new List<KeyInfo>();
                    for (int i = 0; i < stringKeyList.Count; i++)
                    {
                        string value = stringKeyList[i];

                        KeyInfo info = new KeyInfo();
                        info.key = value;
                        info.index = i;
                        info.type = info.index == 0 ? "int" : "string";
                        info.desc = descList[i];
                        keyList.Add(info);
                    }

                    InitStepOne(myWorkbook, sheetLanguage);

                    SetCellByList(sheetLanguage, myWorkbook, languageList);

                    myWorkbook.Write(myAddress);
                }
                else
                {
                    #region 读取原来的excel

                    myWorkbook = GetIWorkBook(Langxls, myAddress);
                    ///原来的lang.xml的
                    List<Language> originList = new List<Language>();
                    keyList = new List<KeyInfo>();
                    //读取xls
                    sheetLanguage = myWorkbook.GetSheetAt(0);
                    row = sheetLanguage.GetRow(0);
                    var descRow = sheetLanguage.GetRow(2);
                    int index = 0;
                    foreach (var item in row.Cells)
                    {
                        ICell cell = item;
                        KeyInfo info = new KeyInfo();
                        info.key = GetCellStringInfo(cell);
                        info.index = index;
                        if (descRow.Cells.Count > index)
                            info.desc = GetCellStringInfo(descRow.Cells[index]);
                        info.type = index == 0 ? "int" : "string";
                        index++;
                        keyList.Add(info);
                    }

                    int startIndex = 4;
                    for (int i = startIndex; i < sheetLanguage.PhysicalNumberOfRows; i++)
                    {
                        row = sheetLanguage.GetRow(i);
                        Language lg = new Language();

                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            if (j >= keyList.Count)
                                continue;

                            ICell cell = row.Cells[j];
                            if (keyList[j].key == "id")
                            {
                                lg.keyvalue.Add(keyList[j].key, cell.NumericCellValue.ToString());
                            }
                            else
                            {
                                lg.keyvalue.Add(keyList[j].key, GetCellStringInfo(cell));
                            }

                            if (cell.CellStyle != null)
                                switch (cell.CellStyle.FillForegroundColor)
                                {
                                    case HSSFColor.Red.Index:
                                        lg.isChange = true;
                                        break;

                                    case HSSFColor.Green.Index:
                                        lg.isNew = true;
                                        break;

                                    case HSSFColor.Grey50Percent.Index:
                                        lg.isDelete = true;
                                        break;

                                    default:
                                        break;
                                }
                            if (lg.keyvalue.ContainsKey("key"))
                                lg.name = lg.keyvalue["key"];
                            if (lg.keyvalue.ContainsKey("mz"))
                                lg.mz = lg.keyvalue["mz"];
                        }
                        originList.Add(lg);
                    }
                    ///遍历新导出的fgui xml
                    foreach (var item in languageList)
                    {
                        ///
                        Language lg = originList.Find((tmp) => tmp.name == item.name);
                        if (lg == null)
                        {
                            originList.Add(item);
                            item.isNew = true;
                        }
                        else
                        {
                            foreach (var keyValue in item.keyvalue)
                            {
                                if (lg.keyvalue.ContainsKey(keyValue.Key))
                                {
                                    if (keyValue.Key == "en")
                                    {
                                        if (lg.keyvalue["en"] != keyValue.Value)
                                        {
                                            lg.isChange = true;
                                        }
                                        lg.keyvalue["en"] = keyValue.Value;
                                    }
                                    continue;
                                }
                                else
                                {
                                    lg.keyvalue.Add(keyValue.Key, keyValue.Value);
                                }
                            }
                        }
                    }

                    #endregion 读取原来的excel

                    for (int i = 0; i < originList.Count; i++)
                    {
                        var lg = originList[i];
                        lg.id = i + 1;
                        bool isDelete = languageList.Find((tmp) => tmp.name == lg.name) == null;
                        if (isDelete)
                        {
                            lg.isDelete = true;
                        }
                    }
                    originList.Sort(SortLanguage);

                    myWorkbook = NewIWorkBook();

                    sheetLanguage = myWorkbook.CreateSheet("TextLanguage");

                    InitStepOne(myWorkbook, sheetLanguage);

                    SetCellByList(sheetLanguage, myWorkbook, originList);

                    myAddress.Close();
                    myAddress = new FileStream(Langxls, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    myWorkbook.Write(myAddress);
                }

                myWorkbook.Close();
                myAddress.Close();
            }
            else
            {
                Debug.LogError("[FGUIMultiLanguageTool]请把FGUI的字符串文件导出到" + EnXmlath);
                return;
            }
            Debug.Log("[FGUIMultiLanguageTool]创建FGUI多语言Excel完成");
        }

        private static int SortLanguage(Language x, Language y)
        {
            return x.id.CompareTo(y.id);
        }

        private static void SetCellByList(ISheet Sheet01, IWorkbook MyWorkbook, List<Language> list)
        {
            int curRowIndex = 3;
            for (int i = 0; i < list.Count; i++)
            {
                curRowIndex++;
                var lg = list[i];

                lg.keyvalue["id"] = lg.id.ToString();
                var tmpRow = Sheet01.CreateRow(curRowIndex);
                for (int j = 0; j < keyList.Count; j++)
                {
                    var tmpCell = tmpRow.CreateCell(j);

                    if (lg.keyvalue.ContainsKey(keyList[j].key))
                    {
                        string value = lg.keyvalue[keyList[j].key];
                        if (keyList[j].key == "id")
                        {
                            tmpCell.SetCellValue(int.Parse(value));
                        }
                        else
                        {
                            tmpCell.SetCellValue(value);
                        }
                    }
                    else
                    {
                        tmpCell.SetCellValue("");
                    }
                    if (keyList[j].key == "en" && lg.isChange)
                    {
                        Debug.LogWarning("[FGUIMultiLanguageTool]Change: " + lg.name);
                        tmpCell.CellStyle = MyWorkbook.CreateCellStyle();
                        tmpCell.CellStyle.FillPattern = FillPattern.SolidForeground;
                        tmpCell.CellStyle.FillForegroundColor = HSSFColor.Red.Index;
                    }
                    if (keyList[j].key == "en" && lg.isNew)
                    {
                        Debug.LogWarning("[FGUIMultiLanguageTool]New: " + lg.name);
                        tmpCell.CellStyle = MyWorkbook.CreateCellStyle();
                        tmpCell.CellStyle.FillPattern = FillPattern.SolidForeground;
                        tmpCell.CellStyle.FillForegroundColor = HSSFColor.Green.Index;
                    }
                    if (keyList[j].key == "en" && lg.isDelete)
                    {
                        Debug.LogWarning("[FGUIMultiLanguageTool]Delete: " + lg.name);
                        tmpCell.CellStyle = MyWorkbook.CreateCellStyle();
                        tmpCell.CellStyle.FillPattern = FillPattern.SolidForeground;
                        tmpCell.CellStyle.FillForegroundColor = HSSFColor.Grey50Percent.Index;
                    }
                }
            }
        }

        private class KeyInfo
        {
            public int index;
            public string key;
            public string type;
            public string desc;
        }

        private static List<KeyInfo> keyList;

        private static void InitStepOne(IWorkbook MyWorkbook, ISheet Sheet01)
        {
            int curRowIndex = 0;
            IRow Row = Sheet01.CreateRow(curRowIndex);
            Row.RowStyle = MyWorkbook.CreateCellStyle();
            ICell cell = Row.CreateCell((short)0);
            for (int i = 0; i < keyList.Count; i++)
            {
                string value = keyList[i].key;
                cell = Row.CreateCell((short)i);
                cell.SetCellValue(value);
            }

            curRowIndex++;
            Row = Sheet01.CreateRow(curRowIndex);
            Row.RowStyle = MyWorkbook.CreateCellStyle();
            for (int i = 0; i < keyList.Count; i++)
            {
                string value = keyList[i].type;
                cell = Row.CreateCell((short)i);
                cell.SetCellValue(value);
            }

            curRowIndex++;
            Row = Sheet01.CreateRow(curRowIndex);
            Row.RowStyle = MyWorkbook.CreateCellStyle();
            for (int i = 0; i < keyList.Count; i++)
            {
                string value = keyList[i].desc;
                cell = Row.CreateCell((short)i);
                cell.SetCellValue(value);
            }

            curRowIndex++;
            Row = Sheet01.CreateRow(curRowIndex);
            Row.RowStyle = MyWorkbook.CreateCellStyle();
            for (int i = 0; i < keyList.Count; i++)
            {
                cell = Row.CreateCell((short)i);
                cell.SetCellValue("L");
            }
        }

        [MenuItem("[FC Project]/FGUI/MultiLanguage/5) 创建FGUI多语言XML (运行时)", false, 2)]
        private static void CreateLanguageXMLs()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
            string XmlPathPre = directoryInfo.Parent.Parent.FullName + i18nDir;
            string UnityPathPre = Application.dataPath + "/_Res/Resources/Data/";
            string Langxls = directoryInfo.Parent.Parent.FullName + languageExcelXlsxPath;
            FileStream MyAddress;
            IWorkbook MyWorkbook;
            ISheet Sheet01;
            IRow Row;
            List<Language> orginList = new List<Language>();
            List<string> keyList = new List<string>();

            MyAddress = new FileStream(Langxls, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            MyWorkbook = GetIWorkBook(Langxls, MyAddress);
            Sheet01 = MyWorkbook.GetSheetAt(0);
            Row = Sheet01.GetRow(0);
            foreach (var item in Row.Cells)
            {
                ICell cell = item;
                keyList.Add(GetCellStringInfo(cell));
            }

            for (int i = 1; i < Sheet01.PhysicalNumberOfRows; i++)
            {
                Row = Sheet01.GetRow(i);

                // 检查空行并且跳过
                ICell cell0 = Row.Cells[0];
                if (cell0.CellType == CellType.Blank) continue;

                Language lg = new Language();
                for (int j = 0; j < Row.Cells.Count; j++)
                {
                    ICell cell = Row.Cells[j];
                    if (keyList.Count > j)
                    {
                        if (keyList[j] == "id")
                        {
                            continue;
                        }
                        else
                        {
                            lg.keyvalue.Add(keyList[j], GetCellStringInfo(cell));
                        }
                    }
                }
                if (lg.keyvalue.ContainsKey("key"))
                {
                    lg.name = lg.keyvalue["key"];
                    lg.mz = lg.keyvalue["mz"];
                    orginList.Add(lg);
                }
                else
                {
                    Debug.Log("[FGUIMultiLanguageTool]这行没有key: " + i);
                }
            }
            int EnIndex = 3;

            if (keyList.Count > EnIndex)
            {
                string dirName = XmlPathPre + "MultiLanguage/";
                if (Directory.Exists(dirName))
                {
                    Directory.Delete(dirName, true);
                }
                Directory.CreateDirectory(dirName);

                for (int i = EnIndex; i < keyList.Count; i++)
                {
                    string value = keyList[i];
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNode header = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    string fileName = XmlPathPre + "MultiLanguage/" + value + ".xml";

                    xmlDoc.AppendChild(header);
                    //创建一级节点
                    XmlElement rootNode = xmlDoc.CreateElement("resources");

                    for (int j = 0; j < orginList.Count; j++)
                    {
                        var element = orginList[j].GetElement(value, xmlDoc);
                        rootNode.AppendChild(element);
                    }
                    xmlDoc.AppendChild(rootNode);

                    xmlDoc.Save(fileName);
                }
                string svnPath = XmlPathPre + "MultiLanguage/";
                string unityPath = UnityPathPre + "MultiLanguage/";
                string infoJsonPath = svnPath + "#MultiLanguageInfo.bytes";
                if (Directory.Exists(unityPath))
                    Directory.Delete(unityPath, true);
                keyList.RemoveRange(0, EnIndex);
                File.WriteAllText(infoJsonPath, MiniJSON.Json.Serialize(keyList));

                CopyDir(svnPath, unityPath);

                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError("[FGUIMultiLanguageTool]文件 lang.xls 不包含除en外的语言，不生成语言文件");
                return;
            }
            Debug.Log("[FGUIMultiLanguageTool]创建FGUI多语言XML完成");
        }

        private static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                {
                    aimPath += Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!Directory.Exists(aimPath))
                {
                    Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[FGUIMultiLanguageTool]" + e.ToString());
            }
        }

        private static string GetCellStringInfo(ICell cell)
        {
            if (cell.CellType == CellType.String)
            {
                return cell.StringCellValue;
            }
            else if (cell.CellType == CellType.Numeric)
            {
                return cell.NumericCellValue.ToString();
            }
            else if (cell.CellType == CellType.Boolean)
            {
                return cell.BooleanCellValue.ToString();
            }
            return string.Empty;
        }

        private static IWorkbook NewIWorkBook()
        {
            return new XSSFWorkbook();
        }

        private static IWorkbook GetIWorkBook(string excelPath, FileStream address)
        {
            IWorkbook result = null;
            if (Path.GetExtension(excelPath) == ".xlsx")
            {
                try
                {
                    result = new XSSFWorkbook(address);
                }
                catch
                {
                    Debug.LogError("[FGUIMultiLanguageTool]读取Excel.xlsx失败，请检查文件是否合法!");
                    return null;
                }
            }
            else if (Path.GetExtension(excelPath) == ".xls")
            {
                try
                {
                    result = new HSSFWorkbook(address);
                }
                catch
                {
                    Debug.LogError("[FGUIMultiLanguageTool]读取Excel.xls失败，请检查文件是否合法!");
                    return null;
                }
            }

            if (result == null)
            {
                Debug.LogError("[FGUIMultiLanguageTool]未能识别文件格式，请选择正确的文件!");
            }
            return result;
        }

        private class Language
        {
            public Dictionary<string, string> keyvalue = new Dictionary<string, string>();
            public string name;
            public string mz;
            public int id;
            public bool isChange;
            public bool isNew;
            public bool isDelete;
            public string languaName;

            public XmlElement GetElement(string key, XmlDocument xmlDoc)
            {
                XmlElement xn = xmlDoc.CreateElement("string");
                xn.SetAttribute("mz", mz);
                xn.SetAttribute("name", name);
                if (keyvalue.ContainsKey(key))
                {
                    string value = keyvalue[key];
                    xn.InnerText = value;
                }
                else
                {
                    xn.InnerText = "";
                }

                return xn;
            }
        }
    }
}