using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RRHH.Models
{
    public class CompanysManager
    {
        public void Insertar(Companys newCompany)
        {
            string sqlquery = "insert into Companys (Name) VALUES (@Name)";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@Name", newCompany.Name);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public void Eliminar(Companys company)
        {
            this.Eliminar(company.ID);
        }

        public void Eliminar(long ID)
        {
            string sqlquery = "delete from Company where ID = @ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        /// <summary>
        /// Modificamos una compañia
        /// </summary>
        /// <param name="company"></param>
        public void Actualizar(Companys company)
        {
            string sqlquery = "update Companys set Name = @Name WHERE ID = @ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            sentencia.Parameters.AddWithValue("@Name", company.Name);
            sentencia.Parameters.AddWithValue("@ID", company.ID);
            //5-Ejecutar!
            sentencia.ExecuteNonQuery();

            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();
        }

        public List<Companys> ConsultarTodos()
        {
            List<Companys> companys = new List<Companys>();

            string sqlquery = "select * from Companys";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);

            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                Companys company = new Companys();
                company.ID = (int)reader["ID"];
                company.Name = (string)reader["Name"];
                //AGREGO LA company A LA LISTA
                companys.Add(company);
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return companys;
        }

        public Companys Consultar(int ID)
        {
            string sqlquery = "select * from Companys WHERE ID=@ID";
            DataBase ConexionBD = new DataBase();
            SqlCommand sentencia = ConexionBD.Conectar(sqlquery);
            sentencia.Parameters.AddWithValue("@ID", ID);

            Companys company = new Companys();
            SqlDataReader reader = sentencia.ExecuteReader();
            while (reader.Read()) //mientras haya un registro para leer
            {
                //creo el artículo, le completo los datos 
                company.ID = (int)reader["ID"];
                company.Name = (string)reader["Name"];
            }

            //CERRAR EL READER AL TERMINAR DE LEER LOS REGISTROS
            reader.Close();
            //CERRAR LA CONEXION AL TERMINAR!!!!
            ConexionBD.Desconectar();

            return company;
        }

    }
}