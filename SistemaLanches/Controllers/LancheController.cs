using Microsoft.AspNetCore.Mvc;
using SistemaLanches.Models;
using SistemaLanches.Repositories;
using SistemaLanches.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLanches.Controllers
{
    public class LancheController : Controller
    {
        //DI
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }


        public IActionResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //código otimizado
                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderByDescending(l => l.LancheId);

                categoriaAtual = _categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.
                Lanches.
                FirstOrDefault(l => l.LancheId == lancheId);

            if (lanche == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(lanche);
        }


        public IActionResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Lanche> lanches;
            if (string.IsNullOrEmpty(_searchString))
            {
                lanches = _lancheRepository.
                    Lanches.
                    OrderBy(l => l.LancheId);
            }
            else
            {
                lanches = _lancheRepository.
                    Lanches.
                    Where(l => l.Nome.ToLower().
                    Contains(_searchString.ToLower()));
            }
            return View("~/Views/Lanche/List.cshtml",
                new LancheListViewModel
                {
                    Lanches = lanches,
                    CategoriaAtual = "Todos os Lanches"
                });
        }



    }
}
