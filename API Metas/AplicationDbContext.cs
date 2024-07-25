using Microsoft.EntityFrameworkCore;
using API_Metas.Models;

namespace API_Metas
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Estatus> Estatus { get; set; }
        public DbSet<Metas> Metas { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
    }
}