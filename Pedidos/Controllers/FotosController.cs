﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pedidos.Data;
using Pedidos.Helpers;
using Pedidos.Models;

namespace Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotosController : ControllerBase
    {
        private readonly deliveryContext _context;
        private readonly AppSettings _appSettings;

        public FotosController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets All the Fotos from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Fotos/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Foto>>> GetFoto([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Foto> listaFotos = null;
            if (param.Direccion.ToLower() == "asc")
                listaFotos = await _context.Fotos.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaFotos = await _context.Fotos.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaFotos = await _context.Fotos.OrderBy(p => p.Id).ToListAsync();

            if (listaFotos == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaFotos = listaFotos.Where(ele => ele.Descripcion.Equals(param.Filtro));
            }
            total = listaFotos.Count();
            listaFotos = listaFotos.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Foto>
            {
                Datos = listaFotos,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        /// Get a Foto specific for Id from the BDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Fotos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Foto>> GetFoto(long id)
        {
            var foto = await _context.Fotos.FindAsync(id);

            if (foto == null)
            {
                return NotFound();
            }

            return foto;
        }

        /// <summary>
        /// Modifies an existing Foto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foto"></param>
        /// <returns></returns>
        // PUT: api/Fotos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoto(long id, Foto foto)
        {
            if (id != foto.Id)
            {
                return BadRequest();
            }

            _context.Entry(foto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
            //return NoContent();
        }

        /// <summary>
        /// Creates a new Foto 
        /// </summary>
        /// <param name="foto"></param>
        /// <returns></returns>
        // POST: api/Fotos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Foto>> PostFoto(Foto foto)
        {
            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoto", new { id = foto.Id }, foto);
        }

        /// <summary>
        /// Removes a Foto from BDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Fotos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Foto>> DeleteFoto(long id)
        {
            var foto = await _context.Fotos.FindAsync(id);
            if (foto == null)
            {
                return NotFound();
            }

            _context.Fotos.Remove(foto);
            await _context.SaveChangesAsync();

            return Ok();
            //return foto;
        }

        private bool FotoExists(long id)
        {
            return _context.Fotos.Any(e => e.Id == id);
        }
    }
}
