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
        public Task<bool> Authenticate(User user);
        // Updates the User's refresh token
        public Task Refresh(User user);
    }
    public class AuthenticationService(HttpClient httpClient, ILogger<AuthenticationService> logger) : IAuthenticationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<AuthenticationService> _logger = logger;

        public async Task<bool> Authenticate(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress,user);
            try
            {
                response.EnsureSuccessStatusCode();
                // TODO: Make this method more efficient
                string content = await response.Content.ReadAsStringAsync();
                JsonDocument doc = JsonDocument.Parse(content);
                JsonElement token = doc.RootElement.GetProperty("token");
                JsonElement refresh = doc.RootElement.GetProperty("refresh");
                user.AuthenticationToken = token.ToString();
                user.RefreshToken = refresh.ToString();
                return true;
            }

            catch (HttpRequestException ex) 
            {
                _logger.LogError("There was exception during login: {Reason}", ex.Message);
                return false;
            }

        }

        public Task Refresh(User user)
        {
            throw new NotImplementedException();
        }
    }
}
