using System;
using System.Threading;
using TaskQueueLib;

namespace lab1
{
    internal static class Program
    {
        public const int TasksCount = 10;
        public const int ThreadsCount = 3;

        private static void Main()
        {
            var threadPool = new TaskQueue(ThreadsCount);

            //lab1
            for (var i = 0; i < TasksCount; i++)
            {
                threadPool.EnqueueTask(DoSmth);
            }
            threadPool.WaitAndStop();

            //lab5
            //var tasks = new TaskDelegate[TasksCount];
            //for (int i = 0; i < TasksCount; i++)
            //{
            //    tasks[i] = DoSmth;
            //}
            //threadPool.WaitAll(tasks);

            Console.ReadKey();
        }

        public static void DoSmth()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}. Hello");
            Thread.Sleep(100);
        }
    }
}