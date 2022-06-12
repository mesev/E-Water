using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Areas.Admin.Water.Models
{
    public class Branch
    {
        public short BranchId { get; set; }
        [Required]
        [Column(TypeName ="nchar(50)")]
        public string BranchName { get; set; }
        [Required]
        public byte CityId { get; set; }
        
        public City? City { get; set; }
    }
}
