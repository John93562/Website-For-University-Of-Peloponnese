using DeluzionalPenguinz.API.Services;
using DeluzionalPenguinz.API.Services.IServices;
using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
       
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            var response = await authenticationRepository.Register(userRegister);

            return response is null ? BadRequest() : Ok(response);
        }
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var response = await authenticationRepository.Login(
                userLogin.Username, userLogin.Password);



            return response is null ? BadRequest() : Ok(response);
        }

       

    }
}
