using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace RRHH.Models
{
    public class EmailsManager
    {
        public void Insertar(Emails email)
        {
            string sqlquery = "INSERT INTO Emails (Date,ApplicantReference,Asunto,Cuerpo) VALUES (getdate(),@ApplicantReference,@Asunto,@Cuerpo)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ApplicantReference", email.ApplicantReference.ID);
            sentencia.Parameters.AddWithValue("@Asunto", email.Asunto);
            sentencia.Parameters.AddWithValue("@Cuerpo", email.Cuerpo);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public string EmailCambioEstado(Applicants applicant)
        {
            string mensaje = "";
            string cuerpo = "";
            string asunto = "";
            try
            {
                //Definimos la conexión al servidor SMTP que vamos a usar
                //para mandar el mail. Hay que buscarla en nuestro proveedor
                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
                clienteSmtp.Credentials = new NetworkCredential("rrhh.proyecto.comit@gmail.com", "proyectoCom17");
                clienteSmtp.EnableSsl = true;

                //Defino el cuerpo del email dependiendo el estado de la aplicación del trabajo y el estado de la entrevista
                if (applicant.ApplicationStatus.ID == 2)
                {
                    cuerpo = "Tu Cv ha sido leido";
                }
                if (applicant.ApplicationStatus.ID == 3)
                {
                    cuerpo = "Tu perfil ha sido seleccionado para una entrevista.";
                    if (applicant.InterviewStatus.ID == 2)
                    {
                        cuerpo = cuerpo + " Ya se ha programado la entrevista";
                    }
                    if (applicant.InterviewStatus.ID == 3)
                    {
                        cuerpo = cuerpo + " El resultado de la entrevista ha sido positivo, nos contactaremos para indicarte como sigue el proceso";
                    }
                    if (applicant.InterviewStatus.ID == 4)
                    {
                        cuerpo = cuerpo + " Lo lamentamos pero tu perfil no continuara en la busqueda";
                    }
                    if (applicant.InterviewStatus.ID == 5)
                    {
                        cuerpo = cuerpo + " No has asistido a la entrevista";
                    }
                }
                if (applicant.ApplicationStatus.ID == 4)
                {
                    cuerpo = "Tu perfil ha sido seleccionado para una entrevista con la empresa " + applicant.Job.Company.Name +".";
                    if (applicant.InterviewStatus.ID == 2)
                    {
                        cuerpo = cuerpo + " Ya se ha programado la entrevista";
                    }
                    if (applicant.InterviewStatus.ID == 3)
                    {
                        cuerpo = cuerpo + " El resultado de la entrevista ha sido positivo, nos contactaremos para indicarte como sigue el proceso";
                    }
                    if (applicant.InterviewStatus.ID == 4)
                    {
                        cuerpo = cuerpo + " Lo lamentamos pero tu perfil no continuara en la busqueda";
                    }
                    if (applicant.InterviewStatus.ID == 5)
                    {
                        cuerpo = cuerpo + " No has asistido a la entrevista";
                    }
                }
                if (applicant.ApplicationStatus.ID == 5)
                {
                    cuerpo = "Felicitaciones!!! Has sido contratado para " + applicant.Job.Name + ".";
                }


                //Generamos el objeto MAIL a enviar
                MailMessage mailAEnviar = new MailMessage();
                mailAEnviar.From = new MailAddress("rrhh.proyecto.comit@gmail.com", "RRHH Proyecto ComIT-2017");
                mailAEnviar.To.Add(applicant.Postulant.Email);
                asunto= "Avance proceso de selección";
                mailAEnviar.Subject = asunto;
                mailAEnviar.IsBodyHtml = true;
                mailAEnviar.Body = "Hola " + applicant.Postulant.FirstName + " " + applicant.Postulant.LastName + ", <br />Te comentamos como avanza tu proceso de selección: <br />" + cuerpo + "<br /><br /><b>Saludos!!!<br />World Solution RRHH";

                //mandamos el mail
                clienteSmtp.Send(mailAEnviar);
                //Si todo sale bien configuro una mensaje
                mensaje = "Se ha enviado correctamente el informe del cambio estado al candidato mediante un e-mail.";
            }
            catch (Exception ex)
            {
                mensaje = "Lo sentimos, ha ocurrido un error al enviar el mail. Consulta al administrador";
                string detalleError = ex.Message;   //guardo el error para ver que paso pero no lo muestro al usuario
                                                    //mensaje = "ERROR: " + ex.Message;
            }

            Emails email = new Emails();
            email.ApplicantReference = applicant;
            email.Asunto = asunto;
            email.Cuerpo = cuerpo;

            EmailsManager eManager = new EmailsManager();
            eManager.Insertar(email);

            return mensaje;
        }
    }
}