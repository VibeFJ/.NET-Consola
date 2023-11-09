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
    public class ctrTCGenero
    {
        private string administradorBD = ConfigurationManager.ConnectionStrings["NombreConexionBD"].ConnectionString;
        public List<TCGenero> Obtener()
        {
            var respuesta = new List<TCGenero>();
            try
            {
                using (var conexion = new SqlConnection(administradorBD))
                {
                    conexion.Open();

                    var query = "SELECT * FROM TCGenero";
                    var comando = new SqlCommand(query, conexion);

                    using (var atributo = comando.ExecuteReader())
                    {
                        while (atributo.Read())
                        {
                            var genero = new TCGenero
                            {
                                GeneroId = Convert.ToInt32(atributo["GeneroId"]),
                                Descripcion = atributo["Descripcion"].ToString(),
                            };

                            respuesta.Add(genero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Add(new TCGenero { Descripcion = "Error: " + ex.ToString() });
            }
            return respuesta;
        }
    }
}
