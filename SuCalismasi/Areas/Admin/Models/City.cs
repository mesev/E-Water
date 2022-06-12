using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Areas.Admin.Water.Models
{
    public class City
    {
        public byte CityId { get; set; }
        [Required] 
        [Column(TypeName ="nchar(20)")]
        public string CityName { get; set; }


    }
}
