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
    public class PaginasController : ControllerBase
    {
        private readonly deliveryContext _context;

        public PaginasController(deliveryContext context)
        {
            _context = context;
        }

        // GET: api/Paginas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagina>>> GetPaginas()
        {
            return await _context.Paginas.ToListAsync();
        }

        // GET: api/Paginas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagina>> GetPagina(long id)
        {
            var pagina = await _context.Paginas.FindAsync(id);

            if (pagina == null)
            {
                return NotFound();
            }

            return pagina;
        }

        // PUT: api/Paginas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagina(long id, Pagina pagina)
        {
            if (id != pagina.Id)
            {
                return BadRequest();
            }

            _context.Entry(pagina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaginaExists(id))
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

        // POST: api/Paginas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pagina>> PostPagina(Pagina pagina)
        {
            _context.Paginas.Add(pagina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagina", new { id = pagina.Id }, pagina);
        }

        // DELETE: api/Paginas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pagina>> DeletePagina(long id)
        {
            var pagina = await _context.Paginas.FindAsync(id);
            if (pagina == null)
            {
                return NotFound();
            }

            _context.Paginas.Remove(pagina);
            await _context.SaveChangesAsync();

            return pagina;
        }

        private bool PaginaExists(long id)
        {
            return _context.Paginas.Any(e => e.Id == id);
        }
    }
}
