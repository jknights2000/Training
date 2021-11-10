using System;

namespace Greeting
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Program pro = new Program();
            pro.run();


            //use @ for special characters
            //{1} as placement followed by ,
            //string.format
            //:C currecny :N adds commas to numbers :P percentage
            //use hashes to be replace by numbers
        }
        public void run()
        {
            Person p = new Person();
            Console.WriteLine("Whats your First name");
            p.FirstName = Console.ReadLine();

            Console.WriteLine("Whats your Second name");
            p.SecondName = Console.ReadLine();
            p.Birthday = p.getBirth();

            Response(p);
        }
        public void Response(Person person)
        {

            string fullName = (person.FirstName + " " + person.SecondName).Trim();
            if (fullName.Length == 0)
            {
                Console.WriteLine("Hello. anyone there?");
                return; // abort

            }
            Console.WriteLine($"Hello {fullName}, nice to meet you.{((person.IsBlank(person.FirstName) || person.IsBlank(person.SecondName)) ? " One name I see!" : "")}");
            CheckAge(person.age);
            CheckUpper(fullName);
            CheckLength(fullName);
            /*
            if (person.FirstName != "" && person.SecondName != "")
            {
                Console.WriteLine("Hello " + person.FirstName + " " + person.SecondName + ". nice to meet you");
                if (CheckUpper(person.FirstName) && CheckUpper(person.SecondName))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if (person.FirstName.Length + person.SecondName.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
                howold(person.age);
            }
            else if (person.SecondName == "" && person.FirstName != "")
            {
                Console.WriteLine("Hello " + person.FirstName + ". nice to meet you. one name i see");
                if (CheckUpper(person.FirstName))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if (person.FirstName.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
            }
            else if (person.FirstName == "" && person.SecondName != "")
            {
                Console.WriteLine("Hello " + person.SecondName + ". nice to meet you. one name i see");
                if (CheckUpper(person.SecondName))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if (person.SecondName.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
            }
            else
            {
                Console.WriteLine("Hello. anyone there");
            }
            */
        }

        private static void CheckAge(int age)
        {
            if (age < 10)
            {
                Console.WriteLine("hey there little guy");
            }
        }
        public void CheckUpper(string input)
        {
            if (input.ToUpper() == input)
            {
                Console.WriteLine("Bit loud tho");
            }
        }
        public void CheckLength(string input)
        {
            if (input.Length > 12)
            {
                Console.WriteLine("ooooo. long name");
            }
        }

    }
   
}
