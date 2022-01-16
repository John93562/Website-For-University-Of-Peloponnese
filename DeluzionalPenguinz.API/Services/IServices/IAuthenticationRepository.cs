using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.API.Services.IServices
{
    public interface IAuthenticationRepository
    {
        Task<UserRegister> Register(UserRegister user);
        Task<JwtResponse> Login(string username, string password);
      
        Task<bool> UserExists(string email);
       
    }
}
