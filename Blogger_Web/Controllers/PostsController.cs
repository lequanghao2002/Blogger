using Blogger_Model;
using Blogger_Web.Models.PostsDTO;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Blogger_Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IActionResult Search(string keyword, int page = 1)
        {
            var pageSize = 1;
            var listPostByKeyword = _postRepository.GetAllByKeyword(keyword, page, pageSize);

            ViewBag.keyword = keyword;

            return View(listPostByKeyword);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var postById = await _postRepository.GetById(id);

            ViewBag.listPostRelated = await _postRepository.GetAllByRelated(postById.ListCategoriesID.First());
            
            return View(postById);
        }

        
    }
}
