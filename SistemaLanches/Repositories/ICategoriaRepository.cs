using SistemaLanches.Models;
using System.Collections.Generic;


namespace SistemaLanches.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
