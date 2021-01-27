using System;
using System.Collections.Generic;
using System.Text;

namespace DurFunc
{
    public class WorkflowResponse : WorkflowRequest
    {
        public WorkflowResponse()
        { }

        public WorkflowResponse(WorkflowRequest request)
        {
            if (request != null)
            {
                this.WaitSeconds = request.WaitSeconds;
            }
        }

        public override string ToString()
        {
            return $"WaitSeconds={this.WaitSeconds}";
        }
    }
}
