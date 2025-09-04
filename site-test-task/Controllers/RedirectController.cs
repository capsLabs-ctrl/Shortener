using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using site_test_task.Data; // твой AppDbContext
[Route("/{shortCode}")]
public class RedirectController : Controller
{
    private readonly AppDbContext _db;

    public RedirectController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> RedirectToOriginal(string shortCode)
    {
        if (string.IsNullOrEmpty(shortCode))
            return NotFound();

        var url = await _db.ShortUrls.FirstOrDefaultAsync(u => u.ShortCode == shortCode);

        if (url == null)
            return NotFound();

        url.ClickCount++;
        await _db.SaveChangesAsync();
        return Redirect(url.OriginalUrl); // Перенаправляем на оригинальный URL

        return Redirect(url.OriginalUrl);
    }
}
