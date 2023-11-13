using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers;

public class FilledOutCheckListController : Controller
{
    private readonly CheckListRepository _repository;

    public FilledOutCheckListController(CheckListRepository repository)
    {
        _repository = repository;
    }
    
    public IActionResult Index(int id)
    {
        var Checklist = _repository.GetOneRowById(id);
        if (Checklist == null)
        {
            return NotFound();
        }
        return View(Checklist);
    }
}