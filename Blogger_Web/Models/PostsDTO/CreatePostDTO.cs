using Blogger_Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blogger_Web.Models.PostsDTO
{
    public class CreatePostDTO
    {
		[Required(ErrorMessage = "Tiêu đề không được để trống")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Nội dung tóm tắt không được để trống")]
		public string BriefContent { get; set; }

		[Required(ErrorMessage = "Nội dung chính không được để trống")]
		public string Content { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn ảnh")]
		public string Image { get; set; }
		public bool Published { get; set; }
		public int AccountID { get; set; }
		public IEnumerable<int> ListCategoriesID { get; set; }
	}
}
