using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    
    public class ServiceOrderConnectorController : Controller
    {
        private readonly ServiceFormRepository _repository;
        
        public ServiceOrderConnectorController(ServiceFormRepository repository)
        {
            _repository = repository;
        }
        
        public IActionResult Index(int id)
        {
            var serviceFormEntry = _repository.GetRelevantData(id);
            if (serviceFormEntry == null)
            {
                return NotFound();
            }
            return View(serviceFormEntry);
        }
    }
}