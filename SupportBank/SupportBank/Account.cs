using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Account:List<Transaction>
    {
        
        public string Name { get; set;}
        public decimal Amount { get; set;}
        public Account(string Name,decimal Ammount)
        {
            this.Name = Name;
            this.Amount = Ammount;
        }


    }
}
