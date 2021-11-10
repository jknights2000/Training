using System;
using System.Collections.Generic;
using System.Text;

namespace Greeting
{
    class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public DateTime Birthday { get; set; }
        public int age
        {
            get
            {
                int age = DateTime.Today.Year - Birthday.Year;
                if (DateTime.Now < Birthday.AddYears(age))// birthday hasnt actually happended yet this year
                    age--; // subtract a year

                return age;

            }
        }
        public bool IsBlank(string s)
        {
            if (s.Trim().Length == 0)
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
        

        

    }
}
