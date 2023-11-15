// Lisensiert til .NET Foundation under en eller flere avtaler.
// .NET Foundation lisensierer denne filen til deg under MIT-lisensen.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ReficioSolution.Areas.Identity.Data;

namespace ReficioSolution.Areas.Identity.Pages.Account
{
    // Dette er registermodellen, ansvarlig for registreringssiden
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ReficioSolutionUser> _signInManager;
        private readonly UserManager<ReficioSolutionUser> _userManager;
        private readonly IUserStore<ReficioSolutionUser> _userStore;
        private readonly IUserEmailStore<ReficioSolutionUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Konstruktøren injiserer avhengigheter som kreves for funksjonaliteten
        public RegisterModel(
            UserManager<ReficioSolutionUser> userManager,
            IUserStore<ReficioSolutionUser> userStore,
            SignInManager<ReficioSolutionUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        // BindProperty brukes til å binde modellen til skjemaet i Razor-visningen
        [BindProperty]
        public InputModel Input { get; set; }

        // ReturnUrl holder på URLen brukeren ønsker å gå til etter registrering
        public string ReturnUrl { get; set; }

        // ExternalLogins inneholder eksterne påloggingsalternativer som Google, Facebook, etc.
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        // InputModel representerer dataene som blir sendt fra registreringsskjemaet
        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string Lastname { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string? Role { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        // OnGetAsync blir kalt når brukeren går til registreringssiden
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Setter opp RoleList for rullegardinmenyen basert på tilgjengelige roller
            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
        }

        // OnPostAsync blir kalt når skjemaet på registreringssiden sendes
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Hvis ReturnUrl er null, settes den til standardverdien "~/"
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Sjekker om skjemaet er gyldig
            if (ModelState.IsValid)
            {
                // Oppretter en ny bruker
                var user = CreateUser();

                // Setter e-post og brukernavn for brukeren
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                // Legger til ekstra informasjon om brukeren (fornavn, etternavn)
                user.Firstname = Input.Firstname;
                user.Lastname = Input.Lastname;

                // Prøver å opprette brukeren i systemet
                var result = await _userManager.CreateAsync(user, Input.Password);

                // Hvis brukeren opprettes vellykket
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Legger brukeren til i den valgte rollen
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    // Genererer en lenke for e-postbekreftelse
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme
                    );


                    // Sender en bekreftelses-e-post til brukeren
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // Hvis bekreftelse av e-post er påkrevd, omdiriger til bekreftelsessiden
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    // Ellers logg brukeren inn og omdiriger til ønsket side
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                // Hvis det oppstår feil, legg feilmeldingene til ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Hvis skjemaet er ugyldig, vis skjemaet igjen med feilmeldinger
            return Page();
        }

        // Denne metoden oppretter en instans av ReficioSolutionUser
        // Den brukes for å kunne tilpasse opprettelsen av brukeren ved behov
        private ReficioSolutionUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ReficioSolutionUser>();
            }
            catch
            {
                // Kaster en unntak hvis det ikke er mulig å opprette en instans av ReficioSolutionUser
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ReficioSolutionUser)}'. " +
                    $"Ensure that '{nameof(ReficioSolutionUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        // Henter e-postlagringstjenesten
        private IUserEmailStore<ReficioSolutionUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                // Kaster et unntak hvis brukertjenesten ikke støtter e-post
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ReficioSolutionUser>)_userStore;
        }
    }
}
