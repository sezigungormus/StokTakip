using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StokTakip.Models;



namespace StokTakip.Pages.Products
{
    [Authorize(Roles = "Admin,Viewer")]
    public class IndexModel : PageModel
    {
        private readonly StokTakip.Data.ApplicationDbContext _context;

        public IndexModel(StokTakip.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;
        public int CriticalCount { get; set; }
        //ürün filtreleme  ve kritik sayısını belirleme kodu
        public async Task OnGetAsync(string? search, bool? onlyCritical)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string term = search.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(term));
            }

            if (onlyCritical == true)
            {
                query = query.Where(p => p.Stock <= p.CriticalLevel);
            }

            Products = await query.ToListAsync();

            CriticalCount = Products.Count(p => p.IsCritical);
            CheckCriticalProducts(); 
        }

        private void CheckCriticalProducts()
        {
            var criticalProducts = Products.Where(p => p.IsCritical).ToList();
            if (criticalProducts.Any())
            {
                TempData["Alert"] = $"⚠ Kritik stokta {criticalProducts.Count} ürün var!";
                ViewData["CriticalNames"] = string.Join(", ", criticalProducts.Select(p => p.Name));
            }
        }
    }
}
