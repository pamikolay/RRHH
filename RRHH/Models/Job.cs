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
        public Company Company_claseJob { get; set; }
        public JobStatus JobStatus_claseJob { get; set; }
    }
}