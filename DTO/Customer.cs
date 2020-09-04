using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace E4App.DTO
{
    public class Customer
    {
        [Required]
        public Guid CustomerID { get; set; }
        [ MaxLength(20, ErrorMessage = "Name is too long."), MinLength(2, ErrorMessage = "Name is too short.")]
        public string Name { get; set; }
        [MaxLength(20, ErrorMessage = "Surname is too short."), MinLength(2, ErrorMessage = "Surname is too short.")]
        public string Surname { get; set; }
 
        [RegularExpression(@"^0[78][2347][0-9]{7}", ErrorMessage = "Cellphone is not valid")]
        public string Cellphone { get; set; }
    }
}

 