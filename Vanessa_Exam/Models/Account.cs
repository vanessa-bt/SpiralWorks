using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vanessa_Exam.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string UserName { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
