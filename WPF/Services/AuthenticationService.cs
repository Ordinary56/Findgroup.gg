using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WPF.Extensions;
using WPF.Helpers;
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
    public class AuthenticationService(HttpClient httpClient, ILogger<AuthenticationService> logger, IStorageHelper storage, IConfiguration config) : IAuthenticationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILogger<AuthenticationService> _logger = logger;
        private readonly IStorageHelper _storage = storage;
        private readonly IConfiguration _config = config;
        private Uri AuthUri = new(config["ApiUrl"] + "/Auth");
        private Uri RefreshUri = new(config["ApiUrl"] + "/refresh");
        public async Task<bool> Authenticate(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(AuthUri,user);
            try
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var tokens = await JsonSerializer.DeserializeAsync<(string token, string validTo, string refreshToken)>(stream);
                user.AuthenticationToken = tokens.token;
                user.RefreshToken = tokens.refreshToken;
                _storage.SaveData("saved/data.txt", user.SerializeData());
                return true;
            }

            catch (HttpRequestException ex) 
            {
                _logger.LogError("There was exception during login: {Reason}", ex.Message);
                return false;
            }

        }

        public async Task Refresh(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(RefreshUri, user);
            try
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var tokens = await JsonSerializer.DeserializeAsync<(string token, string refreshToken)>(stream);
                user.AuthenticationToken = tokens.token;
                user.RefreshToken = tokens.refreshToken;
                _storage.SaveData("saved", user.SerializeData());
            }
            catch { }
           
        }
    }
}
