using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class ApplicantManager
    {
        public void Insertar(Applicant newApplicant)
        {
            string sqlquery = "INSERT INTO Applicant (ApplicantDate,UserTableID,JobID,JobApplicationID,InterviewID) VALUES (getdate(),@UserTableID,@JobID,1,1)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@UserTableID", newApplicant.User);
            sentencia.Parameters.AddWithValue("@JobID", newApplicant.Job);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
    }
}