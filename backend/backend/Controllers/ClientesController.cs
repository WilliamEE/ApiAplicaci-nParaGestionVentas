using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend;
using backend.Models;
using System.IO;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly GestionVentasBDContext _context;

        public ClientesController(GestionVentasBDContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Permite obtener clientes paginados
        /// Enviar parámetros para paginar, sin parámetros para obtener el listado completo
        /// </summary>
        /// <param name="page">No obligatorio, cantidad de elementos a saltar, tipo entero</param>
        /// <param name="quantity">No obligatorio, cantidad de elementos a saltar, tipo entero</param>
        /// <returns></returns>
        // GET: api/Clientes/Paginado/1&10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientePaginado([FromQuery(Name = "page")] int page, [FromQuery(Name = "quantity")] int quantity)
        {
            List<Cliente> cliente; //= await _context.PTask<ActionResult<IEnumerable<Producto>>>roducto.Skip((page - 1) * quantity).Take(quantity).ToListAsync();

            if (page != 0 && quantity != 0)
            {
                cliente = await _context.Cliente.Skip((page - 1) * quantity).Take(quantity).ToListAsync();
            }
            else
            {
                cliente = await _context.Cliente.ToListAsync();
            }
            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// Permite obtener un cliente encontrado por identificador de tipo integer
        /// </summary>
        /// <param name="id">Obligatorio, identificador para encontrar un cliente, tipo entero</param>
        /// <returns></returns>
        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// Permite actualizar un cliente por medio de su identificador
        /// </summary>
        /// <param name="id">Obligatorio, identificador del cliente a modificar, tipo entero</param>
        /// <param name="cliente">Datos en formato Json</param>
        /// <returns></returns>
        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            string ruta = PostImage(cliente, 0);
            cliente.Imagen = ruta;
            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        /// Permite la inserción de un nuevo cliente.
        /// </summary>
        /// <param name="cliente">Datos en formato Json</param>
        /// <returns></returns>
        // POST: api/Clientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            string ruta = PostImage(cliente, 0);
            cliente.Imagen = ruta;
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Permite la eliminación de un cliente mediante su identificador
        /// </summary>
        /// <param name="id">Obligatorio, identificador de cliente a elminar, tipo entero</param>
        /// <returns></returns>
        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }

        private string PostImage(Cliente cli, int modo)
        {
            string filePath = Path.GetFullPath(@"Images");
            string ruta = cli.Imagen;
            if (modo != 0)
            {
                //Eliminando imagen de carpeta
                Cliente cliente_comparado = _context.Cliente.Find(cli.Id);

                if (cliente_comparado.Imagen != "" && cliente_comparado.Imagen != null)
                {
                    string ruta_eliminar = cliente_comparado.Imagen.Remove(0, 8);
                    System.IO.File.Delete(filePath + "\\" + ruta_eliminar);
                }
                _context.Entry(cliente_comparado).State = EntityState.Detached;

            }
            if (cli.Imagen != "" && modo < 2)
            {
                //Agregando imagen a carpeta
                //string nombreImagen = producto.Nombre.Replace(" ", "");
                Guid nombreImagen = Guid.NewGuid();
                string rutaImagen = filePath + "\\" + nombreImagen + ".png";
                string imagenBase = cli.Imagen;//.Remove(0, 22);
                byte[] archivoBase64 = Convert.FromBase64String(imagenBase);
                System.IO.File.WriteAllBytes(rutaImagen, archivoBase64);

                ruta = "/Images/" + nombreImagen + ".png";

            }

            return ruta;
        }
    }
}
