using Consola.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola.ControladorNegocio
{
    public class ctrTAUsuarioDetalle
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

                    var query = "SELECT * FROM TAUsuarioDetalle";
                    var comando = new SqlCommand(query, conexion);

                    using (var atributo = comando.ExecuteReader())
                    {
                        while (atributo.Read())
                        {
                            var usuarios = new TAUsuario()
                            {
                                UsuarioId = Convert.ToInt32(atributo["UsuarioId"]),
                                Direccion = atributo["Direccion"].ToString(),
                                Telefono = atributo["Telefono"].ToString(),
                                GeneroId = Convert.ToInt32(atributo["GeneroId"]),
                            };

                            respuesta.Add(usuarios);
                        }
                    }
                }
            }
            finally
            {

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
                        INSERT INTO TAUsuarioDetalle (Direccion, Telefono, GeneroId)
                        VALUES (@Direccion, @Telefono, @GeneroId)
                    ";

                    var comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                    comando.Parameters.AddWithValue("@Telefono", objeto.Telefono);
                    comando.Parameters.AddWithValue("@GeneroId", objeto.GeneroId);

                    comando.ExecuteNonQuery();
                }
            }
            finally
            {

            }
            return true;
        }
        public bool Actualizar(TAUsuario objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    var query = @"
                        UPDATE TAUsuarioDetalle
                        SET Direccion = @Direccion, Telefono = @Telefono, GeneroId = @GeneroId
                        WHERE UsuarioId = @UsuarioId
                    ";

                    var comando = new SqlCommand(query, conexion);

                    comando.Parameters.AddWithValue("@UsuarioId", objeto.UsuarioId);
                    comando.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                    comando.Parameters.AddWithValue("@Telefono", objeto.Telefono);
                    comando.Parameters.AddWithValue("@GeneroId", objeto.GeneroId);

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
                        DELETE FROM TAUsuarioDetalle
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
