// Lisensiert til .NET Foundation under en eller flere avtaler.
// .NET Foundation lisenserer denne filen til deg under MIT-lisensen.
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ReficioSolution.Areas.Identity.Data;

namespace ReficioSolution.Areas.Identity.Pages.Account
{
    // Tillater tilgang for alle (ikke autentisert)
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ReficioSolutionUser> _userManager;
        private readonly IEmailSender _sender;

        // Konstruktør som injiserer UserManager og IEmailSender
        public RegisterConfirmationModel(UserManager<ReficioSolutionUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        // E-postadresse som skal bekreftes
        public string Email { get; set; }

        // Angir om bekreftelseslenken skal vises
        public bool DisplayConfirmAccountLink { get; set; }

        // URL for bekreftelseslenken
        public string EmailConfirmationUrl { get; set; }

        // Metode som håndterer GET-forespørselen for bekreftelsessiden
        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            // Redirect til startsiden hvis e-postadressen er null
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            // Sett returnUrl til standardverdi hvis det ikke er angitt
            returnUrl = returnUrl ?? Url.Content("~/");

            // Finn brukeren basert på e-postadressen
            var user = await _userManager.FindByEmailAsync(email);

            // Hvis brukeren ikke ble funnet, returner NotFound
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            // Setter egenskaper for visning i Razor-visningen
            Email = email;

            // Legg til kode for å bekrefte kontoen
            DisplayConfirmAccountLink = true;

            // Generer bekreftelseslenken hvis den skal vises
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            return Page();
        }
    }
}
