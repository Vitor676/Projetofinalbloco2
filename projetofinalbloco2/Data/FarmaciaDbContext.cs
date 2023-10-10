using Microsoft.EntityFrameworkCore;
using projetofinalbloco2.Model;
using System;

namespace projetofinalbloco2.Data
{
    public class FarmaciaDbContext : DbContext
    {
        public FarmaciaDbContext(DbContextOptions<FarmaciaDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produtos");
            modelBuilder.Entity<Categoria>().ToTable("tb_categorias");

            _ = modelBuilder.Entity<Produto>()
              .HasOne(p => p.Categorias)
              .WithMany(c => c.Produtos)
              .HasForeignKey("CategoriaId")
              .OnDelete(DeleteBehavior.Cascade);

        }

        // Registrar DbSet - Objeto responsável por manipular a Tabela
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;


    }
}
