using Microsoft.AspNetCore.Mvc;
using SistemaLanches.Repositories;
using SistemaLanches.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLanches.Controllers
{
    public class LancheController : Controller
    {
        // DI
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        // Método para retornar os lanches
        public IActionResult List()
        {
            ViewBag.Lanche = "Lanches"; // passar informações do controlador para a view
            ViewData["Categoria"] = "Categoria"; // passar informações do controlador para a view

            //var lanches = _lancheRepository.Lanches;
            //return View(lanches);

            var lancheslistViewModel = new LancheListViewModel();
            lancheslistViewModel.Lanches = _lancheRepository.Lanches;
            lancheslistViewModel.CategoriaAtual = "Categoria Atual";
            return View(lancheslistViewModel);

        }

    }
}
