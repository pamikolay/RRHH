using System;
using System.Collections.Generic;
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
        public Company CompanyID_claseJob { get; set; }    //preguntar si la propiedad tiene q tener el mismo nombre de la tabla
        public JobStatus JobStatusID_claseJob { get; set; }
    }
}