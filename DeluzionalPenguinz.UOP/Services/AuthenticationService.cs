using Blazored.LocalStorage;
using DeluzionalPenguinz.Models.Identity;
using DeluzionalPenguinz.UOP.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeluzionalPenguinz.UOP.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authStateProvider;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService LocalStorage, AuthenticationStateProvider AuthStateProvider)
        {
            this.httpClient = httpClient;
            localStorage = LocalStorage;
            authStateProvider = AuthStateProvider;
        }
        public async Task<JwtResponse> Login(UserLogin user)
        {
            var result = await httpClient.PostAsJsonAsync("api/authentication/login", user);

            JwtResponse status = null;

            if (result.IsSuccessStatusCode)
            {
                status = await result.Content.ReadFromJsonAsync<JwtResponse>();
                await localStorage.SetItemAsync("Token", status.Token);
                await authStateProvider.GetAuthenticationStateAsync();
            }

            return status;
        }

        public async Task<UserRegister> Register(UserRegister user)
        {

            var content = JsonConvert.SerializeObject(user);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"api/authentication/register", bodyContent);

            UserRegister status = null;

            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();

                status = JsonConvert.DeserializeObject<UserRegister>(contentResult);
            }

            return status;
        }



    }
}
