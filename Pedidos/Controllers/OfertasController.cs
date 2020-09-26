using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Pedidos.Helpers;
using System.Text;
using Microsoft.Extensions.Options;

namespace Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly PedidosPollomonContext _context;
      
        public OfertasController(PedidosPollomonContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
          
        }
        // GET: api/Localizacions/columna/direccion
       [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Oferta>>> GetOfertas([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Oferta> listaOfertas = null;
            if (param.Direccion.ToLower() == "asc")
                listaOfertas = await _context.Oferta.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaOfertas = await _context.Oferta.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaOfertas = await _context.Oferta.OrderBy(p => p.Id).ToListAsync();

            if (listaOfertas == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaOfertas = listaOfertas.Where(ele => ele.PrecioOferta.Equals(param.Filtro));
            }
            total = listaOfertas.Count();
            listaOfertas = listaOfertas.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Oferta>
            {
                Datos = listaOfertas,
                TotalFilas = total
            };

            return result;
        }

        //// GET: api/Ofertas
        //[Helpers.Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Oferta>>> GetOferta()
        //{
        //    return await _context.Oferta.ToListAsync();
        //}

        // GET: api/Ofertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> GetOferta(long id)
        {
            var oferta = await _context.Oferta.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }
        
        // PUT: api/Ofertas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferta(long id, Oferta oferta)
        {
            if (id != oferta.Id)
            {
                return BadRequest();
            }

            _context.Entry(oferta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(id))
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
        //hola
        // POST: api/Ofertas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Oferta>> PostOferta(Oferta oferta)
        {
            _context.Oferta.Add(oferta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOferta", new { id = oferta.Id }, oferta);
        }

        // DELETE: api/Ofertas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oferta>> DeleteOferta(long id)
        {
            var oferta = await _context.Oferta.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            _context.Oferta.Remove(oferta);
            await _context.SaveChangesAsync();

            return oferta;
        }

        private bool OfertaExists(long id)
        {
            return _context.Oferta.Any(e => e.Id == id);
        }

    }
}
