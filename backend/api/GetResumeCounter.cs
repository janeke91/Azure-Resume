using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class ResumeCounter
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = "1";

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }

    public class GetResumeCounter
    {
        private readonly ILogger<GetResumeCounter> _logger;
        private static CosmosClient _cosmosClient = new CosmosClient(
            Environment.GetEnvironmentVariable("AzureResumeConnectionString"));

        public GetResumeCounter(ILogger<GetResumeCounter> logger)
        {
            _logger = logger;
        }

        [Function("GetResumeCounter")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var container = _cosmosClient.GetContainer("AzureResume", "Counter");

            var response = await container.ReadItemAsync<ResumeCounter>("1", new PartitionKey("1"));
            var counter = response.Resource;

            counter.Count += 1;

            await container.ReplaceItemAsync(counter, "1", new PartitionKey("1"));

            return new OkObjectResult(counter.Count);
        }
    }
}

