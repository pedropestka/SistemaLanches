using SistemaLanches.Context;
using SistemaLanches.Models;
using System.Collections.Generic;

namespace SistemaLanches.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        
        public CategoriaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
