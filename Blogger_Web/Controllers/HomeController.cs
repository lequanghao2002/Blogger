using Blogger_Web.Models.PostsDTO;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blogger_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository) 
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(int? categoryID, int page = 1)
        {
            var pageSize = 1;
            var listPost = _postRepository.GetAll(categoryID, page, pageSize);

            if(categoryID != null)
            {
                var getCategoryById = _categoryRepository.GetById((int)categoryID);

                ViewBag.Category = getCategoryById;
            }

            return View(listPost);
        }

        public IActionResult SideBarRight()
        {
            return PartialView();
        }
    }
}
