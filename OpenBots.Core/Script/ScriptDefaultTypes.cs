using OpenQA.Selenium;
using SHDocVw;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.Diagnostics;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using WordApplication = Microsoft.Office.Interop.Word.Application;

namespace OpenBots.Core.Script
{
    public static class ScriptDefaultTypes
    {
        public static Dictionary<string, Type> DefaultVarArgTypes = new Dictionary<string, Type>()
        {
            { "More Options...", typeof(MoreOptions) },
            { typeof(string).ToString(), typeof(string) },
            { typeof(int).ToString(), typeof(int) },
            { typeof(bool).ToString(), typeof(bool) },
            { typeof(List<>).ToString(), typeof(List<>) },
            { typeof(Dictionary<,>).ToString(), typeof(Dictionary<,>) },
            { typeof(DataTable).ToString(), typeof(DataTable) },
            { typeof(DataRow).ToString(), typeof(DataRow) },
            { typeof(Bitmap).ToString(), typeof(Bitmap) }
        };

        public static Dictionary<string, Type> DefaultInstanceTypes = new Dictionary<string, Type>()
        {
            { "More Options...", typeof(MoreOptions) },
            { typeof(IWebDriver).ToString(), typeof(IWebDriver) },
            { typeof(ExcelApplication).ToString(), typeof(ExcelApplication) },
            { typeof(WordApplication).ToString(), typeof(WordApplication) },
            { typeof(InternetExplorer).ToString(), typeof(InternetExplorer) },
            { typeof(OleDbConnection).ToString(), typeof(OleDbConnection) },
            { typeof(Stopwatch).ToString(), typeof(Stopwatch) }
        };
    }

    public static class MoreOptions
    {

    }
}
