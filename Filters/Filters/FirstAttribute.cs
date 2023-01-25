using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters;

public class FirstAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine($"{nameof(FirstAttribute)} {nameof(OnResultExecuting)}");

        base.OnResultExecuting(context);
    }
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine($"{nameof(FirstAttribute)} {nameof(OnResultExecuted)}");

        base.OnResultExecuted(context);
    }
}