using Microsoft.EntityFrameworkCore;
using SistemaLanches.Models;

namespace SistemaLanches.Context
{
    public class AppDbContext : DbContext
    {
        //construtor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // tabelas - entidades mapeadas:
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }


    }
}
