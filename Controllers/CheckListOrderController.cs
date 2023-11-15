using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Models;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    // Kontrollerklasse for håndtering av sjekklister
    [Authorize] // Krever at brukeren er autentisert for å få tilgang til denne kontrolleren
    public class CheckListOrderController : Controller
    {
        private readonly CheckListRepository _repository; // Repository for håndtering av sjekklister

        // Konstruktør som injiserer sjekklisterepository
        public CheckListOrderController(CheckListRepository repository)
        {
            _repository = repository;
        }

        // Handling for visning av indekssiden for sjekklister
        public IActionResult Index()
        {
            var Checklist = _repository.GetSomeOrderInfo(); // Hent informasjon om noen ordrer fra sjekklisterepository
            return View(Checklist); // Send informasjonen til visningen
        }
    }
}