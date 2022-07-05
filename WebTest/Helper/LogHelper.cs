using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebTest.Helper
{
    public static class LogHelper
    {
        public static void ToLog(this string _Message)
        {
            var Path = HttpContext.Current.Server.MapPath($@"/Log/");
            Directory.CreateDirectory(Path);
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTime.Now} {_Message}\r\n");
            File.AppendAllText(Path + $"Log{DateTime.Now.ToString("yyyyMMdd")}.txt", sb.ToString());
            sb.Clear();
        }
        public static void ToLog(this Exception _Ex)
        {
            var Path = HttpContext.Current.Server.MapPath($@"/Log/");
            Directory.CreateDirectory(Path);
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTime.Now} {_Ex.StackTrace}\r\n{_Ex.Message}\r\n");
            File.AppendAllText(Path + $"Log{DateTime.Now.ToString("yyyyMMdd")}.txt", sb.ToString());
            sb.Clear();
        }
    }
}