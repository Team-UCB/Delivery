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
    public class VendedoresController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public VendedoresController(PedidosPollomonContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Gets All the Vendedores from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Vendedores/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Vendedor>>> GetVendedor([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Vendedor> listaVendedor = null;
            if (param.Direccion.ToLower() == "asc")
                listaVendedor = await _context.Vendedors.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaVendedor = await _context.Vendedors.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaVendedor = await _context.Vendedors.OrderBy(p => p.Id).ToListAsync();

            if (listaVendedor == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaVendedor = listaVendedor.Where(ele => ele.NombreEmpresa.Contains(param.Filtro));
            }
            total = listaVendedor.Count();
            listaVendedor = listaVendedor.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Vendedor>
            {
                Datos = listaVendedor,
                TotalFilas = total
            };

            return result;
        }



        // GET: api/Vendedores
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Vendedor>>> GetVendedor()
        //{
        //    return await _context.Vendedor.ToListAsync();
        //}




        /// <summary>
        /// Get a Vendedor specific for Id from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Vendedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendedor>> GetVendedor(long id)
        {
            var vendedor = await _context.Vendedors.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound();
            }

            return vendedor;
        }




















        /// <summary>
        /// Modifies an existing Vendedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vendedor"></param>
        /// <returns></returns>
        // PUT: api/Vendedores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendedor(long id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExists(id))
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
        /// <param name="vendedor"></param>
        /// <returns></returns>
        // POST: api/Vendedores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vendedor>> PostVendedor(Vendedor vendedor)
        {
            _context.Vendedors.Add(vendedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendedor", new { id = vendedor.Id }, vendedor);
        }



        /// <summary>
        /// Removes a Vendedor from BDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Vendedores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vendedor>> DeleteVendedor(long id)
        {
            var vendedor = await _context.Vendedors.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            _context.Vendedors.Remove(vendedor);
            await _context.SaveChangesAsync();

            return vendedor;
        }

        private bool VendedorExists(long id)
        {
            return _context.Vendedors.Any(e => e.Id == id);
        }
    }
}
