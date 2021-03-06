﻿using System;
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
    public class TransportadoresController : ControllerBase
    {
        private readonly deliveryContext _context;

        public TransportadoresController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Transportadores from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Transportador/columna/direccion
        // [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Transportador>>> GetTransportador([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Transportador> listaTransportadores = null;
            if (param.Direccion.ToLower() == "asc")
                listaTransportadores = await _context.Transportadors.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaTransportadores = await _context.Transportadors.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaTransportadores = await _context.Transportadors.OrderBy(p => p.Id).ToListAsync();

            if (listaTransportadores == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaTransportadores = listaTransportadores.Where(ele => ele.NombreCompleto.Equals(param.Filtro));
            }
            total = listaTransportadores.Count();
            listaTransportadores = listaTransportadores.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Transportador>
            {
                Datos = listaTransportadores,
                TotalFilas = total
            };

            return result;
        }

        // GET: api/Transportadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transportador>> GetTransportador(long id)
        {
            var transportador = await _context.Transportadors.FindAsync(id);

            if (transportador == null)
            {
                return NotFound();
            }

            return transportador;
        }

        // PUT: api/Transportadores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransportador(long id, Transportador transportador)
        {
            if (id != transportador.Id)
            {
                return BadRequest();
            }

            _context.Entry(transportador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportadorExists(id))
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

        // POST: api/Transportadores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transportador>> PostTransportador(Transportador transportador)
        {
            _context.Transportadors.Add(transportador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransportador", new { id = transportador.Id }, transportador);
        }

        // DELETE: api/Transportadores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transportador>> DeleteTransportador(long id)
        {
            var transportador = await _context.Transportadors.FindAsync(id);
            if (transportador == null)
            {
                return NotFound();
            }

            _context.Transportadors.Remove(transportador);
            await _context.SaveChangesAsync();

            return transportador;
        }

        private bool TransportadorExists(long id)
        {
            return _context.Transportadors.Any(e => e.Id == id);
        }
    }
}
