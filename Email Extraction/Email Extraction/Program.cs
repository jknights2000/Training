using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Email_Extraction
{
    class Program
    {
        static void Main(string[] args)
        {

            string text = System.IO.File.ReadAllText(@"C:\Users\Joshua.knights\Work\Training\Training\Email Extraction\Email Extraction\sample.txt");
            Console.WriteLine(text);
            Naive(text);
            Regexmethod(text);
            DictionaryMatch(text);
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
            Regex email = new Regex("[a-z0-9]+@softwire.com");
           int count = email.Matches(text).Count;
            Console.WriteLine("Amount of @softwire.com Using Regex: " + count);
        }
        public static void DictionaryMatch(string text)
        {
            IDictionary<string, int> emailenddic = new Dictionary<string, int>();
            Regex email = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            foreach(Match match in email.Matches(text))
            {
                string emailmatch = match.Value;
                string emailend = emailmatch.Substring(emailmatch.LastIndexOf('@') + 1);
                if (emailenddic.ContainsKey(emailend))
                {
                    emailenddic[emailend] += 1;
                }
                else
                {
                    emailenddic.Add(emailend, 1);
                }
                
            }
            /*
            foreach(KeyValuePair<string,int> dicitem in emailenddic)
            {
                Console.WriteLine("Key = {0}, Value = {1}", dicitem.Key, dicitem.Value);
            }
            */
            var top10 = emailenddic.OrderByDescending(pair => pair.Value).Take(10);
            
            foreach (KeyValuePair<string, int> dicitem in top10)
            {
                Console.WriteLine("Key = {0}, Value = {1}", dicitem.Key, dicitem.Value);
            }

        }
    }
}
