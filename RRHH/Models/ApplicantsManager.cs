using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class ApplicantsManager
    {
        public void Insertar(Applicants newApplicant)
        {
            string sqlquery = "INSERT INTO Applicants (Date,Postulant,Job,ApplicationStatus,InterviewStatus) VALUES (getdate(),@Postulant,@Job,1,1)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", newApplicant.Postulant.ID);
            sentencia.Parameters.AddWithValue("@Job", newApplicant.Job.ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
    }
}