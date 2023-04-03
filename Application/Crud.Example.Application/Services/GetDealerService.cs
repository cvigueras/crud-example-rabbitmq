using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class GetDealerService : IGetDealerService
    {
        private readonly IDealerRepository _dealerRepository;

        public GetDealerService(IDealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }
        public IEnumerable<Dealer> GetAllDealer()
        {
            return _dealerRepository.GetAll();
        }
    }
}
