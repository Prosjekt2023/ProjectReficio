using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ReficioSolution.Areas.Identity.Data;

// Legg til profildata for applikasjonsbrukere ved å legge til egenskaper i ReficioSolutionUser-klassen
public class ReficioSolutionUser : IdentityUser
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}

