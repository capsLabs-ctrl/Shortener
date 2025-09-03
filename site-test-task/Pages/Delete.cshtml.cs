using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using site_test_task.Data;
using site_test_task.Models;

namespace site_test_task.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly site_test_task.Data.AppDbContext _context;

        public DeleteModel(site_test_task.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShortUrl ShortUrl { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shorturl = await _context.ShortUrls.FirstOrDefaultAsync(m => m.Id == id);

            if (shorturl == null)
            {
                return NotFound();
            }
            else
            {
                ShortUrl = shorturl;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shorturl = await _context.ShortUrls.FindAsync(id);
            if (shorturl != null)
            {
                ShortUrl = shorturl;
                _context.ShortUrls.Remove(ShortUrl);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
