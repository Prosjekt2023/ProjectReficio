// Lisensiert til .NET Foundation under en eller flere avtaler.
// .NET Foundation lisenserer denne filen til deg under MIT-lisensen.
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ReficioSolution.Areas.Identity.Data;

namespace ReficioSolution.Areas.Identity.Pages.Account.Manage
{
    // Klassen representerer en Razor-siden for administrasjon av personlige data
    public class PersonalDataModel : PageModel
    {
        // UserManager brukes for å administrere brukerinformasjon
        private readonly UserManager<ReficioSolutionUser> _userManager;

        // Logger brukes for å logge hendelser i applikasjonen
        private readonly ILogger<PersonalDataModel> _logger;

        // Konstruktøren mottar nødvendige tjenester via avhengighetsinjeksjon
        public PersonalDataModel(
            UserManager<ReficioSolutionUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        // OnGet-metoden håndterer GET-forespørsler til siden
        public async Task<IActionResult> OnGet()
        {
            // Henter brukerinformasjon basert på innlogget bruker
            var user = await _userManager.GetUserAsync(User);

            // Sjekker om brukeren ikke ble funnet
            if (user == null)
            {
                // Returnerer NotFound-resultat med feilmelding hvis brukeren ikke ble funnet
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Returnerer siden
            return Page();
        }
    }
}