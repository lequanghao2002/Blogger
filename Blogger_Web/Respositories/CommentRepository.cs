using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Models.Categories;
using Blogger_Web.Models.CommentDTO;
using Blogger_Web.Models.CommentViewModel;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blogger_Web.Respositories
{
    public interface ICommentRepository
    {
        public Task<List<GetCommentDTO>> GetListComment(int parentID, int postID);
        public Task<int> GetCountAll(int postID);

        public CreateCommentDTO Create(CreateCommentDTO createCommentDTO);
    }
    public class CommentRepository : ICommentRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;
        public CommentRepository(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }

        public async Task<List<GetCommentDTO>> GetListComment(int parentID, int postID)
        {
            var getCommnent = await _bloggerDbContext.Comments.Select(comment => new GetCommentDTO()
            {
                ID = comment.ID,
                CommentMsg = comment.CommentMsg,
                CommentDate = comment.CommentDate,
                PostID = comment.PostID,
                UserID = comment.UserID,
                FullName = comment.User.FullName,
                ParentID = comment.ParentID,
            }).OrderByDescending(c => c.ID).Where(c => c.ParentID == parentID && c.PostID == postID).ToListAsync();

            return getCommnent;
        }

        public async Task<int> GetCountAll(int postID)
        {
            var countComment =  _bloggerDbContext.Comments.Where(c => c.PostID == postID).Count();
            return countComment;
        }

        public CreateCommentDTO Create(CreateCommentDTO createCommentDTO)
        {
            var commentDomain = new Comment()
            {
                CommentMsg = createCommentDTO.CommentMsg,
                CommentDate = DateTime.Now,
                ParentID = createCommentDTO.ParentID,
                UserID  = createCommentDTO.UserID,
                PostID = createCommentDTO.PostID
            };

            _bloggerDbContext.Comments.Add(commentDomain);
            _bloggerDbContext.SaveChanges();

            return createCommentDTO;
        }
    }
}
