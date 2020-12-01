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
    public class CalificacionesController : ControllerBase
    {
        private readonly deliveryContext _context;

        public CalificacionesController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Calificaciones from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Calificaciones/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Calificacion>>> GetCalificacion([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Calificacion> listaCalificacion = null;
            if (param.Direccion.ToLower() == "asc")
                listaCalificacion = await _context.Calificacions.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaCalificacion = await _context.Calificacions.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaCalificacion = await _context.Calificacions.OrderBy(p => p.Id).ToListAsync();

            if (listaCalificacion == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaCalificacion = listaCalificacion.Where(ele => ele.Observaciones.Contains(param.Filtro));
            }
            total = listaCalificacion.Count();
            listaCalificacion = listaCalificacion.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Calificacion>
            {
                Datos = listaCalificacion,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        ///  Gets an specific data Calificacion from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Calificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calificacion>> GetCalificacion(long id)
        {
            var calificacion = await _context.Calificacions.FindAsync(id);

            if (calificacion == null)
            {
                return NotFound();
            }

            return calificacion;
        }

        /// <summary>
        /// Send data to a server to update a resource about Calificacion.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Calificaciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacion(long id, Calificacion calificacion)
        {
            if (id != calificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(calificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionExists(id))
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
        /// Send data to a server to create a resource about Calificacion.
        /// </summary>
        /// <returns></returns>
        // POST: api/Calificaciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Calificacion>> PostCalificacion(Calificacion calificacion)
        {
            _context.Calificacions.Add(calificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalificacion", new { id = calificacion.Id }, calificacion);
        }

        /// <summary>
        /// Deletes the specified resource about Calificacion.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Calificaciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Calificacion>> DeleteCalificacion(long id)
        {
            var calificacion = await _context.Calificacions.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }

            _context.Calificacions.Remove(calificacion);
            await _context.SaveChangesAsync();

            return calificacion;
        }

        private bool CalificacionExists(long id)
        {
            return _context.Calificacions.Any(e => e.Id == id);
        }
    }
}
