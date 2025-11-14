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

        public bool actualizarArticulo (Articulo articulo)
        {
            AccesoDatos.AccesoDatos datos = new AccesoDatos.AccesoDatos();

            // Función local para mapear string -> valor o NULL
            object ValorONull(string valor)
            {
                return string.IsNullOrWhiteSpace(valor)
                    ? (object)DBNull.Value
                    : valor;
            }


            try
            {
                //"ImagenUrl = @ImagenUrl, " 
                datos.setearConsulta(
                    "UPDATE ARTICULOS SET " +
                    "Codigo = @Codigo, " +
                    "Nombre = @Nombre, " +
                    "Descripcion = @Descripcion, " +
                    "IdMarca = @IdMarca, " +
                    "IdCategoria = @IdCategoria, " +
                    
                    "Precio = @Precio " +
                    "WHERE Id = @Id"
                );

                // Strings: si vienen null o vacíos -> DB NULL
                datos.agregarParametro("@Codigo", ValorONull(articulo.codigo));
                datos.agregarParametro("@Nombre", ValorONull(articulo.nombre));
                datos.agregarParametro("@Descripcion", ValorONull(articulo.descripcion));
                //datos.agregarParametro("@ImagenUrl", ValorONull(articulo.imagenUrl));

                
                datos.agregarParametro("@IdMarca",
                    articulo.marca != null ? articulo.marca.id : (object)DBNull.Value);

                datos.agregarParametro("@IdCategoria",
                    articulo.categoria != null ? articulo.categoria.id : (object)DBNull.Value);

                datos.agregarParametro("@Precio", articulo.precio);

                datos.agregarParametro("@Id", articulo.id);

                int filas = datos.ejecutarAccion();

                if (filas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
