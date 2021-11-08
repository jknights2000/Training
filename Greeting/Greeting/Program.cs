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

            bool isvaliddate = false;
            DateTime birthday = new DateTime();
            while (!isvaliddate)
            {
                Console.WriteLine("Whats your birthday");
                string date = Console.ReadLine();
                isvaliddate = DateTime.TryParse(date, out birthday);
                if(!isvaliddate)
                {
                    Console.WriteLine("{0} is not a valid date",date);
                }
             }
            int yearsold = DateTime.Today.Year - birthday.Year;

            if (afirstname != "" && asecondname != "")
            {
                Console.WriteLine("Hello " + afirstname +" "+ asecondname + ". nice to meet you");
                if(CheckUpper(afirstname) && CheckUpper(asecondname))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if(afirstname.Length + asecondname.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
                howold(yearsold);
            }
            else if (asecondname == "" && afirstname != "")
            {
                Console.WriteLine("Hello " + afirstname +  ". nice to meet you. one name i see");
                if (CheckUpper(afirstname))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if (afirstname.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
            }
            else if (afirstname == "" && asecondname != "")
            {
                Console.WriteLine("Hello " + asecondname + ". nice to meet you. one name i see");
                if (CheckUpper(asecondname))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if (asecondname.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
            }
            else
            {
                Console.WriteLine("Hello. anyone there");
            }

            //use @ for special characters
            //{1} as placement followed by ,
            //string.format
            //:C currecny :N adds commas to numbers :P percentage
            //use hashes to be replace by numbers
        }

        private static void howold(int age)
        {
            if(age < 10)
            {
                Console.WriteLine("hey there little guy");
            }
        }

        public static bool CheckUpper(string input)
        {
            if(input.ToUpper() == input)
            {
                return true;
            }
            return false;
        }
    }
}
