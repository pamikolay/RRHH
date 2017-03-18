using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Applicant
    {
        public int ApplicantID { get; set; }
        public DateTime ApplicantDate { get; set; }
        //Relaciones
        public UserTable User { get; set; }
        public Job Job { get; set; }
        public JobApplication JobApplicationStatus { get; set; }
        public Interview InterviewStatus { get; set; }
    }
}