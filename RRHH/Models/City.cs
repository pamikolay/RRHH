using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class City
    {
        public  int CityID { get; set; }
        public string CityName { get; set; }
        //Relaciones
        public Province ProvinceID { get; set; }
    }
}