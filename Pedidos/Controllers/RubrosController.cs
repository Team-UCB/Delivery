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
    public class RubrosController : ControllerBase
    {
        private readonly deliveryContext _context;

        public RubrosController(deliveryContext context)
        {
            _context = context;
        }

        // GET: api/Rubros
        //[Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Rubro>>> GetRubro([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Rubro> listaRubros = null;
            if (param.Direccion.ToLower() == "asc")
                listaRubros = await _context.Rubros.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaRubros = await _context.Rubros.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaRubros = await _context.Rubros.OrderBy(p => p.Id).ToListAsync();
            if (listaRubros == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaRubros = listaRubros.Where(ele => ele.Nombre.Equals(param.Filtro));
            }
            total = listaRubros.Count();
            listaRubros = listaRubros.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Rubro>
            {
                Datos = listaRubros,
                TotalFilas = total
            };

            return result;
        }

        // GET: api/Rubros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rubro>> GetRubro(long id)
        {
            var rubro = await _context.Rubros.FindAsync(id);

            if (rubro == null)
            {
                return NotFound();
            }

            return rubro;
        }

        // PUT: api/Rubros/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRubro(long id, Rubro rubro)
        {
            if (id != rubro.Id)
            {
                return BadRequest();
            }

            _context.Entry(rubro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RubroExists(id))
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

        // POST: api/Rubros
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Rubro>> PostRubro(Rubro rubro)
        {
            _context.Rubros.Add(rubro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRubro", new { id = rubro.Id }, rubro);
        }

        // DELETE: api/Rubros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rubro>> DeleteRubro(long id)
        {
            var rubro = await _context.Rubros.FindAsync(id);
            if (rubro == null)
            {
                return NotFound();
            }

            _context.Rubros.Remove(rubro);
            await _context.SaveChangesAsync();

            return rubro;
        }

        private bool RubroExists(long id)
        {
            return _context.Rubros.Any(e => e.Id == id);
        }
    }
}
