using System;
using System.Collections.Generic;
using System.Text;

namespace SupportBank
{
    class Transaction
    {
        public string Date { get; set; }
        public string From { get; set; }

        public string To { get; set; }
        public string Narritive { get; set; }
        public decimal Amount { get; set; }
    }
}
