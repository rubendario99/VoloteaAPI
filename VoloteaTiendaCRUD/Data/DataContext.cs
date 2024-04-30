using Microsoft.EntityFrameworkCore;
using VoloteaTiendaCRUD.Models;

namespace VoloteaTiendaAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        //Establecemos precisión al precio para que no se trunquen valores de manera incorrecta
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasPrecision(18, 2);
            });
        }
    }
}
