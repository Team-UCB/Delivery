using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Data;
using Pedidos.Models;

namespace Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosificacionesController : ControllerBase
    {
        private readonly deliveryContext _context;

        public DosificacionesController(deliveryContext context)
        {
            _context = context;
        }

        // GET: api/Dosificacion/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Dosificacion>>> GetDosificaciones([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Dosificacion> listaDosificaciones = null;
            if (param.Direccion.ToLower() == "asc")
                listaDosificaciones = await _context.Dosificacions.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaDosificaciones = await _context.Dosificacions.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaDosificaciones = await _context.Dosificacions.OrderBy(p => p.Id).ToListAsync();

            if (listaDosificaciones == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaDosificaciones = listaDosificaciones.Where(ele => ele.NroAutorizacion.Equals(param.Filtro));
            }
            total = listaDosificaciones.Count();
            listaDosificaciones = listaDosificaciones.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Dosificacion>
            {
                Datos = listaDosificaciones,
                TotalFilas = total
            };

            return result;
        }

        // GET: api/Dosificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dosificacion>> GetDosificacion(long id)
        {
            var dosificacion = await _context.Dosificacions.FindAsync(id);

            if (dosificacion == null)
            {
                return NotFound();
            }

            return dosificacion;
        }

        // PUT: api/Dosificaciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDosificacion(long id, Dosificacion dosificacion)
        {
            if (id != dosificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(dosificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DosificacionExists(id))
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

        // POST: api/Dosificaciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dosificacion>> PostDosificacion(Dosificacion dosificacion)
        {
            _context.Dosificacions.Add(dosificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDosificacion", new { id = dosificacion.Id }, dosificacion);
        }

        // DELETE: api/Dosificaciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dosificacion>> DeleteDosificacion(long id)
        {
            var dosificacion = await _context.Dosificacions.FindAsync(id);
            if (dosificacion == null)
            {
                return NotFound();
            }

            _context.Dosificacions.Remove(dosificacion);
            await _context.SaveChangesAsync();

            return dosificacion;
        }

        private bool DosificacionExists(long id)
        {
            return _context.Dosificacions.Any(e => e.Id == id);
        }
    }
}
