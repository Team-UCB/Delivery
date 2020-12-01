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
    public class ChatsController : ControllerBase
    {
        private readonly deliveryContext _context;

        public ChatsController(deliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the data Chats from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Chats/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Chat>>> GetChat([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Chat> listaChat = null;
            if (param.Direccion.ToLower() == "asc")
                listaChat = await _context.Chats.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaChat = await _context.Chats.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaChat = await _context.Chats.OrderBy(p => p.Id).ToListAsync();

            if (listaChat == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaChat = listaChat.Where(ele => ele.Estado.Contains(param.Filtro));
            }
            total = listaChat.Count();
            listaChat = listaChat.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Chat>
            {
                Datos = listaChat,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        ///  Gets an specific data Chat from the BDB by id
        /// </summary>
        /// <returns></returns>
        // GET: api/Chats/5
        [HttpGet("{IdOrigen}/{IdDestino}")]
        public async Task<List<Chat>> GetChat(long IdOrigen, long IdDestino)
        {
            var chat = await _context.Chats.Where(ele => ele.IdOrigen == IdOrigen && ele.IdDestino == IdDestino).ToListAsync();
            return chat;
        }

        /// <summary>
        /// Send data to a server to update a resource about Chat.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Chats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(long id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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
        /// Send data to a server to create a resource about Chat.
        /// </summary>
        /// <returns></returns>
        // POST: api/Chats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChat", new { id = chat.Id }, chat);
        }

        /// <summary>
        /// Deletes the specified resource about Chat.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chat>> DeleteChat(long id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        private bool ChatExists(long id)
        {
            return _context.Chats.Any(e => e.Id == id);
        }
    }
}
