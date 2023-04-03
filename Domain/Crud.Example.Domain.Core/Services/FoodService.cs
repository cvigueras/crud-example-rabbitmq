using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Crud.Example.Domain.Services;

namespace Crud.Example.Domain.Core.Services
{
    public class FoodService : BaseService<Food>, IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        public FoodService(IFoodRepository foodRepository) : base(foodRepository)
        { 
            _foodRepository = foodRepository;
        }

        public void CreateFood(Food food)
        {
            _foodRepository.Add(food);
        }

        public IEnumerable<Food> GetAllFood()
        {
            return _foodRepository.GetAll();
        }

    }
}
