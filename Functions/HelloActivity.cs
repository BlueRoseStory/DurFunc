using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DurFunc
{
    public class HelloActivity
    {
        [FunctionName("HelloActivity")]
        public static string HelloActivityAsync([ActivityTrigger] string name,
            [DurableClient] IDurableEntityClient client,
            ILogger log)
        {

            var entityId = new EntityId(nameof(CampaignEntity), "1");
            client.SignalEntityAsync(entityId, "Add", 1);

            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}
