using Crud.Example.Domain.Entities;

namespace Crud.Example.Domain.Services
{
    public interface IFoodService : IBaseService<Food>
    {
        void CreateFood(Food food);
        IEnumerable<Food> GetAllFood();
    }
}
