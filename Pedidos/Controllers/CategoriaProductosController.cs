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
    public class CategoriaProductosController : ControllerBase
    {
        private readonly deliveryContext _context;

        public CategoriaProductosController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data CategoriaProducto from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/CategoriaProductos/columna/direccion
        // [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<CategoriaProducto>>> GetCategoriaProductos([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<CategoriaProducto> listaCategoriaPproductos = null;
            if (param.Direccion.ToLower() == "asc")
                listaCategoriaPproductos = await _context.CategoriaProductos.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaCategoriaPproductos = await _context.CategoriaProductos.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaCategoriaPproductos = await _context.CategoriaProductos.OrderBy(p => p.Id).ToListAsync();

            if (listaCategoriaPproductos == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaCategoriaPproductos = listaCategoriaPproductos.Where(ele => ele.Nombre.Equals(param.Filtro));
            }
            total = listaCategoriaPproductos.Count();
            listaCategoriaPproductos = listaCategoriaPproductos.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<CategoriaProducto>
            {
                Datos = listaCategoriaPproductos,
                TotalFilas = total
            };

            return result;
        }


        /// <summary>
        ///  Gets an specific data CategoriaProducto from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/CategoriaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProducto>> GetCategoriaProducto(long id)
        {
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);

            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return categoriaProducto;
        }




        /// <summary>
        /// Send data to a server to update a resource about CategoriaProducto.
        /// </summary>
        /// <returns></returns>
        // PUT: api/CategoriaProductos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaProducto(long id, CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaProductoExists(id))
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
        /// Send data to a server to create a resource about CategoriaProducto.
        /// </summary>
        /// <returns></returns>
        // POST: api/CategoriaProductos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CategoriaProducto>> PostCategoriaProducto(CategoriaProducto categoriaProducto)
        {
            _context.CategoriaProductos.Add(categoriaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaProducto", new { id = categoriaProducto.Id }, categoriaProducto);
        }





        /// <summary>
        /// Deletes the specified resource about CategoriaProducto.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/CategoriaProductos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaProducto>> DeleteCategoriaProducto(long id)
        {
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            _context.CategoriaProductos.Remove(categoriaProducto);
            await _context.SaveChangesAsync();

            return categoriaProducto;
        }

        private bool CategoriaProductoExists(long id)
        {
            return _context.CategoriaProductos.Any(e => e.Id == id);
        }
    }
}
