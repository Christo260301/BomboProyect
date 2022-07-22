using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BomboProyect.Models;
using System.Data.SqlClient;
using System.Data;

namespace BomboProyect.Logica
{
    public class Logica_Usuario
    {
        public Usuarios EncontrarUsuario(string correo, string contrasennia)
        {
            Usuarios objeto = new Usuarios();
            using (SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-4DPSMOU; Initial Catalog=bombo_app_bd_xd; Integrated Security=true"))
            {
                string query = "select Nombre,Correo,Contrasennia from USUARIOS where Correo = @pcorreo and Contrasennia = @pcontrasennia";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@pcorreo", correo);
                cmd.Parameters.AddWithValue("@pcontrasennia", contrasennia);
                cmd.CommandType = CommandType.Text;
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objeto = new Usuarios()
                        {
                            Nombre = dr["Nombre"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Contrasennia = dr["Contrasennia"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }
    }
}