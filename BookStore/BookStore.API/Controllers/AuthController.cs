using BookStore.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(models.DTO.LoginRequest loginRequest)
        {
            


            var user = await userRepository.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if(user == null)
            {
                return BadRequest("Username or password is incorrect.");
            }

            var token = await tokenHandler.CreateTokenAsync(user);
            return Ok(token);
        }
        
    }
}
