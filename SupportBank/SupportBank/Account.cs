using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Account
    {
        List<Transaction> Transactions = new List<Transaction>();
        public string Name { get; set;}
        public decimal Amount { get; set;}

        public void addToTrans(Transaction t)
        {
            Transactions.Add(t);
        }
        public List<Transaction> getTrans()
        {
            return Transactions;
        }

    }
}
