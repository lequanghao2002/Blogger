using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Infrastructure.Core;
using Blogger_Web.Models.PostsDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Blogger_Web.Respositories
{
    public interface IPostRepository
    {
        public Task<PaginationSet<GetPostDTO>> GetAll(int page, int pageSize);
        public Task<GetPostByIdDTO> GetById(int id);
        public Task<CreatePostDTO> Create(CreatePostDTO createPostDTO);
        public Task<CreatePostDTO> Update(CreatePostDTO createPostDTO, int id);
        public Task<Post> Delete(int id);
    }
    public class PostRepository : IPostRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;

        public PostRepository(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }
        public async Task<PaginationSet<GetPostDTO>> GetAll(int page, int pageSize)
        {
            var listPostDomain = await _bloggerDbContext.Posts.Select(post => new GetPostDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                AccountName = post.Account.FullName,
                ListCategoriesName = post.Post_Categories.Select(pc => pc.Category.Name).ToList(),
            }).OrderByDescending(post => post.CreateDate).ToListAsync();

            var totalCount = listPostDomain.Count();
            var listPostPagination = listPostDomain.Skip((page) * pageSize).Take(pageSize);

            PaginationSet<GetPostDTO> paginationSet = new PaginationSet<GetPostDTO>()
            {
                List = listPostPagination,
                Page = page,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize)
            };

            return paginationSet;
        }

        public async Task<GetPostByIdDTO> GetById(int id)
        {
            //var postDomainById = await _bloggerDbContext.Posts.FirstOrDefaultAsync(post => post.ID == id);
            //if (postDomainById != null)
            //{
            //    var post = new GetPostByIdDTO()
            //    {
            //        ID = postDomainById.ID,
            //        Title = postDomainById.Title,
            //        BriefContent = postDomainById.BriefContent,
            //        Content = postDomainById.Content,
            //        Image = postDomainById.Image,
            //        Published = postDomainById.Published,
            //        AccountID = postDomainById.AccountID,
            //        ListCategoriesID = postDomainById.Post_Categories.Select(postCategory => postCategory.CategoryID),
            //    };
            //    return post;
            //}
            //else
            //{
            //    return null!;
            //}
            var postDomain = await _bloggerDbContext.Posts.Select(post => new GetPostByIdDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                AccountID = post.AccountID,
                ListCategoriesID = post.Post_Categories.Select(postCategory => postCategory.CategoryID),
            }).FirstOrDefaultAsync(post => post.ID == id);

            return postDomain;
        }

        public async Task<CreatePostDTO> Create(CreatePostDTO createPostDTO)
        {
            //Kiểm tra xem id của Category nhập vào có tồn tại
            foreach (var idCategory in createPostDTO.ListCategoriesID)
            {
                var checkIdCategory = await _bloggerDbContext.Categories.FirstOrDefaultAsync(c => c.ID == idCategory);
                if (checkIdCategory == null)
                {
                    return null!;
                }
            }

            var createPostDomain = new Post()
            {
                Title = createPostDTO.Title,
                BriefContent = createPostDTO.BriefContent,
                Content = createPostDTO.Content,
                Image = createPostDTO.Image,
                Published = createPostDTO.Published,
                CreateDate = DateTime.Now,
                AccountID = createPostDTO.AccountID,
            };
            await _bloggerDbContext.Posts.AddAsync(createPostDomain);
            await _bloggerDbContext.SaveChangesAsync();

            foreach (var pc in createPostDTO.ListCategoriesID)
            {
                var postCategoryDomain = new Post_Category()
                {
                    PostID = createPostDomain.ID,
                    CategoryID = pc,
                };

                await _bloggerDbContext.Post_Categories.AddAsync(postCategoryDomain);
                await _bloggerDbContext.SaveChangesAsync();
            }

            return createPostDTO;
        }

        public async Task<CreatePostDTO> Update(CreatePostDTO createPostDTO, int id)
        {
            //Kiểm tra xem id của Category nhập vào có tồn tại
            foreach (var idCategory in createPostDTO.ListCategoriesID)
            {
                var checkIdCategory = await _bloggerDbContext.Categories.FirstOrDefaultAsync(c => c.ID == idCategory);
                if (checkIdCategory == null)
                {
                    return null!;
                }
            }

            var updatePostDomain = await _bloggerDbContext.Posts.FirstOrDefaultAsync(post => post.ID == id);
            if (updatePostDomain != null)
            {
                updatePostDomain.Title = createPostDTO.Title;
                updatePostDomain.BriefContent = createPostDTO.BriefContent;
                updatePostDomain.Content = createPostDTO.Content;
                updatePostDomain.Image = createPostDTO.Image;
                updatePostDomain.Published = createPostDTO.Published;
                updatePostDomain.UpdateDate = DateTime.Now;
                updatePostDomain.AccountID = createPostDTO.AccountID;

                await _bloggerDbContext.SaveChangesAsync();

                var deletePostCategoryDomain = _bloggerDbContext.Post_Categories.Where(pc => pc.PostID == id);
                if (deletePostCategoryDomain != null)
                {
                    foreach (var pc in deletePostCategoryDomain)
                    {
                        _bloggerDbContext.Post_Categories.Remove(pc);
                    }
                    await _bloggerDbContext.SaveChangesAsync();
                }

                foreach (var pc in createPostDTO.ListCategoriesID)
                {
                    var postCategoryDomain = new Post_Category()
                    {
                        PostID = id,
                        CategoryID = pc,
                    };

                    await _bloggerDbContext.Post_Categories.AddAsync(postCategoryDomain);
                    await _bloggerDbContext.SaveChangesAsync();
                }
            }
            else
            {
                return null!;
            }

            return createPostDTO;
        }

        public async Task<Post> Delete(int id)
        {
            var deletePostDomain = await _bloggerDbContext.Posts.FirstOrDefaultAsync(post => post.ID == id);
            if (deletePostDomain != null)
            {
                _bloggerDbContext.Posts.Remove(deletePostDomain);
                await _bloggerDbContext.SaveChangesAsync();

                var deletePostCategoryDomain = _bloggerDbContext.Post_Categories.Where(pc => pc.PostID == id);
                if (deletePostCategoryDomain != null)
                {
                    foreach (var pc in deletePostCategoryDomain)
                    {
                        _bloggerDbContext.Post_Categories.Remove(pc);
                    }
                    await _bloggerDbContext.SaveChangesAsync();
                }
            }
            else
            {
                return null!;
            }

            return deletePostDomain;
        }
    }
}
