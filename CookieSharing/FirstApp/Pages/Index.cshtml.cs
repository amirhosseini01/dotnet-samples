using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        //todo: Read the cookie name from const string variable
        //todo: use DataProtection for cookie value
        Response.Cookies.Append(".AspNet.SharedCookie", "hello_world");
    }
}
