using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AsyncAwaitBestPractices
{
    public class GitHubService
    {
        private readonly IMemoryCache _cachedGitHubUserInfo = new MemoryCache(new MemoryCacheOptions());
        private static readonly HttpClient HttpClient = new()
        {
            BaseAddress = new Uri("https://api.github.com/"),
        };

        static GitHubService()
        {
            HttpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
            HttpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, $"Medium-Story-{Environment.MachineName}");
        }

        public async Task<GitHubUserInfo?> GetGitHubUserInfoAsyncTask(string username)
        {
            var cacheKey = ("github-", username);

            var gitHubUserInfo = _cachedGitHubUserInfo.Get<GitHubUserInfo>(cacheKey);

            if (gitHubUserInfo is null)
            {
                var response = await HttpClient.GetAsync($"/users/{username}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    gitHubUserInfo = await response.Content.ReadFromJsonAsync<GitHubUserInfo>();

                    _cachedGitHubUserInfo.Set(cacheKey, gitHubUserInfo, TimeSpan.FromHours(1));
                }
            }

            return gitHubUserInfo;
        }

        public async ValueTask<GitHubUserInfo?> GetGitHubUserInfoAsyncValueTask(string username)
        {
            var cacheKey = ("github-", username);

            var gitHubUserInfo = _cachedGitHubUserInfo.Get<GitHubUserInfo>(cacheKey);

            if (gitHubUserInfo is null)
            {
                var response = await HttpClient.GetAsync($"/users/{username}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    gitHubUserInfo = await response.Content.ReadFromJsonAsync<GitHubUserInfo>();

                    _cachedGitHubUserInfo.Set(cacheKey, gitHubUserInfo, TimeSpan.FromHours(1));
                }
            }

            return gitHubUserInfo;
        }
    }

    public record GitHubUserInfo([property: JsonPropertyName("login")] string Username,
        [property: JsonPropertyName("html_url")] string ProfileUrl,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("company")] string Company);
}
