using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StokTakip.Data;
using StokTakip.Models;
using Microsoft.AspNetCore.Authorization;


namespace StokTakip.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public Product Product { get; set; } = default!;

        // GET: Düzenlenecek ürünü getir
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FindAsync(id);
            if (Product == null) return NotFound();
            return Page();
        }
        // POST: Değişiklikleri kaydet
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // alan hatalarını sayfada göster
                return Page();
            }

            // Var olan kaydı update et
            var entity = await _context.Products.FindAsync(Product.Id);
            if (entity == null) return NotFound();

            entity.Name = Product.Name;
            entity.Stock = Product.Stock;
            entity.CriticalLevel = Product.CriticalLevel;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Ürün güncellendi.";
            return RedirectToPage("./Index");
        }
    }
}