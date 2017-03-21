using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Jobs
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //Relaciones
        public Companys Company { get; set; }
        public JobStatuses Status { get; set; }
    }
}