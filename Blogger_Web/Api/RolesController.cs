using Blogger_Web.Models.RolesDTO;
using Blogger_Web.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger_Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet("get-list-roles")]
        public async Task<IActionResult> GetListRoles()
        {
            try
            {
                var listRoles = await _roleRepository.GetAll();
                return BadRequest(listRoles);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO createRoleDTO)
        {
            try
            {
                var createRole = await _roleRepository.Create(createRoleDTO);
                return Ok(createRole);
            }
            catch
            {
                return BadRequest("Tạo role không thành công");
            }
        }

        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> Updaterole(CreateRoleDTO createRoleDTO, int id)
        {
            try
            {
                var updateRole = await _roleRepository.Update(createRoleDTO, id);
                if (updateRole != null)
                {
                    return Ok(updateRole);
                }
                else
                {
                    return BadRequest($"Không tìm thấy role có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Chỉnh sửa role không thành công");
            }
        }

        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var deleteRole = await _roleRepository.Delete(id);
                if (deleteRole != null)
                {
                    return Ok(deleteRole);
                }
                else
                {
                    return BadRequest($"Không tìm thấy role có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Xóa role không thành công");
            }
        }
    }
}
