using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int id { get; set; }
        string nombre { get; set; }
        string apellido { get; set; }
        string email { get; set; }
        string password { get; set; }
        string urlImagenPerfil { get; set; }
        bool esAdmin { get; set; }


    }
}
