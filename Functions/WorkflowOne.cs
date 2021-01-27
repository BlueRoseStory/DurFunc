using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DurFunc.Functions
{
    public class WorkflowOne
    {
        [FunctionName("WorkflowOneHttpStart")]
        public static async Task<HttpResponseMessage> WorkflowOneHttpStartAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            WorkflowRequest request = await req.Content.ReadAsAsync<WorkflowRequest>();

            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("WorkflowOneOrchestrator", request);

            log.LogInformation($"Started orchestration with ID = '{instanceId}', requestWait: {request.WaitSeconds}.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        [FunctionName("WorkflowOneOrchestrator")]
        public static async Task<List<string>> WorkflowOneOrchestratorAsync(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            WorkflowRequest request = context.GetInput<WorkflowRequest>();


            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("HelloActivity", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("HelloActivity", "Seattle"));

            outputs.Add((await context.CallActivityAsync<WorkflowResponse>("FirstActivity", request)).ToString());

            outputs.Add(await context.CallActivityAsync<string>("HelloActivity", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}
