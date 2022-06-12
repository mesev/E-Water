using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Water.Models
{
    public class Material
    {
        public short MaterialId { get; set; }
        [Required]
        [Column(TypeName ="nchar(50)")]
        [MinLength(2)]
        public string MaterialName { get; set; }

    }
}
