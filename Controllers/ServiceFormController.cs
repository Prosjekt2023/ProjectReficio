using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Models;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    
    public class ServiceFormController : Controller
    {
        private readonly ServiceFormRepository _repository;

        public ServiceFormController(ServiceFormRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ServiceFormViewModel serviceFormViewModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(serviceFormViewModel);
                return RedirectToAction("Index", "ServiceOrder");
            }
            
            return View(serviceFormViewModel);
        }
        
        //public IActionResult Post()
    }
}