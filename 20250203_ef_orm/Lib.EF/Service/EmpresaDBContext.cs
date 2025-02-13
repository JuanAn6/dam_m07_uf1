using Lib.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace Lib.EF.Service
{
    public class EmpresaDBContext : DbContext
    {

        public EmpresaDBContext(DbContextOptions options ) : base (options)
        {

        }
        public DbSet<Empleat> Empleats { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Projecte> Projectes { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleat>().HasKey(e => e.Id);
            modelBuilder.Entity<Departament>().HasKey(e => e.Id);
            modelBuilder.Entity<Departament>().HasIndex(e => e.Nom).IsUnique();
            modelBuilder.Entity<Projecte>().HasKey(e => e.Id);
        }

    }
}