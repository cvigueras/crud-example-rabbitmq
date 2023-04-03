using Crud.Example.Domain.Services;

namespace Crud.Example.Main.Services
{
    public class DealerAppService
    {
        private readonly IDealerService _dealerService;

        public DealerAppService(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }
    }
}