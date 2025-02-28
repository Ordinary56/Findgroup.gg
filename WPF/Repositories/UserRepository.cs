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
using WPF.MVVM.Model.DTOs.Input;
using WPF.MVVM.Model.DTOs.Output;

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


        public async IAsyncEnumerable<UserDTO> GetUsers()
        {
            var result = await _client.GetAsync(_client.BaseAddress);
            result.EnsureSuccessStatusCode();
            var stream = await result.Content.ReadAsStreamAsync();
            IAsyncEnumerable<UserDTO> values = await JsonSerializer.DeserializeAsync<IAsyncEnumerable<UserDTO>>(stream) ?? throw new Exception("the stream returned null");
            await foreach (UserDTO user in values) yield return user;
        }

        public async Task DeleteUser(UserDTO user)
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

        public async Task ModifyUser(RegisterNewUserDTO modifiedUser)
        {
            try
            {
                using HttpRequestMessage message = new(HttpMethod.Get, _client.BaseAddress)
                {
                    Content = JsonContent.Create(new
                    {
                        Username = modifiedUser.UserName,
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

        public async Task CreateNew(RegisterNewUserDTO newUser)
        {
            using HttpRequestMessage message = new(HttpMethod.Get, _client.BaseAddress)
            {
                Content = JsonContent.Create(new
                {
                    newUser.UserName,
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
