using Crud.Example.Api.Filters;
using Crud.Example.Domain.Entities;
using Crud.Example.Main.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Example.Api.Controllers
{
    [ServiceFilter(typeof(BearerAuthenticationFilterAttribute))]
    [Route("api/Dealer")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        #region Private Members
        private readonly IGetDealerService _getDealerService;
        private readonly ICreateDealerService _createDealerService;
        #endregion

        #region Constructor
        public DealerController(IGetDealerService getDealerService,
                                ICreateDealerService createDealerService)
        {
            _getDealerService = getDealerService;
            _createDealerService = createDealerService;   
        }
        #endregion

        #region Public Method Controller
        [HttpGet]
        [Route("getdealers")]
        public IActionResult GetDealers()
        {
            return Ok(_getDealerService.GetAllDealer());
        }

        [HttpPost]
        [Route("createdealer")]
        public IActionResult CreateDealer(Dealer dealer) 
        {
            _createDealerService.CreateDealer(dealer);
            return Ok("Dealer has been created");
        }
        #endregion
    }
}
