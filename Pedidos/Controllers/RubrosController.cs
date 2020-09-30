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
    public class RubrosController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public RubrosController(PedidosPollomonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets All the Rubros from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Rubros/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Rubro>>> GetRubro([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Rubro> listaOfertas = null;
            if (param.Direccion.ToLower() == "asc")
                listaOfertas = await _context.Rubro.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaOfertas = await _context.Rubro.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaOfertas = await _context.Rubro.OrderBy(p => p.Id).ToListAsync();

            if (listaOfertas == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaOfertas = listaOfertas.Where(ele => ele.Nombre.Equals(param.Filtro));
            }
            total = listaOfertas.Count();
            listaOfertas = listaOfertas.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Rubro>
            {
                Datos = listaOfertas,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        /// Get a Chat specific for Id from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Rubros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rubro>> GetRubro(long id)
        {
            var rubro = await _context.Rubro.FindAsync(id);

            if (rubro == null)
            {
                return NotFound();
            }

            return rubro;
        }

        /// <summary>
        /// Modifies an existing Rubro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rubro"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new rubro 
        /// </summary>
        /// <param name="rubro"></param>
        /// <returns></returns>
        // POST: api/Rubros
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Rubro>> PostRubro(Rubro rubro)
        {
            _context.Rubro.Add(rubro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRubro", new { id = rubro.Id }, rubro);
        }


        /// <summary>
        /// Removes a rubro from BDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Rubros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rubro>> DeleteRubro(long id)
        {
            var rubro = await _context.Rubro.FindAsync(id);
            if (rubro == null)
            {
                return NotFound();
            }

            _context.Rubro.Remove(rubro);
            await _context.SaveChangesAsync();

            return rubro;
        }

        private bool RubroExists(long id)
        {
            return _context.Rubro.Any(e => e.Id == id);
        }
    }
}
