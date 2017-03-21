using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class UserTypeManager
    {
        public UserType Consultar(int ID)
        {
            string sqlquery = "select * from UserType WHERE UserTypeID=@UserTypeID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@UserTypeID", ID);

            UserType userType = new UserType();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                userType.UserTypeID = (int)reader["UserTypeID"];
                userType.UserTypeName = (string)reader["UserTypeName"];
            }
            else
            {
                userType = null;
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return userType;
        }
    }
}