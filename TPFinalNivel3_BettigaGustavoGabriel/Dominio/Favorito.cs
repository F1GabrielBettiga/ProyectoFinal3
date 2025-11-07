using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Favorito
    {
        public int id { get; set; }
        public Usuario usuario { get; set; }
        public Articulo articulo { get; set; }


    }
}
