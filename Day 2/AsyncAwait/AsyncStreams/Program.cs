using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit;

namespace AsyncStreamsNetwork
{
    public class Program
    {
        private const string PagedIssueQuery =
            @"query ($repo_name: String!,  $start_cursor:String) {
              repository(owner: ""dotnet"", name: $repo_name) {
                issues(last: 25, before: $start_cursor)
                 {
                    totalCount
                    pageInfo {
                      hasPreviousPage
                      startCursor
                    }
                    nodes {
                      title
                      number
                      createdAt
                    }
                  }
                }
              }
            ";

        static async Task Main(string[] args)
        {
            //Follow these steps to create a GitHub Access Token
            // https://help.github.com/articles/creating-a-personal-access-token-for-the-command-line/#creating-a-token
            //Select the following permissions for your GitHub Access Token:
            // - repo:status
            // - public_repo
            var client = new GitHubClient(new ProductHeaderValue("IssueQueryDemo"))
            {
                Credentials = new Credentials("bb860e29d04fc186b67a8e406c726bd6d70b3b68")
            };

            // Without consuming async streams
            // After creating the GitHub client, we create a progress reporting object and a cancellation token. 
            // Once those objects are created, Main calls RunPagedQueryAsync to retrieve the most recent 250 created issues.
            // After that task has finished, the results are displayed.
            var progressReporter = new ProgressStatus((num) =>
            {
                Console.WriteLine($"Received {num} issues in total");
            });

            CancellationTokenSource cancellationSource = new CancellationTokenSource();

            try
            {
                var results = await RunPagedQueryAsync(client, PagedIssueQuery, "docs",
                    cancellationSource.Token, progressReporter);

                foreach (var issue in results)
                {
                    Console.WriteLine(issue);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Work has been cancelled");
            }

            // Consuming async streams
            //int num = 0;

            //await foreach (var issue in RunPagedQueryAsync(client, PagedIssueQuery, "docs"))
            //{
            //    Console.WriteLine(issue);
            //    Console.WriteLine($"Received {++num} issues in total");
            //}
        }

        private static async Task<JArray> RunPagedQueryAsync(GitHubClient client, string queryText, string repoName, CancellationToken cancel, IProgress<int> progress)
        {
            var issueAndPRQuery = new GraphQLRequest
            {
                Query = queryText
            };

            issueAndPRQuery.Variables["repo_name"] = repoName;

            JArray finalResults = new JArray();
            bool hasMorePages = true;
            int pagesReturned = 0;
            int issuesReturned = 0;

            // The RunPagedQueryAsync method enumerates the issues from most recent to oldest. 
            // It requests 25 issues per page and examines the pageInfo structure of the response to continue with the previous page. 
            // That follows GraphQL's standard paging support for multi-page responses. 
            // The response includes a pageInfo object that includes a hasPreviousPages value and a startCursor value used to request the previous page. 
            // The issues are in the nodes array. The RunPagedQueryAsync method appends these nodes to an array that contains all the results from all pages.
            while (hasMorePages && (pagesReturned++ < 10)) // Stop with 10 pages, because these are large repos:
            {
                var postBody = issueAndPRQuery.ToJsonText();

                var response = await client.Connection.Post<string>(new Uri("https://api.github.com/graphql"),
                    postBody, "application/json", "application/json");

                JObject results = JObject.Parse(response.HttpResponse.Body.ToString());

                int totalCount = (int)issues(results)["totalCount"];
                hasMorePages = (bool)pageInfo(results)["hasPreviousPage"];
                issueAndPRQuery.Variables["start_cursor"] = pageInfo(results)["startCursor"].ToString();
                issuesReturned += issues(results)["nodes"].Count();

                finalResults.Merge(issues(results)["nodes"]);

                // After retrieving and restoring a page of results, RunPagedQueryAsync reports progress and checks for cancellation. 
                // If cancellation has been requested, RunPagedQueryAsync throws an OperationCanceledException.
                progress?.Report(issuesReturned);
                cancel.ThrowIfCancellationRequested();
            }

            return finalResults;

            JObject issues(JObject result) => (JObject)result["data"]["repository"]["issues"];
            JObject pageInfo(JObject result) => (JObject)issues(result)["pageInfo"];

            // There are several elements in this code that can be improved. 
            // Most importantly, RunPagedQueryAsync must allocate storage for all the issues returned. 
            // This sample stops at 250 issues because retrieving all open issues would require much more memory to store all the retrieved issues. 
            // The protocols for supporting progress reports and cancellation make the algorithm harder to understand on its first reading. 
            // More types and APIs are involved. 
            // You must trace the communications through the CancellationTokenSource and its associated CancellationToken to understand where cancellation is requested and where it's granted.
        }

        private static async IAsyncEnumerable<JToken> RunPagedQueryAsync(GitHubClient client, string queryText, string repoName)
        {
            var issueAndPRQuery = new GraphQLRequest
            {
                Query = queryText
            };

            issueAndPRQuery.Variables["repo_name"] = repoName;

            bool hasMorePages = true;
            int pagesReturned = 0;
            int issuesReturned = 0;

            // Stop with 10 pages, because these are large repos:
            while (hasMorePages && (pagesReturned++ < 10))
            {

                var postBody = issueAndPRQuery.ToJsonText();
                var response = await client.Connection.Post<string>(new Uri("https://api.github.com/graphql"),
                    postBody, "application/json", "application/json");

                JObject results = JObject.Parse(response.HttpResponse.Body.ToString());

                hasMorePages = (bool)pageInfo(results)["hasPreviousPage"];
                issueAndPRQuery.Variables["start_cursor"] = pageInfo(results)["startCursor"].ToString();
                issuesReturned += issues(results)["nodes"].Count();

                // The first page of results is enumerated as soon as it's available. 
                // There's an observable pause as each new page is requested and retrieved, then the next page's results are quickly enumerated. 
                // The try / catch block isn't needed to handle cancellation: the caller can stop enumerating the collection. 
                // Progress is clearly reported because the async stream generates results as each page is downloaded. 
                // The status for each issue returned is seamlessly included in the await foreach loop. 
                // You don't need a callback object to track progress.
                // You can see improvements in memory use by examining the code. 
                // You no longer need to allocate a collection to store all the results before they're enumerated. 
                // The caller can determine how to consume the results and if a storage collection is needed.
                foreach (JObject issue in issues(results)["nodes"])
                {
                    yield return issue;
                }
            }

            JObject issues(JObject result) => (JObject)result["data"]["repository"]["issues"];
            JObject pageInfo(JObject result) => (JObject)issues(result)["pageInfo"];
        }
    }
}