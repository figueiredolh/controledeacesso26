using ControleDeAcesso26.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleDeAcesso26.Infrastructure.DataAccess.DatabaseContext
{
    public class ControleDeAcesso26DbContext : DbContext
    {
        public ControleDeAcesso26DbContext(DbContextOptions options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ControleDeAcesso26DbContext).Assembly);
        }
    }
}
