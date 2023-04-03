using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Services;
using Crud.Example.Main.Interfaces;

namespace Crud.Example.Main.Services
{
    public class CreateDealerService : ICreateDealerService
    {
        private readonly IDealerService _dealerService;

        public CreateDealerService(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }
        public void CreateDealer(Dealer dealer)
        {
            _dealerService.CreateDealer(dealer);
        }
    }
}