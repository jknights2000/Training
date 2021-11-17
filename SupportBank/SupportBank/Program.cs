using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Account> accounts = new List<Account>();
            using (var reader = new StreamReader(@"C:\Users\Joshua.knights\Work\Training\Training\SupportBank\SupportBank\Transactions2014.csv"))
            {
                string headerLine = reader.ReadLine();
                //Console.WriteLine(headerLine);
                
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    bool from = false;
                    bool to = false;
                    foreach(Account account in accounts)
                    {
                        if(account.Name == values[1] && !from)
                        {
                            Transaction t = new Transaction();
                            t.Date = values[0];
                            t.From = values[1];
                            t.To = values[2];
                            t.Narritive = values[3];
                            t.Amount = -(decimal.Parse(values[4]));
                            account.addToTrans(t);
                            account.Amount += -(decimal.Parse(values[4]));
                            from = true;
                        }
                        else if(account.Name == values[2] && !to)
                        {
                            Transaction t = new Transaction();
                            t.Date = values[0];
                            t.From = values[1];
                            t.To = values[2];
                            t.Narritive = values[3];
                            t.Amount = (decimal.Parse(values[4]));
                            account.addToTrans(t);
                            account.Amount += (decimal.Parse(values[4]));
                            to = true;
                        }
                    }
                    if(!from)
                    {
                        Account fromA = new Account();
                        fromA.Name = values[1];
                        fromA.Amount = -(decimal.Parse(values[4]));
                        Transaction tfrom = new Transaction();
                        tfrom.Date = values[0];
                        tfrom.From = values[1];
                        tfrom.To = values[2];
                        tfrom.Narritive = values[3];
                        tfrom.Amount = -(decimal.Parse(values[4]));
                        fromA.addToTrans(tfrom);
                        accounts.Add(fromA);
                    }
                    if(!to)
                    {
                        Account toA = new Account();
                        toA.Name = values[2];
                        toA.Amount = (decimal.Parse(values[4]));
                        Transaction t = new Transaction();
                        t.Date = values[0];
                        t.From = values[1];
                        t.To = values[2];
                        t.Narritive = values[3];
                        t.Amount = -(decimal.Parse(values[4]));
                        toA.addToTrans(t);
                        accounts.Add(toA);
                    }
                    Console.WriteLine(line);
                    
                }
            }
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("what you want to do?");
                string answer = Console.ReadLine();
                answer = answer.Trim();
                if (answer.ToLower().Equals("listall"))
                {
                    ListAll(accounts);
                }else if (answer.ToLower().StartsWith("list"))
                {
                    int findname = answer.LastIndexOf("List") + 5;
                    string name = answer.Substring(findname);
                    List(accounts, name.Trim());
                }
                else if (answer.ToLower().Equals("exit"))
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("invalid option");
                }
            }
           
            //ListAll(accounts);
            //List(accounts,"Jon A");
        }
        
        public static void ListAll(List<Account> accounts)
        {
            foreach(Account a in accounts)
            {
                Console.WriteLine($"Name:{a.Name} Bank Balance:{a.Amount.ToString("C0")}");
            }
        }
        
        public static void List(List<Account> accounts,string name)
        {
            foreach (Account account in accounts)
            {
                if(name.ToLower() == account.Name.ToLower())
                {
                    foreach (Transaction t in account.getTrans())
                    {
                        Console.WriteLine($"Date:{t.Date} From:{t.From} To:{t.To} Narrative:{t.Narritive} Amount:{t.Amount.ToString("C0")}");
                    }
                    return;
                }
            }
            Console.WriteLine("no account with name " + name);
          
        }
    }
}
