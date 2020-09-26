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
    public class DetallePedidosController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public DetallePedidosController(PedidosPollomonContext context)
        {
            _context = context;
        }

        // GET: api/DetallePedidos/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<DetallePedido>>> GetRubro([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<DetallePedido> listaOfertas = null;
            if (param.Direccion.ToLower() == "asc")
                listaOfertas = await _context.DetallePedido.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaOfertas = await _context.DetallePedido.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaOfertas = await _context.DetallePedido.OrderBy(p => p.Id).ToListAsync();

            if (listaOfertas == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaOfertas = listaOfertas.Where(ele => ele.Id.Equals(param.Filtro));
            }
            total = listaOfertas.Count();
            listaOfertas = listaOfertas.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<DetallePedido>
            {
                Datos = listaOfertas,
                TotalFilas = total
            };

            return result;
        }

        // GET: api/DetallePedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetallePedido(long id)
        {
            var detallePedido = await _context.DetallePedido.FindAsync(id);

            if (detallePedido == null)
            {
                return NotFound();
            }

            return detallePedido;
        }

        // PUT: api/DetallePedidos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallePedido(long id, DetallePedido detallePedido)
        {
            if (id != detallePedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(detallePedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePedidoExists(id))
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

        // POST: api/DetallePedidos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetallePedido(DetallePedido detallePedido)
        {
            _context.DetallePedido.Add(detallePedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallePedido", new { id = detallePedido.Id }, detallePedido);
        }

        // DELETE: api/DetallePedidos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetallePedido>> DeleteDetallePedido(long id)
        {
            var detallePedido = await _context.DetallePedido.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            _context.DetallePedido.Remove(detallePedido);
            await _context.SaveChangesAsync();

            return detallePedido;
        }

        private bool DetallePedidoExists(long id)
        {
            return _context.DetallePedido.Any(e => e.Id == id);
        }
    }
}
