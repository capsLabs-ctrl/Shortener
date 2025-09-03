using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using site_test_task.Data;
using site_test_task.Models;
using site_test_task.Services;

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
        public UrlShortenerService shortener = new UrlShortenerService();

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("ModelState.IsValid = " + ModelState.IsValid);
            Console.WriteLine("OriginalUrl = " + ShortUrl?.OriginalUrl);
            if (!Uri.TryCreate(ShortUrl.OriginalUrl, UriKind.Absolute, out var uriResult)
                || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                ModelState.AddModelError("ShortUrl.OriginalUrl", "Введите корректный URL (http или https).");
                return Page();
            }


            ShortUrl.ClickCount = 0;
            ShortUrl.CreatedAt = DateTime.Now;
            ShortUrl.ShortCode = shortener.GenerateShortCode();
            _context.ShortUrls.Add(ShortUrl);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
