using ReficioSolution.Models;

namespace ReficioSolution.Repositories;

    public interface IServiceFormRepository
    {
        public IEnumerable<ServiceFormViewModel> GetAll();

        public IEnumerable<ServiceFormViewModel> GetSomeOrderInfo();
        
        
    }