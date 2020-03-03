using System;

namespace HalloDebugging
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            for (int i = 0; i < 10; i++)
            {
                ZeigeZahl(i);
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void ZeigeZahl(int i)
        {
            Console.WriteLine(GetText(i));
        }

        private static string GetText(int i)
        {

            if (i > 5)
                Console.ReadLine();
            return $"Wert: {i}";
        }


    }
}
