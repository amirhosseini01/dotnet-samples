using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters;

public class GlobalSampleActionFilter : IActionFilter
{
     public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"{nameof(GlobalSampleActionFilter)} => {nameof(OnActionExecuting)}");
        // Do something before the action executes.
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"{nameof(GlobalSampleActionFilter)} => {nameof(OnActionExecuted)}");
        // Do something after the action executes.
    }
}