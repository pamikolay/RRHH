using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        public string Genre { get; set; }
        public string CvStatus { get; set; }
        //Relaciones
        public Citys City { get; set; }
        public Provinces Province { get; set; }
        public UserTypes UserType { get; set; }
    }
}