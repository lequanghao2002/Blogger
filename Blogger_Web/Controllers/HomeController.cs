﻿using Blogger_Common;
using Blogger_Web.Models.PostsDTO;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace Blogger_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository) 
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index(int? categoryID, int page = 1)
        {
            var pageSize = 6;
            var listPost = _postRepository.GetAllByCategory(categoryID, page, pageSize);

            if (categoryID != null)
            {
                var getCategoryById = await _categoryRepository.GetById((int)categoryID);

                ViewBag.Category = getCategoryById;
            }

            ViewBag.listAllCategories = _categoryRepository.GetAllNoAsync();
            return View(listPost);
        }

        public IActionResult SideBarRight()
        {
            
            return PartialView();
        }

        public JsonResult GetListPostByTitle(string keyword)
        {
            var listPostByTitle = _postRepository.GetAllByTitle(keyword);

            return Json(new
            {
                data = listPostByTitle,

            });
        }
     
    }
}
