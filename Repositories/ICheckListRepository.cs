using ReficioSolution.Models;

namespace ReficioSolution.Repositories;

    public interface ICheckListRepository
    {
        public IEnumerable<CheckListViewModel> GetAll();
        
        public IEnumerable<CheckListViewModel> GetSomeOrderInfo();
    }