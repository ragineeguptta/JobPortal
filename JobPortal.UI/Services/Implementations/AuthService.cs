using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Auth;
using System.Text;
using System.Text.Json;

namespace JobPortal.UI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("API");
        }


        public async Task<string?> LoginAsync(LoginViewModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();

            var tokenObj = JsonSerializer.Deserialize<TokenResponse>(result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return tokenObj?.Token;
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}