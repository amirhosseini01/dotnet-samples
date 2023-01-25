using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters;

public class ShortCircuitingResourceFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine($"{nameof(ShortCircuitingResourceFilterAttribute)} {nameof(OnResultExecuting)}");

        context.Result = new ContentResult
        {
            Content = nameof(ShortCircuitingResourceFilterAttribute)
        };

        base.OnResultExecuting(context);
    }
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine($"{nameof(ShortCircuitingResourceFilterAttribute)} {nameof(OnResultExecuted)}");

        base.OnResultExecuted(context);
    }
}