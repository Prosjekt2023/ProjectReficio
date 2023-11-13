using Microsoft.AspNetCore.Identity;
using ReficioSolution.Areas.Identity.Data;
using System.Collections.Generic;

namespace ReficioSolution.Models
{
    public class AccountViewModel
    {
        public List<ReficioSolutionUser> Users { get; set; }
        
    }
}
    