using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Jobs;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IJobs _jobs;

        public IndexModel(IJobs jobs)
        {
            _jobs = jobs;
        }

        public async Task OnGet()
        {
            await _jobs.IndexProducts();
        }
    }
}