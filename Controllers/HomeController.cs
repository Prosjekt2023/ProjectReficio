using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Models;

/*
 * Linjen "System.Diagnostics"- namespace gir tilgang
 * til klasser for å håndtere feillogging og
 * feisporing.
 * Linjen "Microsoft.AspNetCore.Mvc" importerer namespacet
 * som inneholder klasser og funksjonalitet knyttet til ASP.NET
 * Core MVC (Model-View-Controller).
 * Linjen "ReficioSolution.Models" implementerer denne
 * namespacet som inneholder modellklasser relatert til
 * applikasjonens datastruktur.
 * 
 */
namespace ReficioSolution.Controllers
{
	/*
	 * Dette er et namespace-deklarasjon  som plasserer
	 * klassen "HomeController" innenfor "ReficioSolution.
	 * Controllers"-namespace. Dette hjelper til med organisering og unngår konflikter med andre klasser som kan ha samme navn i andre deler av prosjektet.
	 */
	public class HomeController : Controller
	/*
	 * Dette er en deklarasjon av "HomeController"- klassen
	 * som arver fra "Controller"-klassen i ASP.NET Core MVC.
	 * "Controller"-klassen gir funksjonalitet for å håndtere forespørsler
	 * i et MVC-mønster.
	 */
	{
		private readonly ILogger<HomeController> _logger;
		/*
		 * Dette er en privat variabel "_logger" av typen "ILogger<HomeController>".
		 * Dette brukes til å logge meldinger, og det blir
		 * sannsynligvis injisert via konstruktøren.
		 */

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		/*
		 * Dette er konstruktøren for "HomeController" som tar imot en
		 * "ILogger<HomeController>" som en parameter og lagrer den i
		 * "_logger"_variabelen.
		 */

		public IActionResult Index()
		{
			return View();
		}
		/*
		 * Dette er en action-metode som håndter forespørsler for hjemmesiden
		 * ("/Home/Index"). Den returnerer en "View" for å vise hjemmesiden.
		 */

		public IActionResult Privacy()
		{
			return View();
		}
		/*
		 * Dette er en annen action-metode som håndterer forespørsler for personvernssiden
		 * ("/Home/Privacy"). Den returnerer også en "View".
		 */

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		/*
		 * Dette er en attributtdeklarasjon for "Error"-metoden. Den angir at responsen ikke skal
		 * caches (Duration = 0), og den skal ikke lagres (NoStore = true).
		 */
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		/*
		 * Dette er en action-metode som håndterer feil. Den returnerer en "View" for feil
		 * og gir også informasjon om feilen ved å oprette en instans av "ErrorViewModel".
		 * Den bruker "Activity.Current=.Id ?? HttpContext.TraceIdentifier"
		 * for å generere en unik forespørsels-ID for feilsporing
		 */
	}
	/*
	 * Samlet sett er "HomController" en del av ASP.NET Core MVC-strukturen og håndterer forespørsler for hjemmesiden,
	 * personvernsiden, og feilhåndtering. Den bruker logging og inneholder referanser til modellklasser og
	 * ASP.NET Core MVC-funksjonalitet.
	 */
}