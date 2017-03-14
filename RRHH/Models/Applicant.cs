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
        public UserTable UserTableID { get; set; }
        public Job JobID { get; set; }
        public JobApplication JobApplicationID { get; set; }
        public Interview InterviewID { get; set; }
    }
}