using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend;
using System.IO;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly GestionVentasBDContext _context;

        public ProductosController(GestionVentasBDContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        //{
        //    return await _context.Producto.ToListAsync();
        //}

        
        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            
            string ruta = PostImage(producto, 1);
            producto.Imagen = ruta;
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Producto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {

            string ruta = PostImage(producto, 0);
            producto.Imagen = ruta;
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();
            

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            
            if (producto == null)
            {
                return NotFound();
            }
            PostImage(producto, 3);
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        // GET: api/Productos/Paginado/1&10
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductoPaginado([FromQuery(Name = "page")] int page, [FromQuery(Name = "quantity")] int quantity)
        {
            List<Producto> producto; //= await _context.PTask<ActionResult<IEnumerable<Producto>>>roducto.Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            
            if (page != 0 && quantity != 0)
            {
                producto = await _context.Producto.Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
            else
            {
                producto = await _context.Producto.ToListAsync();
            }
            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        private string PostImage(Producto prod, int modo) {
            string filePath = Path.GetFullPath(@"Images");
            string ruta = prod.Imagen;
            if (modo != 0)
            {
                //Eliminando imagen de carpeta
                Producto producto_comparado = _context.Producto.Find(prod.Id);

                    if (producto_comparado.Imagen != "" && producto_comparado.Imagen != null)
                    {
                        string ruta_eliminar = producto_comparado.Imagen.Remove(0, 8);
                        System.IO.File.Delete(filePath + "\\" + ruta_eliminar);
                    }
                    _context.Entry(producto_comparado).State = EntityState.Detached;
                    
            }
            if (prod.Imagen != "" && modo < 2)
            {
                //Agregando imagen a carpeta
                //string nombreImagen = producto.Nombre.Replace(" ", "");
                Guid nombreImagen = Guid.NewGuid();
                string rutaImagen = filePath + "\\" + nombreImagen + ".png";
                string imagenBase = prod.Imagen.Remove(0, 22);
                byte[] archivoBase64 = Convert.FromBase64String(imagenBase);
                System.IO.File.WriteAllBytes(rutaImagen, archivoBase64);

                ruta = "/Images/" + nombreImagen + ".png";
                
            }
                        
            return ruta;
        }
    }
}
