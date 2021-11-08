using System;

namespace SentenceMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("how long do you want the sentence");
            int length = int.Parse(Console.ReadLine());
            int current = 0;
            string output = "";
            while(current < length)
            {
                Console.WriteLine("Add word");
                string addedword = Console.ReadLine();
                current += 1;
                output += addedword + " ";
                Console.WriteLine("current sentence: " + output);

            }
            
        }
    }
}
