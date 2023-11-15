// Lisensiert til .NET-stiftelsen under ett eller flere avtaler.
// .NET-stiftelsen lisenserer denne filen til deg under MIT-lisensen.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ReficioSolution.Areas.Identity.Data;

namespace ReficioSolution.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ReficioSolutionUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ReficioSolutionUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // Egenskap for å binde input fra skjemaet
        [BindProperty]
        public InputModel Input { get; set; }

        // Liste over eksterne påloggingsalternativer
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        // Returnerings-URL etter pålogging
        public string ReturnUrl { get; set; }

        // Melding for feil ved pålogging (midlertidig data)
        [TempData]
        public string ErrorMessage { get; set; }

        // Modell for inputdata
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Husk meg?")]
            public bool RememberMe { get; set; }
        }

        // Utføres når GET-forespørsel mottas
        public async Task OnGetAsync(string returnUrl = null)
        {
            // Legg til feilmelding hvis det er en
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Sett standard-URL hvis ingen er angitt
            returnUrl ??= Url.Content("~/");

            // Logg ut eksisterende ekstern informasjonskapsel for å sikre en ren påloggingsprosess
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Hent og sett opp eksterne påloggingsalternativer
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        // Utføres når POST-forespørsel mottas
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Sett standard-URL hvis ingen er angitt
            returnUrl ??= Url.Content("~/");

            // Hent og sett opp eksterne påloggingsalternativer
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Hvis modellen er gyldig, prøv pålogging
            if (ModelState.IsValid)
            {
                // Dette teller ikke påloggingsfeil mot kontolås
                // For å aktivere kontolås ved påloggingsfeil, sett lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Bruker pålogget.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Brukerkonto låst.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ugyldig påloggingsforsøk.");
                    return Page();
                }
            }

            // Hvis vi kommer hit, har noe feilet, vis skjemaet på nytt
            return Page();
        }
    }
}
