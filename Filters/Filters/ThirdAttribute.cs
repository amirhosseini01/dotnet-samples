using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters;

public class ThirdAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine($"{nameof(ThirdAttribute)} {nameof(OnResultExecuting)}");

        base.OnResultExecuting(context);
    }
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine($"{nameof(ThirdAttribute)} {nameof(OnResultExecuted)}");

        base.OnResultExecuted(context);
    }
}