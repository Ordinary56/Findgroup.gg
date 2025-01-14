using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WPF.Extensions;
using WPF.MVVM.Model;

namespace WPF.Services
{
    public interface IAuthenticationService
    {
        // Logs in the user
        public Task Authenticate(User user);
        // Updates the User's refresh token
        public Task Refresh(User user);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(HttpClient httpClient, ILogger<AuthenticationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task Authenticate(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress,user);
            try
            {
                response.EnsureSuccessStatusCode();
                // TODO: Make this method more efficient
                var content = await JsonSerializer.DeserializeAsync<Dictionary<string,string>>(response.Content.ReadAsStream());
                user.AuthenticationToken = content["token"];
                user.RefreshToken = content["refresh"];
            }
            catch (HttpRequestException ex) 
            {
                _logger.LogError("There was exception during login: {Reason}", ex.Message);
            }

        }

        public Task Refresh(User user)
        {
            throw new NotImplementedException();
        }
    }
}
