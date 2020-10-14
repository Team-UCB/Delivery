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
    public class ClientesController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public ClientesController(PedidosPollomonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Clientes from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Clientes/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Cliente>>> GetCliente([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Cliente> listaCliente = null;
            if (param.Direccion.ToLower() == "asc")
                listaCliente = await _context.Clientes.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaCliente = await _context.Clientes.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaCliente = await _context.Clientes.OrderBy(p => p.Id).ToListAsync();

            if (listaCliente == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaCliente = listaCliente.Where(ele => ele.NombresApellidos.Contains(param.Filtro));
            }
            total = listaCliente.Count();
            listaCliente = listaCliente.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Cliente>
            {
                Datos = listaCliente,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        ///  Gets an specific data Cliente from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// Send data to a server to update a resource about Cliente.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(long id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        /// Send data to a server to create a resource about Cliente.
        /// </summary>
        /// <returns></returns>
        // POST: api/Clientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        /// <summary>
        /// Deletes the specified resource about Cliente.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(long id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        private bool ClienteExists(long id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
