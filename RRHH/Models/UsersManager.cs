using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class UsersManager
    {
        public void Insertar(Users newUser)
        {
            string sqlquery = "INSERT INTO Users (FirstName,LastName,Email,Address,Phone,Password,Genre,Province,City,CvStatus,UserType) VALUES (@FirstName,@LastName,@Email,@Address,@Phone,@Password,@Genre,@Province,@City,@CvStatus,2)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@FirstName", newUser.FirstName);
            sentencia.Parameters.AddWithValue("@LastName", newUser.LastName);
            sentencia.Parameters.AddWithValue("@Email", newUser.Email);
            sentencia.Parameters.AddWithValue("@Address", newUser.Address);
            sentencia.Parameters.AddWithValue("@Phone", newUser.Phone);
            sentencia.Parameters.AddWithValue("@Password", newUser.Password);
            sentencia.Parameters.AddWithValue("@Genre", newUser.Genre);
            sentencia.Parameters.AddWithValue("@Province", newUser.Province.ID);
            sentencia.Parameters.AddWithValue("@City", newUser.City.ID);
            sentencia.Parameters.AddWithValue("@CvStatus", "NoCargado");
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
        public int CheckUser(Users user)
        {
            string sqlquery = "uspLogin";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.CommandType = System.Data.CommandType.StoredProcedure;
            sentencia.Parameters.Add (new SqlParameter ("@Email", user.Email));
            sentencia.Parameters.Add(new SqlParameter("@Password", user.Password));
            //Retorno el ID
            return Convert.ToInt32(sentencia.ExecuteScalar());
        }
        public Users Consultar(string email)
        {
            string sqlquery = "select * from Users WHERE Email=@Email";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Email", email);

            Users user = new Users();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                user.ID = (int)reader["ID"];
                user.FirstName = (string)reader["FirstName"];
                user.LastName= (string)reader["LastName"];
                user.Email = (string)reader["Email"];
                CitysManager cManager = new CitysManager();
                user.City = cManager.Consultar((int)reader["City"]);
                ProvincesManager pManager = new ProvincesManager();
                user.Province = pManager.Consultar((int)reader["Province"]);
                UserTypesManager uManager = new UserTypesManager();
                user.UserType = uManager.Consultar((int)reader["UserType"]);
                user.CvStatus = (string)reader["CvStatus"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return user;
        }
        public Users Validar(string email, string password)
        {
            string sqlquery = "select * from Users WHERE Email=@Email AND Password = @Password ";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Email", email);
            sentencia.Parameters.AddWithValue("@Password", password);

            Users user = new Users();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                user.ID = (int)reader["ID"];
                user.FirstName = (string)reader["FirstName"];
                user.LastName = (string)reader["LastName"];
                user.Email = (string)reader["Email"];
                CitysManager cManager = new CitysManager();
                user.City = cManager.Consultar((int)reader["City"]);
                ProvincesManager pManager = new ProvincesManager();
                user.Province = pManager.Consultar((int)reader["Province"]);
                UserTypesManager uManager = new UserTypesManager();
                user.UserType = uManager.Consultar((int)reader["UserType"]);
                user.CvStatus = (string)reader["CvStatus"];
            }
            else
            {
                user = null;
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return user;
        }
    }
}