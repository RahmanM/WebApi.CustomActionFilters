using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.API.Filters.Models
{
    public class Customer
    {
        [Required, MaxLength(25)]
        public string FirstName { get; set; }
        [Required, MaxLength(25)]
        public string LastName { get; set; }
        [Required, MaxLength(18)]
        public string Phone { get; set; }
        public int Id { get; set; }
    }
}