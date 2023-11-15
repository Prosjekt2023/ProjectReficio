using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    [Authorize]
    public class FilledOutCheckListController : Controller
    {
        private readonly CheckListRepository _repository;

        public FilledOutCheckListController(CheckListRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int id)
        {
            var checklist = _repository.GetOneRowById(id);
            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }
    }
}