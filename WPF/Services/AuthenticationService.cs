using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WPF.Extensions;
using WPF.Helpers;
using WPF.MVVM.Model;
using WPF.Services.Interfaces;

namespace WPF.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly HttpClient _client;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IStorageHelper _storage;
        #endregion

        public AuthenticationService( HttpClient client, 
            ILogger<AuthenticationService> logger, 
            IStorageHelper helper)
        {
            _client = client;
            _logger = logger;
            _storage = helper;
        }
        public async Task<bool> Authenticate(AdminUser user)
        {
            var credentials = new
            {
                Username = user.UserName,
                Password = user.Password
            };
            var json = JsonSerializer.Serialize(credentials);
            _logger.LogInformation("Sending login request: {Json}", json);
            _logger.LogInformation("Requesting URL: {Url}", new Uri(_client.BaseAddress, "Auth/login"));

            using var response = await _client.PostAsJsonAsync(_client.BaseAddress + "/Auth/login", credentials);
            _logger.LogInformation("Attempting to login admin with {UserName} {Password}", user.UserName, user.Password);
            try
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return true;
            }

            catch (HttpRequestException ex) 
            {
                _logger.LogError("There was exception during login: {Reason}", ex.Message);
                return false;
            }

        }
    }
}
