using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace AsyncAwaitBestPractices
{
    [MemoryDiagnoser]
    public class GitHubServiceBenchmarks
    {
        private static readonly GitHubService GitHubService = new();

        [Benchmark]
        public async Task<GitHubUserInfo?> GetGitHubUserInfoAsyncTask()
        {
            return await GitHubService.GetGitHubUserInfoAsyncTask("ormikopo1988");
        }

        [Benchmark]
        public async Task<GitHubUserInfo?> GetGitHubUserInfoAsyncValueTask()
        {
            return await GitHubService.GetGitHubUserInfoAsyncValueTask("ormikopo1988");
        }
        
        [Benchmark]
        public async ValueTask<GitHubUserInfo?> GetGitHubUserInfoAsyncValueTaskTimesTwo()
        {
            return await GitHubService.GetGitHubUserInfoAsyncValueTask("ormikopo1988");
        }
    }
}
