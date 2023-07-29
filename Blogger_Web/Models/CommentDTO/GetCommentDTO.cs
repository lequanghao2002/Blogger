namespace Blogger_Web.Models.CommentViewModel
{
    public class GetCommentDTO
    {
        public int ID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime CommentDate { get; set; }
        public int ParentID { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public int PostID { get; set; }
    }
}
