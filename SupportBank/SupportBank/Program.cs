using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace SupportBank
{
    class Program
    {
       
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Log.txt", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            Trans2012();
            //XAML(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2012.xml");
            //Trans2013();
            //Trans2014();
            //Trans2015();
            //JSON(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2013.json");
            
        }
       

        
        public static void Trans2012()
        {
            Logger.Info("Transaction 2012 strarted");
            Dictionary<string, Account> accountsd = XAML(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2012.xml");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("what you want to do?");
                string answer = Console.ReadLine();
                answer = answer.Trim();
                if (answer.ToLower().Equals("listall"))
                {
                    Logger.Info("User listed all accounts");
                    ListAll(accountsd);
                }
                else if (answer.ToLower().StartsWith("list"))
                {
                    Logger.Info($"User listed all transactions for account named {answer}");
                    int findname = answer.LastIndexOf("List") + 5;
                    string name = answer.Substring(findname);
                    List(accountsd, name.Trim());
                }
                else if (answer.ToLower().Equals("exit"))
                {
                    Logger.Info("User wants to exit");
                    exit = true;
                }
                else
                {
                    Logger.Warn("invalid option chosen");
                    Console.WriteLine("invalid option");
                }
            }
            Logger.Info("Transaction 2012 ended");
        }
        
        
        
        public static Dictionary<string,Account> CSV(string path)
        {
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
                        Logger.Error($"{date} is not a vaild date");
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
                        Logger.Error($"{values[4]} is not a valid decimal");
                    }

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
            Logger.Info("Data added");
            return accountsd;
        }
        public static Dictionary<string, Account> JSON(string path)
        {
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
                if (accountsd.ContainsKey(from.ToLower()) )
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
            return accountsd;
        }
        public static Dictionary<string, Account> XAML(string path)
        {
            Dictionary<string, Account> accountsd = new Dictionary<string, Account>();
            using (var reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Transaction>),new XmlRootAttribute("TransactionList"));

                //List<Transaction> a = (List<Transaction>)serializer.Deserialize(reader);
                List<Transaction> b = (List < Transaction > )serializer.Deserialize(reader);
                foreach(Transaction t in b) {
                    t.FromAccount = t.p.FromAccount;
                    t.ToAccount = t.p.ToAccount;

                    string date = t.Date;
                    string from = t.FromAccount;
                    string to = t.ToAccount;
                    string narrative = t.Narrative;
                    decimal amount = t.Amount;
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
/*
                if (a.Count == 0)
                    Console.WriteLine("nothing added");

                foreach(Transaction t in a)
                {
                    Console.WriteLine(string.Format("{0,10} || {1,-10} || {2,-10} || {3,-40} || {4,-8} ||", t.Date, t.FromAccount, t.ToAccount, t.Narrative, t.Amount.ToString("C")));
                
                }
*/
            }
            
                return accountsd;
        }
        
        
        
        
        
        
        
        
        public static void Trans2013()
        {
            Logger.Info("Transaction 2013 strarted");
            Dictionary<string, Account> accountsd = JSON(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2013.json");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("what you want to do?");
                string answer = Console.ReadLine();
                answer = answer.Trim();
                if (answer.ToLower().Equals("listall"))
                {
                    Logger.Info("User listed all accounts");
                    ListAll(accountsd);
                }
                else if (answer.ToLower().StartsWith("list"))
                {
                    Logger.Info($"User listed all transactions for account named {answer}");
                    int findname = answer.LastIndexOf("List") + 5;
                    string name = answer.Substring(findname);
                    List(accountsd, name.Trim());
                }
                else if (answer.ToLower().Equals("exit"))
                {
                    Logger.Info("User wants to exit");
                    exit = true;
                }
                else
                {
                    Logger.Warn("invalid option chosen");
                    Console.WriteLine("invalid option");
                }
            }
            Logger.Info("Transaction 2013 ended");
        }
        public static void Trans2014()
        {
            Logger.Info("Transaction 2014 strarted");
            Dictionary<string, Account> accountsd = CSV(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2014.csv");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("what you want to do?");
                string answer = Console.ReadLine();
                answer = answer.Trim();
                if (answer.ToLower().Equals("listall"))
                {
                    Logger.Info("User listed all accounts");
                    ListAll(accountsd);
                }
                else if (answer.ToLower().StartsWith("list"))
                {
                    Logger.Info($"User listed all transactions for account named {answer}");
                    int findname = answer.LastIndexOf("List") + 5;
                    string name = answer.Substring(findname);
                    List(accountsd, name.Trim());
                }
                else if (answer.ToLower().Equals("exit"))
                {
                    Logger.Info("User wants to exit");
                    exit = true;
                }
                else
                {
                    Logger.Warn("invalid option chosen");
                    Console.WriteLine("invalid option");
                }
            }
            Logger.Info("Transaction 2014 ended");
        }
        public static void Trans2015()
        {

            Logger.Info("Transaction 2015 strarted");
            Dictionary<string, Account> accountsd = CSV(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\DodgyTransactions2015.csv");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("what you want to do?");
                string answer = Console.ReadLine();
                answer = answer.Trim();
                if (answer.ToLower().Equals("listall"))
                {
                    Logger.Info("User listed all accounts");
                    ListAll(accountsd);
                }
                else if (answer.ToLower().StartsWith("list"))
                {
                    Logger.Info($"User listed all transactions for account named {answer}");
                    int findname = answer.LastIndexOf("List") + 5;
                    string name = answer.Substring(findname);
                    List(accountsd, name.Trim());
                }
                else if (answer.ToLower().Equals("exit"))
                {
                    Logger.Info("User wants to exit");
                    exit = true;
                }
                else
                {
                    Logger.Warn("invalid option chosen");
                    Console.WriteLine("invalid option");
                }
            }
            Logger.Info("Transaction 2015 ended");
        }
        public static void NewAccount(string accountname,string date,string from, string to, string narrative, decimal amount, Dictionary<string, Account> accountsd)
        {
            Account A = new Account(accountname, amount);
            Transaction t = new Transaction(date, from, to, narrative, amount);
            A.Add(t);
            accountsd.Add(accountname.ToLower(), A);
            Logger.Info($"New Account for name {accountname} has been made");
        }
        public static void ExistingAccount(string accountname,string date, string from, string to, string narrative, decimal amount, Dictionary<string, Account> accountsd)
        {
            accountsd[accountname.ToLower()].Add(new Transaction(date, from, to, narrative, amount));
            accountsd[accountname.ToLower()].Amount += amount;
            Logger.Info($"update list of transactions and amount for account with the name {accountname}");
        }
        public static void ListAll(Dictionary<string, Account> accountsd)
        {
            foreach(KeyValuePair<string,Account> a in  accountsd)
            {
                if(a.Value.Amount < 0)
                {
                    decimal output = -(a.Value.Amount);
                    
                    Console.WriteLine(String.Format("{0,-10} {1,-10} {2,-10}", a.Value.Name,"owes", output.ToString("C")));
                }
                else
                {
                    Console.WriteLine(String.Format("{0,-10} {1,-10} {2,-10}", a.Value.Name,"is owed", a.Value.Amount.ToString("C")));
                    
                }
                
            }
            Logger.Info("Listed all data");
        }
        
        public static void List(Dictionary<string, Account> accountsd, string name)
        {
            Console.WriteLine(string.Format("{0,10} || {1,-10} || {2,-10} || {3,-40} || {4,-8} ||", "Date","From","To", "Narritive","Amount"));
            

            if (accountsd.ContainsKey(name.ToLower()))
            {
                    foreach (Transaction t in accountsd[name.ToLower()])
                    {
                        Console.WriteLine(string.Format("{0,10} || {1,-10} || {2,-10} || {3,-40} || {4,-8} ||", t.Date, t.FromAccount, t.ToAccount, t.Narrative, t.Amount.ToString("C")));
                    }
                Logger.Info($"Listed data for {name}");
                return;
                
            }
            
            Logger.Warn($"no data for {name}");
            Console.WriteLine("no account with name " + name);
          
        }
    }
}
