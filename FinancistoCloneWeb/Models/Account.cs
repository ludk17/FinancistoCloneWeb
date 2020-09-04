using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
