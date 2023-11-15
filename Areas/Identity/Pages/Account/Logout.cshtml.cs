// Lisensiert til .NET Foundation under ett eller flere avtaler.
// .NET Foundation lisenserer denne filen til deg under MIT-lisensen.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ReficioSolution.Areas.Identity.Data;

namespace ReficioSolution.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ReficioSolutionUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ReficioSolutionUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Logg ut brukeren og returner til den opprinnelige siden eller oppdater gjeldende side.
        /// </summary>
        /// <param name="returnUrl">Den opprinnelige siden brukeren ønsket å gå til.</param>
        /// <returns>ActionResult basert på brukerens utlogging.</returns>
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            // Logg brukeren ut
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Bruker logget ut.");

            // Returner til den opprinnelige siden hvis angitt, ellers oppdater gjeldende side
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // Dette må være en redirect for at nettleseren skal utføre en ny
                // forespørsel, og identiteten til brukeren blir oppdatert.
                return RedirectToPage();
            }
        }
    }
}