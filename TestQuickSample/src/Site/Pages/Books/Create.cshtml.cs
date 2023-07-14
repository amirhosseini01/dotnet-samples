using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Data;
using Site.Models;
using Site.ViewModels.Books;

namespace Site.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Site.Data.ApplicationDbContext _context;

        public CreateModel(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public VmBookInput Book { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
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
