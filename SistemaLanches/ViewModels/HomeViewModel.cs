using SistemaLanches.Models;
using System.Collections.Generic;

namespace SistemaLanches.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> Lanches_Preferidos { get; set; }
    }
}
