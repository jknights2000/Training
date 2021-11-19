using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SupportBank
{
    class Import
    {
       public Dictionary<string, Account> accountsd { get; set; }
        public AllTransactions alltransactions { get; set; }
        public Import(string answer)
        {
            string extention;
            int pathstart = answer.ToLower().LastIndexOf("import") + 7;
            string path = answer.Substring(pathstart);
            if (File.Exists(path))
            {
                extention = Path.GetExtension(path);
                switch (extention)
                {
                    case ".csv":
                        CSV(path, alltransactions);
                        break;
                    case ".json":
                        JSON(path, alltransactions);
                        break;
                    case ".xml":
                        XML(path, alltransactions);
                        break;
                    case "":
                        throw (new Exception("this is not directed to a file"));

                    default:
                        throw (new Exception("This file is not supported"));


                }
            }
            else
            {
                throw (new Exception("File doesnt exist"));

            }
        }
        public static void CSV(string path, AllTransactions alltransactions)
        {
            alltransactions.Clear();
            Dictionary<string, Account> accountsd = new Dictionary<string, Account>();
            using (var reader = new StreamReader(path))
            {
                string headerLine = reader.ReadLine();
                //Console.WriteLine(headerLine);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    bool skip = false;
                    var values = line.Split(',');
                    string date = values[0];
                    try
                    {
                        DateTime d = DateTime.Parse(date);
                    }
                    catch (Exception e)
                    {
                        skip = true;
                        
                    }
                    string from = values[1];
                    string to = values[2];
                    string narrative = values[3];
                    decimal amount = 0;
                    try
                    {
                        amount = decimal.Parse(values[4]);
                    }
                    catch (Exception e)
                    {
                        skip = true;
                        
                    }
                    alltransactions.Add(new Transaction(date, from, to, narrative, amount));
                    if (accountsd.ContainsKey(from.ToLower()) && !skip)
                    {
                        ExistingAccount(from, date, from, to, narrative, -amount, accountsd);

                    }
                    else if (!skip)
                    {
                        NewAccount(from, date, from, to, narrative, -amount, accountsd);
                    }

                    if (accountsd.ContainsKey(to.ToLower()) && !skip)
                    {
                        ExistingAccount(to, date, from, to, narrative, amount, accountsd);

                    }
                    else if (!skip)
                    {
                        NewAccount(to, date, from, to, narrative, amount, accountsd);
                    }
                }
            }
            
            
        }
        public static void JSON(string path, AllTransactions alltransactions)
        {
            alltransactions.Clear();
            List<Transaction> a = new List<Transaction>();
            using (var reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                a = JsonConvert.DeserializeObject<List<Transaction>>(json);
            }

            Dictionary<string, Account> accountsd = new Dictionary<string, Account>();

            foreach (Transaction t in a)
            {
                string date = t.Date;
                string from = t.FromAccount;
                string to = t.ToAccount;
                string narrative = t.Narrative;
                decimal amount = t.Amount;

                alltransactions.Add(new Transaction(date, from, to, narrative, amount));

                if (accountsd.ContainsKey(from.ToLower()))
                {
                    ExistingAccount(from, date, from, to, narrative, -amount, accountsd);

                }
                else
                {
                    NewAccount(from, date, from, to, narrative, -amount, accountsd);
                }

                if (accountsd.ContainsKey(to.ToLower()))
                {
                    ExistingAccount(to, date, from, to, narrative, amount, accountsd);

                }
                else
                {
                    NewAccount(to, date, from, to, narrative, amount, accountsd);
                }
            }
            
        }
        public static void XML(string path, AllTransactions alltransactions)
        {
            alltransactions.Clear();
            Dictionary<string, Account> accountsd = new Dictionary<string, Account>();
            using (var reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Transaction>), new XmlRootAttribute("TransactionList"));

                //List<Transaction> a = (List<Transaction>)serializer.Deserialize(reader);
                List<Transaction> b = (List<Transaction>)serializer.Deserialize(reader);
                foreach (Transaction t in b)
                {
                    t.FromAccount = t.p.FromAccount;
                    t.ToAccount = t.p.ToAccount;

                    string date = t.Date;
                    string from = t.FromAccount;
                    string to = t.ToAccount;
                    string narrative = t.Narrative;
                    decimal amount = t.Amount;
                    alltransactions.Add(new Transaction(date, from, to, narrative, amount));
                    if (accountsd.ContainsKey(from.ToLower()))
                    {
                        ExistingAccount(from, date, from, to, narrative, -amount, accountsd);

                    }
                    else
                    {
                        NewAccount(from, date, from, to, narrative, -amount, accountsd);
                    }

                    if (accountsd.ContainsKey(to.ToLower()))
                    {
                        ExistingAccount(to, date, from, to, narrative, amount, accountsd);

                    }
                    else
                    {
                        NewAccount(to, date, from, to, narrative, amount, accountsd);
                    }
                }

            }

            
        }
        public static void NewAccount(string accountname, string date, string from, string to, string narrative, decimal amount, Dictionary<string, Account> accountsd)
        {
            Account A = new Account(accountname, amount);
            Transaction t = new Transaction(date, from, to, narrative, amount);
            A.Add(t);
            accountsd.Add(accountname.ToLower(), A);
            
        }
        public static void ExistingAccount(string accountname, string date, string from, string to, string narrative, decimal amount, Dictionary<string, Account> accountsd)
        {
            accountsd[accountname.ToLower()].Add(new Transaction(date, from, to, narrative, amount));
            accountsd[accountname.ToLower()].Amount += amount;
           
        }
    }
}
