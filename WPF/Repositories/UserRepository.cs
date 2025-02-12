using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF.MVVM.Model;

namespace WPF.Repositories
{
    internal class UserRepository : IUserRepostory
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private bool _disposed = false;
        public UserRepository(HttpClient client, ILogger<UserRepository> logger)
        {
            _client = client;
            _logger = logger;
        }


        public async IAsyncEnumerable<User> GetUsers()
        {
            var result = await _client.GetAsync(_client.BaseAddress);
            result.EnsureSuccessStatusCode();
            var stream = await result.Content.ReadAsStreamAsync();
            IAsyncEnumerable<User> values = await JsonSerializer.DeserializeAsync<IAsyncEnumerable<User>>(stream) ?? throw new Exception("the stream returned null");
            await foreach (User user in values) yield return user;

        }

        public async Task DeleteUser(User user)
        {
            try
            {
                await _client.DeleteAsync(_client.BaseAddress + $"/{user.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(DeleteUser), ex.Message);
                return;
            }
        }

        public async Task ModifyUser(User modifiedUser)
        {
            try
            {
                using HttpRequestMessage message = new(HttpMethod.Get, _client.BaseAddress)
                {
                    Content = JsonContent.Create(new
                    {
                        Username = modifiedUser.Username,
                        Password = modifiedUser.Password,
                        Email = modifiedUser.Email
                    })
                };
                await _client.SendAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(ModifyUser), ex.Message);
                throw;

            }
        }

        public async Task CreateNew(User newUser)
        {
            using HttpRequestMessage message = new(HttpMethod.Get, _client.BaseAddress)
            {
                Content = JsonContent.Create(new
                {
                    newUser.Username,
                    newUser.Password,
                    newUser.Email
                })
            };
            try
            {
                await _client.SendAsync(message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(CreateNew), ex.Message);
                return;
            }
        }
    }
}
