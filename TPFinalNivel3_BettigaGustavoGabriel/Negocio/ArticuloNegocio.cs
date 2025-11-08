using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarArticulos ()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT A.Id,A.Codigo,A.Nombre,A.Descripcion,A.IdMarca AS MarcaId,M.Descripcion AS MarcaDescripcion,A.IdCategoria AS CategoriaId,C.Descripcion AS CategoriaDescripcion,A.ImagenUrl,A.Precio " +
                     "FROM ARTICULOS A,CATEGORIAS C,MARCAS M " +
                     "WHERE A.IdMarca=M.Id AND A.IdCategoria=C.Id");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.codigo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.imagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.precio = (decimal)datos.Lector["Precio"];

                    aux.marca = new Marca();
                    aux.marca.id = (int)datos.Lector["MarcaId"];
                    aux.marca.descripcion = (string)datos.Lector["MarcaDescripcion"];

                    aux.categoria = new Categoria();
                    aux.categoria.id = (int)datos.Lector["CategoriaId"];
                    aux.categoria.descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }




    }
}
