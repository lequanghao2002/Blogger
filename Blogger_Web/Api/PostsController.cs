using Blogger_Web.Models.Categories;
using Blogger_Web.Models.PostsDTO;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger_Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IPostRepository _postRepository;

		public PostsController(IPostRepository postRepository) {
			_postRepository = postRepository;
		}

		[HttpGet("get-list-posts")]
		public async Task<IActionResult> GetListPosts(int page = 0, int pageSize = 6)
		{
			try
			{
				var listPostsPagination = await _postRepository.GetAll(page, pageSize);
				return Ok(listPostsPagination);
			}
			catch
			{
				return BadRequest();
			}
		}

        [HttpGet("get-post-by-id/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _postRepository.GetById(id);
				if(post != null)
				{
					return Ok(post);
				}
				else
				{
                    return BadRequest($"Không tìm thấy post có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Lấy post theo id không thành công");
            }
        }

        [HttpPost("create-post")]
		public async Task<IActionResult> CreatePost(CreatePostDTO createPostDTO)
		{
			try
			{
				var createPost = await _postRepository.Create(createPostDTO);
				if(createPost != null)
				{
					return Ok(createPost);
				}
				else
				{
					return BadRequest("Category của post không hợp lệ");
				}
				
			}
			catch
			{
				return BadRequest("Tạo post không thành công");
			}
		}

		[HttpPut("update-post/{id}")]
		public async Task<IActionResult> UpdatePost(int id, CreatePostDTO createPostDTO)
		{
			try
			{
				var updatePost = await _postRepository.Update(createPostDTO, id);
				if (updatePost != null)
				{
					return Ok(updatePost);
				}
				else
				{
					return BadRequest("Vui lòng xem lại thông tin cần chỉnh sửa");

				}
			}
			catch
			{
				return BadRequest("Chỉnh sửa post không thành công");
			}
		}

		[HttpDelete("delete-post/{id}")]
		public async Task<IActionResult> DeletePost(int id)
		{
			try
			{
				var deletePost = await _postRepository.Delete(id);
				if (deletePost != null)
				{
					return Ok(deletePost);
				}
				else
				{
					return BadRequest($"Không tìm thấy post có id = {id}");
				}
			}
			catch
			{
				return BadRequest("Xóa post không thành công");
			}
		}
	}
}
