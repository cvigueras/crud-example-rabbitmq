using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Infrastructure.Data.Context;

namespace Crud.Example.Infrastructure.Data.Repositories
{
    public class EFShopRepository : EFBaseRepository<Shop>, IShopRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EFShopRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Remove a food by Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Shop entity</returns>
        public Shop? RemoveShopByName(string name)
        {
            var shop = _applicationDbContext.Shop?.Where(x => x.Name != null && x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
            if(shop == null)
            {
                return null;
            }
            _applicationDbContext.Shop?.Remove(shop);
            var idRemoved = _applicationDbContext.SaveChanges();
            return idRemoved > 0 ? shop : null;
        }

        public List<Shop>? GetAllShopsExpirated()
        {
            return _applicationDbContext.Shop?.Where(x => x.ExpirationDateTime.CompareTo(DateTime.Now) <= 0 && !x.Processed).ToList();
        }
    }
}
