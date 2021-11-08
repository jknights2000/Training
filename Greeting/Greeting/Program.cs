using System;

namespace Greeting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Whats your First name");
            string afirstname = Console.ReadLine();
            Console.WriteLine("Whats your Second name");
            string asecondname = Console.ReadLine();
            if (afirstname != "" && asecondname != "")
            {
                Console.WriteLine("Hello " + afirstname +" "+ asecondname + ". nice to meet you");
            }
            else if (asecondname == "" && afirstname != "")
            {
                Console.WriteLine("Hello " + afirstname +  ". nice to meet you. one name i see");
            }
            else if (afirstname == "" && asecondname != "")
            {
                Console.WriteLine("Hello " + asecondname + ". nice to meet you. one name i see");
            }
            else
            {
                Console.WriteLine("Hello. anyone there");
            }
                

        }
    }
}
