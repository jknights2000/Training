using System;

namespace Adder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("input number");
            int ainput = int.Parse(Console.ReadLine());
            int output = 0;
            for (int i = 1; i <= ainput; i++)
            {
                output = output + i;
            }
            Console.WriteLine(output);
            Console.WriteLine("input number");
            int ainput2 = int.Parse(Console.ReadLine());
            for (int y = 1; y <= ainput; y++)
            {
                for (int z = 1; z <= ainput; z++)
                {
                    output = z * y;
                    Console.WriteLine(z + " x " + y + " = " + output);
                }
                Console.WriteLine();
            }
        }
            
    }
}
