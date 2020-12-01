using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pedidos.Helpers;
using Pedidos.Models;

namespace Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly deliveryContext _context;
        private readonly AppSettings _appSettings;
        public UsuariosController(deliveryContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Gets All the Usuarios from the BDB
        /// </summary>
        /// <returns></returns>
        // GET: api/Usuarios/columna/direccion
        [Helpers.Authorize]
        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Usuario>>> GetUsuario([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<Usuario> listaUsuario = null;
            if (param.Direccion.ToLower() == "asc")
                listaUsuario = await _context.Usuarios.OrderBy(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else if (param.Direccion.ToLower() == "desc")
                listaUsuario = await _context.Usuarios.OrderByDescending(p => EF.Property<object>(p, param.Columna)).ToListAsync();
            else
                listaUsuario = await _context.Usuarios.OrderBy(p => p.Id).ToListAsync();

            if (listaUsuario == null)
            {
                return NotFound();
            }

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filtro))
            {
                listaUsuario = listaUsuario.Where(ele => ele.Estado.Contains(param.Filtro));
            }
            total = listaUsuario.Count();
            listaUsuario = listaUsuario.Skip((param.Pagina - 1) * param.TamPagina).Take(param.TamPagina);

            var result = new PageAndSortResponse<Usuario>
            {
                Datos = listaUsuario,
                TotalFilas = total
            };

            return result;
        }

        /// <summary>
        /// Get an existing Usuario
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Clave"></param>
        /// <returns></returns>
        // GET: api/Usuarios/Nombre/Clave
        [HttpGet("{Nombre}/{Clave}")]
        public ActionResult<Usuario> GetUsuario(string Nombre, string Clave)
        {
            var usuario = _context.Usuarios.FirstOrDefault(ele => ele.Nombre == Nombre && ele.Clave == Clave);
            if (usuario == null)
            {
                return NotFound();
            }
            var token = generateJwtToken(usuario);
            usuario.Token = token;
            return usuario;
        }

        /// <summary>
        /// Modifies an existing Usuario
        /// </summary>
        /// <param name="id"></param>
        /// /// <param name="usuario"></param>
        /// <returns></returns>
        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Creates a new usuario 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (usuario.IdRol == 0)
            {
                usuario.IdRol = 2;
            }
            if (usuario.Estado == null)
            {
                usuario.Estado = "activo";
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Removes a usuario from BDB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        /// <summary>
        /// Gets Usuario token 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        private string generateJwtToken(Usuario user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("asdfsdff%#$%FSDFsdf");
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
