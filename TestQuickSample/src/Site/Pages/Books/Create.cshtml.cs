using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Data;
using Site.Helpers.Identity;
using Site.Models;
using Site.ViewModels.Books;

namespace Site.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _context;

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGet()
        {
            var authorizationResult =
                await _authorizationService.AuthorizeUser(user: User, policyName: nameof(CreateModel));
            if (!authorizationResult.Succeeded) return AuthorizationHelper.ReturnForbiddenResult();

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

       

        [BindProperty] public VmBookInput Book { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            var authorizationResult =
                await _authorizationService.AuthorizeUser(user: User, policyName: nameof(CreateModel));
            if (!authorizationResult.Succeeded) return AuthorizationHelper.ReturnForbiddenResult();
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entity = new Book()
            {
                UserId = Book.UserId,
                Title = Book.Title,
                ReleaseDate = Book.ReleaseDate,
            };

            _context.Books.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}