using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokTakip.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ürün adı 2–100 karakter olmalıdır.")]
        public string Name { get; set; }

        [Display(Name = "Stok")]
        [Required(ErrorMessage = "Stok sayısı zorunludur.")]
        [Range(0, 1000000, ErrorMessage = "Stok 0 veya daha büyük olmalıdır.")]
        public int Stock { get; set; }

        [Display(Name = "Kritik Seviye")]
        [Required(ErrorMessage = "Kritik seviye sayısı zorunludur.")]
        [Range(0, 1000000, ErrorMessage = "Kritik seviye 0 veya daha büyük olmalıdır.")]
        public int CriticalLevel { get; set; }

        [NotMapped]
        public bool IsCritical => Stock <= CriticalLevel;
    }
}
