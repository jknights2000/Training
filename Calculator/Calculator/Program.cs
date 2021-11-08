using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Calculator!");
           
            Console.WriteLine("Input radius of circle");
            string ainput = Console.ReadLine();
            double radius = Double.Parse(ainput);
            double area = Math.PI * (radius * radius);
            Console.WriteLine("The area of a circle with a radius of " + radius + " is "+area);
            
            Console.WriteLine("Input operator");
            ainput = Console.ReadLine();
            Console.WriteLine("how many times doy you want to "+ ainput);
            int ainput2 = int.Parse(Console.ReadLine());

            int[] numbers = new int[ainput2];
            int output2 = 0;
            string output3 = "";
            for (int i = 0; i < ainput2;i++)
            {
                Console.WriteLine("Input number 1");
                string first = Console.ReadLine();
                numbers[i] = int.Parse(first);
            }
           
            foreach(int a in numbers)
            {
                if (ainput == "*")
                {
                    if (output2 == 0)
                        {
                        output2 = a;
                        output3 += a;
                    }else
                    {
                        output2 = output2 * a;
                        output3 += " * " + a;
                    }
                    
                }
                else if (ainput == "+")
                {
                    
                    if (output2 == 0)
                    {
                        output2 += a;
                        output3 += a;
                    }
                    else
                    {
                        output2 += a;
                        output3 += " + " + a;
                    }
                }
                else if (ainput == "-")
                {
                    if (output2 == 0)
                    {
                        output2 = a;
                        output3 += a;
                    }
                    else
                    {
                        output2 = output2 - a;
                        output3 += " - " + a;
                    }
                }
                else if (ainput == "/")
                {
                    if (output2 == 0)
                    {
                        output2 = a;
                        output3 += a;
                    }
                    else
                    {
                        output2 = output2 / a;
                        output3 += " / " +a;
                    }
                }
                else
                {
                    Console.WriteLine("operator invalid");
                    break;
                }
            }
            Console.WriteLine(output3 + " = " + output2);



        }
    }
}
