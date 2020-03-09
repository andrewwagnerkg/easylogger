using Logger;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileLogger logger = new FileLogger();
            //logger.FileSize = 2; // Rotation file size 2 MB by default
            //logger.Path = Directory.GetCurrentDirectory(); //path for log storage by default current directory
            for (int i = 0; i < 10000; i++)
            {
                logger.Write<Program>($"string for example. Write iterator value {i}");
            }
        }
    }
}
