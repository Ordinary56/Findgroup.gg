using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WPF.MVVM.Model.DTOs.Input;
using WPF.MVVM.Model.DTOs.Output;
using WPF.Repositories.Interfaces;

namespace WPF.Repositories
{
    internal class UserRepository : IUserRepostory
    {
        private readonly HttpClient _client;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(HttpClient client, ILogger<UserRepository> logger)
        {
            _client = client;
            _logger = logger;
            // Állítsd be a base URL-t itt
            _client.BaseAddress = new Uri("http://localhost:5110/"); // Itt a helyes URL
        }

        public async IAsyncEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            try
            {
                var response = await _client.GetAsync("users");
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                users = await JsonSerializer.DeserializeAsync<List<UserDTO>>(stream) ?? new List<UserDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load users");
                // Továbbra is üres listát használunk, ha hibát kaptunk
            }

            // Az iterálás itt történik, kívül a try-catch blokkon
            foreach (var user in users)
            {
                yield return user;
            }
        }


        public async Task DeleteUser(UserDTO user)
        {
            try
            {
                var response = await _client.DeleteAsync($"users/{user.Id}"); // Hívja a törlés végpontját
                response.EnsureSuccessStatusCode(); // Ellenőrizd, hogy sikerült a törlés
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete user");
                throw;
            }
        }

        public async Task ModifyUser(RegisterNewUserDTO modifiedUser)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"users/{modifiedUser.Id}", modifiedUser);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to modify user");
                throw;
            }
        }

        public async Task CreateNew(RegisterNewUserDTO newUser)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("users", newUser); // Új felhasználó létrehozása
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create new user");
                throw;
            }
        }
    }
}
