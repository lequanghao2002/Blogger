﻿using Blogger_Data;
using Blogger_Model;
using Blogger_Web.Infrastructure.Core;
using Blogger_Web.Models.Categories;
using Blogger_Web.Models.CategoriesDTO;
using Blogger_Web.Models.RolesDTO;
using Microsoft.EntityFrameworkCore;

namespace Blogger_Web.Respositories
{
    public interface IRoleRepository
    {
        public Task<PaginationSet<GetRoleDTO>> GetAll(int page, int pageSize);
        public Task<CreateRoleDTO> Create(CreateRoleDTO createRoleDTO);
        public Task<GetRoleDTO> GetById(int id);
        public Task<CreateRoleDTO> Update(CreateRoleDTO createRoleDTO, int id);
        public Task<Role> Delete(int id);
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
                ID = role.ID,
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

        public async Task<GetRoleDTO> GetById(int id)
        {
            var categoryDomainById = await _bloggerDbContext.Categories.Select(category => new GetCategoryByIdDTO
            {
                ID = category.ID,
                Name = category.Name
            }).FirstOrDefaultAsync(category => category.ID == id);

            var listRoleDomainById = await _bloggerDbContext.Roles.Select(role => new GetRoleDTO()
            {
                ID = role.ID,
                Name = role.Name,
            }).FirstOrDefaultAsync(role => role.ID == id);

            return listRoleDomainById;
        }

        public async Task<CreateRoleDTO> Create(CreateRoleDTO createRoleDTO)
        {
            var createRoleDomain = new Role()
            {
                Name = createRoleDTO.Name,
            };

            await _bloggerDbContext.Roles.AddAsync(createRoleDomain);
            await _bloggerDbContext.SaveChangesAsync();

            return createRoleDTO;
        }

        public async Task<CreateRoleDTO> Update(CreateRoleDTO createRoleDTO, int id)
        {
            var updateRoleDomain = await _bloggerDbContext.Roles.FirstOrDefaultAsync(role => role.ID == id);
            if (updateRoleDomain != null)
            {
                updateRoleDomain.Name = createRoleDTO.Name;
                await _bloggerDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }
            return createRoleDTO;
        }

        public async Task<Role> Delete(int id)
        {
            var deleteRoleDomain = await _bloggerDbContext.Roles.FirstOrDefaultAsync(role => role.ID == id);
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
