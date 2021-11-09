using System;

namespace Greeting
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            Console.WriteLine("Whats your First name");
            p.FirstName = Console.ReadLine();

            Console.WriteLine("Whats your Second name");
            p.SecondName = Console.ReadLine();
            p.Birthday = p.getBirth();
            p.yearsold = p.getAge();
            



            //use @ for special characters
            //{1} as placement followed by ,
            //string.format
            //:C currecny :N adds commas to numbers :P percentage
            //use hashes to be replace by numbers
        }

        public void Response(Person person)
        {
            if (person.FirstName != "" && person.SecondName != "")
            {
                Console.WriteLine("Hello " + person.FirstName + " " + person.SecondName + ". nice to meet you");
                if (CheckUpper(afirstname) && CheckUpper(person.SecondName))
                {
                    Console.WriteLine("Bit loud tho");
                }
                if (afirstname.Length + person.SecondName.Length > 12)
                {
                    Console.WriteLine("ooooo. long name");
                }
                howold(yearsold);
            }
            else if (person.SecondName == "" && afirstname != "")
            {
                Console.WriteLine("Hello " + afirstname + ". nice to meet you. one name i see");
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
        }

        private static void howold(int age)
        {
            if (age < 10)
            {
                Console.WriteLine("hey there little guy");
            }
        }


    }
    class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public DateTime Birthday { get; set; }
        public int yearsold { get; set; }
        public  bool CheckUpper(string input)
        {
            if (input.ToUpper() == input)
            {
                return true;
            }
            return false;
        }
        public DateTime getBirth()
        {
            bool isvaliddate = false;
            DateTime birthday = new DateTime();
            while (!isvaliddate)
            {
                Console.WriteLine("Whats your birthday");
                string date = Console.ReadLine();
                isvaliddate = DateTime.TryParse(date, out birthday);
                if (!isvaliddate)
                {
                    Console.WriteLine("{0} is not a valid date", date);
                }
            }
            return birthday;
        }
        public int getAge()
        {
            int yearsold = DateTime.Today.Year - Birthday.Year;
            return yearsold;
        }

    }
}
