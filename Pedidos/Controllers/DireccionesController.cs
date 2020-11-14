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
    public class DireccionesController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public DireccionesController(PedidosPollomonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Direcciones from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Direcciones/columna/direccion
        //[Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Direccion>>> GetDireccion([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Direccion> listaDireccion = null;
            if (param.Direccion.ToLower() == "asc")
                listaDireccion = await _context.Direccions.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaDireccion = await _context.Direccions.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaDireccion = await _context.Direccions.OrderBy(p => p.Id).ToListAsync();

            if (listaDireccion == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaDireccion = listaDireccion.Where(ele => ele.Descripcion.Contains(param.Filtro));
                
            }
            total = listaDireccion.Count();
            listaDireccion = listaDireccion.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Direccion>
            {
                Datos = listaDireccion,
                TotalFilas = total
            };
        
            return result;
        }

        /// <summary>
        ///  Gets an specific data Direccion from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Direcciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Direccion>> GetDireccion(long id)
        {
            var direccion = await _context.Direccions.FindAsync(id);

            if (direccion == null)
            {
                return NotFound();
            }

            return direccion;
        }

        /// <summary>
        /// Send data to a server to update a resource about Direccion.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Direcciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccion(long id, Direccion direccion)
        {
            if (id != direccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(id))
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
        /// Send data to a server to create a resource about Direccion.
        /// </summary>
        /// <returns></returns>
        // POST: api/Direcciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Direccion>> PostDireccion(Direccion direccion)
        {
            _context.Direccions.Add(direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDireccion", new { id = direccion.Id }, direccion);
        }

        /// <summary>
        /// Deletes the specified resource about Direccion.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Direcciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Direccion>> DeleteDireccion(long id)
        {
            var direccion = await _context.Direccions.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direccions.Remove(direccion);
            await _context.SaveChangesAsync();

            return direccion;
        }

        private bool DireccionExists(long id)
        {
            return _context.Direccions.Any(e => e.Id == id);
        }
    }
}
