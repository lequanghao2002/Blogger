using Blogger_Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogger_Web.Models.PostsDTO
{
    public class GetPostDTO
    {
		public int ID { get; set; }
		public string Title { get; set; }
		public string BriefContent { get; set; }
		public string Content { get; set; }
		public string Image { get; set; }
		public bool Published { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public string AccountName { get; set; }
		public IEnumerable<string> ListCategoriesName { get; set; }
	}
}
