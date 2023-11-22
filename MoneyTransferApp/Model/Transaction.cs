using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }        
        public string Category { get; set; }     
        public DateTime TransactionTime { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
       
    }
}
