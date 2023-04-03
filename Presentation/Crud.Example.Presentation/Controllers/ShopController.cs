using System.Text.Json;
using Crud.Example.Api.Filters;
using Crud.Example.Domain.Entities;
using Crud.Example.Main.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Example.Api.Controllers
{
    [ServiceFilter(typeof(BearerAuthenticationFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ICreateShopService _createShopService;
        private readonly IGetShopService _getShopService;
        private readonly IRemoveShopService _removeShopService;
        
        public ShopController(ICreateShopService createShopService,
                            IGetShopService getShopService,
                            IRemoveShopService removeShopService)
        {
            _createShopService = createShopService;
            _getShopService = getShopService;
            _removeShopService = removeShopService;
        }

        [HttpPost]
        [Route("createshop")]
        public IActionResult CreateShop(Shop shop)
        {
            int result = _createShopService.CreateShop(shop);
            if (result > 0)
            {
                return Ok("Shop has been created");
            }
            return BadRequest("An error occurred while inserting: " + JsonSerializer.Serialize(shop));
        }

        [HttpGet]
        [Route("getshops")]
        public IActionResult GetShops()
        {
            return Ok(_getShopService.GetShops());
        }

        [HttpPost]
        [Route("removeshop")]
        public IActionResult RemoveShop(string name)
        {
            return _removeShopService.RemoveShopByName(name) ? 
                Ok("The shop has been removed successfully") : 
                NotFound(name);
        }
    }
}