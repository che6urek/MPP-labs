using System;

namespace lab6
{
    internal class Program
    {
        private static void Main()
        {
            var test = new DynamicList<int>();
            for (int i = 0; i < 20; i++)
            {
                test.Add(i);
            }
            Console.WriteLine($"Size: {test.Count}");
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Size: {test.Count}");
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
            for (int i = 0; i < 19; i++)
            {
                test.Remove(i);
            }
            for (int i = 0; i < 20; i++)
            {
                test.Add(i);
            }
            test.Clear();
            Console.ReadKey();
        }
    }
}
