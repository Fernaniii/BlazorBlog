using System;
using System.Collections.Generic;
using System.Text;
using TickerQ.Utilities.Base;

namespace BlazorBlog.Infrastructure.Jobs
{
    public class WelcomeJobs
    {
        [TickerFunction("send-welcome")]
        public async Task SendWelcome(
       TickerFunctionContext context,
       CancellationToken cancellationToken)
        {
            Console.WriteLine($"Job {context.Id} executed");
        }
    }
}
