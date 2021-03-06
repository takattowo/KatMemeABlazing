using KatMemeABlazing.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KatMemeABlazing.Client.Actions
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            KatUser currentUser = await _httpClient.GetFromJsonAsync<KatUser>("katuser/getcurrentuser");
            if (currentUser != null && currentUser.Email != null)
            {
                var claimEmail = new Claim(ClaimTypes.Name, currentUser.Email);
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.Id));
                var claimsIndentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier }, "serverAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIndentity);

                return new AuthenticationState(claimsPrincipal);
            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public CustomAuthentication(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    }
}
