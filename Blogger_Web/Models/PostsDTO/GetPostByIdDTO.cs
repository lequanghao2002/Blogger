using System.ComponentModel.DataAnnotations;

namespace Blogger_Web.Models.PostsDTO
{
    public class GetPostByIdDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string BriefContent { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool Published { get; set; }
        public string UserID { get; set; }
        public IEnumerable<int> ListCategoriesID { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string AccountName { get; set; }
        public IEnumerable<string> ListCategoriesName { get; set; }
    }
}
