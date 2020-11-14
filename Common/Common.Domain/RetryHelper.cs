using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Polly;

namespace Common.Domain
{
    public class RetryHelper : IRetryHelper
    {
        public async Task InvokeAsync(Func<int, Task> action)
        {
            var count = 0;
            var fallbackPolicy = Policy.Handle<Exception>()
                .FallbackAsync(async ct => await Task.Run(() =>
                {
                    count++;
                    return action(count);
                }));

            await fallbackPolicy.ExecuteAsync(() => action(count));
        }
    }
}
