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
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<ShortUrl> ShortUrls { get; set; } = new List<ShortUrl>();

        public async Task OnGetAsync()
        {
            ShortUrls = await _context.ShortUrls
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var entity = await _context.ShortUrls.FindAsync(id);
            if (entity != null)
            {
                _context.ShortUrls.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
