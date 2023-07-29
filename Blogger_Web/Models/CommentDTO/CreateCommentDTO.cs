namespace Blogger_Web.Models.CommentDTO
{
    public class CreateCommentDTO
    {
        public string CommentMsg { get; set; }
        public int ParentID { get; set; }
        public string UserID { get; set; }
        public int PostID { get; set; }
    }
}
