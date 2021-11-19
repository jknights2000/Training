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
            Menu();
           // AllTransactions alltransactions = new AllTransactions();
            //Dictionary<string, Account> accountsd = CSV(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2014.csv",alltransactions);
            //alltransactions.ExportXML("test3");
            
        }
       
        public static void Menu()
        {
            AllTransactions alltransactions = new AllTransactions();
            Dictionary<string, Account> accountsd = new Dictionary<string, Account>();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("what you want to do?");
                string answer = Console.ReadLine();
                answer = answer.Trim();
                if (answer.ToLower().Equals("listall"))
                {
                    if (accountsd.Count > 0)
                    {
                        Logger.Info("User listed all accounts");
                        ListAll(accountsd);
                    }
                    else
                    {
                        Console.WriteLine("no file imported");
                    }
                }
                else if (answer.ToLower().StartsWith("list"))
                {
                    if (accountsd.Count > 0)
                    {
                        Logger.Info($"User listed all transactions for account named {answer}");
                        int findname = answer.ToLower().LastIndexOf("list") + 5;
                        string name = answer.Substring(findname);
                        List(accountsd, name.Trim());
                    }
                    else
                    {
                        Console.WriteLine("no file imported");
                        Logger.Warn("no file imported");
                    }
                }
                else if (answer.ToLower().StartsWith("import"))
                { 
                    try
                    {
                        Import i = new Import(answer);
                        accountsd =  i.accountsd;
                        alltransactions = i.alltransactions;
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Logger.Error(e.Message);
                    }

                }
                else if (answer.ToLower().StartsWith("export"))
                {
                    new Export(answer, alltransactions);
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
} /*
        public static void Trans2012()
        {
            Logger.Info("Transaction 2012 strarted");
            Dictionary<string, Account> accountsd = XML(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2012.xml");
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
        
        */
/*  

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
*/
