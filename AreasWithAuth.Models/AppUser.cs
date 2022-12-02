using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("AppUsers")]
    public class AppUser : ModelBase
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public string? Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; } = false;
        public Guid AppUserRoleId { get; set; }
        public virtual AppUserRole? AppUserRole { get; set; }
    }
}
