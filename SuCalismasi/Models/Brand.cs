using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Water.Models
{
    public class Brand
    {
        public short BrandId { get; set; }
        [Required]
        [Column(TypeName ="nchar(50)")]
        [MinLength(2)]
        public string? BrandName { get; set; }
        [Required]
        public float PH { get; set; }
        [Required]
        public float Hardness { get; set; } 
        public string? Description { get; set; }


    }
}
