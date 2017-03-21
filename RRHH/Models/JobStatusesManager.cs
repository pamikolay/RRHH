using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class JobStatusesManager
    {
        public List<JobStatuses> ConsultarTodos()
        {
            List<JobStatuses> jobstatuses = new List<JobStatuses>();

            string sqlquery = "select * from JobStatuses";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                JobStatuses jobstatus = new JobStatuses();
                jobstatus.ID = (int)reader["ID"];
                jobstatus.Details = (string)reader["Details"];
                //AGREGO LA company A LA LISTA
                jobstatuses.Add(jobstatus);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobstatuses;
        }

        public JobStatuses Consultar(int ID)
        {
            string sqlquery = "select * from JobStatuses WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            JobStatuses jobstatus = new JobStatuses();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                jobstatus.ID = (int)reader["ID"];
                jobstatus.Details = (string)reader["Details"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobstatus;
        }
    }
}