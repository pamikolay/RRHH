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

        public void SubirCvOk(Users user)
        {
            string sqlquery = "UPDATE Users set CvStatus = @CvStatus where ID = @ID";
            //LA FECHA NO LA QUIERO MODIFICAR
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            //4-escribrimos la sentencia
            sentencia.Parameters.AddWithValue("@ID", user.ID);
            sentencia.Parameters.AddWithValue("@CvStatus", "Cargado-OK");
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

            int a = Convert.ToInt32(sentencia.ExecuteScalar());

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
            return a;  //Retorno el ID
        }

        public Users UpdateStatusCv(Users user)
        {
            string sqlquery = "select * from Users WHERE Email=@Email";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Email", user.Email);

            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                user.ID = (int)reader["ID"];
                user.FirstName = (string)reader["FirstName"];
                user.LastName = (string)reader["LastName"];
                user.Email = (string)reader["Email"];
                user.CvStatus = (string)reader["CvStatus"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return user;
        }

        public Users Consultar(string email)
        {
            string sqlquery =   "SELECT        Users.*, Citys.Name AS CityName, Provinces.Name AS ProvinceName " +
                                "FROM Citys INNER JOIN " +
                                "Provinces ON Citys.Province = Provinces.ID INNER JOIN " +
                                "Users ON Citys.ID = Users.City AND Provinces.ID = Users.Province WHERE Email=@Email";
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
                user.City = new Citys();
                user.City.Name = (string)reader["CityName"];
                user.Province = new Provinces();
                user.Province.Name = (string)reader["ProvinceName"];
                user.UserType = new UserTypes();
                user.UserType.ID = (int)reader["UserType"];
                user.CvStatus = (string)reader["CvStatus"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return user;
        }
        public Users Consultar(int ID)
        {
            string sqlquery = "SELECT        Users.*, Citys.Name AS CityName, Provinces.Name AS ProvinceName " +
                                "FROM Citys INNER JOIN " +
                                "Provinces ON Citys.Province = Provinces.ID INNER JOIN " +
                                "Users ON Citys.ID = Users.City AND Provinces.ID = Users.Province WHERE Users.ID=@ID";
            //string sqlquery = "select * from Users WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Users user = new Users();
            SqlDataReader reader = sentencia.ExecuteReader();
            if (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                user.ID = (int)reader["ID"];
                user.FirstName = (string)reader["FirstName"];
                user.LastName = (string)reader["LastName"];
                user.Email = (string)reader["Email"];
                user.City = new Citys();
                user.City.Name = (string)reader["CityName"];
                user.Province = new Provinces();
                user.Province.Name = (string)reader["ProvinceName"];
                user.UserType = new UserTypes();
                user.UserType.ID = (int)reader["UserType"];
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
            string sqlquery = "SELECT        Users.*, Citys.Name AS CityName, Provinces.Name AS ProvinceName " +
                                "FROM Citys INNER JOIN " +
                                "Provinces ON Citys.Province = Provinces.ID INNER JOIN " +
                                "Users ON Citys.ID = Users.City AND Provinces.ID = Users.Province WHERE Email=@Email AND Password = @Password ";
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
                user.City = new Citys();
                user.City.Name = (string)reader["CityName"];
                user.Province = new Provinces();
                user.Province.Name = (string)reader["ProvinceName"];
                user.UserType = new UserTypes();
                user.UserType.ID = (int)reader["UserType"];
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
        public List<Users> GetUsuariosPorBusqueda(int ID)
        {
            List<Users> users = new List<Users>();

            string sqlquery =   "SELECT        dbo.Applicants.Postulant, dbo.Users.*, dbo.Applicants.Job " +
                                "FROM dbo.Applicants INNER JOIN " +
                                "dbo.Users ON dbo.Applicants.Postulant = dbo.Users.ID WHERE Job=@Job";

            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Job", ID);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                Users user = new Users();
                user.ID = ((int)reader["Postulant"]);
                user.Email = (string)reader["Email"];
                                
                //AGREGO LA company A LA LISTA
                users.Add(user);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return users;
        }
    }
}