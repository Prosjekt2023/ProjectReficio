using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    // Kontrollerklasse for håndtering av utfylte sjekklister
    [Authorize] // Krever at brukeren er autentisert for å få tilgang til denne kontrolleren
    public class FilledOutCheckListController : Controller
    {
        private readonly CheckListRepository _repository; // Repository for håndtering av sjekklister

        // Konstruktør som injiserer sjekklisterepository
        public FilledOutCheckListController(CheckListRepository repository)
        {
            _repository = repository;
        }

        // Handling for visning av indekssiden for utfylte sjekklister
        public IActionResult Index(int id)
        {
            var Checklist = _repository.GetOneRowById(id); // Hent informasjon om en utfylt sjekkliste basert på ID
            if (Checklist == null)
            {
                return NotFound(); // Hvis sjekklisten ikke finnes, returner NotFound-resultat
            }

            return View(Checklist); // Send informasjonen til visningen
        }
    }
}