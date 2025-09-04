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
using site_test_task.Services;

namespace site_test_task.Pages
{
    public class EditModel : PageModel
    {
        private readonly site_test_task.Data.AppDbContext _context;
        private string alph = UrlShortenerService.alph;
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

            var shorturl = await _context.ShortUrls.FirstOrDefaultAsync(m => m.Id == id);
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
            if (ShortUrl.ShortCode.Length != 8 || !ShortUrl.ShortCode.All(c => alph.Contains(c)))
            {
                ModelState.AddModelError("ShortUrl.ShortCode", "Код должен состоять из 8 символов и содержать только a-z, A-Z, 0-9.");
                return Page();
            }

            bool exists = await _context.ShortUrls
                .AnyAsync(u => u.ShortCode == ShortUrl.ShortCode && u.Id != ShortUrl.Id);

            if (exists)
            {
                ModelState.AddModelError("ShortUrl.ShortCode", "Такой короткий код уже существует.");
                return Page();
            }

            _context.Attach(ShortUrl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ShortUrls.Any(e => e.Id == ShortUrl.Id))
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
    }
}
