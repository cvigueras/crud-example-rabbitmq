using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Domain.Services;

namespace Crud.Example.Domain.Core.Services
{
    public class DealerService : BaseService<Dealer>, IDealerService
    {
        #region Public Members
        private readonly IDealerRepository _dealerRepository;
        private readonly IShopRepository _shopRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Corresponde al tipo de interfaz de tipo IDealerRepository.
        /// </summary>
        /// <param name="dealerRepository"></param>
        public DealerService(IDealerRepository dealerRepository,
                            IShopRepository shopRepository) 
                            : base(dealerRepository)
        {
            _dealerRepository = dealerRepository;
            _shopRepository = shopRepository;
        }
        #endregion

        #region Public Methods
        public void CreateDealer(Dealer dealer)
        {
            dealer.Shop = AssingShopToDealer();
            _dealerRepository.Add(dealer);
        }

        public IEnumerable<Dealer> GetAllDealers()
        {
            return _dealerRepository.GetAll();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <returns></returns>
        private Shop? AssingShopToDealer()
        {
            return _shopRepository.GetById(10);
        }
        #endregion
    }
}
