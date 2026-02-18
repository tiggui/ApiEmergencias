using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webapi_emergencias.Data;
using webapi_emergencias.Models;

namespace webapi_emergencias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergenciasController : ControllerBase
    {
        private readonly DBContext _context;

        public EmergenciasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Emergencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emergencia>>> GetEmergencias()
        {
            var ListaEmergencia = await _context.Emergencias.ToListAsync();
            var emergenciaDTO = ListaEmergencia.Select(oE => new Emergencia 
            {
                IdEmergencia =oE.IdEmergencia,
                Descripcion = oE.Descripcion,
                Tipo = oE.Tipo,
                Gravedad = oE.Gravedad,
                Urgencia = oE.Urgencia,
                Foto = oE.Foto,
                Ruta = getImageURL(oE),

            });

             return Ok(emergenciaDTO);

        }

        private string getImageURL(Emergencia oE)
        {
            if (!string.IsNullOrEmpty(oE.Ruta))//oE.Ruta != null || oE.Ruta != ""
            {
                return Request.Scheme + "://" + Request.Host + "/" + oE.Ruta.Replace("\\", "/");
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        // GET: api/Emergencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emergencia>> GetEmergencia(int id)
        {
            var emergencia = await _context.Emergencias.FindAsync(id);

            if (emergencia == null)
            {
                return NotFound();
            }

            return emergencia;
        }

        // PUT: api/Emergencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmergencia(int id, Emergencia emergencia)
        {
            if (id != emergencia.IdEmergencia)
            {
                return BadRequest();
            }

            _context.Entry(emergencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmergenciaExists(id))
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

        // POST: api/Emergencias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emergencia>> PostEmergencia([FromForm] Emergencia emergencia)
        {
            var ruta = "";
            if (emergencia.Archivo.Length > 0)//si existe imagen
            {
                var filePath = Guid.NewGuid().ToString()+ ".jpg" ;
                ruta = "Imagenes/" + filePath;

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                   await  emergencia.Archivo.CopyToAsync(stream);
                    emergencia.Foto = filePath; 
                    emergencia.Ruta = ruta;

                }
            }
            _context.Emergencias.Add(emergencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmergencia", new { id = emergencia.IdEmergencia }, emergencia);
        }

        // DELETE: api/Emergencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmergencia(int id)
        {
            var emergencia = await _context.Emergencias.FindAsync(id);
            if (emergencia == null)
            {
                return NotFound();
            }

            _context.Emergencias.Remove(emergencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmergenciaExists(int id)
        {
            return _context.Emergencias.Any(e => e.IdEmergencia == id);
        }
    }
}
