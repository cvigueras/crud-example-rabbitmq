using Crud.Example.Main.Auth.Interfaces;
using Crud.Example.Main.Auth.Models;
using Crud.Example.Main.ValidatorModels;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Example.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService _iAuthenticationService;

        public AuthenticateController(IAuthService athenticationService)
        {
            _iAuthenticationService = athenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _iAuthenticationService.LoginUser(model);
            if (token != null)
            {
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            UserValidator userValidator = new();
            var validatorResult = userValidator.Validate(model);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            if (model != null && !string.IsNullOrEmpty(model.Username))
            {
                var userExists = await _iAuthenticationService.UserExist(model.Username);
                if (userExists)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
                }

                var result = await _iAuthenticationService.Registeruser(model);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User must have a value!" });
        }
    }
}