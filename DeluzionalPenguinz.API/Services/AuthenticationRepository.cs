using DeluzionalPenguinz.API.Services.IServices;
using DeluzionalPenguinz.DataAccess;
using DeluzionalPenguinz.DataAccess.Models;
using DeluzionalPenguinz.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.API.Services
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext appDb;
        private readonly UserManager<Human> userManager;
        private readonly SignInManager<Human> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UrlEncoder urlEncoder;

        public AuthenticationRepository(ApplicationDbContext appDb,
                                UserManager<Human> userManager,
                                SignInManager<Human> signInManager,
                                IConfiguration configuration,
                                RoleManager<IdentityRole> roleManager,
                                UrlEncoder urlEncoder
                                )
        {
            this.appDb = appDb;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.urlEncoder = urlEncoder;
            try
            {

                    appDb.Database.Migrate();
            }
            catch (Exception ex)
            {

            }

        }




        public async Task<JwtResponse> Login(string username, string password)
        {
            Human user = await userManager.FindByNameAsync(username.ToLower());

            if (user is null)
                return null;

            //if (user.UserName == "John93562")
            //{
            //    bool IsUserSuperman = await roleManager.RoleExistsAsync("Superman");
            //    if (IsUserSuperman == false)
            //    {

            //        await roleManager.CreateAsync(new IdentityRole("Superman"));
            //        await roleManager.CreateAsync(new IdentityRole("Mpatiris"));


            //        await userManager.AddToRoleAsync(user, "Superman");
            //        await userManager.AddToRoleAsync(user, "Mpatiris");


            //        await userManager.AddClaimAsync(user, new Claim("CanEdit", "YesCanedit"));
            //        await userManager.AddClaimAsync(user, new Claim("CanSuckMyDick", "YesCanSuckMyDick"));
            //    }
            //}

            var result = await signInManager.PasswordSignInAsync(user, password, false, true);

            JwtResponse response = new();

            if (result.RequiresTwoFactor)
            {
                response.UserEmail = user.Email;
            }
            else if (result.Succeeded == false || result.IsLockedOut)
                return null;
            else if (result.Succeeded)
                response.Token = await CreateToken(user);

            return response;
        }
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<UserRegister> Register(UserRegister user)
        {
            if (await UserExists(user.Username.ToLower()))
            {
                return null;
            }
            Human identityUser = new Human(user.FirstName, user.LastName, user.AM, user.Username.ToLower(), user.HumanType, new List<Anouncement>()) ;

            var res = await userManager.CreateAsync(identityUser, user.Password);
            
            await appDb.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await appDb.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
        }



        async Task<string> CreateToken(Human user)
        {
            List<Claim> allClaims = await GetClaims(user);
          
          

            var SecretToken = configuration.GetSection("AppSettings")["Token"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretToken));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                                claims: allClaims,
                                expires: DateTime.UtcNow.AddDays(1),
                                signingCredentials: creds
                                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
            //var tokenHandler = new JwtSecurityTokenHandler();

            //var tokenDescriptor = new SecurityTokenDescriptor()
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    SigningCredentials = creds
            //};


            //var token = tokenHandler.
            //        CreateToken(tokenDescriptor);


            //return tokenHandler.WriteToken(token);

        }

        private async Task<List<Claim>> GetClaims(Human applicationUser)
        {

            List<Claim> allClaims = new List<Claim>();

            if (applicationUser.UserName is not null)
                allClaims.Add(new Claim(ClaimTypes.Name, applicationUser.UserName.ToLower()));


            if (applicationUser.Email is not null)
                allClaims.Add(new Claim(ClaimTypes.Email, applicationUser.Email));


            if (applicationUser.Id is not null)
                allClaims.Add(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id));

       

            var user = await userManager.FindByNameAsync(applicationUser.UserName.ToLower());

            var allClaimsOfUser = await userManager.GetClaimsAsync(user);

            foreach (var claim in allClaimsOfUser)
            {
                allClaims.Add(claim);
            }

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                allClaims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }



            return allClaims;
        }




    }
}
