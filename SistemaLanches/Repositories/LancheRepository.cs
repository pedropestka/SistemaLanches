using Microsoft.EntityFrameworkCore;
using SistemaLanches.Context;
using SistemaLanches.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLanches.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context; 

        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p =>
                p.IsLanchePreferido).Include(c => c.Categoria);

        public Lanche GetLancheById(int lancheId) // retorno o lanche que passei o id
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
        //public Lanche GetLancheById(int lancheId) => _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);


    }
}
