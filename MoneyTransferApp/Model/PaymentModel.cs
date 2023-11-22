using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.Model
{
    public class PaymentModel
    {
        public string CardholderName { get; set; }
        public string Price { get; set; }
        public string ExpiryDate { get; set; }
        public string CardNumber { get; set; }

       
    }
}
