using System;
using System.Threading;

namespace lab3
{
    internal class Program
    {
        private static void Main()
        {
            var mutex = new Mutex();

            var thread1 = new Thread(() => Test(mutex)) { Name = "1" };
            var thread2 = new Thread(() => Test(mutex)) { Name = "2" };

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        public static void Test(Mutex mutex)
        {
            var text = $"I'm thread {Thread.CurrentThread.Name}";
            for (var i = 0; i < 100000; i++)
            {
                mutex.Lock();
                foreach (var symbol in text)
                {
                    Console.Write(symbol);
                }
                Console.WriteLine();
                mutex.Unlock();
            }
        }
    }
}