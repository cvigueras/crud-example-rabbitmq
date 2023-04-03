using Crud.Example.Domain.Entities;
using Crud.Example.Infrastructure.Data.Context;

namespace Crud.Example.Infrastructure.Data.Seed
{
    public class ModelBuilderSeed
    {
        #region Private members
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly List<Shop> _shops;
        //private readonly List<Order> _orders;
        private readonly List<Dealer> _dealers;
        private readonly List<Food> _foods;
        private readonly Random _rand;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public ModelBuilderSeed(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _shops = new List<Shop>();
            //_orders = new List<Order>();
            _dealers = new List<Dealer>();
            _foods = new List<Food>();
            _rand = new Random();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Call Seed to create all Test Data.
        /// </summary>
        public void Seed()
        {
            CreateAllData();
            InsertSeedDataBBDD();
        }
        #endregion

        #region Private Methods
        private void AddFood()
        {
            _applicationDbContext.AddRange(_foods);
        }

        /// <summary>
        /// Insert Orders test data in SQL Server.
        /// </summary>
        /// <param name="dealers"></param>
        /// <param name="shops"></param>
        /// <param name="foods"></param>
        //private void AddOrders()
        //{
        //    _applicationDbContext.AddRange(_orders);
        //}

        /// <summary>
        /// Insert Dealers test data in SQL Server.
        /// </summary>
        private void AddDealer()
        {
            _applicationDbContext.AddRange(_dealers);
        }

        /// <summary>
        /// Insert Shops test data in SQL Server.
        /// </summary>
        private void AddShops()
        {
            _applicationDbContext.AddRange(_shops);
        }

        /// <summary>
        /// Create all data for Foods.
        /// </summary>
        private void CreateListFoods()
        {
            var listFoods = new string[20] {"Pizza","Hamburguesa", "Perrito", "Patatas fritas",
                                        "Patatas bravas", "Nugets", "Ensalada", "Tarta",
                                        "Bocadillo", "Sandwich","Hamburgusa","Pizza", "Patatas fritas", "Perrito",
                                        "Nugets", "Patatas bravas", "Tarta", "Ensalada",
                                        "Sandwich", "Bocadillo"};
            for (int i = 0; i < 20; i++)
            {
                var food = new Food
                {
                    Name = listFoods[i],
                    Price = i % 2 == 0 ? 4.35 + 3*i : 4.35 + i,
                    Size = i % 2 == 0 ? "XL" : "L",
                };
                _foods.Add(food);
            }
        }

        /// <summary>
        /// Create all seed data for Dealers
        /// </summary>
        private void CreateListDealers()
        {
            for (int i = 0; i < 100; i++)
            {
                var dealer = new Dealer
                {
                    Name = "Dealer Name" + i.ToString(),
                    Surname = "Dealer Surname" + i.ToString(),
                    Shop = _shops.OrderBy(x => _rand.Next(0, _shops.Count())).FirstOrDefault(),
                };
                _dealers.Add(dealer);
            }
        }

        /// <summary>
        /// Create seed data for shops.
        /// </summary>
        private void CreateListShops()
        {
            for (int i = 0; i < 500; i++)
            {
                var shop = new Shop
                {
                    Name = "PicaPizza " + i.ToString(),
                    Address = "Avenida PicaPizza " + i.ToString(),
                    Dealers = new List<Dealer>(_dealers.OrderBy(x => _rand.Next(0, _dealers.Count())).Take(4).AsEnumerable()),
                    ExpirationDateTime = i % 2 == 0 ? DateTime.Now.AddDays(-2) : DateTime.Now.AddDays(7),
                };
                _shops.Add(shop);
            }
        }

        /// <summary>
        /// Call to create all necessay seed data.
        /// </summary>
        private void CreateAllData()
        {
            CreateListDealers();
            CreateListFoods();
            CreateListShops();
        }

        /// <summary>
        /// Insert all seed data created in DataBase.
        /// </summary>
        private void InsertSeedDataBBDD()
        {
            AddFood();
            AddDealer();
            AddShops();
            _applicationDbContext.SaveChanges();
        }
        #endregion
    }
}