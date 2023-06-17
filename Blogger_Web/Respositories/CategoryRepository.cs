using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Infrastructure.Core;
using Blogger_Web.Models.Categories;
using Blogger_Web.Models.CategoriesDTO;
using Microsoft.EntityFrameworkCore;

namespace Blogger_Web.Respositories
{
    public interface ICategoryRepository
    {
        public Task<PaginationSet<GetCategoryDTO>> GetAll(int page, int pageSize);
        public Task<List<GetCategoryDTO>> GetAll();
        public List<GetCategoryDTO> GetAllNoAsync();
        public Task<GetCategoryByIdDTO> GetById(int id);
        public Task<CreateCategoryDTO> Create(CreateCategoryDTO createCategoryDTO);
        public Task<CreateCategoryDTO> Update(CreateCategoryDTO createCategoryDTO, int id);
        public Task<Category> Delete(int id);

    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;
        public CategoryRepository(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }
        public async Task<PaginationSet<GetCategoryDTO>> GetAll(int page, int pageSize)
        {
            var listCategoriesDomain = await _bloggerDbContext.Categories.Select(category => new GetCategoryDTO()
            {
                ID = category.ID,
                Name = category.Name,
            }).OrderByDescending(p => p.ID).ToListAsync();

            var totalCount = listCategoriesDomain.Count();

            var listCategoryPagination = listCategoriesDomain.Skip(page * pageSize).Take(pageSize);

            var paginationSet = new PaginationSet<GetCategoryDTO>()
            {
                List = listCategoryPagination,
                Page = page,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize)
            };

            return paginationSet;
        }

        public async Task<List<GetCategoryDTO>> GetAll()
        {
            var listCategoriesDomain = await _bloggerDbContext.Categories.Select(category => new GetCategoryDTO()
            {
                ID = category.ID,
                Name = category.Name,
            }).ToListAsync();

            return listCategoriesDomain;
        }

        public List<GetCategoryDTO> GetAllNoAsync()
        {
            var listCategoriesDomain = _bloggerDbContext.Categories.Select(category => new GetCategoryDTO()
            {
                ID = category.ID,
                Name = category.Name,
            }).ToList();

            return listCategoriesDomain;
        }

        public async Task<GetCategoryByIdDTO> GetById(int id)
        {
            var categoryDomainById = await _bloggerDbContext.Categories.Select(category => new GetCategoryByIdDTO
            {
                ID = category.ID,
                Name = category.Name
            }).FirstOrDefaultAsync(category => category.ID == id);

            return categoryDomainById;
        }

        public async Task<CreateCategoryDTO> Create(CreateCategoryDTO createCategoryDTO)
        {
            var createCategoryDomain = new Category()
            {
                Name = createCategoryDTO.Name,
            };

            await _bloggerDbContext.Categories.AddAsync(createCategoryDomain);
            await _bloggerDbContext.SaveChangesAsync();

            return createCategoryDTO;
        }

        public async Task<CreateCategoryDTO> Update(CreateCategoryDTO createCategoryDTO, int id)
        {
            var updateCategoryDomain = await _bloggerDbContext.Categories.FirstOrDefaultAsync(category => category.ID == id);
            if (updateCategoryDomain != null)
            {
                updateCategoryDomain.Name = createCategoryDTO.Name;
                await _bloggerDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }

            return createCategoryDTO;
        }

        public async Task<Category> Delete(int id)
        {
            var deleteCategoryDomain = await _bloggerDbContext.Categories.FirstOrDefaultAsync(category => category.ID == id);
            if (deleteCategoryDomain != null)
            {
                _bloggerDbContext.Categories.Remove(deleteCategoryDomain);
                await _bloggerDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }

            return deleteCategoryDomain;
        }
    }
}
