using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using site_test_task.Data;
using site_test_task.Models;

namespace site_test_task.Pages
{
    public class CreateModel : PageModel
    {
        private readonly site_test_task.Data.AppDbContext _context;

        public CreateModel(site_test_task.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ShortUrl ShortUrl { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShortUrls.Add(ShortUrl);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
