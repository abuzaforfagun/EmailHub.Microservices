using System;
using System.Threading.Tasks;

namespace Common.Domain
{
    public interface IRetryHelper
    {
        Task InvokeAsync(Func<int, Task> action);
    }
}
