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
            // using (SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-4DPSMOU; Initial Catalog=bombo_app_bd_xd; Integrated Security=true"))
            using (SqlConnection conexion = new SqlConnection("Data Source=LAPTOP-VIB4ASD2; Initial Catalog=bombo_app_bd_xd; Integrated Security=true"))
            //using (SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-33Q1FRF\\SQLEXPRESS; Initial Catalog=bombo_app_bd_xd; Integrated Security=true"))
            // using (SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-TKTAIIK; Initial Catalog=bombo_app_bd_xd; Integrated Security=true"))
            {

                string query = "select UsuarioId,Nombre,ApePat,Correo,Contrasennia,Rol_RolId from USUARIOS where Correo = @pcorreo and Contrasennia = @pcontrasennia";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@pcorreo", correo);
                cmd.Parameters.AddWithValue("@pcontrasennia", contrasennia);
                cmd.CommandType = CommandType.Text;
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Roles rol = new Roles();
                        rol.RolId = Int32.Parse(dr["Rol_RolId"].ToString());

                        objeto = new Usuarios()
                        {   UsuarioId = Convert.ToInt32(dr["UsuarioId"]),
                            Nombre = dr["Nombre"].ToString(),
                            ApePat = dr["ApePat"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Contrasennia = dr["Contrasennia"].ToString(),
                            Rol = rol
                        };
                    }
                }
            }
            return objeto;
        }
    }
}