using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasWithAuth.Models
{
    [Table("AppUserRoles")]
    public class AppUserRole : ModelBase
    {
        public string? RoleName { get; set; }


    }
}
