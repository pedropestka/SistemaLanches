﻿using SistemaLanches.Models;
using System.Collections.Generic;

namespace SistemaLanches.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }

    }
}
