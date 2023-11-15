using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Add this namespace
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ReficioSolution.Areas.Identity.Data;
using ReficioSolution.Models;

namespace ReficioSolution.Data
{
    public class ReficioSolutionContext : IdentityDbContext<ReficioSolutionUser>
    {
        private readonly IConfiguration _configuration; // Add a field to store the configuration

        public ReficioSolutionContext(DbContextOptions<ReficioSolutionContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration; // Initialize the configuration
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
                // Get the connection string from appsettings.json using the IConfiguration
                var connectionString = _configuration.GetConnectionString("ReficioSolutionContextConnection");

                // Configure the database connection
                optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 5, 12)));
            }
        }
    }
}