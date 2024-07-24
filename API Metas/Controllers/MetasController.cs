using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Metas.Models;

namespace API_Metas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetasController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public MetasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Metas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lstMetas = await _context.Metas.ToListAsync();
                return Ok(lstMetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/MetasController/Detalle/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var lstMetas = await _context.Metas.Where(u => u.IdMeta == id).ToListAsync();
                return Ok(lstMetas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Metas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Metas metas)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Add(metas);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(metas);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/MetasController/Editar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Metas metas)
        {
            // Inicia una nueva transacción
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verifica si el ID proporcionado coincide con el ID del objeto
                if (id != metas.IdMeta)
                {
                    return NotFound();
                }

                // Actualiza la entidad
                _context.Update(metas);
                await _context.SaveChangesAsync();

                // Confirma la transacción
                await transaction.CommitAsync();
                return Ok(new { message = "La meta fue actualizada con éxito" });
            }
            catch (Exception ex)
            {
                // Revierte la transacción en caso de error
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }


        // POST: api/MetasController/Eliminar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var meta = await _context.Metas.FindAsync(id);
                if (meta == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Metas.Remove(meta);
                    await _context.SaveChangesAsync();
                    return Ok(new { messaje = "La meta fue eliminada con exito" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
