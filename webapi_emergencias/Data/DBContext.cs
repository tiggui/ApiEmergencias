using Microsoft.EntityFrameworkCore;
using webapi_emergencias.Models;

namespace webapi_emergencias.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options) { }

        public DbSet<Emergencia> Emergencias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emergencia>().ToTable("Emergencias");
        }


    }
}
