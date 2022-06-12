using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Water.Models
{
    public class Product
    {
        public short ProductId { get; set; }
        [Required]
        public float Volume { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public short BrandId { get; set; }
        [Required]
        public short MaterialId { get; set; }
        public Brand? Brand { get; set; }    
        public Material? Material { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

    }
}
