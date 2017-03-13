using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public DateTime JobDate { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        //Relaciones
        public Company CompanyID_claseJob { get; set; }
        public JobStatus JobStatusID_claseJob { get; set; }
    }
}