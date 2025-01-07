using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace Findgroup_Backend.Tests;
public class TestAuthController(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task Post_Returns_AuthenticationToken()
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/Login", new
        {
            Username = "janedoe",
            Password = "Passwd123$"
        });
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        Assert.NotNull(responseData);
        Assert.Contains("Token", responseData);
    }
}
