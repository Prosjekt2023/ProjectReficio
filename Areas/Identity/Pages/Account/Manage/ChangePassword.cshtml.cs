﻿// Lisensiert til .NET Foundation under en eller flere avtaler.
// .NET Foundation lisenserer denne filen til deg under MIT-lisensen.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ReficioSolution.Areas.Identity.Data;

namespace ReficioSolution.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ReficioSolutionUser> _userManager;
        private readonly SignInManager<ReficioSolutionUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<ReficioSolutionUser> userManager,
            SignInManager<ReficioSolutionUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        ///     Denne API-en støtter ASP.NET Core Identity sitt standard UI-infrastruktur og er ikke ment å bli brukt
        ///     direkte fra koden din. Denne API-en kan endres eller fjernes i fremtidige utgivelser.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     Denne API-en støtter ASP.NET Core Identity sitt standard UI-infrastruktur og er ikke ment å bli brukt
        ///     direkte fra koden din. Denne API-en kan endres eller fjernes i fremtidige utgivelser.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     Denne API-en støtter ASP.NET Core Identity sitt standard UI-infrastruktur og er ikke ment å bli brukt
        ///     direkte fra koden din. Denne API-en kan endres eller fjernes i fremtidige utgivelser.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     Denne API-en støtter ASP.NET Core Identity sitt standard UI-infrastruktur og er ikke ment å bli brukt
            ///     direkte fra koden din. Denne API-en kan endres eller fjernes i fremtidige utgivelser.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Nåværende passord")]
            public string OldPassword { get; set; }

            /// <summary>
            ///     Denne API-en støtter ASP.NET Core Identity sitt standard UI-infrastruktur og er ikke ment å bli brukt
            ///     direkte fra koden din. Denne API-en kan endres eller fjernes i fremtidige utgivelser.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = " {0} må være minst {2} og maks {1} tegn langt.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nytt passord")]
            public string NewPassword { get; set; }

            /// <summary>
            ///     Denne API-en støtter ASP.NET Core Identity sitt standard UI-infrastruktur og er ikke ment å bli brukt
            ///     direkte fra koden din. Denne API-en kan endres eller fjernes i fremtidige utgivelser.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Bekreft nytt passord")]
            [Compare("NewPassword", ErrorMessage = "Det nye passordet og bekreftelsespassordet stemmer ikke overens.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kan ikke laste bruker med ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kan ikke laste bruker med ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("Bruker endret passordet sitt vellykket.");
            StatusMessage = "Passordet ditt er endret.";

            return RedirectToPage();
        }
    }
}
