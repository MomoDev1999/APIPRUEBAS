using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIPRUEBAS.Models;
using Oracle.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Cors;

namespace APIPRUEBAS.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly ModelContext _dbcontext;

        public ProductoController(ModelContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista() {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Productos.Include(c => c.oCategoria).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" , response = lista });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message , response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = null;

            try
            {
                oProducto = _dbcontext.Productos.Include(c => c.oCategoria)
                                                  .FirstOrDefault(p => p.Idproducto == idProducto);

                if (oProducto == null)
                {
                    return NotFound("Producto no encontrado");
                }

                return Ok(new { mensaje = "ok", response = oProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new { mensaje = ex.Message, response = oProducto });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto producto)
        {
            try
            {
                _dbcontext.Productos.Add(producto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto guardado correctamente" });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto producto)
        {

            Producto oProducto = _dbcontext.Productos.Find(producto.Idproducto);

            if (oProducto == null)
            {
                return NotFound("Producto no encontrado");
            }

            try
            {
                // Actualiza las propiedades del objeto oProducto según las del objeto producto recibido
                oProducto.Codigobarra = producto.Codigobarra is null ? oProducto.Codigobarra : producto.Codigobarra;
                oProducto.Descripcion = producto.Descripcion is null ? oProducto.Descripcion : producto.Descripcion;
                oProducto.Marca = producto.Marca is null ? oProducto.Marca : producto.Marca;
                oProducto.Idcategoria = producto.Idcategoria is null ? oProducto.Idcategoria : producto.Idcategoria;
                oProducto.Precio = producto.Precio is null ? oProducto.Precio : producto.Precio;

                // Actualiza el objeto oProducto en el contexto
                _dbcontext.Productos.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto editado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(decimal idProducto)
        {
            try
            {
                // Busca el producto por su id
                var oProducto = _dbcontext.Productos.Find(idProducto);

                // Si no se encuentra el producto, devuelve un error 404
                if (oProducto == null)
                {
                    return NotFound("Producto no encontrado");
                }

                // Elimina el producto del contexto y guarda los cambios
                _dbcontext.Productos.Remove(oProducto);
                _dbcontext.SaveChanges();

                // Devuelve un mensaje de éxito
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto eliminado correctamente" });
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devuelve un mensaje de error 500 junto con el mensaje de la excepción
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new { mensaje = ex.Message });
            }
        }



    }
}
