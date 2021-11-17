using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
namespace Email_Extraction
{
    class Program
    {
        static void Main(string[] args)
        {

            //string text = System.IO.File.ReadAllText(@"C:\Users\Joshua.knights\Work\Training\Training\Email Extraction\Email Extraction\sample.txt");
            WebClient wc = new WebClient();
            string text = wc.DownloadString("https://api.media.atlassian.com/file/3a67c49e-cfa0-4456-9302-b6e9dc005bc1/binary?token=eyJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJlOThiMzVlMS1kZmMxLTQwZDgtYmExMS1lYWIwZjg2MWNjYmMiLCJhY2Nlc3MiOnsidXJuOmZpbGVzdG9yZTpmaWxlOjNhNjdjNDllLWNmYTAtNDQ1Ni05MzAyLWI2ZTlkYzAwNWJjMSI6WyJyZWFkIl19LCJleHAiOjE2MzcyNDE0OTEsIm5iZiI6MTYzNzE1ODUxMX0.c2T727lUqEijydVR5nA6uzf6w4oYqY_HeqN3xx1PQ0g&client=e98b35e1-dfc1-40d8-ba11-eab0f861ccbc&name=sample.txt&max-age=2940");

            //Console.WriteLine(text);
            Naive(text);
            Regexmethod(text);
            DictionaryMatch(text);
            Phone("07222 555555");
            Phone("+44 7222 555 555");
            Phone("+44 07222 555555");
            Name("Dr John Smith");
        }
        public static void Phone(string number)
        {
            Regex phone = new Regex(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$");//accounts for +44 and just 0
            if (phone.IsMatch(number))
            {
                Console.WriteLine(number + " is a valid UK number");
            }
            else
            {
                Console.WriteLine(number + " is not a valid UK number");
            }
        }
        public static void Name(string text)
        {
            Regex name = new Regex(@"(\w+)\s+(\w+)\s+(\w+)");
            foreach (Match match in name.Matches(text))
            {
                Console.WriteLine("Title: " + match.Groups[1]);
                Console.WriteLine("First Name: " + match.Groups[2]);
                Console.WriteLine("Second Name: " + match.Groups[3]);
            }
        }
        public static void Naive(string text)
        {
            int counter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (i + 13 < text.Length)
                {
                    if (text.Substring(i, 13) == "@softwire.com")
                    {
                        counter++;
                    }
                }
            }
            Console.WriteLine("Amount of @softwire.com Using Naive: " + counter);
        }
        public static void Regexmethod(string text)
        {
            Regex email = new Regex(@"[A-Za-z0-9-+.%_]+@softwire.com\s");
           int count = email.Matches(text).Count;
            Console.WriteLine("Amount of @softwire.com Using Regex: " + count);
        }
        public static void DictionaryMatch(string text)
        {
            //all emails
            Dictionary<string, int> emailenddic = new Dictionary<string, int>();
            Regex email = new Regex(@"\w+([-+.%]\w+)*@(\w+)([-.]\w+)*\.\w+([-.]\w+)*");
            foreach (Match match in email.Matches(text))
            {
                /*
                string emailmatch = match.Value;
                string emailend = emailmatch.Substring(emailmatch.LastIndexOf('@') + 1);
                
                int thefirst = emailend.IndexOf(".");
                emailend = emailend.Substring(0, thefirst);

                emailenddic.TryGetValue(emailend, out int currentnumber);
                emailenddic[emailend] = currentnumber + 1;
                */
                emailenddic.TryGetValue(match.Groups[2].ToString(), out int currentnumber);
                emailenddic[match.Groups[2].ToString()] = currentnumber + 1;
            }


            //only .com and co.uk
            Dictionary<string, int> emailenddicspec = new Dictionary<string, int>();
            Regex emailcom = new Regex(@"\w+([-+.%]\w+)*@(\w+)([-.]\w+)*(.com|.co.uk)");
            foreach (Match match in emailcom.Matches(text))
            {
                //string emailmatch = match.Value;
                /*
                string emailend = emailmatch.Substring(emailmatch.LastIndexOf('@') + 1);
                //only domains of vaild emails

                int thefirst = emailend.IndexOf(".");
                emailend = emailend.Substring(0, thefirst);
                */
                emailenddicspec.TryGetValue(match.Groups[2].ToString(), out int currentnumber);
                emailenddicspec[match.Groups[2].ToString()] = currentnumber +1;
           
            }

            //top ten of all emails
            var top10 = emailenddic.OrderByDescending(pair => pair.Value).Take(10);
            int postion = 0;
            Console.WriteLine("===========================");
            Console.WriteLine("   top ten of all emails    ");
            Console.WriteLine("===========================");
            foreach (KeyValuePair<string, int> dicitem in top10)
            {
                postion += 1;
                
                Console.WriteLine(String.Format("{0,2}. {1,-9} = {2,5}", postion, dicitem.Key, dicitem.Value));
            }
            Console.WriteLine();

            //top ten of specified emails
            var top10spec = emailenddicspec.OrderByDescending(pair => pair.Value).Take(10);
            postion = 0;
            Console.WriteLine("===========================");
            Console.WriteLine("top ten of specified emails");
            Console.WriteLine("===========================");
            foreach (KeyValuePair<string, int> dicitem1 in top10spec)
            {
                postion += 1;

                Console.WriteLine(String.Format("{0,2}. {1,-9} = {2,5}", postion, dicitem1.Key, dicitem1.Value));
            }


            Console.WriteLine("what frequency you want to see above");
            int userint = int.Parse(Console.ReadLine());

            var userdict = emailenddic.Where(pair => pair.Value > userint).OrderByDescending(pair => pair.Value);
            postion = 0;
            Console.WriteLine("==================================================");
            Console.WriteLine($"all emails that appear more than {userint} times");
            Console.WriteLine("==================================================");
            foreach (KeyValuePair<string, int> dicitem2 in userdict)
            {
                postion += 1;

                Console.WriteLine(String.Format("{0,2}. {1,-9} = {2,5}", postion, dicitem2.Key, dicitem2.Value));
            }
        }
    }
}
