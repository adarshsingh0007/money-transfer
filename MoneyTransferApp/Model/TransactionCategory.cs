using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.Model
{
    public class TransactionCategory
    {
        public string CategoryName { get; set; }
        public ObservableCollection<string> Brands { get; set; }
        
    }
}
