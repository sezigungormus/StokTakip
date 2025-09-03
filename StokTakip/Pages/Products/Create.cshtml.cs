using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;          
using StokTakip.Data;
using StokTakip.Models;


namespace StokTakip.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;   

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger) 
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // GET: Formu göster
        public IActionResult OnGet() => Page();

        // POST : ürün kaydetme
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Ürün başarıyla eklendi.";
                return RedirectToPage("./Index");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Ürün eklenirken beklenmeyen bir hata.");
                ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu. Lütfen tekrar deneyin.");
                return Page();
            }
        }
    }
}
