using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }        
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
