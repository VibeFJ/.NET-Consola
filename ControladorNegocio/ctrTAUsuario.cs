using Consola.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola.ControladorNegocio
{
    public class ctrTAUsuario
    {
        private string administradorBD = ConfigurationManager.ConnectionStrings["NombreConexionBD"].ConnectionString;
        public List<TAUsuario> Obtener()
        {
            var respuesta = new List<TAUsuario>();
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    var query = "SELECT * FROM TAUsuario";
                    var comando = new SqlCommand(query, conexion);

                    using (var atributo = comando.ExecuteReader())
                    {
                        while (atributo.Read())
                        {
                            var usuarios = new TAUsuario()
                            {
                                UsuarioId = Convert.ToInt32(atributo["UsuarioId"]),
                                Nombre = atributo["Nombre"].ToString(),
                                ApellidoPaterno = atributo["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = atributo["ApellidoMaterno"].ToString(),
                                NombreUsuario = atributo["NombreUsuario"].ToString(),
                                Contraseña = atributo["Contraseña"].ToString(),
                            };

                            respuesta.Add(usuarios);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Add(new TAUsuario { Nombre = "Error: " + ex.ToString() });
            }
            return respuesta;
        }

        public bool Insertar(TAUsuario objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    var query = @"
                        INSERT INTO TAUsuario (Nombre, ApellidoPaterno, ApellidoMaterno, NombreUsuario, Contraseña)
                        VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @NombreUsuario, @Contraseña)
                    ";

                    var comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    comando.Parameters.AddWithValue("@ApellidoPaterno", objeto.ApellidoPaterno);
                    comando.Parameters.AddWithValue("@ApellidoMaterno", objeto.ApellidoMaterno);
                    comando.Parameters.AddWithValue("@NombreUsuario", objeto.NombreUsuario);
                    comando.Parameters.AddWithValue("@Contraseña", objeto.Contraseña);

                    comando.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(TAUsuario objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    var query = @"
                        UPDATE TAUsuario
                        SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, NombreUsuario = @NombreUsuario, Contraseña = @Contraseña
                        WHERE UsuarioId = @UsuarioId
                    ";

                    var comando = new SqlCommand(query, conexion);

                    comando.Parameters.AddWithValue("@UsuarioId", objeto.UsuarioId);
                    comando.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    comando.Parameters.AddWithValue("@ApellidoPaterno", objeto.ApellidoPaterno);
                    comando.Parameters.AddWithValue("@ApellidoMaterno", objeto.ApellidoMaterno);
                    comando.Parameters.AddWithValue("@NombreUsuario", objeto.NombreUsuario);
                    comando.Parameters.AddWithValue("@Contraseña", objeto.Contraseña);

                    comando.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(TAUsuario objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    var query = @"
                        DELETE FROM TAUsuario
                        WHERE UsuarioId = @UsuarioId
                    ";

                    var comando = new SqlCommand(query, conexion);

                    comando.Parameters.AddWithValue("@UsuarioId", objeto.UsuarioId);

                    comando.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
