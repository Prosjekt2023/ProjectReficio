using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    // Kontrollerklasse for håndtering av utfylte tjenesteskjemaer
    [Authorize] // Krever at brukeren er autentisert for å få tilgang til denne kontrolleren
    public class FilledOutServiceFormController : Controller
    {
        private readonly ServiceFormRepository _repository; // Repository for håndtering av tjenesteskjemaer

        // Konstruktør som injiserer tjenesteskjemarepository
        public FilledOutServiceFormController(ServiceFormRepository repository)
        {
            _repository = repository;
        }

        // Handling for visning av indekssiden for utfylte tjenesteskjemaer
        public IActionResult Index(int id)
        {
            var serviceFormEntry = _repository.GetOneRowById(id); // Hent informasjon om en utfylt tjenesteskjemaoppføring basert på ID
            if (serviceFormEntry == null)
            {
                return NotFound(); // Hvis tjenesteskjemaoppføringen ikke finnes, returner NotFound-resultat
            }

            return View(serviceFormEntry); // Send informasjonen til visningen
        }
    }
}