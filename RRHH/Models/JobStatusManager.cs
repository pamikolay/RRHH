using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class JobStatusManager
    {
        public List<JobStatus> ConsultarTodos()
        {
            List<JobStatus> jobstatuses = new List<JobStatus>();

            string sqlquery = "select * from JobStatus";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                JobStatus jobstatus = new JobStatus();
                jobstatus.JobStatusID = (int)reader["JobStatusID"];
                jobstatus.JobStatusName = (string)reader["JobStatusDetails"];
                //AGREGO LA company A LA LISTA
                jobstatuses.Add(jobstatus);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobstatuses;
        }

        public JobStatus Consultar(int ID)
        {
            string sqlquery = "select * from JobStatus WHERE JobStatusID=@JobStatusID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@JobStatusID", ID);

            JobStatus jobstatus = new JobStatus();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                jobstatus.JobStatusID = (int)reader["JobStatusID"];
                jobstatus.JobStatusName = (string)reader["JobStatusDetails"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobstatus;
        }
    }
}