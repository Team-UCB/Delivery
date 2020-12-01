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
    public class DetalleFacturasController : ControllerBase
    {
        private readonly deliveryContext _context;

        public DetalleFacturasController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets All the Detalle Factura from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/DetalleFacturas/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<DetalleFactura>>> GetDetalleFactura([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<DetalleFactura> listaOfertas = null;
            if (param.Direccion.ToLower() == "asc")
                listaOfertas = await _context.DetalleFacturas.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaOfertas = await _context.DetalleFacturas.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaOfertas = await _context.DetalleFacturas.OrderBy(p => p.Id).ToListAsync();

            if (listaOfertas == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaOfertas = listaOfertas.Where(ele => ele.Descripcion.Equals(param.Filtro));
            }
            total = listaOfertas.Count();
            listaOfertas = listaOfertas.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<DetalleFactura>
            {
                Datos = listaOfertas,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        /// Get a Detalle Factura specific for Id from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/DetalleFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFactura>> GetDetalleFactura(long id)
        {
            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);

            if (detalleFactura == null)
            {
                return NotFound();
            }

            return detalleFactura;
        }

        /// <summary>
        /// Modifies an existing Detalle Factura
        /// </summary>
        /// <param name="id"></param>
        /// <param name="detalleFactura"></param>
        /// <returns></returns>
        // PUT: api/DetalleFacturas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleFactura(long id, DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(id))
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
        /// Creates a new Detalle Factura 
        /// </summary>
        /// <param name="detalleFactura"></param>
        /// <returns></returns>
        // POST: api/DetalleFacturas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DetalleFactura>> PostDetalleFactura(DetalleFactura detalleFactura)
        {
            _context.DetalleFacturas.Add(detalleFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleFactura", new { id = detalleFactura.Id }, detalleFactura);
        }

        /// <summary>
        /// Removes a Detalle Factura from BDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/DetalleFacturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleFactura>> DeleteDetalleFactura(long id)
        {
            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            _context.DetalleFacturas.Remove(detalleFactura);
            await _context.SaveChangesAsync();

            return detalleFactura;
        }

        private bool DetalleFacturaExists(long id)
        {
            return _context.DetalleFacturas.Any(e => e.Id == id);
        }
    }
}
