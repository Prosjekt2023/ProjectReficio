using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Legg til dette navnerommet.
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ReficioSolution.Areas.Identity.Data;
using ReficioSolution.Models;

namespace ReficioSolution.Data
{
    public class ReficioSolutionContext : IdentityDbContext<ReficioSolutionUser>
    {
        private readonly IConfiguration _configuration; //Legg til et felt for å lagre konfigurasjonen

        public ReficioSolutionContext(DbContextOptions<ReficioSolutionContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration; // Initialiser konfigurasjonen
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Hent tilkoblingsstrengen fra appsettings.json ved å bruke IConfiguration
                var connectionString = _configuration.GetConnectionString("ReficioSolutionContextConnection");

                // Konfigurer database-tilkoblingen
                optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 5, 12)));
            }
        }
    }
}