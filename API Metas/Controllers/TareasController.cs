using API_Metas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Metas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : Controller
    {
        private readonly AplicationDbContext _context;

        public TareasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lstTareas = await _context.Tareas.ToListAsync();
                return Ok(lstTareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/TareasController/Detalle/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var lstTareas = await _context.Tareas.Where(u => u.IdTarea == id).ToListAsync();
                return Ok(lstTareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Tareas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tareas tareas)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Add(tareas);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/TareasController/Editar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Tareas tareas)
        {
            // Inicia una nueva transacción
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verifica si el ID proporcionado coincide con el ID del objeto
                if (id != tareas.IdTarea)
                {
                    return NotFound();
                }

                // Actualiza la entidad
                _context.Update(tareas);
                await _context.SaveChangesAsync();

                // Confirma la transacción
                await transaction.CommitAsync();
                return Ok(new { message = "La tarea fue actualizada con éxito" });
            }
            catch (Exception ex)
            {
                // Revierte la transacción en caso de error
                await transaction.RollbackAsync();
                return BadRequest(ex.Message);
            }
        }

        // POST: api/TareasController/Eliminar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarea = await _context.Tareas.FindAsync(id);
                if (tarea == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Tareas.Remove(tarea);
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
