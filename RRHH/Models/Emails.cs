using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class Emails
    {
        public int ID { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public Applicants ApplicantReference { get; set; }
    }
}