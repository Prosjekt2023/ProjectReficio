using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Models;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    [Authorize]
    public class ServiceOrderController : Controller
    {
        private readonly ServiceFormRepository _repository;

        public ServiceOrderController(ServiceFormRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var serviceFormEntry = _repository.GetSomeOrderInfo();
            return View(serviceFormEntry);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int serviceFormId, string status)
        {
            try
            {
                _repository.InsertServiceOrderStatus(serviceFormId, status);
                // Omdiriger til en passende side etter oppdatering
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Logg feilen og vis en feilmelding til brukeren
                return View("Error");
            }
        }

        
    }
}