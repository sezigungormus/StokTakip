using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StokTakip.Data;
using StokTakip.Models;


namespace StokTakip.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // GET: Formu doldurmak için ürünü getirir
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
        // POST: Değişiklikleri kaydet
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var entity = await _context.Products.FindAsync(Product.Id);
                if (entity == null)
                {
                    TempData["Error"] = "Ürün bulunamadı.";
                    return RedirectToPage("./Index");
                }

                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Ürün silindi.";
                return RedirectToPage("./Index");
            }
            catch {  
                TempData["Error"] = "Silme sırasında bir hata oluştu.";
                return RedirectToPage("./Index");
            }
        }
    }
}
