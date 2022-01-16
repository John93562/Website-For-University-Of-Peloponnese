using DeluzionalPenguinz.Models;
using DeluzionalPenguinz.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.UOP.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<UserRegister> Register(UserRegister user);
        Task<JwtResponse> Login(UserLogin user);


    }
}
