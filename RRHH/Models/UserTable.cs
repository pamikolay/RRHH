using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class UserTable
    {
        public int UserTableID { get; set; }
        public string UserTableFirstName { get; set; }
        public string UserTableLastName { get; set; }
        [Required]
        public string UserTableEmail { get; set; }
        public string UserTableAddress { get; set; }
        public string UserTablePhone { get; set; }
        [Required]
        public string UserTablePassword { get; set; }
        public string UserTableGenre { get; set; }
        public string CvStatus { get; set; }
        //Relaciones
        public City City { get; set; }
        public Province Province { get; set; }
        public UserType UserType { get; set; }
    }
}