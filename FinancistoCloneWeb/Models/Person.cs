using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [MinLength(2)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        public string Document { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Address { get; set; }
        [Required]
        [EmailAddress]        
        public string Email { get; set; }
        [RegularExpression(@"[a-zA-Z0-9]+")]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
