using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Findgroup_Backend.Tests
{
    public class TestPostController(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient = factory.CreateClient();
        private const string API_URL = "/api/Post";

        [Fact]
        public async Task Get_Returns_Posts()
        {
            var response = await _httpClient.GetAsync(API_URL);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Post_Create_New_Post()
        {
            // TODO: place auth tokens here 
            // Create new post
            // Check if return code is successful
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        // TODO: Create these tests
        // Modify Post
        // Delete Post

            
    }
}
