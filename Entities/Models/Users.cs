
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Users: Base
    {
        [StringLength(50)]
        public string UserName { get; set; }

        [MinLength(8, ErrorMessage = "En az 8 karakter olmalıdır.")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set;}
        public UserRole UserRole { get; set;}
        public int UserRoleId { get; set; }

    }
}
