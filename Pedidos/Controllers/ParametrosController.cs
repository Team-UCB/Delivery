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
    public class ParametrosController : ControllerBase
    {
        private readonly deliveryContext _context;

        public ParametrosController(deliveryContext context)
        {
            _context = context;
        }

        // GET: api/Parametros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parametro>>> GetParametros()
        {
            return await _context.Parametros.ToListAsync();
        }

        // GET: api/Parametros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parametro>> GetParametro(long id)
        {
            var parametro = await _context.Parametros.FindAsync(id);

            if (parametro == null)
            {
                return NotFound();
            }

            return parametro;
        }

        // PUT: api/Parametros/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametro(long id, Parametro parametro)
        {
            if (id != parametro.Id)
            {
                return BadRequest();
            }

            _context.Entry(parametro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametroExists(id))
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

        // POST: api/Parametros
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Parametro>> PostParametro(Parametro parametro)
        {
            _context.Parametros.Add(parametro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParametro", new { id = parametro.Id }, parametro);
        }

        // DELETE: api/Parametros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Parametro>> DeleteParametro(long id)
        {
            var parametro = await _context.Parametros.FindAsync(id);
            if (parametro == null)
            {
                return NotFound();
            }

            _context.Parametros.Remove(parametro);
            await _context.SaveChangesAsync();

            return parametro;
        }

        private bool ParametroExists(long id)
        {
            return _context.Parametros.Any(e => e.Id == id);
        }
    }
}
