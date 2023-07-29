using System.ComponentModel.DataAnnotations;

namespace Blogger_Web.Models.RolesDTO
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
    }
}
