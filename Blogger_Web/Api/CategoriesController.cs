using Blogger_Web.Models.Categories;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger_Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("get-list-categories")]
        public async Task<IActionResult> GetListCategories()
        {
            try
            {
                var listCategories = await _categoryRepository.GetAll();
                return Ok(listCategories);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            try
            {
                var createCategory = await _categoryRepository.Create(createCategoryDTO);
                return Ok(createCategory);
            }
            catch
            {
                return BadRequest("Tạo category không thành công");
            }
        }

        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> UpdateCategory(CreateCategoryDTO createCategory, int id)
        {
            try
            {
                var updateCategory = await _categoryRepository.Update(createCategory, id);
                if(updateCategory != null)
                {
                    return Ok(updateCategory);
                }
                else
                {
                    return BadRequest($"Không tìm thấy category có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Chỉnh sửa category không thành công");
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var deleteCategory = await _categoryRepository.Delete(id);
                if (deleteCategory != null)
                {
                    return Ok(deleteCategory);
                }
                else
                {
                    return BadRequest($"Không tìm thấy category có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Xóa category không thành công");
            }
        }
    }
}
