using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReficioSolution.Areas.Identity.Data;
using ReficioSolution.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ReficioSolution.Controllers
{
    // Kontrollerklasse for administrasjon av brukerkontoer
    [Authorize(Roles="Admin")] // Krever at brukeren har rollen "Admin" for å få tilgang til denne kontrolleren
    public class AccountController : Controller
    {
        private readonly UserManager<ReficioSolutionUser> _userManager; // UserManager for håndtering av brukeroperasjoner
        private readonly RoleManager<IdentityRole> _roleManager; // RoleManager for håndtering av roller

        // Konstruktør som injiserer UserManager og RoleManager
        public AccountController(UserManager<ReficioSolutionUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // HTTP POST-metode for sletting av brukerkonto basert på brukerens ID
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Bruker ikke funnet
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // Valgfritt: Utfør ytterligere handlinger etter vellykket sletting
                // For eksempel, oppdater brukerlisten eller vis en suksessmelding.
                TempData["SuccessMessage"] = "Bruker slettet vellykket.";
            }
            else
            {
                // Håndter feil, for eksempel visning av en feilmelding
                TempData["ErrorMessage"] = "Feil ved sletting av bruker.";
            }

            return RedirectToAction("Index");
        }

        // HTTP GET-metode for visning av brukerindeksen
        public async Task<IActionResult> Index()
        {
            // Hent en liste over brukere
            var users = await _userManager.Users.ToListAsync();

            // Send brukerne til visningen
            var viewModel = new AccountViewModel { Users = users, };
            return View(viewModel);
        }
    }
}
