using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReficioSolution.Models;
using ReficioSolution.Repositories;

namespace ReficioSolution.Controllers
{
    [Authorize]
    public class ServiceOrderConnectorController : Controller
    {
        private readonly ServiceFormRepository _serviceFormRepository;
        private readonly CheckListRepository _checkListRepository; // Assuming you have a CheckListRepository

        public ServiceOrderConnectorController(ServiceFormRepository serviceFormRepository, CheckListRepository checkListRepository)
        {
            _serviceFormRepository = serviceFormRepository;
            _checkListRepository = checkListRepository;
        }

        public IActionResult Index(int id, string status) // Added 'status' parameter
        {
            var serviceFormEntry = _serviceFormRepository.GetRelevantData(id);
            var checkListEntry = _checkListRepository.GetRelevantData(id); // Assuming you have a method to get CheckList data

            if (serviceFormEntry == null || checkListEntry == null)
            {
                return NotFound();
            }

            var compositeViewModel = new CompositeViewModel
            {
                ServiceForm = serviceFormEntry,
                CheckList = checkListEntry,
                Status = status // Add a Status property in your CompositeViewModel if it doesn't already exist
            };

            return View(compositeViewModel);
        }
    }
}