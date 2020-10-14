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
    public class PedidosController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public PedidosController(PedidosPollomonContext context)
        {
            _context = context;
        }

<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
        // GET: api/Pedido/columna/direccion
========
        /// <summary>
        /// Gets all the data Clientes from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Clientes/columna/direccion
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Pedido>>> GetPedidos([FromQuery] PageAndSortRequest param)
        {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
            IEnumerable<Pedido> listaPedidos = null;
            if (param.Direccion.ToLower() == "asc")
                listaPedidos = await _context.Pedidos.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaPedidos = await _context.Pedidos.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaPedidos = await _context.Pedidos.OrderBy(p => p.Id).ToListAsync();

            if (listaPedidos == null)
========
            IEnumerable<Cliente> listaCliente = null;
            if (param.Direccion.ToLower() == "asc")
                listaCliente = await _context.Clientes.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaCliente = await _context.Clientes.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaCliente = await _context.Clientes.OrderBy(p => p.Id).ToListAsync();

            if (listaCliente == null)
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
                listaPedidos = listaPedidos.Where(ele => ele.CodigoQrFactura.Equals(param.Filtro));
            }
            total = listaPedidos.Count();
            listaPedidos = listaPedidos.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);
========
                listaCliente = listaCliente.Where(ele => ele.NombresApellidos.Contains(param.Filtro));
            }
            total = listaCliente.Count();
            listaCliente = listaCliente.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs

            var result = new PageAndSortResponse<Pedido>
            {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
                Datos = listaPedidos,
========
                Datos = listaCliente,
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
                TotalFilas = total
            };

            return result;
        }

<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
        // GET: api/Pedidos/5
========
        /// <summary>
        ///  Gets an specific data Cliente from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Clientes/5
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(long id)
        {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
            var pedido = await _context.Pedidos.FindAsync(id);
========
            var cliente = await _context.Clientes.FindAsync(id);
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
        // PUT: api/Pedidos/5
========
        /// <summary>
        /// Send data to a server to update a resource about Cliente.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Clientes/5
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
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

<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
        // POST: api/Pedidos
========
        /// <summary>
        /// Send data to a server to create a resource about Cliente.
        /// </summary>
        /// <returns></returns>
        // POST: api/Clientes
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
            _context.Pedidos.Add(pedido);
========
            _context.Clientes.Add(cliente);
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Id }, pedido);
        }

<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
        // DELETE: api/Pedidos/5
========
        /// <summary>
        /// Deletes the specified resource about Cliente.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Clientes/5
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> DeletePedido(long id)
        {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
========
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
            {
                return NotFound();
            }

<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
            _context.Pedidos.Remove(pedido);
========
            _context.Clientes.Remove(cliente);
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
            await _context.SaveChangesAsync();

            return pedido;
        }

        private bool PedidoExists(long id)
        {
<<<<<<<< HEAD:Pedidos/Controllers/PedidosController.cs
            return _context.Pedidos.Any(e => e.Id == id);
========
            return _context.Clientes.Any(e => e.Id == id);
>>>>>>>> origin/Sprint#2:Pedidos/Controllers/ClientesController.cs
        }
    }
}
