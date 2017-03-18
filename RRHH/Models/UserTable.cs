using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class UserTable
    {
        public int UserTableID { get; set; }
        public string UserTableFirstName { get; set; }
        public string UserTableLastName { get; set; }
        public string UserTableEmail { get; set; }
        public string UserTableAddress { get; set; }
        public string UserTablePhone { get; set; }
        public string UserTablePassword { get; set; }
        public string UserTableGenre { get; set; }
        //Relaciones
        public City CityID { get; set; }
        public Province ProvinceID { get; set; }
        public Cv CvID { get; set; }
        public UserType UserTypeID { get; set; }
    }
}