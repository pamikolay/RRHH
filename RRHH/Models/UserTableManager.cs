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
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }
    }
}