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
    public class DetailsModel : PageModel
    {
        private readonly site_test_task.Data.AppDbContext _context;

        public DetailsModel(site_test_task.Data.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
