using Microsoft.Build.Framework;

namespace DiskApp.Models
{
    public class DiskModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Quantiy { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
