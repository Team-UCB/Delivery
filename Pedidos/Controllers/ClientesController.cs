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
    public class ClientesController : ControllerBase
    {
        private readonly deliveryContext _context;

        public ClientesController(deliveryContext context)
        {
            _context = context;
        }

        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Cliente>>> GetCliente([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Cliente> listaClientes = null;
            if (param.Direccion.ToLower() == "asc")
                listaClientes = await _context.Clientes.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaClientes = await _context.Clientes.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaClientes = await _context.Clientes.OrderBy(p => p.Id).ToListAsync();

            if (listaClientes == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaClientes = listaClientes.Where(ele => ele.NombresApellidos.Equals(param.Filtro));
            }
            total = listaClientes.Count();
            listaClientes = listaClientes.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Cliente>
            {
                Datos = listaClientes,
                TotalFilas = total
            };

            return result;
        }

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




        [HttpGet]
        [Route("getClientePedido/{param1}")]
        public async Task<ActionResult<Cliente>> getClientePedido(long param1)
        {
            var cliente = new List<Cliente>();
            cliente = _context.Clientes.Where(p => p.Id == param1).ToList();


            if (cliente == null)
            {
                return NotFound();
            }

            return cliente[0];
        }






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
