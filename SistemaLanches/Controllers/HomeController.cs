using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaLanches.Repositories;
using SistemaLanches.ViewModels;

namespace SistemaLanches.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }
        
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Lanches_Preferidos = _lancheRepository.LanchesPreferidos
            };
            
            return View(homeViewModel);
        }


    }
}
