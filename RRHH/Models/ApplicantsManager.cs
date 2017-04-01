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

            string sqlquery =   "SELECT        Applicants.ID, Applicants.Date, Applicants.Postulant, Applicants.Job, Applicants.InterviewStatus, Applicants.ApplicationStatus, " +
                                "Companys.Name AS CompanyName, Jobs.Company, Jobs.Name AS JobName, Jobs.ID AS JobID, Jobs.Description, Jobs.Status, JobStatuses.Details, JobStatuses.ID AS JobStatusID, " +
                                "JobApplications.ID AS JobApplicationID, JobApplications.Details AS JobApplicationDetails, dbo.Interviews.Status AS InterviewStatusName, Interviews.ID AS InterviewID " +
                                "FROM            dbo.Applicants INNER JOIN " +
                                "Jobs ON Applicants.Job = Jobs.ID " +
                                "INNER JOIN Companys ON Jobs.Company = Companys.ID INNER JOIN " +
                                "Interviews ON Applicants.InterviewStatus = Interviews.ID INNER JOIN " +
                                "JobApplications ON Applicants.ApplicationStatus = JobApplications.ID INNER JOIN " +
                                "JobStatuses ON Jobs.Status = JobStatuses.ID " +
                                "WHERE Applicants.Postulant = @Postulant ORDER BY Applicants.Date DESC";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Postulant", user.ID);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo la busqueda y le completo los datos 
                Applicants apply = new Applicants();
                apply.ID = (int)reader["ID"];
                apply.Date = (DateTime)reader["Date"];
                apply.Job = new Jobs();
                apply.Job.ID = (int)reader["JobID"];
                apply.Job.Name = (string)reader["JobName"];
                apply.Job.Description = (string)reader["Description"];
                apply.Job.Company = new Companys();
                apply.Job.Company.Name = (string)reader["CompanyName"];
                apply.Job.Status = new JobStatuses();
                apply.Job.Status.Details = (string)reader["Details"];
                apply.ApplicationStatus = new JobApplications();
                apply.ApplicationStatus.ID = (int)reader["JobApplicationID"];
                apply.ApplicationStatus.Details = (string)reader["JobApplicationDetails"];
                apply.InterviewStatus = new Interviews();
                apply.InterviewStatus.Status = (string)reader["InterviewStatusName"];
                applys.Add(apply);
            }

            return applys;
        }

        [Obsolete]
        public List<Applicants> ConsultarPorUserViejo(Users user)
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
                apply.ApplicationStatus = new JobsApplicationsManager().Consultar((int)reader["ApplicationStatus"]);
                apply.InterviewStatus = new InterviewsManager().Consultar((int)reader["InterviewStatus"]);
                applys.Add(apply);
            }

            return applys;
        }

        public Applicants ConsultarEstado(int busqueda_id, int user_id)
        {
            string sqlquery = "SELECT        Applicants.ID, Applicants.Date, Applicants.Postulant, Applicants.Job, Applicants.InterviewStatus, Applicants.ApplicationStatus, Users.FirstName," +
                                "Companys.Name AS CompanyName, Jobs.Company, Jobs.Name AS JobName, Jobs.Description, Jobs.Status, JobStatuses.Details, JobStatuses.ID AS JobStatusID, " +
                                "JobApplications.ID AS JobApplicationID, JobApplications.Details AS JobApplicationDetails, dbo.Interviews.Status AS InterviewStatusName, Interviews.ID AS InterviewID " +
                                "FROM            dbo.Applicants INNER JOIN " +
                                "Jobs ON Applicants.Job = Jobs.ID " +
                                "INNER JOIN Users ON Applicants.Postulant = Users.ID " +
                                "INNER JOIN Companys ON Jobs.Company = Companys.ID INNER JOIN " +
                                "Interviews ON Applicants.InterviewStatus = Interviews.ID INNER JOIN " +
                                "JobApplications ON Applicants.ApplicationStatus = JobApplications.ID INNER JOIN " +
                                "JobStatuses ON Jobs.Status = JobStatuses.ID " + 
                                "WHERE Postulant=@Postulant AND Job=@Job";
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
                apply.Postulant = new Users();
                apply.Postulant.FirstName = (string)reader["FirstName"];
                apply.Job = new Jobs();
                apply.Job.Name = (string)reader["JobName"];
                apply.Job.Description = (string)reader["Description"];
                apply.Job.Company = new Companys();
                apply.Job.Company.Name = (string)reader["CompanyName"];
                apply.Job.Status = new JobStatuses();
                apply.Job.Status.Details = (string)reader["Details"];
                apply.ApplicationStatus = new JobApplications();
                apply.ApplicationStatus.Details = (string)reader["JobApplicationDetails"];
                apply.InterviewStatus = new Interviews();
                apply.InterviewStatus.Status = (string)reader["InterviewStatusName"];
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
            string sqlquery = "SELECT        Applicants.ID, Applicants.Date, Applicants.Postulant, Applicants.Job, Applicants.InterviewStatus, Applicants.ApplicationStatus, Users.FirstName, Users.LastName, Users.Email, " +
                                "Companys.Name AS CompanyName, Jobs.Company, Jobs.Name AS JobName, Jobs.Description, Jobs.Status, JobStatuses.Details, JobStatuses.ID AS JobStatusID, " +
                                "JobApplications.ID AS JobApplicationID, JobApplications.Details AS JobApplicationDetails, dbo.Interviews.Status AS InterviewStatusName, Interviews.ID AS InterviewID " +
                                "FROM            dbo.Applicants INNER JOIN " +
                                "Jobs ON Applicants.Job = Jobs.ID " +
                                "INNER JOIN Users ON Applicants.Postulant = Users.ID " +
                                "INNER JOIN Companys ON Jobs.Company = Companys.ID INNER JOIN " +
                                "Interviews ON Applicants.InterviewStatus = Interviews.ID INNER JOIN " +
                                "JobApplications ON Applicants.ApplicationStatus = JobApplications.ID INNER JOIN " +
                                "JobStatuses ON Jobs.Status = JobStatuses.ID " +
                                "WHERE Applicants.ID=@ID";
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
                apply.Postulant = new Users();
                apply.Postulant.FirstName = (string)reader["FirstName"];
                apply.Postulant.LastName = (string)reader["LastName"];
                apply.Postulant.Email = (string)reader["Email"];
                apply.Job = new Jobs();
                apply.Job.Name = (string)reader["JobName"];
                apply.Job.Description = (string)reader["Description"];
                apply.Job.Company = new Companys();
                apply.Job.Company.Name = (string)reader["CompanyName"];
                apply.Job.Status = new JobStatuses();
                apply.Job.Status.Details = (string)reader["Details"];
                apply.ApplicationStatus = new JobApplications();
                apply.ApplicationStatus.Details = (string)reader["JobApplicationDetails"];
                apply.InterviewStatus = new Interviews();
                apply.InterviewStatus.Status = (string)reader["InterviewStatusName"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return apply;
        }

        [Obsolete]
        public Applicants ConsultarViejo(int ID)
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