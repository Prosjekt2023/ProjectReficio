using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Models;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    [Authorize]
    public class CheckListOrderController : Controller
    {
        private readonly CheckListRepository _repository;

        public CheckListOrderController(CheckListRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var Checklist = _repository.GetSomeOrderInfo();
            return View(Checklist);
        }
    }
}