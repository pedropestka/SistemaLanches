using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaLanches.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [StringLength(100)]
        public string CategoriaNome { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        // relacionamento
        // uma categoria pode ter muitos lanches
        public List<Lanche> Lanches { get; set; }

    }
}
