using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Seguridad
    {
        public static bool ValidarSesionActiva(object user)
        {
            Usuario usuarioLogeado = user != null ? (Usuario)user : null;

            if (usuarioLogeado != null && usuarioLogeado.id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        public static bool esAdmin(object user)
        {
            Usuario usuarioLogeado = user != null ? (Usuario)user : null;


            if (usuarioLogeado != null && usuarioLogeado.esAdmin == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
