using System;
using System.IO;
using System.Threading;
using TaskQueueLib;

namespace lab2
{
    internal class Program
    {
        public const int ThreadsCount = 2;

        private static int _filesCount;
        private static TaskQueue _threadPool;

        private static void Main()
        {
            Console.Write("Copy files\nFrom: ");
            var directoryFrom = Console.ReadLine();
            Console.Write("To: ");
            var directoryTo = Console.ReadLine();

            _threadPool = new TaskQueue(ThreadsCount);
            CopyDirectory(directoryFrom, directoryTo);
            _threadPool.WaitAndStop();

            Console.WriteLine($"Copied files: {_filesCount}");
            Console.ReadKey();
        }

        private static void CopyDirectory(string directoryFrom, string directoryTo)
        {
            Directory.CreateDirectory(directoryTo);

            foreach (var file in Directory.GetFiles(directoryFrom))
            {
                var newFile = file.Replace(directoryFrom, directoryTo);
                _threadPool.EnqueueTask(() => CopyFile(file, newFile));
            }

            foreach (var dir in Directory.GetDirectories(directoryFrom))
            {
                var newDir = dir.Replace(directoryFrom, directoryTo);
                CopyDirectory(dir, newDir);
            }
        }

        private static void CopyFile(string from, string to)
        {
            File.Copy(from, to);
            Interlocked.Increment(ref _filesCount);
        }
    }
}