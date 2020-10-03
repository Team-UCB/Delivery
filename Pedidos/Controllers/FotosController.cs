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
    public class FotosController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public FotosController(PedidosPollomonContext context)
        {
            _context = context;
        }

        // GET: api/Fotos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fotos>>> GetFotos()
        {
            return await _context.Fotos.ToListAsync();
        }

        // GET: api/Fotos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fotos>> GetFotos(long id)
        {
            var fotos = await _context.Fotos.FindAsync(id);

            if (fotos == null)
            {
                return NotFound();
            }

            return fotos;
        }

        // PUT: api/Fotos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFotos(long id, Fotos fotos)
        {
            if (id != fotos.Id)
            {
                return BadRequest();
            }

            _context.Entry(fotos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotosExists(id))
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

        // POST: api/Fotos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fotos>> PostFotos(Fotos fotos)
        {
            _context.Fotos.Add(fotos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFotos", new { id = fotos.Id }, fotos);
        }

        // DELETE: api/Fotos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fotos>> DeleteFotos(long id)
        {
            var fotos = await _context.Fotos.FindAsync(id);
            if (fotos == null)
            {
                return NotFound();
            }

            _context.Fotos.Remove(fotos);
            await _context.SaveChangesAsync();

            return fotos;
        }

        private bool FotosExists(long id)
        {
            return _context.Fotos.Any(e => e.Id == id);
        }
    }
}
