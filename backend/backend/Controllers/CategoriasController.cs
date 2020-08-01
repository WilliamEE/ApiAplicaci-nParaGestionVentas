using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly GestionVentasBDContext _context;

        public CategoriasController(GestionVentasBDContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Permite obtener categorias paginadas
        /// Enviar parámetros para paginar, sin parámetros para obtener el listado completo
        /// </summary>
        /// <param name="page">No obligatorio, cantidad de elementos a saltar, tipo entero</param>
        /// <param name="quantity">No obligatorio, cantidad de elementos a mostrar, tipo entero</param>
        /// <returns></returns>
        // GET: api/Categorias?page=1&quantity=2
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaPaginado([FromQuery(Name = "page")] int page, [FromQuery(Name = "quantity")] int quantity)
        {
            List<Categoria> categoria; //= await _context.Categoria.Skip((page - 1) * quantity).Take(quantity).ToListAsync();

            if (page != 0 && quantity != 0)
            {
                categoria = await _context.Categoria.Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
            else
            {
                categoria = await _context.Categoria.ToListAsync();
            }

            return categoria;
        }

        /// <summary>
        /// Permite obtener una categoría encontrado por identificador de tipo integer
        /// </summary>
        /// <param name="id">Obligatorio, identificador para encontrar una categoría, tipo entero</param>
        /// <returns></returns>
        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        /// <summary>
        /// Permite actualizar una categoria por medio de su identificador
        /// </summary>
        /// <param name="id">Obligatorio, identificador de la categoría a modificar, tipo entero</param>
        /// <param name="categoria">Datos en formato Json</param>
        /// <returns></returns>
        // PUT: api/Categorias/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        /// <summary>
        /// Permite la inserción de una nueva categoría.
        /// </summary>
        /// <param name="categoria">Datos en formato Json</param>
        /// <returns></returns>
        // POST: api/Categorias
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
        }

        /// <summary>
        /// Permite la eliminación de una categoría mediante su identificador
        /// </summary>
        /// <param name="id">Obligatorio, identificador de categoría a elminar, tipo entero</param>
        /// <returns></returns>
        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }

        
    }

}
