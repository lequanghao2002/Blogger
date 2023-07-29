using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Infrastructure.Core;
using Blogger_Web.Models.Categories;
using Blogger_Web.Models.CategoriesDTO;
using Blogger_Web.Models.RolesDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogger_Web.Respositories
{
    public interface IRoleRepository
    {
        public Task<PaginationSet<GetRoleDTO>> GetAll(int page, int pageSize);
        public Task<CreateRoleDTO> Create(CreateRoleDTO createRoleDTO);
        public Task<GetRoleDTO> GetById(string id);
        public Task<CreateRoleDTO> Update(CreateRoleDTO createRoleDTO, string id);
        public Task<IdentityRole> Delete(string id);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly BloggerDbContext _bloggerDbContext;
        public RoleRepository(BloggerDbContext bloggerDbContext)
        {
            _bloggerDbContext = bloggerDbContext;
        }
        public async Task<PaginationSet<GetRoleDTO>> GetAll(int page, int pageSize)
        {
            var listRoleDomain = await _bloggerDbContext.Roles.Select(role => new GetRoleDTO()
            {
                ID = role.Id,
                Name = role.Name,
            }).OrderByDescending(r => r.ID).ToListAsync();

            var totalCount = listRoleDomain.Count();

            var listRolePagination = listRoleDomain.Skip(page * pageSize).Take(pageSize);

            var paginationSet = new PaginationSet<GetRoleDTO>()
            {
                List = listRolePagination,
                Page = page,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize)
            };

            return paginationSet;
        }

        public async Task<GetRoleDTO> GetById(string id)
        {
            var roleDomainById = await _bloggerDbContext.Roles.Select(role => new GetRoleDTO()
            {
                ID = role.Id,
                Name = role.Name
            }).FirstOrDefaultAsync(role => role.ID == id);
            
            return roleDomainById;
        }

        public async Task<CreateRoleDTO> Create(CreateRoleDTO createRoleDTO)
        {
            var createRoleDomain = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = new Guid().ToString(),
                Name = createRoleDTO.Name,
                NormalizedName = createRoleDTO.Name.ToUpper(),
            };

            await _bloggerDbContext.Roles.AddAsync(createRoleDomain);
            await _bloggerDbContext.SaveChangesAsync();

            return createRoleDTO;
        }

        public async Task<CreateRoleDTO> Update(CreateRoleDTO createRoleDTO, string id)
        {
            var updateRoleDomain = await _bloggerDbContext.Roles.FirstOrDefaultAsync(role => role.Id == id);
            if (updateRoleDomain != null)
            {
                updateRoleDomain.Name = createRoleDTO.Name;
                updateRoleDomain.NormalizedName = createRoleDTO.Name.ToUpper();

                await _bloggerDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }
            return createRoleDTO;
        }

        public async Task<IdentityRole> Delete(string id)
        {
            var deleteRoleDomain = await _bloggerDbContext.Roles.FirstOrDefaultAsync(role => role.Id == id);
            if (deleteRoleDomain != null)
            {
                _bloggerDbContext.Roles.Remove(deleteRoleDomain);
                await _bloggerDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }

            return deleteRoleDomain;
        }
    }
}
