using BLL.Request;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewDepController : ControllerBase
    {
        private readonly INewDepService _Departmentrepo;

        public NewDepController(INewDepService DepartmentRepository)
        {
            _Departmentrepo = DepartmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Ok(await _Departmentrepo.GetAllDepartments());
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetADepartment(string code)
        {
            return Ok(await _Departmentrepo.GetADepartment(code));
        }
        [HttpPost]
        public async Task<IActionResult> InsertADepartment(DepartmentViewModelInsert department)
        {
            return Ok(await _Departmentrepo.InsertDepartment(department));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateADepartment(DepartmentViewModelInsert department)
        {
            return Ok(await _Departmentrepo.UpdateDepartment(department));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteADepartment(string code)
        {
            return Ok(await _Departmentrepo.DeleteDepartment(code));
        }

    }
}
