using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Models;

namespace Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public MensajesController(PedidosPollomonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Mensajes from the BDB
        /// </summary>
        /// <returns></returns>
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Mensaje>>> GetMensaje([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Mensaje> listaMensaje = null;
            if (param.Direccion.ToLower() == "asc")
                listaMensaje = await _context.Mensajes.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaMensaje = await _context.Mensajes.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaMensaje = await _context.Mensajes.OrderBy(p => p.Id).ToListAsync();

            if (listaMensaje == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaMensaje = listaMensaje.Where(ele => ele.Text.Contains(param.Filtro));
            }
            total = listaMensaje.Count();
            listaMensaje = listaMensaje.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Mensaje>
            {
                Datos = listaMensaje,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        ///  Gets an specific data Mensaje from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Mensajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mensaje>> GetMensaje(long id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);

            if (mensaje == null)
            {
                return NotFound();
            }

            return mensaje;
        }

        /// <summary>
        /// Send data to a server to update a resource about Mensaje.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Mensajes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensaje(long id, Mensaje mensaje)
        {
            if (id != mensaje.Id)
            {
                return BadRequest();
            }

            _context.Entry(mensaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajeExists(id))
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
        /// Send data to a server to update a resource about Mensaje 
        /// </summary>
        /// <returns></returns>
        // POST: api/Mensajes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Mensaje>> PostMensaje(Mensaje mensaje)
        {
            _context.Mensajes.Add(mensaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMensaje", new { id = mensaje.Id }, mensaje);
        }

        /// <summary>
        /// Deletes the specified resource about Mensaje.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Mensajes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mensaje>> DeleteMensaje(long id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);
            if (mensaje == null)
            {
                return NotFound();
            }

            _context.Mensajes.Remove(mensaje);
            await _context.SaveChangesAsync();

            return mensaje;
        }

        private bool MensajeExists(long id)
        {
            return _context.Mensajes.Any(e => e.Id == id);
        }
    }
}
