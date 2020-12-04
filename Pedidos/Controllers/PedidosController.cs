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
    public class PedidosController : ControllerBase
    {
        private readonly deliveryContext _context;

        public PedidosController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Pedidos from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Pedidos/columna/direccion
        //[Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Pedido>>> GetPedidos([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Pedido> listaPedido = null;
            if (param.Direccion.ToLower() == "asc")
                listaPedido = await _context.Pedidos.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaPedido = await _context.Pedidos.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaPedido = await _context.Pedidos.OrderBy(p => p.Id).ToListAsync();

            if (listaPedido == null)

            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {

                listaPedido = listaPedido.Where(ele => ele.Estado.Contains(param.Filtro));
            }
            total = listaPedido.Count();
            listaPedido = listaPedido.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Pedido>
            {
                Datos = listaPedido,

                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        ///  Gets an specific data Pedido from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(long id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }




        // GET: api/Pedidos/5
        [HttpGet]
        [Route("getPedidoRepartidor/{param1}")]
        public async Task<ActionResult<Pedido>> GetPedidoRepartidor(long param1)
        {
            var pedido = new List<Pedido>();
            pedido = _context.Pedidos.Where(p => p.IdTransporte == param1 && p.Estado.Equals("En Curso")).ToList();

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido[0];
        }






        /// <summary>
        /// Send data to a server to update a resource about Pedido.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Pedidos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(long id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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
        /// Send data to a server to create a resource about Pedido.
        /// </summary>
        /// <returns></returns>
        // POST: api/Pedidos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
        }

        /// <summary>
        /// Deletes the specified resource about Pedido.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> DeletePedido(long id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        private bool PedidoExists(long id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
