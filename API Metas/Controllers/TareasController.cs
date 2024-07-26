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
                // Verificar si existe una tarea con el mismo nombre y el mismo IdMeta
                bool existeTarea = await _context.Tareas.AnyAsync(t => t.NombreTarea == tareas.NombreTarea && t.IdMeta == tareas.IdMeta);

                if (existeTarea)
                {
                    return Conflict("Ya existe una tarea con el mismo nombre para la misma meta.");
                }

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
                // Obtiene la entidad existente
                var existingTarea = await _context.Tareas.FindAsync(id);

                if (existingTarea == null)
                {
                    return NotFound();
                }

                // Verifica si el NombreTarea ya existe en otra tarea con el mismo IdMeta
                var tareaConMismoNombre = await _context.Tareas
                    .Where(t => t.NombreTarea == tareas.NombreTarea && t.IdMeta == tareas.IdMeta && t.IdTarea != id)
                    .FirstOrDefaultAsync();

                if (tareaConMismoNombre != null)
                {
                    return Conflict("Ya existe una tarea con el mismo nombre para esta meta.");
                }

                // Actualiza solo los campos necesarios
                existingTarea.NombreTarea = tareas.NombreTarea;
                existingTarea.IdMeta = tareas.IdMeta;
                existingTarea.FechaActualizacion = DateTime.UtcNow; 

                // Marca la entidad como modificada
                _context.Tareas.Update(existingTarea);
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

        // PUT: api/TareasController/ActualizarEstatus/5
        [HttpPut("ActualizarEstatus/{id}")]
        public async Task<IActionResult> ActualizarEstatus(int id)
        {
            // Inicia una nueva transacción
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tarea = await _context.Tareas.FindAsync(id);
                if (tarea == null)
                {
                    return NotFound();
                }

                // Actualizar solo el estatus de la tarea
                tarea.IdEstatus = 2;
                tarea.FechaActualizacion = DateTime.UtcNow;

                // Marca la entidad como modificada
                _context.Tareas.Update(tarea);
                await _context.SaveChangesAsync();

                // Confirma la transacción
                await transaction.CommitAsync();
                return Ok(new { message = "El estatus de la tarea fue actualizado con éxito" });
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
                    return Ok(new { messaje = "La tarea fue eliminada con exito" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
