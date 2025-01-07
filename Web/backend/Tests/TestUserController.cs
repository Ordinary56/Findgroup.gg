using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Findgroup_Backend.Tests;

public class TestUserController(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    private const string API_URL = "/api/User";

    [Fact] 
    public async Task Get_Returns_Users()
    {
        var response = await _httpClient.GetAsync(API_URL);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseContent);
    }

    [Fact]
    public async Task Post_Create_New_User()
    {
        var response = await _httpClient.PostAsJsonAsync(API_URL, new {
            Username = "",
            Password = "",
            Email = "",
            Telephone = ""
        });
    }
}
