using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters;

public class SecondAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine($"{nameof(SecondAttribute)} {nameof(OnResultExecuting)}");

        base.OnResultExecuting(context);
    }
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine($"{nameof(SecondAttribute)} {nameof(OnResultExecuted)}");

        base.OnResultExecuted(context);
    }
}