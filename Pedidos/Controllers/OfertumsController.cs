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
    public class OfertasController : ControllerBase
    {
        private readonly deliveryContext _context;

        public OfertasController(deliveryContext context)
        {
            _context = context;
        }

        // GET: api/Ofertas
        [Helpers.Authorize]

        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Ofertum>>> GetOfertas([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Ofertum> listaOfertas = null;
            if (param.Direccion.ToLower() == "asc")
                listaOfertas = await _context.Oferta.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaOfertas = await _context.Oferta.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaOfertas = await _context.Oferta.OrderBy(p => p.Id).ToListAsync();

            if (listaOfertas == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaOfertas = listaOfertas.Where(ele => ele.FechaInicio.Equals(param.Filtro));
            }
            total = listaOfertas.Count();
            listaOfertas = listaOfertas.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);
            var result = new PageAndSortResponse<Ofertum>
            {
                Datos = listaOfertas,
                TotalFilas = total
            };

            return result;
        }

        // GET: api/Ofertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ofertum>> GetOfertum(long id)
        {
            var ofertum = await _context.Oferta.FindAsync(id);

            if (ofertum == null)
            {
                return NotFound();
            }

            return ofertum;
        }

        // PUT: api/Ofertas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfertum(long id, Ofertum ofertum)
        {
            if (id != ofertum.Id)
            {
                return BadRequest();
            }

            _context.Entry(ofertum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertumExists(id))
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

        // POST: api/Ofertas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ofertum>> PostOfertum(Ofertum ofertum)
        {
            _context.Oferta.Add(ofertum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfertum", new { id = ofertum.Id }, ofertum);
        }

        // DELETE: api/Ofertas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ofertum>> DeleteOfertum(long id)
        {
            var ofertum = await _context.Oferta.FindAsync(id);
            if (ofertum == null)
            {
                return NotFound();
            }

            _context.Oferta.Remove(ofertum);
            await _context.SaveChangesAsync();

            return ofertum;
        }

        private bool OfertumExists(long id)
        {
            return _context.Oferta.Any(e => e.Id == id);
        }
    }
}
