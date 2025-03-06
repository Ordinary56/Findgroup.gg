
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
    internal class PostRepository : IPostRepository
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        private bool _disposed = false;
        public PostRepository(HttpClient client, ILogger<PostRepository> logger)
        {
            _client = client;
            _logger = logger;
        }


        public async IAsyncEnumerable<PostDTO>GetPost ()
        {
            var result = await _client.GetAsync(_client.BaseAddress);
            result.EnsureSuccessStatusCode();
            var stream = await result.Content.ReadAsStreamAsync();
            IAsyncEnumerable<PostDTO> values = await JsonSerializer.DeserializeAsync<IAsyncEnumerable<PostDTO>>(stream) ?? throw new Exception("the stream returned null");
            await foreach (PostDTO post in values) yield return post;
        }

        public async Task DeletePost(PostDTO post)
        {
            try
            {
                await _client.DeleteAsync(_client.BaseAddress + $"/{post.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(DeletePost), ex.Message);
                return;
            }
        }



     
        public async Task ModifyPost(RegisterNewPostDTO modifiedPost)
        {
            try
            {
                using HttpRequestMessage message = new(HttpMethod.Get, _client.BaseAddress)
                {
                    Content = JsonContent.Create(new
                    {
                        Title = modifiedPost.Title,
                        Content = modifiedPost.Content,
                        UserId = modifiedPost.UserId,
                        CategoryId = modifiedPost.CategoryId,
                    })
                };
                await _client.SendAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send request in {MethodName}. Error Message: {Reason}", nameof(modifiedPost), ex.Message);
                throw;

            }
        }
    }
}
