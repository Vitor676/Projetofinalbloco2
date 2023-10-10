using Microsoft.EntityFrameworkCore;
using projetofinalbloco2.Model;

namespace projetofinalbloco2.Data
{
    public class FarmaciaDbContext : DbContext
    {
        public FarmaciaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produtos");
        }
        public DbSet<Produto> Produtos { get; set; } = null!;
    }
}
