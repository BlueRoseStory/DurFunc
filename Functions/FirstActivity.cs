using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurFunc
{
    public class FirstActivity
    {
        [FunctionName("FirstActivity")]
        public static async Task<WorkflowResponse> FirstActivityAsync([ActivityTrigger] WorkflowRequest request, ILogger log)
        {
            WorkflowResponse retVal = new WorkflowResponse(request);

            log.LogInformation($"FirstActivity, waiting {request.WaitSeconds} seconds...");

            await Task.Delay(request.WaitSeconds * 1000);

            retVal.WaitSeconds = retVal.WaitSeconds + 1;

            return retVal;
        }
    }
}
