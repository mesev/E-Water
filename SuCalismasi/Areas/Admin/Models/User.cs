using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Areas.Admin.Water.Models
{
    public class User
    {
        public short UserId { get; set; }
        [Required(ErrorMessage ="Hatalı Username")]
        [Column(TypeName ="nchar(50)")]
        [MinLength(2)]
        public string UserName { get; set; }
        [Required,Column(TypeName ="char(64)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Column(TypeName = "char(100)")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public byte Authorization { get; set; }

        [Required]
        public short BranchId { get; set; }
        public Branch? Branch { get; set; }


    }
}
