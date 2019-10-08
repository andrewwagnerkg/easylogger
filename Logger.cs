using System;
using System.IO;
using System.Windows.Forms;

namespace Logging
{
    public class Logger
    {
        private static Logger instance = null;

        object lockobj = new object();

        public int LogSize { get; set; } = 1024*1024;

        public string LogPath { get; set; } = @"C:\TerminalClient\Logs";

        public static Logger GetInstance()
        {
            if (instance == null)
                instance = new Logger();
            return instance;
        }

        private Logger()
        {

        }

        public void Write(string msg)
        {
            lock (lockobj)
            {
                try
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(LogPath);
                    if (!dirinfo.Exists)
                        dirinfo.Create();
                    DirectoryInfo currentDir = new DirectoryInfo(LogPath + @"\" + DateTime.Now.ToString("yyMMdd"));
                    if (!currentDir.Exists)
                        currentDir.Create();
                    File.AppendAllText(currentDir.FullName + @"\temp.log", DateTime.Now.ToString("s") + " | " + msg + Environment.NewLine);
                    FileInfo info = new FileInfo(currentDir.FullName + @"\temp.log");
                    if ( info.Length>= LogSize)
                    {
                        info.MoveTo(currentDir.FullName + @"\" + DateTime.Now.ToString("yyMMddhhmmss") + ".log");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Logger");
                }
            }
        }
    }
}
