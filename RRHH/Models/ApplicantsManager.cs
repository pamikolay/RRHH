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
        public int CheckApplicant(Users user, Jobs job)
        {
            string sqlquery = "SELECT COUNT(*) FROM Applicants WHERE Postulant=@Postulant AND Job=@Job";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", user.ID);
            sentencia.Parameters.AddWithValue("@Job", job.ID);

            Int32 a = (Int32)(sentencia.ExecuteScalar());

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
            return a;
        }
        public List<Applicants> ConsultarPorUser(Users user)
        {
            List<Applicants> applys = new List<Applicants>();

            string sqlquery = "SELECT * FROM Applicants WHERE Postulant=@Postulant ORDER BY Date DESC";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", user.ID);
            
            SqlDataReader reader= sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la busqueda y le completo los datos 
                Applicants apply = new Applicants();
                apply.ID = (int)reader["ID"];
                apply.Date = (DateTime)reader["Date"];
                apply.Job = new JobsManager().Consultar((int)reader["Job"]);
                apply.Postulant = new UsersManager().Consultar((int)reader["Postulant"]);
                apply.ApplicationStatus = new JobsApplicationsManager().Consultar((int)reader["ApplicationStatus"]);
                apply.InterviewStatus = new InterviewsManager().Consultar((int)reader["InterviewStatus"]);
                applys.Add(apply);
            }

            return applys;
        }
        public Applicants ConsultarEstado(int busqueda_id, int user_id)
        {
            string sqlquery = "SELECT * FROM Applicants WHERE Postulant=@Postulant AND Job=@Job";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", user_id);
            sentencia.Parameters.AddWithValue("@Job", busqueda_id);

            //creo la busqueda y le completo los datos 
            Applicants apply = new Applicants();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                apply.ID = (int)reader["ID"];
                apply.Date = (DateTime)reader["Date"];
                apply.Job = new JobsManager().Consultar((int)reader["Job"]);
                apply.Postulant = new UsersManager().Consultar((int)reader["Postulant"]);
                apply.ApplicationStatus = new JobsApplicationsManager().Consultar((int)reader["ApplicationStatus"]);
                apply.InterviewStatus = new InterviewsManager().Consultar((int)reader["InterviewStatus"]);
            }

            return apply;
        }
        public void ActualizarEstado(int applicant_id, int jobApp_id, int interview_id)
        {
            string sqlquery = "update Applicants set ApplicationStatus = @ApplicationStatus, InterviewStatus = @InterviewStatus WHERE ID = @ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            //sentencia.Parameters.AddWithValue("@ApplicationStatus", applicant.ApplicationStatus.ID);
            //sentencia.Parameters.AddWithValue("@InterviewStatus", applicant.InterviewStatus.ID);
            //sentencia.Parameters.AddWithValue("@ID", applicant.ID);
            sentencia.Parameters.AddWithValue("@ApplicationStatus", jobApp_id);
            sentencia.Parameters.AddWithValue("@InterviewStatus", interview_id);
            sentencia.Parameters.AddWithValue("@ID", applicant_id);

            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
        public Applicants Consultar(int ID)
        {
            string sqlquery = "select * from Applicants WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Applicants apply = new Applicants();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo la postulación, le completo los datos 
                apply.ID = (int)reader["ID"];
                apply.Date = (DateTime)reader["Date"];
                apply.Job = new JobsManager().Consultar((int)reader["Job"]);
                apply.Postulant = new UsersManager().Consultar((int)reader["Postulant"]);
                apply.ApplicationStatus = new JobsApplicationsManager().Consultar((int)reader["ApplicationStatus"]);
                apply.InterviewStatus = new InterviewsManager().Consultar((int)reader["InterviewStatus"]);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return apply;
        }
    }
}