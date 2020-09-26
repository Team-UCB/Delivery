using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Models;
using Pedidos.Helpers;

namespace Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;

        public ProductosController(PedidosPollomonContext context)
        {
            _context = context;
        }


        // GET: api/Localizacions/columna/direccion
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Producto>>> GetProducto([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Producto> listaProducto = null;
            if (param.Direccion.ToLower() == "asc")
                listaProducto = await _context.Producto.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaProducto = await _context.Producto.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaProducto = await _context.Producto.OrderBy(p => p.Id).ToListAsync();

            if (listaProducto == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaProducto = listaProducto.Where(ele => ele.Nombre.Contains(param.Filtro));
            }
            total = listaProducto.Count();
            listaProducto = listaProducto.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Producto>
            {
                Datos = listaProducto,
                TotalFilas = total
            };

            return result;
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(long id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(long id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }



        // POST: api/Productos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> DeleteProducto(long id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        private bool ProductoExists(long id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
