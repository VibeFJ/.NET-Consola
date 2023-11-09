using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola.Entidades
{
    public class TAUsuario : TAUsuarioDetalle
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set;}
        public string ApellidoMaterno { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }
}
