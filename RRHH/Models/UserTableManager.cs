using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class UserTableManager
    {
        public void Insertar(UserTable newUser)
        {
            string sqlquery = "INSERT INTO UserTable (UserTableFirstName,UserTableLastName,UserTableEmail,UserTableAddress,UserTablePhone,UserTablePassword,UserTableGenre,ProvinceID,CityID,CvID,UserTypeID) VALUES (@UserTableFirstName,@UserTableLastName,@UserTableEmail,@UserTableAddress,@UserTablePhone,@UserTablePassword,@UserTableGenre,@ProvinceID,@CityID,1,2)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@UserTableFirstName", newUser.UserTableFirstName);
            sentencia.Parameters.AddWithValue("@UserTableLastName", newUser.UserTableLastName);
            sentencia.Parameters.AddWithValue("@UserTableEmail", newUser.UserTableEmail);
            sentencia.Parameters.AddWithValue("@UserTableAddress", newUser.UserTableAddress);
            sentencia.Parameters.AddWithValue("@UserTablePhone", newUser.UserTablePhone);
            sentencia.Parameters.AddWithValue("@UserTablePassword", newUser.UserTablePassword);
            sentencia.Parameters.AddWithValue("@UserTableGenre", newUser.UserTableGenre);
            sentencia.Parameters.AddWithValue("@ProvinceID", newUser.Province.ProvinceID);
            sentencia.Parameters.AddWithValue("@CityID", newUser.City.CityID);
            sentencia.Parameters.AddWithValue("@CvStatus", "NoCargado");
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
        public int CheckUser(UserTable user)
        {
            string sqlquery = "uspLogin";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.CommandType = System.Data.CommandType.StoredProcedure;
            sentencia.Parameters.Add (new SqlParameter ("@UserTableEmail", user.UserTableEmail));
            sentencia.Parameters.Add(new SqlParameter("@UserTablePassword", user.UserTablePassword));
            //Retorno el ID
            return Convert.ToInt32(sentencia.ExecuteScalar());
        }
        public UserTable Consultar(string email)
        {
            string sqlquery = "select * from UserTable WHERE UserTableEmail=@UserTableEmail";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@UserTableEmail", email);

            UserTable user = new UserTable();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                user.UserTableID = (int)reader["UserTableID"];
                user.UserTableFirstName = (string)reader["UserTableFirstName"];
                user.UserTableLastName= (string)reader["UserTableLastName"];
                user.UserTableEmail = (string)reader["UserTableEmail"];
                CityManager cManager = new CityManager();
                user.City = cManager.Consultar((int)reader["CityID"]);
                ProvinceManager pManager = new ProvinceManager();
                user.Province = pManager.Consultar((int)reader["ProvinceID"]);
                UserTypeManager uManager = new UserTypeManager();
                user.UserType = uManager.Consultar((int)reader["UserTypeID"]);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return user;
        }
        public UserTable Validar(string email, string password)
        {
            string sqlquery = "select * from UserTable WHERE UserTableEmail=@UserTableEmail AND UserTablePassword = @UserTablePassword ";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@UserTableEmail", email);
            sentencia.Parameters.AddWithValue("@UserTablePassword", password);

            UserTable user = new UserTable();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                user.UserTableID = (int)reader["UserTableID"];
                user.UserTableFirstName = (string)reader["UserTableFirstName"];
                user.UserTableLastName = (string)reader["UserTableLastName"];
                user.UserTableEmail = (string)reader["UserTableEmail"];
                CityManager cManager = new CityManager();
                user.City = cManager.Consultar((int)reader["CityID"]);
                ProvinceManager pManager = new ProvinceManager();
                user.Province = pManager.Consultar((int)reader["ProvinceID"]);
                UserTypeManager uManager = new UserTypeManager();
                user.UserType = uManager.Consultar((int)reader["UserTypeID"]);
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