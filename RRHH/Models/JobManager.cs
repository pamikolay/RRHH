using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class JobManager
    {
        public void Insertar(Job newJob)
        {
            string sqlquery="insert into Job (JobName,JobDate,JobDescription,CompanyID,JobStatusID) VALUES (@JobName, getdate(), @JobDescription, @CompanyID, 2)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@JobName", newJob.JobName);
            sentencia.Parameters.AddWithValue("@JobDescription", newJob.JobDescription);
            sentencia.Parameters.AddWithValue("@CompanyID", newJob.Company.CompanyID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public void InactivarJob(Job job)
        {
            this.InactivarJob(job.JobID);
        }

        public void InactivarJob(long ID)
        {
            string sqlquery = "UPDATE Job set JobStatusID = 1 where Id = @Id";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Id", ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        /// <summary>
        /// Modificamos todo un trabajo
        /// </summary>
        /// <param name="job"></param>
        public void Actualizar(Job job)
        {
            string sqlquery = "UPDATE Job set JobName = @JobName, JobDescription = @JobDescription, Company = @Company, JobStatus = @JobStatus where JobID = @JobID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            //4-escribrimos la sentencia
            sentencia.Parameters.AddWithValue("@JobName", job.JobName);
            sentencia.Parameters.AddWithValue("@JobDescription", job.JobDescription);
            sentencia.Parameters.AddWithValue("@Company", job.Company.CompanyID);
            sentencia.Parameters.AddWithValue("@JobStatus", job.JobStatus.JobStatusID);
            sentencia.Parameters.AddWithValue("@JobID", job.JobID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public List<Job> ConsultarTodos()
        {
            List<Job> jobs = new List<Job>();

            string sqlquery = "SELECT * FROM Job";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Job job = new Job();
                job.JobID = (int)reader["JobID"];
                job.JobDate = (DateTime)reader["JobDate"];
                job.JobName = (string)reader["JobName"];
                job.JobDescription = (string)reader["JobDescription"];
                job.Company.CompanyID=(int)(reader["Company"]);
                job.JobStatus.JobStatusID = (int)(reader["JobStatus"]);
                //AGREGO EL job A LA LISTA
                jobs.Add(job);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobs;
        }

        public List<Job> ConsultarActivas()
        {
            List<Job> jobs = new List<Job>();

            string sqlquery = "SELECT * FROM Job WHERE JobStatusID = 2";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            CompanyManager cManager = new CompanyManager();     //para pasarle un objeto company necesito el CompanyManager
            
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Job job = new Job();
                job.JobID = (int)reader["JobID"];
                job.JobDate = (DateTime)reader["JobDate"];        
                job.JobName = (string)reader["JobName"];
                job.JobDescription = (string)reader["JobDescription"];
                job.Company = cManager.Consultar((int)reader["CompanyID"]);
                //agrego el job a la lista
                jobs.Add(job);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return jobs;
        }

        public Job Consultar(int ID)
        {
            string sqlquery = "select * from Job WHERE JobID=@JobID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@JobID", ID);

            Job job = new Job();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                job.JobID = (int)reader["JobID"];
                job.JobDate = (DateTime)reader["JobDate"];    
                job.JobName = (string)reader["JobName"];
                job.JobDescription = (string)reader["JobDescription"];
                CompanyManager cManager = new CompanyManager();
                job.Company = cManager.Consultar((int)reader["CompanyID"]);
                
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return job;
        }

        /// <summary>
        /// Trae los puestos publicados por la compañia pasada por parámetro
        /// </summary>
        /// <param name="copmany"></param>
        /// <returns></returns>
        public List<Job> Consultar(Company company)
        {
            return new List<Job>();
        }
    }
}