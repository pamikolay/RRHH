using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class JobsApplicationsManager
    {
        public List<JobApplications> ConsultarTodos()
        {
            List<JobApplications> jobApplications = new List<JobApplications>();

            string sqlquery = "select * from JobApplications";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                JobApplications jobApplication = new JobApplications();
                jobApplication.ID = (int)reader["ID"];
                jobApplication.Details = (string)reader["Details"];
                //AGREGO LA company A LA LISTA
                jobApplications.Add(jobApplication);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobApplications;
        }

        public JobApplications Consultar(int ID)
        {
            string sqlquery = "select * from JobApplications WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            JobApplications jobApplication = new JobApplications();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                jobApplication.ID = (int)reader["ID"];
                jobApplication.Details = (string)reader["Details"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobApplication;
        }
    }
}