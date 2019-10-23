using System;
using System.Linq;
using System.Reflection;

namespace lab4
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string path;

            if (args.Length == 0)
            {
                Console.Write("Enter file path: ");
                path = Console.ReadLine();
            }
            else
            {
                path = args[0];
            }

            Type[] types;
            try
            {
                types = Assembly.LoadFile(path ?? throw new Exception()).GetTypes();
            }
            catch
            {
                Console.WriteLine("Invalid file!");
                Console.ReadKey();
                return;
            }

            foreach (var type in types.Where(x => x.IsPublic).OrderBy(y => y.FullName))
            {
                Console.WriteLine(type);
            }
            Console.ReadKey();
        }
    }
    public class Lol { }
    public class Kek { }
    public class Cheburek { }
}
namespace Test { public class Lol { } public class Kek { } public class Cheburek { } public class A { } public class B { } public class C { } }