using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger_Model
{
    [Table("Users")]
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public bool SendEmail { get; set; }

        public IEnumerable<Post> Post { get; set; }
    }
}
