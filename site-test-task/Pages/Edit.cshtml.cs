using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using site_test_task.Data;
using site_test_task.Models;

namespace site_test_task.Pages
{
    public class EditModel : PageModel
    {
        private readonly site_test_task.Data.AppDbContext _context;

        public EditModel(site_test_task.Data.AppDbContext context)
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

            var shorturl =  await _context.ShortUrls.FirstOrDefaultAsync(m => m.Id == id);
            if (shorturl == null)
            {
                return NotFound();
            }
            ShortUrl = shorturl;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ShortUrl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShortUrlExists(ShortUrl.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShortUrlExists(int id)
        {
            return _context.ShortUrls.Any(e => e.Id == id);
        }
    }
}
