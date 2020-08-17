using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vanessa_Exam.Models
{
    public class TransferMoney
    {
        public int SourceAccountID { get; set; }
        public int DestinationAccountID { get; set; }
        public decimal Amount { get; set; }
    }
}
