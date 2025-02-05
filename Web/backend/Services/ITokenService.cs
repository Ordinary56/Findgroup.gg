using Findgroup_Backend.Models;

namespace Findgroup_Backend.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate Access token for the given user
        /// </summary>
        /// <param name="user">The user that requests an access token</param>
        /// <returns>The token in string form</returns>
        /// <exception cref="InvalidOperationException">If the jwt secret is not found</exception>
        public Task<string> GenerateAccessToken(User user);
        /// <summary>
        /// Generate a refresh token
        /// </summary>
        /// <returns>A refresh token</returns>
        public RefreshToken GenerateRefreshToken();

    }
}
