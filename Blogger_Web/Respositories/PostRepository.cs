using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Infrastructure.Core;
using Blogger_Web.Models.PostsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.Linq;
using System.Net;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Blogger_Web.Respositories
{
    public interface IPostRepository
    {
        public Task<PaginationSet<GetPostDTO>> GetAll(int page, int pageSize, DateTime? dateFrom, DateTime? dateTo, int? classify, string? filter);
        public PaginationSet2<GetPostDTO> GetAllByCategory(int? categoryId, int page, int pageSize);
        public PaginationSet2<GetPostDTO> GetAllByKeyword(string keyword, int page, int pageSize);
        public List<string> GetAllByTitle(string keyword);
        public Task<List<GetPostByIdDTO>> GetAllByRelated(int listIdCategory);
        public Task<GetPostByIdDTO> GetById(int id);
        public Task<int> GetNew();
        public Task<List<User>> GetAllUserSendEmail();
        public Task<CreatePostDTO> Create(CreatePostDTO createPostDTO);
        public Task<CreatePostDTO> Update(CreatePostDTO createPostDTO, int id);
        public Task<Post> Delete(int id);
    }
    public class PostRepository : IPostRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;
        private readonly UserManager<User> _userManager;

        public PostRepository(BloggerDbContext bloggerDbContext, UserManager<User> userManager)
        {
            _bloggerDbContext = bloggerDbContext;
            _userManager = userManager;
        }
        public async Task<PaginationSet<GetPostDTO>> GetAll(int page, int pageSize, DateTime? dateFrom, DateTime? dateTo, int? classify, string? filter)
        {
            var listAllPostDomain = _bloggerDbContext.Posts.AsQueryable();

            #region Check date
            if (dateFrom != null && dateTo != null)
            {

                listAllPostDomain = listAllPostDomain.Where(p => p.CreateDate >= dateFrom && p.CreateDate <= dateTo);

            }
            else if (dateFrom != null)
            {
                listAllPostDomain = listAllPostDomain.Where(p => p.CreateDate >= dateFrom);
            }
            else if (dateTo != null)
            {
                listAllPostDomain = listAllPostDomain.Where(p => p.CreateDate <= dateTo);
            }
            #endregion

            #region Check classify
            if (classify != null)
            {
                listAllPostDomain = listAllPostDomain.Where(p => p.Post_Categories.Any(pc => pc.CategoryID == classify));
            }
            #endregion

            #region Check filter
            if (filter != null)
            {
                listAllPostDomain = listAllPostDomain.Where(p => p.Title.Contains(filter));
            }
            #endregion

            var listPostDomain = await listAllPostDomain.Select(post => new GetPostDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                AccountName = post.User.FullName,
                ListCategoriesName = post.Post_Categories.Select(pc => pc.Category.Name).ToList(),
            }).OrderByDescending(post => post.CreateDate).ToListAsync();

            var totalCount = listPostDomain.Count();
            var listPostPagination = listPostDomain.Skip(page * pageSize).Take(pageSize);

            PaginationSet<GetPostDTO> paginationSet = new PaginationSet<GetPostDTO>()
            {
                List = listPostPagination,
                Page = page,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize),
            };

            return paginationSet;
        }

        public PaginationSet2<GetPostDTO> GetAllByCategory(int? categoryId, int page, int pageSize)
        {
            var listAllPostDomain = _bloggerDbContext.Posts.AsQueryable();


            if (categoryId != null)
            {
                listAllPostDomain = listAllPostDomain.Where(p => p.Post_Categories.Any(pc => pc.CategoryID == categoryId));
            }

            var listPostDomain = listAllPostDomain.Select(post => new GetPostDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                AccountName = post.User.FullName,
                ListCategoriesName = post.Post_Categories.Select(pc => pc.Category.Name).ToList(),
            }).OrderByDescending(post => post.CreateDate).Where(post => post.Published == true).ToList();

            var totalCount = listPostDomain.Count();
            var listPostPagination = listPostDomain.Skip((page - 1) * pageSize).Take(pageSize);

            PaginationSet2<GetPostDTO> paginationSet = new PaginationSet2<GetPostDTO>()
            {
                List = listPostPagination,
                Page = page,
                MaxPage = 5,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize),
            };

            return paginationSet;
        }
        public PaginationSet2<GetPostDTO> GetAllByKeyword(string keyword, int page, int pageSize)
        {
            var listAllPostByKeywordDomain = _bloggerDbContext.Posts.Select(post => new GetPostDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                AccountName = post.User.FullName,
                ListCategoriesName = post.Post_Categories.Select(pc => pc.Category.Name).ToList(),
            }).OrderByDescending(post => post.CreateDate).Where(post => post.Published == true && post.Title.Contains(keyword)).ToList();

            var totalCount = listAllPostByKeywordDomain.Count();
            var listPostPagination = listAllPostByKeywordDomain.Skip((page - 1) * pageSize).Take(pageSize);

            PaginationSet2<GetPostDTO> paginationSet = new PaginationSet2<GetPostDTO>()
            {
                List = listPostPagination,
                Page = page,
                MaxPage = 5,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize),
            };

            return paginationSet;
        }

        public List<string> GetAllByTitle(string keyword)
        {
            var listPostDomain = _bloggerDbContext.Posts.Where(post => post.Published == true && post.Title.Contains(keyword)).Select(post => post.Title).ToList();

            return listPostDomain;
        }

        public async Task<List<GetPostByIdDTO>> GetAllByRelated(int idCategory)
        {

            var listAllPostByRelated = await _bloggerDbContext.Posts.Select(post => new GetPostByIdDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                UserID = post.UserID,
                ListCategoriesID = post.Post_Categories.Select(postCategory => postCategory.CategoryID),

                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                AccountName = post.User.FullName,
                ListCategoriesName = post.Post_Categories.Select(pc => pc.Category.Name).ToList(),
            }).Where(p => p.ListCategoriesID.Any(p => p.Equals(idCategory))).Take(3).ToListAsync();

            return listAllPostByRelated;
        }

        public async Task<GetPostByIdDTO> GetById(int id)
        {
            var postDomain = await _bloggerDbContext.Posts.Select(post => new GetPostByIdDTO
            {
                ID = post.ID,
                Title = post.Title,
                BriefContent = post.BriefContent,
                Content = post.Content,
                Image = post.Image,
                Published = post.Published,
                UserID = post.UserID,
                ListCategoriesID = post.Post_Categories.Select(postCategory => postCategory.CategoryID),

                CreateDate = post.CreateDate,
                UpdateDate = post.UpdateDate,
                AccountName = post.User.FullName,
                ListCategoriesName = post.Post_Categories.Select(pc => pc.Category.Name).ToList(),
            }).FirstOrDefaultAsync(post => post.ID == id);

            return postDomain;
        }

        public async Task<List<User>> GetAllUserSendEmail()
        {
            var getAllUserSendEmail = await _userManager.Users.Where(u => u.SendEmail == true).ToListAsync();

            return getAllUserSendEmail;
        }

        public async Task<int> GetNew()
        {
            var postNew = await _bloggerDbContext.Posts.OrderByDescending(p => p.ID).FirstAsync();

            return postNew.ID;
        }    
        
        public async Task<CreatePostDTO> Create(CreatePostDTO createPostDTO)
        {
            // Kiểm tra ListCategoriesID có rỗng ko
            if (createPostDTO.ListCategoriesID.Count() == 0)
            {
                return null!;
            };

            //Kiểm tra xem id của Category nhập vào có tồn tại
            foreach (var idCategory in createPostDTO.ListCategoriesID)
            {
                var checkIdCategory = await _bloggerDbContext.Categories.FirstOrDefaultAsync(c => c.ID == idCategory);
                if (checkIdCategory == null)
                {
                    return null!;
                }
            };

            var createPostDomain = new Post()
            {
                Title = createPostDTO.Title,
                BriefContent = createPostDTO.BriefContent,
                Content = createPostDTO.Content,
                Image = createPostDTO.Image,
                Published = createPostDTO.Published,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                UserID = createPostDTO.UserID,
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
            // Kiểm tra ListCategoriesID có rỗng ko
            if (createPostDTO.ListCategoriesID.Count() == 0)
            {
                return null!;
            };

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
                updatePostDomain.UserID = createPostDTO.UserID;

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
