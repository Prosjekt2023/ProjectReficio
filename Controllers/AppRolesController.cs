using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReficioSolution.Data;
using ReficioSolution.Models;

namespace ReficioSolution.Controllers
{
    // Kontrollerklasse for administrasjon av roller
    [Authorize(Roles="Admin")] // Krever at brukeren har rollen "Admin" for å få tilgang til denne kontrolleren
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager; // RoleManager for håndtering av roller

        // Konstruktør som injiserer RoleManager
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Handling for visning av alle roller opprettet av brukere
        public IActionResult Index()
        {
            var roles = _roleManager.Roles; // Hent alle roller
            return View(roles); // Send roller til visningen
        }

        // HTTP GET-metode for visning av skjemaet for å opprette en ny rolle
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // HTTP POST-metode for opprettelse av en ny rolle
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            // Unngå duplikatroller
            if (!(_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult()))
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult(); // Opprett ny rolle
            }

            return RedirectToAction("Index"); // Omdiriger til indekssiden for roller etter opprettelse
        }
    }
}