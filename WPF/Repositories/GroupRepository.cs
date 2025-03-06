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
using WPF.Repositories.Interfaces;

namespace WPF.Repositories
{
    internal class GroupRepository : IGroupRepository
    {

        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private bool _disposed = false;
        public GroupRepository(HttpClient client, ILogger<GroupRepository> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async IAsyncEnumerable<GroupDTO> GetGroups()
        {
            var result = await _client.GetAsync(_client.BaseAddress);
            result.EnsureSuccessStatusCode();
            var stream = await result.Content.ReadAsStreamAsync();
            IAsyncEnumerable<GroupDTO> values = await JsonSerializer.DeserializeAsync<IAsyncEnumerable<GroupDTO>>(stream) ?? throw new Exception("the stream returned null");
            await foreach (GroupDTO group in values) yield return group;
        }

        public async Task DeleteGroup(GroupDTO group)
        {
            try
            {
                await _client.DeleteAsync(_client.BaseAddress + $"/{group.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(DeleteGroup), ex.Message);
                return;
            }
        }

        public async Task ModifyGroup(CreateNewGroupDTO modifiedGroup)
        {
            try
            {
                using HttpRequestMessage message = new(HttpMethod.Get, _client.BaseAddress)
                {
                    Content = JsonContent.Create(new
                    {
                        GroupName = modifiedGroup.GroupName,
                        Description = modifiedGroup.Description,
                        MemberLimit = modifiedGroup.MemberLimit,
                        PostId = modifiedGroup.PostId,
                        UserId = modifiedGroup.UserId,
                    })
                };
                await _client.SendAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(modifiedGroup), ex.Message);
                throw;

            }
        }
    }
}
