using System.ComponentModel.DataAnnotations;

namespace Blogger_Web.Models.Categories
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
    }
}
