using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logger
{
    public class FileLogger:IFileLogger
    {
        private int _size = 2;
        private Object lockobj = new Object();

        public string Path { get; set; } = Directory.GetCurrentDirectory();
        public int FileSize { get { return _size * 1024 * 1024; } set { _size = value; } }


        public void Write<T>(string text) where T : class
        {
            Write($"{typeof(T).Name} {text}");
        }

        private void Write(string text)
        {
            lock (lockobj)
            {
                try
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(Path);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    DirectoryInfo currentDir = new DirectoryInfo(Path + @"\" + DateTime.Now.ToString("yyMMdd"));
                    if (!currentDir.Exists)
                        currentDir.Create();
                    File.AppendAllText(currentDir.FullName + @"\temp.log", DateTime.Now.ToString("s") + " | " + text + Environment.NewLine);
                    FileInfo info = new FileInfo(currentDir.FullName + @"\temp.log");
                    if (info.Length >= FileSize)
                    {
                        info.MoveTo(currentDir.FullName + @"\" + DateTime.Now.ToString("yyMMddhhmmss") + ".log");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
