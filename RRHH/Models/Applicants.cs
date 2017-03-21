using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Applicants
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        //Relaciones
        public Users Postulant { get; set; }
        public Jobs Job { get; set; }
        public JobApplications ApplicationStatus { get; set; }
        public Interviews InterviewStatus { get; set; }
    }
}