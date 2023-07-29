using Blogger_Model;
using Blogger_Web.Models;
using Blogger_Web.Models.CommentDTO;
using Blogger_Web.Models.PostsDTO;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Blogger_Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;

        public PostsController(IPostRepository postRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public IActionResult Search(string keyword, int page = 1)
        {
            var pageSize = 6;
            var listPostByKeyword = _postRepository.GetAllByKeyword(keyword, page, pageSize);

            ViewBag.keyword = keyword;

            ViewBag.listAllCategories = _categoryRepository.GetAllNoAsync();

            return View(listPostByKeyword);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var postById = await _postRepository.GetById(id);

            ViewBag.listPostRelated = await _postRepository.GetAllByRelated(postById.ListCategoriesID.First());

            ViewBag.listAllCategories = _categoryRepository.GetAllNoAsync();

            ViewBag.ListComment = await _commentRepository.GetListComment(0, id);

            ViewBag.SumComment = await _commentRepository.GetCountAll(id);

            var userSession = HttpContext.Session.GetString("userName");
            if (userSession != null)
            {
                ViewBag.User = await _userManager.FindByEmailAsync(userSession);
            }

            return View(postById);
        }

        //public async Task<IActionResult> _ChildComment()
        //{

        //    return PartialView("_ChildComment");
        //}

        [HttpPost] 
        public JsonResult AddNewComment(CreateCommentDTO createCommentDTO)
        {
            try
            {
                var commentNew = _commentRepository.Create(createCommentDTO);

                if(commentNew != null)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch 
            {
                return Json(new { status = false });
            }
        }
    }
}
