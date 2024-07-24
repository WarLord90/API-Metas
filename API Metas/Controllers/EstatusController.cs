using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Metas.Models;

namespace API_Metas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstatusController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public EstatusController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Estatus
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lstEstatus = await _context.Estatus.ToListAsync();
                return Ok(lstEstatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/EstatusController/Detalle/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var lstEstatus = await _context.Estatus.Where(u => u.IdEstatus == id).ToListAsync();
                return Ok(lstEstatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Estatus
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Estatus Estatus)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Add(Estatus);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(Estatus);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/EstatusController/Editar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Estatus Estatus)
        {
            // Inicia una nueva transacción
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verifica si el ID proporcionado coincide con el ID del objeto
                if (id != Estatus.IdEstatus)
                {
                    return NotFound();
                }

                // Actualiza la entidad
                _context.Update(Estatus);
                await _context.SaveChangesAsync();

                // Confirma la transacción
                await transaction.CommitAsync();
                return Ok(new { message = "El estatus fue actualizado con éxito" });
            }
            catch (Exception ex)
            {
                // Revierte la transacción en caso de error
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }


        // POST: api/EstatusController/Eliminar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Estatus = await _context.Estatus.FindAsync(id);
                if (Estatus == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Estatus.Remove(Estatus);
                    await _context.SaveChangesAsync();
                    return Ok(new { messaje = "El estatus fue eliminado con exito" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
